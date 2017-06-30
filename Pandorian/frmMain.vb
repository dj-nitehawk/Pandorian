Imports Pandorian.Engine
Imports Un4seen.Bass
Imports System.Runtime.InteropServices
Imports System.Net
Imports Microsoft.Win32
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.ComponentModel
Imports Pandorian.Engine.Data

Public Class frmMain
    Dim Pandora As API
    Dim Proxy As WebProxy
    Dim BASSReady As Boolean = False
    Dim ProxyPtr As IntPtr
    Dim AAC As Integer
    Dim Stream As Integer
    Dim Sync As SYNCPROC = New SYNCPROC(AddressOf SongEnded)
    Dim DSP As Misc.DSP_Gain = New Misc.DSP_Gain()
    Dim Downloader As WebClient
    Dim PendingExport As Boolean
    Dim TargetFile As String
    Dim IsActiveForm As Boolean
    Dim SleepAt As Date
    Dim SleepNow As Boolean
    Dim ResumePlaying As Boolean = True
    Dim NagShown As Boolean = False
    Dim VolLastChangedOn As Date
    Dim BPMCounter As New Misc.BPMCounter(20, 44100)
    Dim SongInfo As New frmSongInfo()
    Dim HideSongInfo As Boolean = False
    Dim APIFile As String = Path.GetTempPath + "pandorian.v1.api"

    Public Event SongInfoUpdated(Title As String, Artist As String, Album As String)
    Public Event CoverImageUpdated(FileName As String)

    Dim FS As FileStream
    Dim DownloadProc As DOWNLOADPROC = New DOWNLOADPROC(AddressOf DownloadSong)
    Dim Data() As Byte

    Private Sub DownloadSong(buffer As IntPtr, length As Integer, user As IntPtr)
        If FS Is Nothing Then
            Pandora.CurrentStation.CurrentSong.FinishedDownloading = False
            Pandora.CurrentStation.CurrentSong.DownloadedQuality = Settings.audioQuality
            FS = File.OpenWrite(Pandora.CurrentStation.CurrentSong.AudioFileName)
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

    Private Function StreamDownloadedLength() As Long
        Return Bass.BASS_StreamGetFilePosition(Stream, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD)
    End Function

    Private Sub UpdateDownloadProgress()
        If Not PendingExport Then
            Dim len = Bass.BASS_StreamGetFilePosition(Stream, BASSStreamFilePosition.BASS_FILEPOS_END)
            Dim prg = StreamDownloadedLength() * 100 / len
            If prg > 0 Then
                prgDownload.Value = prg
            End If
            If prgDownload.Value = 100 Then
                If prgDownload.Visible Then
                    prgDownload.Visible = False
                End If
            Else
                If Not prgDownload.Visible Then
                    prgDownload.Visible = True
                End If
            End If
        End If
    End Sub

    Public Sub ClearSession()
        If Not IsNothing(Pandora) Then
            Pandora.ClearSession(Settings.pandoraOne)
            SavePandoraObject()

            Dim bgwCleanCache As New BackgroundWorker
            AddHandler bgwCleanCache.DoWork, AddressOf CleanUpCache
            bgwCleanCache.RunWorkerAsync()
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
                    If Not String.IsNullOrEmpty(Settings.unlockPassword) Then
                        frmLockScreen.Show()
                    Else
                        MsgBox("You have not set an unlock password in Pandorian settings." + vbCrLf +
                               "Right-click on the album cover image and select 'Show Settings'", MsgBoxStyle.Information)
                    End If
                End If
            Case 10
                volSlider.Visible = True
                VolLastChangedOn = Now
                Try
                    volSlider.Value = volSlider.Value - 10
                Catch ex As Exception
                    'do nothing
                End Try
            Case 11
                volSlider.Visible = True
                VolLastChangedOn = Now
                Try
                    volSlider.Value = volSlider.Value + 10
                Catch ex As Exception
                    'do nothing
                End Try
        End Select
    End Sub

    Private Sub registerHotkeys()

        Dim modKeys As New Dictionary(Of String, Integer)
        For Each k As Hotkeys.KeyModifier In [Enum].GetValues(GetType(Hotkeys.KeyModifier))
            modKeys.Add(k.ToString, k)
        Next
        cbModKey.DisplayMember = "Key"
        cbModKey.ValueMember = "Value"
        cbModKey.DataSource = New BindingSource(modKeys, Nothing)
        cbModKey.SelectedIndex = cbModKey.FindStringExact(CType(Settings.hkModifier, Hotkeys.KeyModifier).ToString)
        tbHKPlayPause.Text = [Enum].GetName(GetType(Keys), Settings.hkPlayPause)
        tbHKPlayPause.Tag = Settings.hkPlayPause
        tbHKLikeSong.Text = [Enum].GetName(GetType(Keys), Settings.hkLike)
        tbHKLikeSong.Tag = Settings.hkLike
        tbHKDislikeSong.Text = [Enum].GetName(GetType(Keys), Settings.hkDislike)
        tbHKDislikeSong.Tag = Settings.hkDislike
        tbHKSkipSong.Text = [Enum].GetName(GetType(Keys), Settings.hkSkip)
        tbHKSkipSong.Tag = Settings.hkSkip
        tbHKBlockSong.Text = [Enum].GetName(GetType(Keys), Settings.hkBlock)
        tbHKBlockSong.Tag = Settings.hkBlock
        tbHKShowHide.Text = [Enum].GetName(GetType(Keys), Settings.hkShowHide)
        tbHKShowHide.Tag = Settings.hkShowHide
        tbHKGlobalMenu.Text = [Enum].GetName(GetType(Keys), Settings.hkGlobalMenu)
        tbHKGlobalMenu.Tag = Settings.hkGlobalMenu
        tbHKSleepNow.Text = [Enum].GetName(GetType(Keys), Settings.hkSleep)
        tbHKSleepNow.Tag = Settings.hkSleep
        tbHKLockNow.Text = [Enum].GetName(GetType(Keys), Settings.hkLock)
        tbHKLockNow.Tag = Settings.hkLock
        tbHKVolDown.Text = [Enum].GetName(GetType(Keys), Settings.hkVolDown)
        tbHKVolDown.Tag = Settings.hkVolDown
        tbHKVolUp.Text = [Enum].GetName(GetType(Keys), Settings.hkVolUp)
        tbHKVolUp.Tag = Settings.hkVolUp

        Hotkeys.registerHotkey(Me, 1, Settings.hkPlayPause, Settings.hkModifier) 'play/pause
        Hotkeys.registerHotkey(Me, 2, Settings.hkLike, Settings.hkModifier) 'like
        Hotkeys.registerHotkey(Me, 3, Settings.hkDislike, Settings.hkModifier) 'dislike
        Hotkeys.registerHotkey(Me, 4, Settings.hkSkip, Settings.hkModifier) 'skip
        Hotkeys.registerHotkey(Me, 5, Settings.hkShowHide, Settings.hkModifier) 'show/hide pandorian
        Hotkeys.registerHotkey(Me, 6, Settings.hkBlock, Settings.hkModifier) 'block
        Hotkeys.registerHotkey(Me, 7, Settings.hkSleep, Settings.hkModifier) 'sleep
        Hotkeys.registerHotkey(Me, 8, Settings.hkGlobalMenu, Settings.hkModifier) 'show tray menu
        Hotkeys.registerHotkey(Me, 9, Settings.hkLock, Settings.hkModifier) 'show lock screen
        Hotkeys.registerHotkey(Me, 10, Settings.hkVolDown, Settings.hkModifier) 'vol down
        Hotkeys.registerHotkey(Me, 11, Settings.hkVolUp, Settings.hkModifier) 'vol up
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
            If Pandora.Login(Decrypt(Settings.pandoraUsername), Decrypt(Settings.pandoraPassword), Settings.noQmix) Then
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
        Pandora.ClearSession(Settings.pandoraOne)
        Return False
    End Function

    Sub LoadStationList()
        If Not Pandora.AvailableStations.Count = 0 Then

            Dim FoundLastPlayedStation As Boolean
            Dim Stations As New SortedDictionary(Of String, String)
            For Each Station In Pandora.AvailableStations
                Stations.Add(Station.Name, Station.Id)
                If Settings.lastStationID = Station.Id Then
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

        SongCoverImage.Visible = False
        Dim bgwCoverLoader As New BackgroundWorker
        AddHandler bgwCoverLoader.DoWork, AddressOf DownloadCoverImage
        bgwCoverLoader.RunWorkerAsync()

        PlayCurrentSongWithBASS()
        ddStations.Enabled = True
        Timer.Enabled = True

        btnNext.Enabled = True
        btnNext.BackColor = Control.DefaultBackColor
        btnPrev.Enabled = True
        btnPrev.BackColor = Control.DefaultBackColor

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
        If WindowState = FormWindowState.Normal Then
            Return False
        End If
        Return Not Settings.noLiked
    End Function

    Sub PlayPreviousSong(ExpiredTrack As Boolean)
        If IsNothing(Pandora.CurrentStation.CurrentSong) Then
            Exit Sub
        End If

        If Not IsNothing(Pandora.CurrentStation.CurrentSong.PreviousSong) Then
            prgBar.Value = 0
            Spinner.Visible = True
            Application.DoEvents()
            ResumePlaying = True
            Pandora.CurrentStation.CurrentSong = Pandora.CurrentStation.CurrentSong.PreviousSong
            Execute(Sub() PlayCurrentSong(), "PlayPreviousSong") 'don't ever change this string [handling-of-expired-url]
        Else
            If ExpiredTrack Then
                tbLog.AppendText("No previous track. Trying the next one..." + vbCrLf)
                PlayNextSong()
            End If
        End If
    End Sub

    Sub PlayNextSong()

        prgBar.Value = 0
        Spinner.Visible = True
        Application.DoEvents()
        ResumePlaying = True

        Try
            If IsNothing(Pandora.CurrentStation.CurrentSong.NextSong) Then
                Throw New Exception("no next song")
            End If
            If Not Pandora.CurrentStation.CurrentSong.NextSong.IsStillValid Then
                Pandora.CurrentStation.PlayList.RemoveExpiredSongs()
                tbLog.AppendText("Playlist expired. Fetching new songs..." + vbCrLf)
                Throw New Exception("no valid next songs")
            Else
                Pandora.CurrentStation.CurrentSong = Pandora.CurrentStation.CurrentSong.NextSong
            End If
        Catch ex As Exception
            If Pandora.OkToFetchSongs Then
                Try
                    Pandora.CurrentStation.FetchSongs()
                    tbLog.AppendText(">>>GOT 4 NEW SONGS FROM PANDORA<<<" + vbCrLf)
                Catch x As PandoraException
                    If x.ErrorCode = ErrorCodeEnum.PLAYLIST_EXCEEDED Then
                        Bass.BASS_ChannelSetPosition(Stream, 0)
                        Bass.BASS_ChannelPlay(Stream, False)
                        Spinner.Visible = False
                        Application.DoEvents()
                        tbLog.AppendText("Global skip limit reached. Replaying current song..." + vbCrLf)
                        Pandora.SkipLimitReached = True
                        Pandora.SkipLimitReachedAt = Now
                        ddStations.Enabled = False
                        btnNext.Enabled = False
                        btnNext.BackColor = Color.DarkGray
                        Exit Sub
                    End If
                    Throw x
                End Try
            Else
                tbLog.AppendText("Waiting few mins before fetching new songs..." + vbCrLf)
                Bass.BASS_ChannelSetPosition(Stream, 0)
                Bass.BASS_ChannelPlay(Stream, False)
                Spinner.Visible = False
                Application.DoEvents()
                ddStations.Enabled = False
                btnNext.Enabled = False
                btnNext.BackColor = Color.DarkGray
                Exit Sub
            End If
        End Try

        Execute(Sub() PlayCurrentSong(), "PlayNextSong.PlayCurrentSong")
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

            If Not Settings.noProxy Then
                Dim proxy As String = Decrypt(Settings.proyxUsername) + ":" +
                                      Decrypt(Settings.proxyPassword) + "@" +
                                      Decrypt(Settings.proxyAddress).Replace("http://", "")
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
            prgDownload.Value = 100
        Else
            tbLog.AppendText("Downloading song from pandora." + vbCrLf)
            prgDownload.Value = 0
            Stream = Bass.BASS_StreamCreateURL(
                song.AudioUrlMap(Settings.audioQuality).Url,
                0,
                BASSFlag.BASS_STREAM_AUTOFREE,
                DownloadProc,
                IntPtr.Zero)
        End If

        If Not Stream = 0 Then
            tbLog.AppendText("Playing: " + song.Title + vbCrLf)
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

            song.PlayingStartTime = Now
            song.AudioDurationSecs = SongDurationSecs()
            ShareTheLove()
        Else
            Dim errCode = Bass.BASS_ErrorGetCode
            Select Case errCode
                Case BASSError.BASS_ERROR_FILEOPEN
                    Throw New PandoraException(ErrorCodeEnum.SONG_URL_NOT_VALID, "Audio URL has probably expired...")
                Case BASSError.BASS_ERROR_NONET, BASSError.BASS_ERROR_TIMEOUT
                    Throw New PandoraException(ErrorCodeEnum.NO_NET_FOR_BASS, "Bass.Net can't download audio stream...")
                Case Else
                    MsgBox("Couldn't open stream: " + errCode.ToString + vbCr +
                       "Try restarting the app...", MsgBoxStyle.Critical)
            End Select
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
        Dim launchCount = Settings.launchCount
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
            Settings.launchCount = 39
            Settings.SaveToRegistry()
        End If
    End Sub

    Public Function HasSettings() As Boolean

        If Settings.KeyCount = 0 Then
            File.Delete(APIFile)
            Settings.audioQuality = "mediumQuality"
            Settings.downloadLocation = ""
            Settings.lastStationID = ""
            Settings.launchCount = 0
            Settings.noLiked = 0
            Settings.noProxy = 0
            Settings.noQmix = 0
            Settings.enableBPMCounter = 0
            Settings.pandoraOne = 0
            Settings.pandoraPassword = ""
            Settings.pandoraUsername = ""
            Settings.proxyAddress = "http://server:port"
            Settings.proxyPassword = ""
            Settings.proyxUsername = ""
            Settings.unlockPassword = ""
            Settings.hkModifier = 1
            Settings.hkPlayPause = 32
            Settings.hkLike = 76
            Settings.hkDislike = 68
            Settings.hkSkip = 83
            Settings.hkShowHide = 80
            Settings.hkBlock = 66
            Settings.hkSleep = 27
            Settings.hkGlobalMenu = 77
            Settings.hkLock = 88
            Settings.hkVolDown = 40
            Settings.hkVolUp = 38
            Settings.SaveToRegistry()
        Else
            Settings.LoadFromRegistry()
        End If

        Dim prxSettingsReqd As Boolean

        If Settings.noProxy Then
            prxSettingsReqd = False
        Else
            If Decrypt(Settings.proxyAddress) = "http://server:port" Then
                prxSettingsReqd = True
            End If
        End If

        If prxSettingsReqd Or
            String.IsNullOrEmpty(Decrypt(Settings.pandoraUsername)) Or
            String.IsNullOrEmpty(Decrypt(Settings.pandoraPassword)) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        Dim downLoc = Settings.downloadLocation

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

        Dim s = sender

        If e.Control And e.KeyCode = Keys.D And
            Not IsNothing(Pandora.CurrentStation.CurrentSong) And
            Pandora.Session.User.PartnerCredentials.AccountType = Engine.Data.AccountType.PANDORA_ONE_USER Then

            If Not Directory.Exists(downLoc) Then
                If Me.folderBrowser.ShowDialog = Global.System.Windows.Forms.DialogResult.OK Then
                    downLoc = folderBrowser.SelectedPath
                    Settings.downloadLocation = downLoc
                    Settings.SaveToRegistry()
                Else
                    Exit Sub
                End If
            End If

            If Directory.Exists(downLoc) Then
                TargetFile = downLoc + "\" + CleanUpFileName(Pandora.CurrentStation.CurrentSong.Artist + " - " + Pandora.CurrentStation.CurrentSong.Title) + ".mp3"

                If Not File.Exists(TargetFile) Then
                    If e.Alt Then
                        Downloader = New WebClient
                        AddHandler Downloader.DownloadFileCompleted, AddressOf FileDownloadCompleted
                        AddHandler Downloader.DownloadProgressChanged, AddressOf FileDownloadProgressChanged
                        Dim noProxy As Boolean = Settings.noProxy
                        If Not noProxy Then
                            Downloader.Proxy = Me.Proxy
                        End If
                        prgDownload.Value = 0
                        prgDownload.Visible = True
                        PendingExport = True
                        Downloader.DownloadFileAsync(
                                New Uri(Pandora.CurrentStation.CurrentSong.AudioUrlMap("highQuality").Url), TargetFile)
                    Else
                        If Not prgDownload.Value = 100 Then
                            Exit Sub
                        End If
                        If Pandora.CurrentStation.CurrentSong.DownloadedQuality = "highQuality" Then
                            File.Copy(Pandora.CurrentStation.CurrentSong.AudioFileName, TargetFile)
                            MsgBox("Mp3 File Exported!", MsgBoxStyle.Information)
                        Else
                            MsgBox("Didn't export song..." + vbCrLf + "Because it wasn't downloaded at 192k!", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub FileDownloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        PendingExport = False
        prgDownload.Value = 100
        prgDownload.Visible = False
        If Not IsNothing(e.Error) Then
            File.Delete(TargetFile)
            MsgBox(e.Error.Message, MsgBoxStyle.Critical)
        Else
            MsgBox("Mp3 File Exported!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub FileDownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        prgDownload.Value = e.ProgressPercentage
    End Sub

    Private Function CleanUpFileName(Text As String) As String
        For Each c In Path.GetInvalidFileNameChars
            Text = Text.Replace(c, "_")
        Next
        Return Text
    End Function

    Private Sub CleanUpCache(sender As Object, e As DoWorkEventArgs)
        Dim tmp = Path.GetTempPath
        Dim keep As New List(Of String)
        For Each stn In Pandora.AvailableStations
            For Each s In stn.PlayList.ToArray
                keep.Add(tmp + s.Token + ".stream")
            Next
        Next

        Dim keepFiles = keep.ToArray
        Dim files = Directory.GetFiles(tmp, "*.stream")

        For Each f In files
            If Array.IndexOf(keepFiles, f) < 0 Then
                Try
                    File.Delete(f)
                Catch ex As Exception
                    'do null
                End Try
            End If
        Next

        Dim keepCovers As New List(Of String)
        For Each f In keepFiles
            keepCovers.Add(f.Replace(".stream", ".cover"))
        Next

        files = Directory.GetFiles(tmp, "*.cover")
        keepFiles = keepCovers.ToArray

        For Each f In files
            If Array.IndexOf(keepFiles, f) < 0 Then
                Try
                    File.Delete(f)
                Catch ex As Exception
                    'do null
                End Try
            End If
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
        Settings.launchCount = Settings.launchCount + 1
        Settings.SaveToRegistry()
        CheckForUpdate()
        registerHotkeys()
        populateSleepTimes()
        AddHandler SystemEvents.PowerModeChanged, AddressOf PowerModeChanged
        AddHandler SystemEvents.SessionEnding, AddressOf MachineShutDown
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
        Pandora = New API(Settings.pandoraOne)
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

        Dim noProxy As Boolean = Settings.noProxy
        If Not noProxy Then
            Dim prxUser = Settings.proyxUsername
            Dim prxPass = Settings.proxyPassword
            Me.Proxy = New WebProxy(Decrypt(Settings.proxyAddress))
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

                If IsNothing(Pandora.CurrentStation.CurrentSong) Then '[after-session-exp]
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

            Settings.lastStationID = Pandora.CurrentStation.Id
            Settings.SaveToRegistry()

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

    Public Sub btnPlayPause_Click(sender As Object, e As EventArgs) Handles btnPlayPause.Click

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
        'Pandora.CurrentStation.CurrentSong.NextSong.FetchedAt = DateAdd(DateInterval.Minute, -65, Now)
        Pandora.Session.DebugCorruptAuthToken()
        Pandora.Session.User.DebugCorruptAuthToken()
        'Pandora.CurrentStation.CurrentSong.DebugCorruptAudioUrl(Settings.audioQuality)
        'For Each s In Pandora.CurrentStation.PlayList
        '    s.DebugCorruptAudioUrl(Settings.audioQuality)
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
                        Pandora.CurrentStation.CurrentSong = Nothing 'don't ever change this line [after-session-exp]
                        ReLoginToPandora()
                    Catch ex As Exception
                        MsgBox("Pandora session has expired." + vbCrLf + vbCrLf +
                               "Tried to re-login but something went wrong :-(" + vbCrLf + vbCrLf +
                               "Try restarting Pandorian...", MsgBoxStyle.Exclamation)
                        AfterErrorActions()
                    End Try
                Case ErrorCodeEnum.SONG_URL_NOT_VALID
                    If Caller = "PlayPreviousSong" Then 'don't ever change this string [handling-of-expired-url]
                        tbLog.AppendText("Song URL expired. Trying the previous song..." + vbCrLf)
                        PlayPreviousSong(True)
                    Else
                        tbLog.AppendText("Song URL expired. Trying the next song..." + vbCrLf)
                        PlayNextSong()
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
                Case ErrorCodeEnum.NO_NET_FOR_BASS
                    PlayPreviousSong(False)
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

        If Exception.GetType() Is GetType(ObjectDisposedException) Then
            Exit Sub
        End If


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
        btnPrev.Enabled = True
        btnNext.Enabled = True
        SongCoverImage.Visible = True
    End Sub

    Private Sub ReLoginToPandora()
        Spinner.Visible = True
        Application.DoEvents()
        ClearSession()
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

        Dim song = Pandora.CurrentStation.CurrentSong

        If File.Exists(song.CoverFileName) Then
            tbLog.AppendText("Loading album cover from cache..." + vbCrLf)
            SongCoverImage.ImageLocation = song.CoverFileName
        Else
            tbLog.AppendText("Downloading album cover art..." + vbCrLf)
            DownloadImage(song.AlbumArtLargeURL, song.CoverFileName)
            SongCoverImage.ImageLocation = song.CoverFileName
        End If
        SongCoverImage.Visible = True
        RaiseEvent CoverImageUpdated(song.CoverFileName)

        If Not IsNothing(song.NextSong) Then
            If Not File.Exists(song.NextSong.CoverFileName) Then
                tbLog.AppendText("Pre-fetching next song's album cover art..." + vbCrLf)
                DownloadImage(song.NextSong.AlbumArtLargeURL, song.NextSong.CoverFileName)
            End If
        End If
    End Sub

    Private Sub DownloadImage(ImageURL As String, FileName As String)

        If String.IsNullOrEmpty(ImageURL) Then
            My.Resources.logo.Save(FileName)
            Exit Sub
        End If

        Dim web As New WebClient()
        Dim noProxy As Boolean = Settings.noProxy
        If Not noProxy Then
            web.Proxy = Me.Proxy
        End If
        Try
            Using strm As New MemoryStream(web.DownloadData(ImageURL))
                Image.FromStream(strm).Save(FileName)
            End Using
        Catch ex As Exception
            My.Resources.logo.Save(FileName)
        End Try
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
            miVersion.Text = "Pandorian v" + Application.ProductVersion
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
            Dim currVer As New Version(Application.ProductVersion)
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

        Settings.hkModifier = CType(cbModKey.SelectedValue, Integer)
        Settings.hkBlock = tbHKBlockSong.Tag
        Settings.hkDislike = tbHKDislikeSong.Tag
        Settings.hkGlobalMenu = tbHKGlobalMenu.Tag
        Settings.hkLike = tbHKLikeSong.Tag
        Settings.hkPlayPause = tbHKPlayPause.Tag
        Settings.hkShowHide = tbHKShowHide.Tag
        Settings.hkSkip = tbHKSkipSong.Tag
        Settings.hkSleep = tbHKSleepNow.Tag
        Settings.hkLock = tbHKLockNow.Tag
        Settings.hkVolDown = tbHKVolDown.Tag
        Settings.hkVolUp = tbHKVolUp.Tag
        Settings.SaveToRegistry()


        pnlHotKeys.Visible = False

        MsgBox("HotKeys saved. Restart app to use new configuration.", MsgBoxStyle.Information)


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
        If Settings.enableBPMCounter Then
            If lblBPM.Visible Then
                lblBPM.Visible = False
                bpmTimer.Stop()
                BPMCounter.Reset(44100)
                lblBPM.Text = "000"
            Else
                lblBPM.Visible = True
                bpmTimer.Start()
            End If
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

    Private Sub prgBar_MouseClick(sender As Object, e As MouseEventArgs) Handles prgBar.MouseClick
        If (e.Button = MouseButtons.Right) Then
            volSlider.Visible = True
            VolLastChangedOn = Now
            Exit Sub
        End If

        Dim absoluteMouse As Single = (PointToClient(MousePosition).X - prgBar.Bounds.X)
        Dim calcFactor As Single = prgBar.Width / CSng(100)
        Dim relativeMouse As Single = absoluteMouse / calcFactor
        prgBar.Value = Convert.ToInt32(relativeMouse)
        Bass.BASS_ChannelSetPosition(Stream,
                                         Bass.BASS_ChannelSeconds2Bytes(Stream, prgBar.Value / 100 * SongDurationSecs()),
                                         BASSMode.BASS_POS_BYTES)
    End Sub
End Class
