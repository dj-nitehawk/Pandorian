Imports Pandorian.Engine
Imports Un4seen.Bass
Imports System.Runtime.InteropServices
Imports System.Net
Imports Microsoft.Win32
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.ComponentModel
Imports Pandorian.Utility.ModifyRegistry
Imports Pandorian.Engine.Data

Public Class frmMain
    Dim Settings As RegistryStore = New RegistryStore()
    Dim Pandora As API
    Dim Proxy As WebProxy
    Dim BASSReady As Boolean = False
    Dim ProxyPtr As IntPtr
    Dim AAC As Integer
    Dim Stream As Integer
    Dim Sync As SYNCPROC = New SYNCPROC(AddressOf SongEnded)
    Dim DSP As Misc.DSP_Gain = New Misc.DSP_Gain()
    Dim Downloader As WebClient
    Dim TargeFile As String
    Dim IsActiveForm As Boolean
    Dim SleepAt As Date
    Dim SleepNow As Boolean
    Dim ResumePlaying As Boolean = True
    Dim NagShown As Boolean = False
    Dim VolLastChangedOn As Date
    Dim BPMCounter As New Misc.BPMCounter(20, 44100)
    Dim SongInfo As New frmSongInfo()
    Dim HideSongInfo As Boolean = False
    Dim APIFile As String = "v1.api"

    Public Event SongInfoUpdated(Title As String, Artist As String, Album As String)
    Public Event CoverImageUpdated(Cover As Image)

    Dim FS As FileStream
    Dim DownloadProc As DOWNLOADPROC = New DOWNLOADPROC(AddressOf DownloadSong)
    Dim Data() As Byte

    Private Sub DownloadSong(buffer As IntPtr, length As Integer, user As IntPtr)
        If FS Is Nothing Then
            Pandora.CurrentStation.CurrentSong.FinishedDownloading = False
            FS = File.OpenWrite(Pandora.CurrentStation.CurrentSong.AudioFileName)
            prgDownload.Visible = True
            prgDownload.Value = 0
        End If

        If buffer = IntPtr.Zero Then
            FS.Flush()
            FS.Close()
            FS = Nothing
            If Not StreamDownloadedLength() = -1 Then
                If Not IsNothing(Pandora.CurrentStation.CurrentSong) Then
                    Pandora.CurrentStation.CurrentSong.FinishedDownloading = True
                End If
            End If
        Else
            If Data Is Nothing OrElse Data.Length < length Then
                Data = New Byte(length) {}
            End If
            Marshal.Copy(buffer, Data, 0, length)
            FS.Write(Data, 0, length)
        End If
    End Sub

    Private Function StreamTotalLength() As Long
        Return Bass.BASS_ChannelGetLength(Stream)
    End Function

    Private Function StreamLength() As Long
        Return Bass.BASS_StreamGetFilePosition(Stream, BASSStreamFilePosition.BASS_FILEPOS_END)
    End Function

    Private Function StreamDownloadedLength() As Long
        Return Bass.BASS_StreamGetFilePosition(Stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD)
    End Function

    Private Sub UpdateDownloadProgress()
        If BASSReady And prgDownload.Visible Then
            Dim progress As Single
            progress = StreamDownloadedLength() * 100.0F / StreamLength()
            prgDownload.Value = progress
            If prgDownload.Value = 100 Then
                prgDownload.Visible = False
            End If
        End If
    End Sub

    Public Sub ClearSession()
        If Not IsNothing(Pandora) Then
            Pandora.ClearSession(Settings.Read("pandoraOne"))
            'File.Delete(APIFile)
            SavePandoraObject()
            CleanUp()
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkeys.WM_HOTKEY Then
            handleHotKeyEvent(m.WParam)
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub handleHotKeyEvent(ByVal hotkeyID As IntPtr)
        Select Case hotkeyID
            Case 1
                btnPlayPause_Click(Nothing, Nothing)
            Case 2
                btnLike_Click(Nothing, Nothing)
            Case 3
                btnDislike_Click(Nothing, Nothing)
            Case 4
                btnSkip_Click(Nothing, Nothing)
            Case 5
                If IsActiveForm Then
                    Me.Visible = False
                    frmMain_Resize(Nothing, Nothing)
                Else
                    TrayIcon_MouseClick(Nothing, New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 0, 0, 0))
                End If
            Case 6
                btnBlock_Click(Nothing, Nothing)
            Case 7
                If MsgBox("Are you sure you want to put the machine to sleep?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, Title:="Sleep Now?") = MsgBoxResult.Yes Then
                    If Not chkSleep.Checked Then
                        SleepNow = True
                        chkSleep.Checked = True
                    End If
                End If
            Case 8
                If TrayMenu.Visible Then
                    TrayMenu.Visible = False
                Else
                    TrayMenu.Show(MousePosition)
                End If
            Case 9
                If Not frmLockScreen.Visible Then
                    If Not String.IsNullOrEmpty(Settings.Read("unlockPassword")) Then
                        frmLockScreen.Show()
                    Else
                        MsgBox("You have not set an unlock password in Pandorian settings." + vbCrLf +
                               "Right-click on the album cover image and select 'Show Settings'", MsgBoxStyle.Information)
                    End If
                End If
            Case 10
                volSlider.Visible = True
                Application.DoEvents()
                VolLastChangedOn = Now
                Try
                    volSlider.Value = volSlider.Value - 10
                Catch ex As Exception
                    'do nothing
                End Try
            Case 11
                volSlider.Visible = True
                Application.DoEvents()
                VolLastChangedOn = Now
                Try
                    volSlider.Value = volSlider.Value + 10
                Catch ex As Exception
                    'do nothing
                End Try
        End Select
    End Sub

    Private Sub registerHotkeys()

        Dim hkPlayPause As Integer = Settings.Read("hkPlayPause")
        Dim hkLike As Integer = Settings.Read("hkLike")
        Dim hkDislike As Integer = Settings.Read("hkDislike")
        Dim hkSkip As Integer = Settings.Read("hkSkip")
        Dim hkBlock As Integer = Settings.Read("hkBlock")
        Dim hkShowHide As Integer = Settings.Read("hkShowHide")
        Dim hkGlobalMenu As Integer = Settings.Read("hkGlobalMenu")
        Dim hkSleep As Integer = Settings.Read("hkSleep")
        Dim hkLock As Integer = Settings.Read("hkLock")
        Dim hkVolDown As Integer = Settings.Read("hkVolDown")
        Dim hkVolUp As Integer = Settings.Read("hkVolUp")
        Dim hkModifier As Integer = Settings.Read("hkModifier")

        Dim modKeys As New Dictionary(Of String, Integer)
        For Each k As Hotkeys.KeyModifier In [Enum].GetValues(GetType(Hotkeys.KeyModifier))
            modKeys.Add(k.ToString, k)
        Next
        cbModKey.DisplayMember = "Key"
        cbModKey.ValueMember = "Value"
        cbModKey.DataSource = New BindingSource(modKeys, Nothing)
        cbModKey.SelectedIndex = cbModKey.FindStringExact(CType(hkModifier, Hotkeys.KeyModifier).ToString)
        tbHKPlayPause.Text = [Enum].GetName(GetType(Keys), hkPlayPause)
        tbHKPlayPause.Tag = hkPlayPause
        tbHKLikeSong.Text = [Enum].GetName(GetType(Keys), hkLike)
        tbHKLikeSong.Tag = hkLike
        tbHKDislikeSong.Text = [Enum].GetName(GetType(Keys), hkDislike)
        tbHKDislikeSong.Tag = hkDislike
        tbHKSkipSong.Text = [Enum].GetName(GetType(Keys), hkSkip)
        tbHKSkipSong.Tag = hkSkip
        tbHKBlockSong.Text = [Enum].GetName(GetType(Keys), hkBlock)
        tbHKBlockSong.Tag = hkBlock
        tbHKShowHide.Text = [Enum].GetName(GetType(Keys), hkShowHide)
        tbHKShowHide.Tag = hkShowHide
        tbHKGlobalMenu.Text = [Enum].GetName(GetType(Keys), hkGlobalMenu)
        tbHKGlobalMenu.Tag = hkGlobalMenu
        tbHKSleepNow.Text = [Enum].GetName(GetType(Keys), hkSleep)
        tbHKSleepNow.Tag = hkSleep
        tbHKLockNow.Text = [Enum].GetName(GetType(Keys), hkLock)
        tbHKLockNow.Tag = hkLock
        tbHKVolDown.Text = [Enum].GetName(GetType(Keys), hkVolDown)
        tbHKVolDown.Tag = hkVolDown
        tbHKVolUp.Text = [Enum].GetName(GetType(Keys), hkVolUp)
        tbHKVolUp.Tag = hkVolUp

        Hotkeys.registerHotkey(Me, 1, hkPlayPause, hkModifier) 'play/pause
        Hotkeys.registerHotkey(Me, 2, hkLike, hkModifier) 'like
        Hotkeys.registerHotkey(Me, 3, hkDislike, hkModifier) 'dislike
        Hotkeys.registerHotkey(Me, 4, hkSkip, hkModifier) 'skip
        Hotkeys.registerHotkey(Me, 5, hkShowHide, hkModifier) 'show/hide pandorian
        Hotkeys.registerHotkey(Me, 6, hkBlock, hkModifier) 'block
        Hotkeys.registerHotkey(Me, 7, hkSleep, hkModifier) 'sleep
        Hotkeys.registerHotkey(Me, 8, hkGlobalMenu, hkModifier) 'show tray menu
        Hotkeys.registerHotkey(Me, 9, hkLock, hkModifier) 'show lock screen
        Hotkeys.registerHotkey(Me, 10, hkVolDown, hkModifier) 'vol down
        Hotkeys.registerHotkey(Me, 11, hkVolUp, hkModifier) 'vol up
    End Sub
    Private Sub unRegisterHotkeys()
        Dim i As Integer = 1
        Do While i <= 11
            Hotkeys.unregisterHotkeys(Me, i)
            i = i + 1
        Loop
    End Sub

    Private Sub miShowHotkeys_Click(sender As Object, e As EventArgs) Handles miShowHotkeys.Click
        pnlHotKeys.Visible = True
    End Sub

    Public Function IsLoggedIn() As Boolean
        If Not IsNothing(Pandora) Then
            If Not IsNothing(Pandora.Session) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Function LoginToPandora() As Boolean
        Try
            If Pandora.Login(Decrypt(Settings.Read("pandoraUsername")), Decrypt(Settings.Read("pandoraPassword")), Settings.Read("noQmix")) Then
                tbLog.AppendText("Successfully logged in to pandora..." + vbCrLf)
                Return True
            Else
                MsgBox("Couldn't log in to Pandora. Check pandora a/c details.", MsgBoxStyle.Exclamation)
            End If
        Catch ex As PandoraException
            If ex.ErrorCode = ErrorCodeEnum.LISTENER_NOT_AUTHORIZED Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Else
                MsgBox(ex.Message + ". Please check your internet/proxy settings and try again." + vbCrLf + vbCr + "Error Code: " + ex.ErrorCode.ToString, MsgBoxStyle.Critical)
            End If
        End Try
        Pandora.ClearSession(Settings.Read("pandoraOne"))
        Return False
    End Function
    Sub LoadStationList()
        If Not Pandora.AvailableStations.Count = 0 Then
            Dim FoundLastPlayedStation As Boolean
            Dim Stations As New SortedDictionary(Of String, String)
            For Each Station In Pandora.AvailableStations
                Stations.Add(Station.Name, Station.Id)
                If Settings.Read("lastStationID") = Station.Id Then
                    Pandora.CurrentStation = Station
                    FoundLastPlayedStation = True
                End If
            Next

            If Not FoundLastPlayedStation Then
                Pandora.CurrentStation = Pandora.AvailableStations(0)
            End If

            ddStations.ValueMember = "Value"
            ddStations.DisplayMember = "Key"
            ddStations.DataSource = New BindingSource(Stations, Nothing)
            tbLog.AppendText("Loaded the stations list..." + vbCrLf)
        Else
            MsgBox("Sorry, no stations were found in your a/c." + vbCrLf + "Please visit pandora.com and create some stations.", MsgBoxStyle.Information)
        End If
    End Sub

    Sub PlayCurrentSong() ' THIS SHOULD ONLY HAVE 5 REFERENCES (PlayNextSong/PlayPreviousSong/RunNow/PowerModeChanged/ddStations_SelectedIndexChanged)

        Dim Song As PandoraSong = Pandora.CurrentStation.CurrentSong

        Dim bgwCoverLoader As New BackgroundWorker
        AddHandler bgwCoverLoader.DoWork, AddressOf DownloadCoverImage
        tbLog.AppendText("Loading album cover art..." + vbCrLf)
        If String.IsNullOrEmpty(Song.AlbumArtLargeURL) Then
            bgwCoverLoader.RunWorkerAsync(Nothing)
        Else
            bgwCoverLoader.RunWorkerAsync(Song.AlbumArtLargeURL)
        End If

        PlayCurrentSongWithBASS()
        ddStations.Enabled = True
        Timer.Enabled = True

        btnNext.Enabled = True
        btnNext.BackColor = Control.DefaultBackColor

        Select Case Song.Rating
            Case PandoraRating.Hate
                btnLike.Enabled = True
                btnLike.BackColor = Control.DefaultBackColor
                btnDislike.Enabled = False
                btnDislike.BackColor = Color.Pink
            Case PandoraRating.Love
                btnLike.Enabled = False
                btnLike.BackColor = Color.PaleGreen
                btnDislike.Enabled = True
                btnDislike.BackColor = Control.DefaultBackColor
            Case PandoraRating.Unrated
                btnLike.Enabled = True
                btnLike.BackColor = Control.DefaultBackColor
                btnDislike.Enabled = True
                btnDislike.BackColor = Control.DefaultBackColor
        End Select

        btnPlayPause.Enabled = True
        If ResumePlaying Then
            btnPlayPause.Image = My.Resources.paused
        End If

        If Song.TemporarilyBanned Then
            btnBlock.Enabled = False
            btnBlock.BackColor = Color.Pink
        Else
            btnBlock.Enabled = True
            btnBlock.BackColor = Control.DefaultBackColor
        End If

        If Pandora.CurrentStation.CurrentSong.AudioDurationSecs < 60 Then
            lblSongName.Text = "Ooops! Something went wrong :-("
            lblArtistName.Text = "This doesn't seem to be a normal track."
            lblAlbumName.Text = "Try changing stations or restarting the app."
            SongCoverImage.Image = Nothing
            btnLike.Enabled = False
            btnDislike.Enabled = False
            btnPlayPause.Enabled = False
            btnNext.Enabled = False
            btnPrev.Enabled = False
            btnBlock.Enabled = False
        Else
            lblSongName.Text = Song.GetProperTitle(IndicateLiked)
            lblArtistName.Text = Song.Artist
            lblAlbumName.Text = Song.Album
        End If

        RaiseEvent SongInfoUpdated(lblSongName.Text, lblArtistName.Text, lblAlbumName.Text)

        SavePandoraObject()

        tbLog.AppendText("------------------------------------------------------------------------------------------" + vbCrLf)

        Spinner.Visible = False
        Application.DoEvents()
    End Sub

    Private Function IndicateLiked() As Boolean
        If Settings.Read("noLiked") = 1 Or WindowState = FormWindowState.Normal Then
            Return False
        End If
        Return True
    End Function

    Sub PlayPreviousSong(ExpiredTrack As Boolean)
        If Not IsNothing(Pandora.CurrentStation.CurrentSong.PreviousSong) Then
            Spinner.Visible = True
            prgBar.Value = 0
            prgBar.Update()
            Application.DoEvents()
            ResumePlaying = True
            Pandora.CurrentStation.CurrentSong = Pandora.CurrentStation.CurrentSong.PreviousSong
            Execute(Sub() PlayCurrentSong(), "PlayPreviousSong") 'don't ever change this string
        Else
            If ExpiredTrack Then
                PlayNextSong()
            End If
        End If
    End Sub

    Sub PlayNextSong()

        Spinner.Visible = True
        prgBar.Value = 0
        prgBar.Update()
        Application.DoEvents()
        ResumePlaying = True

        Try
            Pandora.CurrentStation.CurrentSong = Pandora.CurrentStation.CurrentSong.NextSong
            If IsNothing(Pandora.CurrentStation.CurrentSong) Then
                Throw New Exception("no next song")
            End If
        Catch ex As Exception
            If Pandora.OkToFetchSongs Then
                Try
                    Pandora.CurrentStation.FetchSongs()
                    tbLog.AppendText(">>>GOT NEW SONGS FROM PANDORA<<<" + vbCrLf)
                Catch x As PandoraException
                    If x.ErrorCode = ErrorCodeEnum.PLAYLIST_EXCEEDED Then
                        Bass.BASS_ChannelSetPosition(Stream, 0)
                        Bass.BASS_ChannelPlay(Stream, False)
                        Spinner.Visible = False
                        tbLog.AppendText("Global skip limit reached. Replaying current song..." + vbCrLf)
                        Pandora.SkipLimitReached = True
                        Pandora.SkipLimitReachedAt = Now
                        ddStations.Enabled = False
                        btnNext.Enabled = False
                        btnNext.BackColor = Color.DarkGray
                        Application.DoEvents()
                        Exit Sub
                    End If
                    Throw x
                End Try
                Pandora.CurrentStation.CurrentSong = Pandora.CurrentStation.PlayList.ToArray(Pandora.CurrentStation.PlayList.Count - 4)
            Else
                tbLog.AppendText("Waiting few mins before fetching new songs..." + vbCrLf)
                Bass.BASS_ChannelSetPosition(Stream, 0)
                Bass.BASS_ChannelPlay(Stream, False)
                Spinner.Visible = False
                ddStations.Enabled = False
                btnNext.Enabled = False
                btnNext.BackColor = Color.DarkGray
                Application.DoEvents()
                Exit Sub
            End If
        End Try

        Execute(Sub() PlayCurrentSong(), "PlayNextSong") 'don't ever change this string
    End Sub

    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        IsActiveForm = True
    End Sub

    Private Sub frmMain_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        IsActiveForm = False
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If BASSReady Then
            DeInitBass()
        End If
        unRegisterHotkeys()
        SavePandoraObject()
    End Sub

    Sub InitBass()
        If Not BASSReady Then

            BassNet.Registration("pandorian@sharklasers.com", "2X2531425283122")
            BASSReady = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)

            Dim sw As New Stopwatch
            sw.Start()
            Do Until BASSReady
                If sw.ElapsedMilliseconds > 10000 Then
                    Exit Do
                End If
                System.Threading.Thread.Sleep(1000)
            Loop
            sw.Stop()

            If Not BASSReady Then
                MsgBox("Sorry, having trouble accessing your audio device :-(" + vbCrLf + vbCrLf +
                       "Please double check your gear and restart Pandorian...", MsgBoxStyle.Critical)
            End If

            Dim noProxy As Boolean = Settings.Read("noProxy")
            If Not noProxy Then
                Dim proxy As String = Decrypt(Settings.Read("proyxUsername")) + ":" +
                                      Decrypt(Settings.Read("proxyPassword")) + "@" +
                                      Decrypt(Settings.Read("proxyAddress")).Replace("http://", "")
                ProxyPtr = Marshal.StringToHGlobalAnsi(proxy)
                Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_PROXY, ProxyPtr)
            End If
            AAC = Bass.BASS_PluginLoad("bass_aac.dll")
            tbLog.AppendText("Initialized BASS..." + vbCrLf)
        End If
    End Sub

    Sub DeInitBass()
        If BASSReady Then
            Bass.BASS_StreamFree(Stream)
            Bass.BASS_PluginFree(AAC)
            Bass.BASS_Free()
            DSP.Stop()
            Marshal.FreeHGlobal(ProxyPtr)
            BASSReady = False
            tbLog.AppendText("De-Initialized BASS..." + vbCrLf)
        End If
    End Sub

    Private Sub PlayCurrentSongWithBASS()
        If Not Stream = 0 Then
            Bass.BASS_StreamFree(Stream)
            Stream = 0
        End If

        Dim song = Pandora.CurrentStation.CurrentSong

        If File.Exists(song.AudioFileName) And song.FinishedDownloading Then
            tbLog.AppendText("Loaded song from local cache." + vbCrLf)
            Stream = Bass.BASS_StreamCreateFile(song.AudioFileName, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE)
        Else
            tbLog.AppendText("Downloading song from pandora." + vbCrLf)
            Stream = Bass.BASS_StreamCreateURL(
                song.AudioUrlMap(Settings.Read("audioQuality")).Url,
                0,
                BASSFlag.BASS_STREAM_AUTOFREE,
                DownloadProc,
                IntPtr.Zero)
        End If

        If Not Stream = 0 Then
            tbLog.AppendText("Playing the song now..." + vbCrLf)
            Bass.BASS_ChannelSetSync(Stream, BASSSync.BASS_SYNC_END, 0, Sync, IntPtr.Zero)
            Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, volSlider.Value / 100)

            DSP.ChannelHandle = Stream
            DSP.Gain_dBV = song.TrackGain
            DSP.Start()
            tbLog.AppendText("ReplayGain Applied: " + song.TrackGain.ToString + " dB" + vbCrLf)

            If ResumePlaying Then
                Bass.BASS_ChannelPlay(Stream, False)
            End If

            BPMCounter.Reset(44100)

            Application.DoEvents()
            song.PlayingStartTime = Now
            song.AudioDurationSecs = SongDurationSecs()
            ShareTheLove()
        Else
            If Bass.BASS_ErrorGetCode = BASSError.BASS_ERROR_FILEOPEN Then
                Throw New PandoraException(ErrorCodeEnum.SONG_URL_NOT_VALID, "Audio URL has probably expired...")
            Else
                MsgBox("Couldn't open stream: " + Bass.BASS_ErrorGetCode().ToString + vbCr +
                       "Try restarting the app...", MsgBoxStyle.Critical)
            End If
        End If

    End Sub

    Private Sub bpmTimer_Tick(sender As Object, e As EventArgs) Handles bpmTimer.Tick

        If BASSChannelState() = BASSActive.BASS_ACTIVE_PLAYING Then
            Dim beat As Boolean = BPMCounter.ProcessAudio(Stream, True)
            If beat Then
                lblBPM.Text = BPMCounter.BPM.ToString("#00.0")
            End If
        End If

    End Sub

    Private Sub ShareTheLove()
        Dim launchCount = Settings.Read("launchCount")
        Dim triggers As Integer() = {3, 10, 20, 30, 40}
        For Each t In triggers
            If t = launchCount And NagShown = False Then

                If MsgBox("Hi there!" + vbCrLf + vbCrLf +
                          "Glad to see you're enjoying Pandorian..." + vbCrLf + vbCrLf +
                          "Would you like to help Pandorian grow by sharing on Facebook?",
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo,
                          Title:="SHARE THE LOVE?") = MsgBoxResult.Yes Then
                    Process.Start("https://www.facebook.com/dialog/feed?app_id=1442573219316352&link=http://pandorian.djnitehawk.com&redirect_uri=https://www.facebook.com/&name=I%27m%20listening%20to%20Pandora%20on%20my%20desktop%20with%20PANDORIAN...")
                End If
                NagShown = True
                Exit For
            End If
        Next

        If launchCount >= 90 Then
            Settings.Write("launchCount", 39)
        End If
    End Sub

    Public Function HasSettings() As Boolean

        With Settings
            If .ValueCount = 0 Then
                File.Delete(APIFile)
                .Write("proxyAddress", "http://server:port")
                .Write("proyxUsername", "")
                .Write("proxyPassword", "")
                .Write("pandoraUsername", "")
                .Write("pandoraPassword", "")
                .Write("lastStationID", 0)
                .Write("audioQuality", "mediumQuality")
                .Write("pandoraOne", 0)
                .Write("downloadLocation", "")
                .Write("noProxy", 0)
                .Write("launchCount", 0)
                .Write("hkModifier", 1)
                .Write("hkPlayPause", 32)
                .Write("hkLike", 76)
                .Write("hkDislike", 68)
                .Write("hkSkip", 83)
                .Write("hkShowHide", 80)
                .Write("hkBlock", 66)
                .Write("hkSleep", 27)
                .Write("hkGlobalMenu", 77)
                .Write("hkLock", 88)
                .Write("hkVolDown", 40)
                .Write("hkVolUp", 38)
                .Write("unlockPassword", "")
                .Write("noQmix", 0)
                .Write("noLiked", 0)
                Return False
            End If
        End With

        Dim prxSettingsReqd As Boolean

        Dim noProxy As Boolean = Settings.Read("noProxy")
        If noProxy = True Then
            prxSettingsReqd = False
        Else
            If Decrypt(Settings.Read("proxyAddress")) = "http://server:port" Then
                prxSettingsReqd = True
            End If
        End If

        If prxSettingsReqd Or
            String.IsNullOrEmpty(Decrypt(Settings.Read("pandoraUsername"))) Or
            String.IsNullOrEmpty(Decrypt(Settings.Read("pandoraPassword"))) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        Dim downLoc = Settings.Read("downloadLocation")

        If Debugger.IsAttached Then
            If e.Control And e.Alt And e.KeyCode = Keys.E Then
                DebugExpireSessionNow()
            End If
        End If

        If e.Control And e.Alt And e.KeyCode = Keys.L Then
            If tbLog.Visible Then
                tbLog.Visible = False
            Else
                tbLog.Visible = True
                tbLog.SelectionStart = tbLog.Text.Length
                tbLog.ScrollToCaret()
            End If
        End If

        '192k file download while playing any stream
        If e.Control And e.Alt And e.KeyCode = Keys.D And
                            Not IsNothing(Pandora.CurrentStation.CurrentSong) And
                            prgDownload.Value = 100 And
                            Pandora.Session.User.PartnerCredentials.AccountType = Engine.Data.AccountType.PANDORA_ONE_USER Then

            If Not Directory.Exists(downLoc) Then
                If folderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                    downLoc = folderBrowser.SelectedPath
                    Settings.Write("downloadLocation", downLoc)
                Else
                    Exit Sub
                End If
            End If

            If Directory.Exists(downLoc) Then
                TargeFile = downLoc + "\" + ValidFileName(Pandora.CurrentStation.CurrentSong.Artist) + " - " + ValidFileName(Pandora.CurrentStation.CurrentSong.Title) + ".mp3"

                If Not File.Exists(TargeFile) Then
                    Downloader = New WebClient
                    AddHandler Downloader.DownloadFileCompleted, AddressOf FileDownloadCompleted
                    AddHandler Downloader.DownloadProgressChanged, AddressOf FileDownloadProgressChanged
                    Dim noProxy As Boolean = Settings.Read("noProxy")
                    If Not noProxy Then
                        Downloader.Proxy = Me.Proxy
                    End If
                    Downloader.DownloadFileAsync(
                            New Uri(Pandora.CurrentStation.CurrentSong.AudioUrlMap("highQuality").Url), TargeFile)
                    prgDownload.Visible = True
                End If
            End If
        End If

        '192k file export while playing 192k stream
        If e.Control And e.KeyCode = Global.System.Windows.Forms.Keys.D And
                            Not IsNothing(Pandora.CurrentStation.CurrentSong) And
                            prgDownload.Value > 99 And
                            Settings.Read("audioQuality") = "highQuality" And
                            Me.Pandora.Session.User.PartnerCredentials.AccountType = Global.Pandorian.Engine.Data.AccountType.PANDORA_ONE_USER Then

            If Not Directory.Exists(downLoc) Then
                If Me.folderBrowser.ShowDialog = Global.System.Windows.Forms.DialogResult.OK Then
                    downLoc = folderBrowser.SelectedPath
                    Settings.Write("downloadLocation", downLoc)
                Else
                    Exit Sub
                End If
            End If

            If Directory.Exists(downLoc) Then
                TargeFile = downLoc + "\" + ValidFileName(Pandora.CurrentStation.CurrentSong.Artist) + " - " + ValidFileName(Pandora.CurrentStation.CurrentSong.Title) + ".mp3"

                If Not File.Exists(TargeFile) Then
                    File.Copy(Pandora.CurrentStation.CurrentSong.AudioFileName, TargeFile)
                    MsgBox("Mp3 File Exported!", MsgBoxStyle.Information)
                End If
            End If
        End If
    End Sub

    Private Sub FileDownloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        prgDownload.Visible = False
        prgDownload.Value = 0
        If Not IsNothing(e.Error) Then
            File.Delete(TargeFile)
            MsgBox(e.Error.Message, MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub FileDownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        prgDownload.Value = e.ProgressPercentage
    End Sub

    Private Function ValidFileName(Text As String) As String
        For Each c In IO.Path.GetInvalidFileNameChars
            Text = Text.Replace(c, "_")
        Next
        Return Text
    End Function

    Private Sub CleanUp()

        For Each f In Directory.GetFiles(Directory.GetCurrentDirectory(), "*.stream")
            Try
                File.Delete(f)
            Catch ex As Exception
                'do null
            End Try
        Next
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False
        If Not HasSettings() Then
            frmSettings.Show()
            Me.Hide()
            Exit Sub
        End If
        frmLockScreen.Show()
        frmLockScreen.Visible = False
        lblAlbumName.UseMnemonic = False
        lblArtistName.UseMnemonic = False
        lblSongName.UseMnemonic = False
        If Not Debugger.IsAttached Then
            LogAppStartEvent()
        End If
        Dim launchCount As Integer = Settings.Read("launchCount") + 1
        Settings.Write("launchCount", launchCount)
        CheckForUpdate()
        registerHotkeys()
        populateSleepTimes()
        AddHandler SystemEvents.PowerModeChanged, AddressOf PowerModeChanged
        AddHandler SystemEvents.SessionEnding, AddressOf MachineShutDown
        Application.DoEvents()
    End Sub

    Private Sub TrayIcon_MouseClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            TrayIcon.Visible = False
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            Me.Activate()
            HideSongInfo = True
            SongInfo.Hide()
        End If
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If WindowState = FormWindowState.Normal Or Me.Visible = True Then
            TrayIcon.Visible = False
        End If

        If WindowState = FormWindowState.Minimized Or (Me.Visible = False And IsNothing(sender)) Then
            TrayIcon.Visible = True
            HideSongInfo = False
            TrayIcon_MouseMove(Nothing, Nothing)
            Me.Visible = False
        End If

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.DoEvents()
        Execute(Sub() RunNow(), "frmMain_Shown.RunNow")
    End Sub

    Private Sub SavePandoraObject()
        If Not IsNothing(Pandora) Then
            Using stream As Stream = File.Create(APIFile)
                Try
                    Dim formatter As New BinaryFormatter()
                    formatter.Serialize(stream, Pandora)
                    tbLog.AppendText("Saved the api object to disk..." + vbCrLf)
                Catch e As Exception
                    tbLog.AppendText("Failed to save the api object to disk..." + vbCrLf)
                    ReportError(e, "SavePandoraObject")
                End Try
            End Using
        End If
    End Sub

    Private Sub RestorePandoraObject()


        If File.Exists(APIFile) Then
            Try
                Using stream As Stream = File.Open(APIFile, FileMode.Open, FileAccess.Read)
                    Dim formatter As New BinaryFormatter()
                    Pandora = DirectCast(formatter.Deserialize(stream), API)
                    tbLog.AppendText("Restored the api object from disk..." + vbCrLf)
                    ServicePointManager.Expect100Continue = False
                End Using
                Exit Sub
            Catch e As Exception
                File.Delete(APIFile)
                tbLog.AppendText("Failed to restore the api object from disk..." + vbCrLf)
                ReportError(e, "RestorePandoraObject")
            End Try
        End If
        Pandora = New API(Settings.Read("pandoraOne"))
    End Sub

    Sub RunNow()

        Spinner.Visible = True
        Application.DoEvents()

        If Not HasSettings() Then
            frmSettings.Show()
            Me.Hide()
            Exit Sub
        End If

        RestorePandoraObject()

        Dim noProxy As Boolean = Settings.Read("noProxy")
        If Not noProxy Then
            Dim prxUser = Settings.Read("proyxUsername")
            Dim prxPass = Settings.Read("proxyPassword")
            Me.Proxy = New WebProxy(Decrypt(Settings.Read("proxyAddress")))
            If Not String.IsNullOrEmpty(Decrypt(prxUser)) And Not String.IsNullOrEmpty(Decrypt(prxPass)) Then
                Me.Proxy.Credentials = New NetworkCredential(Decrypt(prxUser), Decrypt(prxPass))
            End If
            Pandora.Proxy = Me.Proxy
        Else
            Me.Proxy = Nothing
            Pandora.Proxy = Nothing
        End If

        WaitForNetConnection()

        If Not IsLoggedIn() Then
            If Not LoginToPandora() Then
                frmSettings.Show()
                Me.Hide()
                Exit Sub
            End If
        End If

        LoadStationList()

        If Not IsNothing(Pandora.CurrentStation) Then

            If Not String.IsNullOrEmpty(Pandora.CurrentStation.Id) Then

                ddStations.SelectedIndex = ddStations.FindStringExact(Pandora.CurrentStation.Name)
                tbLog.AppendText("Current station: " + Pandora.CurrentStation.Name + vbCrLf)

                InitBass()

                Pandora.SkipLimitReached = False

                If IsNothing(Pandora.CurrentStation.CurrentSong) Then
                    Execute(Sub() PlayNextSong(), "RunNow.PlayNextSong")
                Else
                    Execute(Sub() PlayCurrentSong(), "RunNow.PlayCurrentSong")
                End If

            End If
        End If

    End Sub

    Private Sub ddStations_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ddStations.SelectionChangeCommitted

        If Not ddStations.SelectedValue = Pandora.CurrentStation.Id Then

            Spinner.Visible = True
            Application.DoEvents()
            ddStations.Enabled = False

            prgBar.Value = 0
            Bass.BASS_ChannelStop(Stream)

            For Each s In Pandora.AvailableStations
                If s.Id = ddStations.SelectedValue Then
                    Pandora.CurrentStation = s
                    Exit For
                End If
            Next

            Settings.Write("lastStationID", Pandora.CurrentStation.Id)

            tbLog.AppendText("Station changed to: " + Pandora.CurrentStation.Name + vbCrLf)

            If IsNothing(Pandora.CurrentStation.CurrentSong) Then
                Execute(Sub() PlayNextSong(), "ddStations_SelectedIndexChanged.PlayNextSong")
            Else
                Execute(Sub() PlayCurrentSong(), "ddStations_SelectedIndexChanged.PlayCurrentSong")
            End If

        End If

    End Sub

    Function BASSChannelState() As BASSActive
        Return Bass.BASS_ChannelIsActive(Stream)
    End Function
    Private Sub btnPlayPause_Click(sender As Object, e As EventArgs) Handles btnPlayPause.Click

        If BASSChannelState() = BASSActive.BASS_ACTIVE_PLAYING Then
            Bass.BASS_ChannelPause(Stream)
            btnPlayPause.Image = My.Resources.play
        ElseIf BASSChannelState() = BASSActive.BASS_ACTIVE_PAUSED Then
            Bass.BASS_ChannelPlay(Stream, False)
            btnPlayPause.Image = My.Resources.paused
        ElseIf BASSChannelState() = BASSActive.BASS_ACTIVE_STOPPED And ResumePlaying = False Then
            ResumePlaying = True
            Bass.BASS_ChannelPlay(Stream, False)
            btnPlayPause.Image = My.Resources.paused
        Else
            btnPlayPause.Image = Nothing
            btnPlayPause.Text = ":-("
        End If
    End Sub
    Sub UpdatePlayPosition()
        Dim pos As Double = (CurrentPositionSecs() / SongDurationSecs() * 100)
        If pos >= 1 And pos <= 100 Then
            prgBar.Value = Convert.ToInt32(pos)
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        UpdatePlayPosition()
        UpdateDownloadProgress()
        SleepCheck()
        If volSlider.Visible Then
            If VolLastChangedOn.AddSeconds(4) <= Now Then
                volSlider.Hide()
            End If
        End If
    End Sub

    Sub SongEnded(ByVal handle As Integer, ByVal channel As Integer, ByVal data As Integer, ByVal user As IntPtr)
        Execute(Sub() PlayNextSong(), "SongEnded.PlayNextSong")
    End Sub

    Private Sub DebugExpireSessionNow()
        Pandora.Session.DebugCorruptAuthToken()
        Pandora.Session.User.DebugCorruptAuthToken()
        'Dim quality = Settings.Read("audioQuality")
        'Pandora.CurrentStation.CurrentSong.DebugCorruptAudioUrl(quality)
        'For Each s In Pandora.CurrentStation.PlayList
        '    s.DebugCorruptAudioUrl(quality)
        'Next
    End Sub

    Private ErrCount As Integer = 0
    Private Delegate Sub ExecuteDelegate()
    Private Sub Execute(Logic As ExecuteDelegate, Caller As String)
        If ErrCount > 0 Then
            ErrCount = 0
            Exit Sub
        End If

        Try
            Logic()
        Catch pex As PandoraException

            Select Case pex.ErrorCode
                Case ErrorCodeEnum.AUTH_INVALID_TOKEN
                    Try
                        tbLog.AppendText("Session expired. Loggin in again..." + vbCrLf)
                        ReLoginToPandora()
                    Catch ex As Exception
                        MsgBox("Pandora session has expired." + vbCrLf + vbCrLf +
                               "Tried to re-login but something went wrong :-(" + vbCrLf + vbCrLf +
                               "Try restarting Pandorian...", MsgBoxStyle.Exclamation)
                        AfterErrorActions()
                    End Try
                Case ErrorCodeEnum.SONG_URL_NOT_VALID
                    If Caller = "PlayNextSong" Then 'don't ever change this string
                        tbLog.AppendText("Song URL expired. Trying the next song..." + vbCrLf)
                        PlayNextSong()
                    End If
                    If Caller = "PlayPreviousSong" Then 'don't ever change this string
                        tbLog.AppendText("Song URL expired. Trying the previous song..." + vbCrLf)
                        PlayPreviousSong(True)
                    End If
                Case ErrorCodeEnum.LICENSE_RESTRICTION
                    MsgBox("Looks like your country is not supported. Try using a proxy...", MsgBoxStyle.Exclamation)
                    AfterErrorActions()
                Case ErrorCodeEnum.PLAYLIST_EXCEEDED
                    MsgBox("Global song skip limit reached." + vbCrLf + vbCrLf +
                           "New songs cannot be played until skip limit is lifted by Pandora." + vbCrLf + vbCrLf +
                           "Please quit the app and try again after 10 minutes...", MsgBoxStyle.Exclamation)
                    AfterErrorActions()
                Case ErrorCodeEnum.PLAYLIST_EMPTY_FOR_STATION
                    MsgBox("No songs were found for this station." + vbCrLf + vbCrLf +
                           "Please try adding a few seeds to this station using Pandora website...", MsgBoxStyle.Exclamation)
                    AfterErrorActions()
                Case Else
                    ReportError(pex, Caller)
                    AfterErrorActions()
                    ErrCount = 1
            End Select

        Catch ex As Exception
            ReportError(ex, Caller)
            AfterErrorActions()
        End Try
    End Sub

    Private Sub ReportError(Exception As Exception, Caller As String)

        Dim msg As String

        If TypeOf Exception Is PandoraException Then

            Dim exp As PandoraException = DirectCast(Exception, PandoraException)
            msg = "Pandora Error: " + exp.Message + vbCrLf +
                  "Error Code: " + exp.ErrorCode.ToString + vbCrLf +
                  "Caller: " + Caller
        Else
            msg = "Error: " + Exception.Message + vbCrLf +
                  "Caller: " + Caller
        End If

        Dim resp = MsgBox(msg + vbCrLf + vbCrLf +
                          "Would you like to report this error?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, Title:="Whoops!")

        If resp = MsgBoxResult.Yes Then
            msg = msg + vbCrLf + vbCrLf + tbLog.Text
            Clipboard.SetText(msg)
            MsgBox("Error details have been copied to the clipboard." + vbCrLf +
                   "You will now be taken to the Pandorian support page.", vbInformation)
            Process.Start("https://github.com/dj-nitehawk/Pandorian/issues/new")
        End If

    End Sub

    Sub AfterErrorActions()
        Spinner.Visible = False
        Application.DoEvents()
        ddStations.Enabled = True
        btnBlock.Enabled = False
        btnPlayPause.Enabled = False
        btnDislike.Enabled = False
        btnLike.Enabled = False
        btnPrev.Enabled = False
        btnNext.Enabled = True
    End Sub

    Private Sub ReLoginToPandora()
        Spinner.Visible = True
        Application.DoEvents()
        Pandora.ClearSession(Settings.Read("pandoraOne"))
        CleanUp()
        SavePandoraObject()
        Execute(Sub() RunNow(), "ReLoginToPandora.RunNow")
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Execute(Sub() PlayNextSong(), "btnSkip_Click.PlayNextSong")
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        PlayPreviousSong(False)
    End Sub

    'Private Sub btnLeft_Click(sender As Object, e As EventArgs)
    '    Bass.BASS_StreamFree(Stream)
    '    Pandora.CurrentStation.LoadPrevSong()
    '    PlayCurrentSong()
    'End Sub

    'Private Sub btnRight_Click(sender As Object, e As EventArgs)
    '    Bass.BASS_StreamFree(Stream)
    '    Pandora.CurrentStation.LoadNextSong()
    '    PlayCurrentSong()
    'End Sub

    Private Sub btnBlock_Click(sender As Object, e As EventArgs) Handles btnBlock.Click
        If btnBlock.Enabled Then
            btnBlock.Enabled = False
            btnBlock.BackColor = Color.Pink
            Execute(Sub() Pandora.TemporarilyBanSong(Pandora.CurrentStation.CurrentSong), "btnBlock_Click.TemporarilyBanSong")

            If Not Pandora.SkipLimitReached Then
                Execute(Sub() PlayNextSong(), "btnBlock_Click.PlayNextSong")
            End If
        End If
    End Sub

    Function SongDurationSecs() As Double
        Return Bass.BASS_ChannelBytes2Seconds(Stream, StreamTotalLength())
    End Function
    Function CurrentPositionSecs() As Double
        Dim pos As Long = Bass.BASS_ChannelGetPosition(Stream)
        Return Bass.BASS_ChannelBytes2Seconds(Stream, pos)
    End Function

    Private Sub btnLike_Click(sender As Object, e As EventArgs) Handles btnLike.Click
        If btnLike.Enabled Then
            btnLike.Enabled = False
            btnLike.BackColor = Color.PaleGreen
            btnDislike.Enabled = True
            btnDislike.BackColor = Control.DefaultBackColor
            Execute(Sub() Pandora.RateSong(Pandora.CurrentStation.CurrentSong, PandoraRating.Love), "btnLike_Click.Pandora.RateSong")
            lblSongName.Text = Pandora.CurrentStation.CurrentSong.GetProperTitle(IndicateLiked)
            RaiseEvent SongInfoUpdated(lblSongName.Text, lblArtistName.Text, lblAlbumName.Text)
        End If
    End Sub

    Private Sub btnDislike_Click(sender As Object, e As EventArgs) Handles btnDislike.Click
        If btnDislike.Enabled Then
            btnDislike.Enabled = False
            btnDislike.BackColor = Color.Pink
            btnLike.Enabled = True
            btnLike.BackColor = Control.DefaultBackColor
            Execute(Sub() Pandora.RateSong(Pandora.CurrentStation.CurrentSong, PandoraRating.Hate), "btnDislike_Click.RateSong")

            If Not Pandora.SkipLimitReached Then
                Execute(Sub() PlayNextSong(), "btnDislike_Click.PlayNextSong")
            End If
        End If
    End Sub

    Private Sub DownloadCoverImage(sender As Object, e As DoWorkEventArgs)
        SongCoverImage.Image = Nothing
        If IsNothing(e.Argument) Then
            SongCoverImage.Image = My.Resources.logo
        Else
            Dim url As String = e.Argument
            Dim web As New WebClient()
            Dim noProxy As Boolean = Settings.Read("noProxy")
            If Not noProxy Then
                web.Proxy = Me.Proxy
            End If
            Try
                Using strm As New IO.MemoryStream(web.DownloadData(url))
                    SongCoverImage.Image = Image.FromStream(strm)
                End Using
            Catch ex As Exception
                SongCoverImage.Image = My.Resources.logo
            End Try
        End If
        RaiseEvent CoverImageUpdated(SongCoverImage.Image)
    End Sub

    Sub LogAppStartEvent()
        Dim web As New WebClient()
        Try
            web.DownloadDataAsync(New Uri("http://s07.flagcounter.com/mini/f3Ey/bg_FFFFFF/txt_000000/border_CCCCCC/flags_0/"))
        Catch ex As Exception
            tbLog.AppendText("LogAppStart: " + ex.Message + vbCrLf)
        End Try
        web = Nothing
    End Sub

    Private Sub miShowSettings_Click(sender As Object, e As EventArgs) Handles miShowSettings.Click
        Me.Hide()
        frmSettings.Show()
    End Sub
    Private Sub miManageStation_Click(sender As Object, e As EventArgs) Handles miManageStation.Click
        btnPlayPause_Click(Nothing, Nothing)
        Me.Hide()
        frmBrowser.Show()
    End Sub
    Public Function GetStationURL() As String
        'Return Pandora.CurrentStation.StationURL.Replace("login?target=%2F", "")
        Return "https://www.pandora.com/station/play/" + Pandora.CurrentStation.Id
    End Function

    Private Sub miUpdate_Click(sender As Object, e As EventArgs) Handles miUpdate.Click
        Process.Start("http://pandorian.djnitehawk.com/?utm_source=pandorian.app&utm_medium=direct.link&utm_campaign=visit.website")
    End Sub

    Private Sub miSendFeedback_Click(sender As Object, e As EventArgs) Handles miSendFeedback.Click
        Process.Start("https://github.com/dj-nitehawk/Pandorian/issues/new")
    End Sub
    Private Sub MenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MenuStrip.Opening
        Try
            miVersion.Text = "Pandorian v" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Catch ex As Exception
            miVersion.Text = "www.djnitehawk.com"
        End Try
    End Sub

    Private Sub CheckForUpdate()
        Dim web As New WebClient()
        AddHandler web.DownloadStringCompleted, AddressOf CheckForUpdateCompleted
        web.DownloadStringAsync(New Uri("http://pandorian.djnitehawk.com/version.html"))
    End Sub

    Private Sub CheckForUpdateCompleted(sender As Object, e As DownloadStringCompletedEventArgs)
        Try

            Dim currVer As New Version(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString)
            Dim newVer As New Version(e.Result)

            If currVer < newVer Then
                Dim res As MsgBoxResult = MsgBox("Pandorian has a new update: v" + e.Result.ToString + vbCrLf + vbCrLf +
                                                 "Would you like to visit the Pandorian website now?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, Title:="New Update Available")
                If res = MsgBoxResult.Yes Then
                    Process.Start("http://pandorian.djnitehawk.com/?utm_source=pandorian.app&utm_medium=direct.link&utm_campaign=download.update")
                End If
            End If

        Catch ex As Exception
            'do none
        End Try
    End Sub

    Private Sub miSleepTimer_Click(sender As Object, e As EventArgs) Handles miSleepTimer.Click
        pnlSleepTimer.Visible = True
    End Sub

    Private Sub populateSleepTimes()
        Dim dict As New Dictionary(Of String, Integer)
        Dim i As Integer = 1

        Do Until i = 25
            dict.Add(i.ToString + " Hours", i)
            i = i + 1
        Loop

        ddSleepTimes.ValueMember = "Value"
        ddSleepTimes.DisplayMember = "Key"
        ddSleepTimes.DataSource = New BindingSource(dict, Nothing)

    End Sub

    Private Sub btnSTDone_Click(sender As Object, e As EventArgs) Handles btnSTDone.Click
        pnlSleepTimer.Visible = False
    End Sub

    Private Sub chkSleep_CheckedChanged(sender As Object, e As EventArgs) Handles chkSleep.CheckedChanged
        If chkSleep.Checked Then
            ddSleepTimes.Enabled = False
            If SleepNow Then
                SleepNow = False
                SleepAt = Now
            Else
                SleepAt = Now.AddHours(ddSleepTimes.SelectedValue)
                'SleepAt = Now.AddSeconds(ddSleepTimes.SelectedValue) 'TEST MODE
            End If
        Else
            ddSleepTimes.Enabled = True
            SleepAt = Date.MinValue
            lblSleepStatus.Text = "Sleep Timer Disabled"
        End If
    End Sub

    Private Sub SleepCheck()
        If chkSleep.Checked And Not SleepAt = Date.MinValue Then

            If Now >= SleepAt Then
                PreSleepActivities()
                Application.SetSuspendState(PowerState.Suspend, False, False)
            Else
                Dim remTime As TimeSpan = SleepAt.Subtract(Now)
                lblSleepStatus.Text = remTime.Hours.ToString + ":" + remTime.Minutes.ToString + ":" + remTime.Seconds.ToString
            End If

        End If
    End Sub

    Private Sub WaitForNetConnection()

        Dim noNet As Boolean
        Dim sw As New Stopwatch
        sw.Start()
        Do Until NetConnectionAvailable()
            If sw.ElapsedMilliseconds > 10000 Then

                MsgBox("Sorry, but it looks like your internet is down." + vbCrLf + vbCrLf +
                       "Please try again later...", MsgBoxStyle.Exclamation)
                noNet = True
                Exit Do
            End If
            Threading.Thread.Sleep(1000)
        Loop
        sw.Stop()
        If noNet Then
            Me.Close()
        End If
    End Sub

    Private Function NetConnectionAvailable() As Boolean
        If My.Computer.Network.IsAvailable Then
            If My.Computer.Network.Ping("8.8.8.8") Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub PowerModeChanged(sender As Object, e As PowerModeChangedEventArgs)
        Select Case e.Mode
            Case PowerModes.Resume
                Spinner.Visible = True
                Application.DoEvents()
                tbLog.AppendText("Machine woke up from sleep..." + vbCrLf)
                WaitForNetConnection()
                InitBass()
                Execute(Sub() PlayCurrentSong(), "PowerModeChanged.PlayCurrentSong")
            Case PowerModes.Suspend
                PreSleepActivities()
        End Select
    End Sub

    Private Sub PreSleepActivities()
        tbLog.AppendText("Machine is going in to sleep now..." + vbCrLf)
        Timer.Enabled = False
        chkSleep.Checked = False
        ddSleepTimes.Enabled = True
        lblSleepStatus.Text = "Sleep Timer Disabled"
        SleepAt = DateTime.MinValue
        If BASSChannelState() = BASSActive.BASS_ACTIVE_PAUSED Then
            ResumePlaying = False
        ElseIf BASSChannelState() = BASSActive.BASS_ACTIVE_PLAYING Then
            ResumePlaying = True
        End If
        DeInitBass()
        SavePandoraObject() 'in case power is lost during sleep
    End Sub

    Public Sub MachineShutDown(ByVal sender As Object, ByVal e As SessionEndingEventArgs)
        frmMain_FormClosing(Nothing, Nothing)
    End Sub

    Private Sub TrayMenu_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles TrayMenu.Closing
        If frmLockScreen.Visible Then
            Windows.Forms.Cursor.Hide()
        End If

        HideSongInfo = False
    End Sub

    Private Sub TrayMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TrayMenu.Opening
        If Not IsNothing(Pandora) Then

            HideSongInfo = True
            SongInfo.Hide()
            Application.DoEvents()

            If Not IsNothing(Pandora.CurrentStation.CurrentSong) And Not IsNothing(Pandora.CurrentStation) Then
                tmiStationTitle.Text = Pandora.CurrentStation.Name.Replace("&", "&&")
                If Pandora.CurrentStation.CurrentSong.AudioDurationSecs > 60 Then
                    tmiArtistTitle.Text = Pandora.CurrentStation.CurrentSong.Artist.Replace("&", "&&")
                    tmiSongTitle.Text = Pandora.CurrentStation.CurrentSong.GetProperTitle(IndicateLiked).Replace("&", "&&")
                Else
                    tmiArtistTitle.Text = "Pandora The Punisher"
                    tmiSongTitle.Text = "42 Sec Blank Audio"
                    tmiPlayPause.Enabled = False
                End If
            End If
            Select Case BASSChannelState()
                Case BASSActive.BASS_ACTIVE_PAUSED
                    tmiPlayPause.Text = "Play"
                Case BASSActive.BASS_ACTIVE_PLAYING
                    tmiPlayPause.Text = "Pause"
                Case Else
                    tmiPlayPause.Text = "Play/Pause"
            End Select
            tmiPlayPause.Enabled = True
            tmiLikeCurrentSong.Enabled = btnLike.Enabled
            tmiDislikeCurrentSong.Enabled = btnDislike.Enabled
            tmiSkipSong.Enabled = btnNext.Enabled
            tmiBlockSong.Enabled = btnBlock.Enabled
            If frmLockScreen.Visible Then
                tmiExit.Enabled = False
                tmiLockScreen.Enabled = False
            Else
                tmiExit.Enabled = True
                tmiLockScreen.Enabled = True
            End If
            If frmLockScreen.Visible Then
                Windows.Forms.Cursor.Show()
            End If
        End If
    End Sub

    Private Sub tmiLikeCurrentSong_Click(sender As Object, e As EventArgs) Handles tmiLikeCurrentSong.Click
        btnLike_Click(Nothing, Nothing)
    End Sub

    Private Sub tmiDislikeCurrentSong_Click(sender As Object, e As EventArgs) Handles tmiDislikeCurrentSong.Click
        btnDislike_Click(Nothing, Nothing)
    End Sub

    Private Sub tmiPlayPause_Click(sender As Object, e As EventArgs) Handles tmiPlayPause.Click
        btnPlayPause_Click(Nothing, Nothing)
    End Sub

    Private Sub tmiSkipSong_Click(sender As Object, e As EventArgs) Handles tmiSkipSong.Click
        btnSkip_Click(Nothing, Nothing)
    End Sub

    Private Sub tmiBlockSong_Click(sender As Object, e As EventArgs) Handles tmiBlockSong.Click
        btnBlock_Click(Nothing, Nothing)
    End Sub

    Private Sub tmiSleepComputer_Click(sender As Object, e As EventArgs) Handles tmiSleepComputer.Click
        handleHotKeyEvent(7)
    End Sub

    Private Sub tmiLockScreen_Click(sender As Object, e As EventArgs) Handles tmiLockScreen.Click
        handleHotKeyEvent(9)
    End Sub

    Private Sub tmiExit_Click(sender As Object, e As EventArgs) Handles tmiExit.Click
        Me.Close()
    End Sub

    Private Sub HotKeyTextBoxes_Enter(sender As Object, e As EventArgs) Handles _
        tbHKPlayPause.Enter,
        tbHKLikeSong.Enter,
        tbHKDislikeSong.Enter,
        tbHKSkipSong.Enter,
        tbHKShowHide.Enter,
        tbHKGlobalMenu.Enter,
        tbHKSleepNow.Enter,
        tbHKBlockSong.Enter,
        tbHKLockNow.Enter,
        tbHKVolDown.Enter,
        tbHKVolUp.Enter

        Dim tb As TextBox = DirectCast(sender, TextBox)
        tb.Clear()
    End Sub

    Private Sub HotKeyTextBoxes_Leave(sender As Object, e As EventArgs) Handles _
        tbHKPlayPause.Leave,
        tbHKLikeSong.Leave,
        tbHKDislikeSong.Leave,
        tbHKSkipSong.Leave,
        tbHKShowHide.Leave,
        tbHKGlobalMenu.Leave,
        tbHKSleepNow.Leave,
        tbHKBlockSong.Leave,
        tbHKLockNow.Leave,
        tbHKVolDown.Leave,
        tbHKVolUp.Leave

        Dim tb As TextBox = DirectCast(sender, TextBox)
        tb.Text = [Enum].GetName(GetType(Keys), tb.Tag)
    End Sub

    Private Sub HotKeyTextBoxes_KeyUp(sender As Object, e As KeyEventArgs) Handles _
        tbHKPlayPause.KeyUp,
        tbHKLikeSong.KeyUp,
        tbHKDislikeSong.KeyUp,
        tbHKSkipSong.KeyUp,
        tbHKShowHide.KeyUp,
        tbHKGlobalMenu.KeyUp,
        tbHKSleepNow.KeyUp,
        tbHKBlockSong.KeyUp,
        tbHKLockNow.KeyUp,
        tbHKVolDown.KeyUp,
        tbHKVolUp.KeyUp

        Dim tb As TextBox = DirectCast(sender, TextBox)
        tb.Tag = e.KeyData
        tb.Text = [Enum].GetName(GetType(Keys), tb.Tag)
        tb.SelectAll()
    End Sub

    Private Sub btnSaveHotkeys_Click(sender As Object, e As EventArgs) Handles btnSaveHotkeys.Click
        Dim HKList As New List(Of Keys)
        For Each c In pnlHotKeys.Controls
            Dim tb As TextBox = TryCast(c, TextBox)
            If Not IsNothing(tb) Then
                If Not HKList.Contains(tb.Tag) Then
                    HKList.Add(tb.Tag)
                End If
            End If
        Next

        If HKList.Count < 11 Then
            MsgBox("You cannot use the same key for more than one function!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        With Settings
            .Write("hkModifier", CType(cbModKey.SelectedValue, Integer))
            .Write("hkBlock", tbHKBlockSong.Tag)
            .Write("hkDislike", tbHKDislikeSong.Tag)
            .Write("hkGlobalMenu", tbHKGlobalMenu.Tag)
            .Write("hkLike", tbHKLikeSong.Tag)
            .Write("hkPlayPause", tbHKPlayPause.Tag)
            .Write("hkShowHide", tbHKShowHide.Tag)
            .Write("hkSkip", tbHKSkipSong.Tag)
            .Write("hkSleep", tbHKSleepNow.Tag)
            .Write("hkLock", tbHKLockNow.Tag)
            .Write("hkVolDown", tbHKVolDown.Tag)
            .Write("hkVolUp", tbHKVolUp.Tag)
        End With

        pnlHotKeys.Visible = False

        MsgBox("HotKeys saved. Restart app to use new configuration.", MsgBoxStyle.Information)


    End Sub

    Private Sub prgBar_Click(sender As Object, e As EventArgs) Handles prgBar.Click
        volSlider.Visible = True
        VolLastChangedOn = Now
    End Sub

    Private Sub volSlider_ValueChanged(sender As Object, e As EventArgs) Handles volSlider.ValueChanged
        If volSlider.Value >= 0 <= 100 Then
            If BASSReady Then
                Bass.BASS_ChannelSetAttribute(Stream, BASSAttribute.BASS_ATTRIB_VOL, volSlider.Value / 100)
            End If
        End If
        VolLastChangedOn = Now
    End Sub

    Private Sub btnLike_MouseHover(sender As Object, e As EventArgs) Handles btnLike.MouseHover
        tip.Show("Like Song", btnLike)
    End Sub

    Private Sub btnDislike_MouseHover(sender As Object, e As EventArgs) Handles btnDislike.MouseHover
        tip.Show("Dislike Song", btnDislike)
    End Sub

    Private Sub btnPlayPause_MouseHover(sender As Object, e As EventArgs) Handles btnPlayPause.MouseHover
        tip.Show("Play/Pause Song", btnPlayPause)
    End Sub

    Private Sub btnSkip_MouseHover(sender As Object, e As EventArgs) Handles btnNext.MouseHover
        tip.Show("Skip Song", btnNext)
    End Sub

    Private Sub btnBlock_MouseHover(sender As Object, e As EventArgs) Handles btnBlock.MouseHover
        tip.Show("Block Song For A Month", btnBlock)
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub SongCoverImage_Click(sender As Object, e As EventArgs) Handles SongCoverImage.Click
        If lblBPM.Visible Then
            lblBPM.Visible = False
            bpmTimer.Stop()
            BPMCounter.Reset(44100)
            lblBPM.Text = "000"
        Else
            lblBPM.Visible = True
            bpmTimer.Start()
        End If
    End Sub

    Private Sub TrayIcon_MouseMove(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseMove
        If SongInfo.Visible = False And HideSongInfo = False Then
            With SongInfo
                .artist.Text = Pandora.CurrentStation.CurrentSong.Artist.Replace("&", "&&")
                .track.Text = Pandora.CurrentStation.CurrentSong.GetProperTitle(IndicateLiked).Replace("&", "&&")
                .station.Text = Pandora.CurrentStation.Name.Replace("&", "&&")
            End With
            SongInfo.Show()
        End If
    End Sub

    Private Function MouseOverControl(control As Control) As Boolean
        Dim pt As Point = control.PointToClient(Control.MousePosition)
        Return (pt.X >= 0 AndAlso pt.Y >= 0 AndAlso pt.X <= control.Width AndAlso pt.Y <= control.Height)
    End Function

End Class
