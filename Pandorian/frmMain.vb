Imports Pandorian.Engine
Imports Un4seen.Bass
Imports System.Runtime.InteropServices
Imports System.Net
Imports Microsoft.Win32
Public Class frmMain
    Dim Pandora As API
    Dim Proxy As Net.WebProxy
    Dim ProxyPtr As IntPtr
    Dim AAC As Integer
    Dim FX As Integer
    Dim Stream As Integer
    Dim Sync As SYNCPROC = New SYNCPROC(AddressOf SongEnded)
    Dim StationCurrentSongBuffer As New Dictionary(Of String, Data.PandoraSong)
    Dim Downloader As WebClient
    Dim IsActiveForm As Boolean
    Dim SleepAt As Date
    Dim SleepNow As Boolean
    Dim BASSReady As Boolean = False
    Dim ResumePlaying As Boolean = True
    Dim NagShown As Boolean = False

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
        cbModKey.SelectedIndex = cbModKey.FindStringExact(CType(My.Settings.hkModifier, Hotkeys.KeyModifier).ToString)
        tbHKPlayPause.Text = [Enum].GetName(GetType(Keys), My.Settings.hkPlayPause)
        tbHKPlayPause.Tag = My.Settings.hkPlayPause
        tbHKLikeSong.Text = [Enum].GetName(GetType(Keys), My.Settings.hkLike)
        tbHKLikeSong.Tag = My.Settings.hkLike
        tbHKDislikeSong.Text = [Enum].GetName(GetType(Keys), My.Settings.hkDislike)
        tbHKDislikeSong.Tag = My.Settings.hkDislike
        tbHKSkipSong.Text = [Enum].GetName(GetType(Keys), My.Settings.hkSkip)
        tbHKSkipSong.Tag = My.Settings.hkSkip
        tbHKBlockSong.Text = [Enum].GetName(GetType(Keys), My.Settings.hkBlock)
        tbHKBlockSong.Tag = My.Settings.hkBlock
        tbHKShowHide.Text = [Enum].GetName(GetType(Keys), My.Settings.hkShowHide)
        tbHKShowHide.Tag = My.Settings.hkShowHide
        tbHKGlobalMenu.Text = [Enum].GetName(GetType(Keys), My.Settings.hkGlobalMenu)
        tbHKGlobalMenu.Tag = My.Settings.hkGlobalMenu
        tbHKSleepNow.Text = [Enum].GetName(GetType(Keys), My.Settings.hkSleep)
        tbHKSleepNow.Tag = My.Settings.hkSleep

        Hotkeys.RegisterHotKey(Me, 1, My.Settings.hkPlayPause, My.Settings.hkModifier) 'play/pause
        Hotkeys.RegisterHotKey(Me, 2, My.Settings.hkLike, My.Settings.hkModifier) 'like
        Hotkeys.RegisterHotKey(Me, 3, My.Settings.hkDislike, My.Settings.hkModifier) 'dislike
        Hotkeys.RegisterHotKey(Me, 4, My.Settings.hkSkip, My.Settings.hkModifier) 'skip
        Hotkeys.RegisterHotKey(Me, 5, My.Settings.hkShowHide, My.Settings.hkModifier) 'show/hide pandorian
        Hotkeys.RegisterHotKey(Me, 6, My.Settings.hkBlock, My.Settings.hkModifier) 'block
        Hotkeys.RegisterHotKey(Me, 7, My.Settings.hkSleep, My.Settings.hkModifier) 'sleep
        Hotkeys.RegisterHotKey(Me, 8, My.Settings.hkGlobalMenu, My.Settings.hkModifier) 'show tray menu
    End Sub
    Private Sub unRegisterHotkeys()
        Dim i As Integer = 1
        Do While i <= 8
            Hotkeys.unregisterHotkeys(Me, i)
            i = i + 1
        Loop
    End Sub

    Private Sub miShowHotkeys_Click(sender As Object, e As EventArgs) Handles miShowHotkeys.Click
        pnlHotkeys.Visible = True
    End Sub

    Sub AddCurrentSongToStationBuffer()

        Dim StationID As String = Pandora.CurrentStation.Id

        If Not StationCurrentSongBuffer.ContainsKey(StationID) Then
            StationCurrentSongBuffer.Add(StationID, Pandora.CurrentSong)
        Else
            If Not StationCurrentSongBuffer(StationID).Token = Pandora.CurrentSong.Token Then
                StationCurrentSongBuffer(StationID) = Pandora.CurrentSong
            End If
        End If

    End Sub
    Public Function IsLoggedIn() As Boolean
        If Not IsNothing(Pandora) Then
            If Not IsNothing(Pandora.CurrentStation) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Function SuccessfulLogin() As Boolean
        Try
            If Pandora.Login(Decrypt(My.Settings.pandoraUsername), Decrypt(My.Settings.pandoraPassword)) Then
                Return True
            Else
                MsgBox("Couldn't log in to Pandora. Check pandora a/c details.", MsgBoxStyle.Exclamation)
                Return False
            End If
        Catch ex As PandoraException
            If ex.ErrorCode = ErrorCodeEnum.LISTENER_NOT_AUTHORIZED Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Else
                MsgBox(ex.Message + " Please check your proxy details.", MsgBoxStyle.Critical)
            End If
            Return False
        End Try
    End Function
    Sub LoadStationList()
        If Not Pandora.AvailableStations.Count = 0 Then
            Dim Stations As New SortedDictionary(Of String, String)
            For Each Station In Pandora.AvailableStations
                Stations.Add(Station.Name, Station.Id)
                If My.Settings.lastStationID = Station.Id Then
                    Pandora.CurrentStation = Station
                End If
            Next
            ddStations.ValueMember = "Value"
            ddStations.DisplayMember = "Key"
            ddStations.DataSource = New BindingSource(Stations, Nothing)
        Else
            MsgBox("Sorry, no stations were found in your a/c." + vbCrLf + "Please visit pandora.com and create some stations.", MsgBoxStyle.Information)
        End If

    End Sub
    Sub ChangeStation(StationID As String)
        For Each s In Pandora.AvailableStations
            If s.Id = StationID Then
                Pandora.CurrentStation = s
            End If
        Next
    End Sub
    Sub PlayCurrentSong() ' THIS SHOULD ONLY HAVE 4 REFERENCES (PlayNextSong/RunNow/ReplaySong/PowerModeChanged)
        Dim Song As New Data.PandoraSong
        If IsNothing(Pandora.CurrentSong) Then
            Song = Pandora.GetNextSong(False)
        Else
            Song = Pandora.CurrentSong
        End If
        PlayCurrentSongWithBASS()
        If String.IsNullOrEmpty(Song.AlbumArtLargeURL) Then
            SongCoverImage.Image = Nothing
        Else
            SongCoverImage.Image = GetCoverViaProxy(Song.AlbumArtLargeURL)
        End If
        lblSongName.Text = Song.Title
        lblArtistName.Text = Song.Artist
        lblAlbumName.Text = Song.Album
        Timer.Enabled = True
        ddStations.Enabled = True
        If Not IsNothing(Pandora.CurrentStation) Then
            My.Settings.lastStationID = Pandora.CurrentStation.Id
            My.Settings.Save()
        End If
        If Pandora.CanSkip Then
            btnSkip.Enabled = True
        Else
            btnSkip.Enabled = False
        End If
        Select Case Song.Rating
            Case PandoraRating.Hate
                btnLike.Text = "Like"
                btnLike.Enabled = True
                btnDislike.Text = "(D)"
                btnDislike.Enabled = False
            Case PandoraRating.Love
                btnLike.Text = "(L)"
                btnLike.Enabled = False
                btnDislike.Text = "Dislike"
                btnDislike.Enabled = True
            Case PandoraRating.Unrated
                btnLike.Text = "Like"
                btnDislike.Text = "Dislike"
                btnLike.Enabled = True
                btnDislike.Enabled = True
        End Select
        btnPlayPause.Enabled = True
        If ResumePlaying Then
            btnPlayPause.Text = "Pause"
        End If
        If Song.TemporarilyBanned Then
            btnBlock.Text = "(B)"
            btnBlock.Enabled = False
        Else
            btnBlock.Text = "Block"
            btnBlock.Enabled = True
        End If
        Spinner.Visible = False
    End Sub
    Sub PlayNextSong(Skip As Boolean)
        Spinner.Visible = True
        Application.DoEvents()
        Bass.BASS_ChannelStop(Stream)
        Bass.BASS_StreamFree(Stream)
        Pandora.GetNextSong(Skip)
        ResumePlaying = True
        PlayCurrentSong() 'no need to use executedelegate as parent uses delegate
    End Sub

    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        IsActiveForm = True
    End Sub

    Private Sub frmMain_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        IsActiveForm = False
    End Sub

    Private Sub SaveSkipHistory()
        If Not IsNothing(Pandora) Then
            My.Settings.stationSkipHistory = Pandora.SkipHistory.GetStationSkipHisoryJSON
            My.Settings.globalSkipHistory = Pandora.SkipHistory.GetGlobalSkipHistoryJSON
            My.Settings.Save()
        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveSkipHistory()
        If Not IsNothing(Pandora) Then
            Pandora.Logout()
        End If
        If BASSReady Then
            DeInitBass()
        End If
        unRegisterHotkeys()
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

            If Not My.Settings.noProxy Then
                Dim proxy As String = Decrypt(My.Settings.proyxUsername) + ":" +
                                      Decrypt(My.Settings.proxyPassword) + "@" +
                                      Decrypt(My.Settings.proxyAddress).Replace("http://", "")
                ProxyPtr = Marshal.StringToHGlobalAnsi(proxy)
                Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_PROXY, ProxyPtr)
            End If
            AAC = Bass.BASS_PluginLoad("bass_aac.dll")
            FX = Bass.BASS_PluginLoad("bass_fx.dll")
            If Utils.HighWord(AddOn.Fx.BassFx.BASS_FX_GetVersion()) <> AddOn.Fx.BassFx.BASSFXVERSION Then
                MsgBox("Wrong BassFx Version. Volume will be not normalize!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Sub DeInitBass()
        If BASSReady Then
            Bass.BASS_ChannelStop(Stream)
            Bass.BASS_StreamFree(Stream)
            Bass.BASS_PluginFree(AAC)
            Bass.BASS_PluginFree(FX)
            Bass.BASS_Free()
            Marshal.FreeHGlobal(ProxyPtr)
            BASSReady = False
        End If
    End Sub

    Sub ApplyReplayGain()
        Dim VolFX As Integer = Bass.BASS_ChannelSetFX(Stream, BASSFXType.BASS_FX_BFX_VOLUME, 0)
        Dim VolParm As New AddOn.Fx.BASS_BFX_VOLUME
        VolParm.lChannel = AddOn.Fx.BASSFXChan.BASS_BFX_CHANNONE
        VolParm.fVolume = Math.Pow(10, Pandora.CurrentSong.TrackGain / 70)
        If Not Bass.BASS_FXSetParameters(VolFX, VolParm) Then
            MsgBox("Normalize Error: " + Bass.BASS_ErrorGetCode.ToString, MsgBoxStyle.Information)
        End If
        'Debug.WriteLine("ReplayGain: " + Pandora.CurrentSong.TrackGain.ToString + " From 0 |" + " New Gain: " + VolParm.fVolume.ToString + " From 1")
    End Sub
    Private Sub PlayCurrentSongWithBASS()
        If Not Stream = 0 Then
            Bass.BASS_ChannelStop(Stream)
        End If
        Stream = Bass.BASS_StreamCreateURL(
                Pandora.CurrentSong.AudioUrlMap(My.Settings.audioQuality).Url,
                0,
                BASSFlag.BASS_STREAM_AUTOFREE,
                Nothing,
                IntPtr.Zero)
        If Not Stream = 0 Then
            Bass.BASS_ChannelSetSync(Stream, BASSSync.BASS_SYNC_END, 0, Sync, IntPtr.Zero)
            ApplyReplayGain()
            If ResumePlaying Then
                Bass.BASS_ChannelPlay(Stream, False)
            End If
            Application.DoEvents()
            Pandora.CurrentSong.PlayingStartTime = Now
            Pandora.CurrentSong.AudioDurationSecs = SongDurationSecs()
            ShareTheLove()
        Else
            If Bass.BASS_ErrorGetCode = BASSError.BASS_ERROR_FILEOPEN Then
                Throw New PandoraException(ErrorCodeEnum.AUTH_INVALID_TOKEN, "Audio URL has probably expired...")
            Else
                MsgBox("Couldn't open stream: " + Bass.BASS_ErrorGetCode().ToString + vbCr +
                       "Try restarting the app...", MsgBoxStyle.Critical)
            End If

        End If
    End Sub

    Private Sub ShareTheLove()
        Dim triggers As Integer() = {3, 10, 20, 30, 40}
        For Each t In triggers
            If t = My.Settings.launchCount And NagShown = False Then

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

        If My.Settings.launchCount >= 90 Then
            My.Settings.launchCount = 39
            My.Settings.Save()
        End If
    End Sub

    Function HasSettings() As Boolean

        Dim prxSettingsReqd As Boolean

        If My.Settings.noProxy = True Then
            prxSettingsReqd = False
        Else
            If Decrypt(My.Settings.proxyAddress) = "http://server:port" Then
                prxSettingsReqd = True
            End If
        End If

        If prxSettingsReqd Or
            String.IsNullOrEmpty(Decrypt(My.Settings.pandoraUsername)) Or
            String.IsNullOrEmpty(Decrypt(My.Settings.pandoraPassword)) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        If System.Diagnostics.Debugger.IsAttached Then
            If e.Control And e.Alt And e.KeyCode = Keys.E Then
                DebugExpireSessionNow()
            End If
        End If

        If e.Control And e.KeyCode = Keys.D And
                            Not IsNothing(Pandora.CurrentSong) And
                            prgDownload.Value = 0 And
                            Pandora.User.PartnerCredentials.AccountType = Data.AccountType.PANDORA_ONE_USER Then

            If Not IO.Directory.Exists(My.Settings.downloadLocation) Then
                If folderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                    My.Settings.downloadLocation = folderBrowser.SelectedPath
                    My.Settings.Save()
                Else
                    Exit Sub
                End If
            End If

            If IO.Directory.Exists(My.Settings.downloadLocation) Then
                Dim TargeFile As String =
                    My.Settings.downloadLocation +
                    "\" +
                    ValidFileName(Pandora.CurrentSong.Artist) + " - " +
                    ValidFileName(Pandora.CurrentSong.Title) +
                    ".mp3"

                If Not System.IO.File.Exists(TargeFile) Then
                    Downloader = New WebClient
                    AddHandler Downloader.DownloadFileCompleted, AddressOf FileDownloadCompleted
                    AddHandler Downloader.DownloadProgressChanged, AddressOf FileDownloadProgressChanged
                    If Not My.Settings.noProxy Then
                        Downloader.Proxy = Me.Proxy
                    End If
                    Downloader.DownloadFileAsync(
                            New Uri(Pandora.CurrentSong.AudioUrlMap("highQuality").Url), TargeFile)
                    prgDownload.Visible = True
                End If
            End If
        End If
    End Sub

    Private Function ValidFileName(Text As String) As String
        For Each c In IO.Path.GetInvalidFileNameChars
            Text = Text.Replace(c, "_")
        Next
        Return Text
    End Function

    Private Sub FileDownloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        prgDownload.Visible = False
        prgDownload.Value = 0
        If Not IsNothing(e.Error) Then
            MsgBox(e.Error.Message, MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub FileDownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        prgDownload.Value = e.ProgressPercentage
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False
        lblAlbumName.UseMnemonic = False
        lblArtistName.UseMnemonic = False
        lblSongName.UseMnemonic = False
        frmSettings.Hide()
        If Not System.Diagnostics.Debugger.IsAttached Then
            LogAppStartEvent()
        End If
        My.Settings.launchCount = My.Settings.launchCount + 1
        My.Settings.Save()
        CheckForUpdate()
        registerHotkeys()
        populateSleepTimes()
        AddHandler SystemEvents.PowerModeChanged, AddressOf PowerModeChanged
        Application.DoEvents()
    End Sub
    Private Sub TrayIcon_MouseClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            TrayIcon.Visible = False
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            Me.Activate()
        End If
    End Sub
    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If WindowState = FormWindowState.Normal Or Me.Visible = True Then
            TrayIcon.Visible = False
        End If

        If WindowState = FormWindowState.Minimized Or (Me.Visible = False And IsNothing(sender)) Then
            TrayIcon.Visible = True
            TrayIcon.BalloonTipText = "Pandorian has been minimized to tray"
            TrayIcon.ShowBalloonTip(1000)
            Me.Visible = False
        End If

    End Sub
    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.DoEvents()
        Execute(Sub() RunNow(), "frmMain_Shown")
    End Sub
    Sub RunNow()
        If Not HasSettings() Then
            frmSettings.Show()
            Me.Hide()
            Exit Sub
        End If
        Pandora = New API(My.Settings.pandoraOne)
        If Not My.Settings.noProxy Then
            Me.Proxy = New Net.WebProxy(Decrypt(My.Settings.proxyAddress))
            If Not String.IsNullOrEmpty(Decrypt(My.Settings.proyxUsername)) And Not String.IsNullOrEmpty(Decrypt(My.Settings.proxyPassword)) Then
                Me.Proxy.Credentials = New Net.NetworkCredential(Decrypt(My.Settings.proyxUsername), Decrypt(My.Settings.proxyPassword))
            End If
            Pandora.Proxy = Me.Proxy
        End If
        WaitForNetConnection()
        If SuccessfulLogin() Then
            LoadStationList()
            RestoreSkipHistory()
            If Not IsNothing(Pandora.CurrentStation) Then
                If Not String.IsNullOrEmpty(Pandora.CurrentStation.Id) Then
                    ddStations.SelectedIndex = ddStations.FindStringExact(Pandora.CurrentStation.Name)

                    Spinner.Visible = True
                    Application.DoEvents()

                    InitBass()

                    Execute(Sub() PlayCurrentSong(), "RunNow.PlayCurrentSong()")
                End If
            End If
        Else
            frmSettings.Show()
            Me.Hide()
        End If
    End Sub
    Function BASSChannelState() As BASSActive
        Return Bass.BASS_ChannelIsActive(Stream)
    End Function
    Private Sub btnPlayPause_Click(sender As Object, e As EventArgs) Handles btnPlayPause.Click
        If BASSChannelState() = BASSActive.BASS_ACTIVE_PLAYING Then
            Bass.BASS_ChannelPause(Stream)
            btnPlayPause.Text = "Play"
        ElseIf BASSChannelState() = BASSActive.BASS_ACTIVE_PAUSED Then
            Bass.BASS_ChannelPlay(Stream, False)
            btnPlayPause.Text = "Pause"
        ElseIf BASSChannelState() = BASSActive.BASS_ACTIVE_STOPPED And ResumePlaying = False Then
            ResumePlaying = True
            Bass.BASS_ChannelPlay(Stream, False)
            btnPlayPause.Text = "Pause"
        Else
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
        SleepCheck()
    End Sub
    Function HasToResumeLastSong() As Boolean

        If StationCurrentSongBuffer.ContainsKey(Pandora.CurrentStation.Id) Then
            Dim song As Data.PandoraSong = StationCurrentSongBuffer(Pandora.CurrentStation.Id)
            If song.DurationElapsed = False Then
                Debug.WriteLine("Has to resume last song :-(")
                Debug.WriteLine("SongDuration: " + song.AudioDurationSecs.ToString)
                Debug.WriteLine("TimeElapsed: " + Now.Subtract(song.PlayingStartTime).TotalSeconds.ToString)
                Return True
            End If
        End If

        Debug.WriteLine("Yay, no need to resume last song :-)")
        Return False
    End Function
    Sub ReplaySong()
        Pandora.CurrentSong = StationCurrentSongBuffer(Pandora.CurrentStation.Id)
        Spinner.Visible = True
        Application.DoEvents()
        Bass.BASS_ChannelStop(Stream)
        Bass.BASS_StreamFree(Stream)
        Execute(Sub() PlayCurrentSong(), "ReplaySong.PlayCurrentSong()")
    End Sub
    Sub SongEnded(ByVal handle As Integer, ByVal channel As Integer, ByVal data As Integer, ByVal user As IntPtr)
        Execute(Sub() PlayNextSong(False), "SongEnded")
    End Sub

    Private Sub DebugExpireSessionNow()
        Pandora.Session.DebugCorruptAuthToken()
        Pandora.User.DebugCorruptAuthToken()
        Pandora.DebugClearPlayList()
        Pandora.CurrentSong.DebugCorruptAudioUrl(My.Settings.audioQuality)
    End Sub

    Private Delegate Sub ExecuteDelegate()
    Private Sub Execute(Logic As ExecuteDelegate, Caller As String)
        Try
            Logic()
        Catch pex As PandoraException

            Select Case pex.ErrorCode
                Case ErrorCodeEnum.AUTH_INVALID_TOKEN
                    Try
                        ReLoginToPandora()
                    Catch ex As Exception
                        MsgBox("Pandora session has expired." + vbCrLf + vbCrLf +
                               "Tried to re-login but something went wrong :-(" + vbCrLf + vbCrLf +
                               "Try restarting Pandorian...", MsgBoxStyle.Exclamation)
                        AfterErrorActions()
                    End Try
                Case Else
                    ReportError(pex, Caller)
                    AfterErrorActions()
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
            Clipboard.SetText(msg)
            MsgBox("Error details have been copied to the clipboard." + vbCrLf +
                   "You will now be taken to the Pandorian support page.", vbInformation)
            Process.Start("https://github.com/dj-nitehawk/Pandorian/issues/new")
        End If

    End Sub

    Sub AfterErrorActions()
        Spinner.Visible = False
        Application.DoEvents()
        btnBlock.Enabled = False
        btnPlayPause.Enabled = False
        btnDislike.Enabled = False
        btnLike.Enabled = False
        btnSkip.Enabled = True
    End Sub

    Private Sub ReLoginToPandora()
        Spinner.Visible = True
        Application.DoEvents()
        SaveSkipHistory()
        Pandora.Logout()
        Pandora = Nothing
        RunNow()
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        If btnSkip.Enabled And Pandora.CanSkip Then
            Execute(Sub() PlayNextSong(True), "btnSkip_Click")
        End If
    End Sub
    Private Sub btnBlock_Click(sender As Object, e As EventArgs) Handles btnBlock.Click
        If btnBlock.Enabled Then
            btnBlock.Text = "(B)"
            btnBlock.Enabled = False
            Execute(Sub() Pandora.TemporarilyBanSong(Pandora.CurrentSong), "btnBlock_Click.TemporarilyBanSong")
            If Pandora.CanSkip Then
                Execute(Sub() PlayNextSong(True), "btnBlock_Click.PlayNextSong")
            End If
        End If
    End Sub
    Function SongDurationSecs() As Double
        Dim len As Long = Bass.BASS_ChannelGetLength(Stream)
        Return Bass.BASS_ChannelBytes2Seconds(Stream, len)
    End Function
    Function CurrentPositionSecs() As Double
        Dim pos As Long = Bass.BASS_ChannelGetPosition(Stream)
        Return Bass.BASS_ChannelBytes2Seconds(Stream, pos)
    End Function
    Private Sub btnLike_Click(sender As Object, e As EventArgs) Handles btnLike.Click
        If btnLike.Enabled Then
            btnLike.Text = "(L)"
            btnLike.Enabled = False
            btnDislike.Text = "Dislike"
            btnDislike.Enabled = True
            Execute(Sub() Pandora.RateSong(Pandora.CurrentSong, PandoraRating.Love), "btnLike_Click")
        End If
    End Sub
    Private Sub btnDislike_Click(sender As Object, e As EventArgs) Handles btnDislike.Click
        If btnDislike.Enabled Then
            btnDislike.Text = "(D)"
            btnDislike.Enabled = False
            btnLike.Text = "Like"
            btnLike.Enabled = True
            Execute(Sub() Pandora.RateSong(Pandora.CurrentSong, PandoraRating.Hate), "btnDislike_Click.RateSong")
            If Pandora.CanSkip() Then
                Execute(Sub() PlayNextSong(True), "btnDislike_Click.PlayNextSong")
            End If
        End If
    End Sub
    Function GetCoverViaProxy(URL As String) As Drawing.Image
        Dim web As New WebClient()
        Dim img As Drawing.Image
        If Not My.Settings.noProxy Then
            web.Proxy = Me.Proxy
        End If
        Try
            Using strm As New IO.MemoryStream(web.DownloadData(URL))
                img = Image.FromStream(strm)
            End Using
        Catch ex As Exception
            img = Nothing
        End Try
        Return img
    End Function

    Sub LogAppStartEvent()
        Dim web As New WebClient()
        Try
            web.DownloadDataAsync(New Uri("http://s07.flagcounter.com/mini/f3Ey/bg_FFFFFF/txt_000000/border_CCCCCC/flags_0/"))
        Catch ex As Exception
            Debug.WriteLine("LogAppStart: " + ex.Message)
        End Try
        web = Nothing
    End Sub
    Private Sub ddStations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddStations.SelectedIndexChanged
        If Not ddStations.SelectedValue = Pandora.CurrentStation.Id Then
            If Not IsNothing(Pandora.CurrentStation) And Not IsNothing(Pandora.CurrentSong) Then
                ddStations.Enabled = False

                AddCurrentSongToStationBuffer()

                ChangeStation(ddStations.SelectedValue)

                If HasToResumeLastSong() Then
                    ReplaySong()
                    Exit Sub
                End If

                Execute(Sub() PlayNextSong(False), "ddStations_SelectedIndexChanged")
            End If
        End If
    End Sub
    Private Sub miShowSettings_Click(sender As Object, e As EventArgs) Handles miShowSettings.Click
        Me.Hide()
        frmSettings.Show()
    End Sub
    Private Sub miManageStation_Click(sender As Object, e As EventArgs) Handles miManageStation.Click
        Me.Hide()
        frmBrowser.Show()
    End Sub
    Public Function GetStationURL() As String
        Return Pandora.CurrentStation.StationURL.Replace("login?target=%2F", "")
    End Function
    Private Sub RestoreSkipHistory()
        Dim sHistory As String = My.Settings.stationSkipHistory
        If Not sHistory = "{}" Then
            Pandora.SkipHistory.SetStationSkipHistory(sHistory)
        End If
        Dim gHistory As String = My.Settings.globalSkipHistory
        If Not gHistory = "[]" Then
            Pandora.SkipHistory.SetGlobalSkipHistory(gHistory)
        End If
    End Sub
    Private Sub miUpdate_Click(sender As Object, e As EventArgs) Handles miUpdate.Click
        Process.Start("http://pandorian.djnitehawk.com/?utm_source=pandorian.app&utm_medium=direct.link&utm_campaign=visit.website")
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
        Dim sw As New Stopwatch
        sw.Start()
        Do Until NetConnectionAvailable()
            If sw.ElapsedMilliseconds > 10000 Then
                MsgBox("Sorry, but it looks like your internet is down." + vbCrLf + vbCrLf +
                       "Please try again later...", MsgBoxStyle.Exclamation)
                Exit Do
            End If
            Threading.Thread.Sleep(1000)
        Loop
        sw.Stop()
    End Sub

    Private Function NetConnectionAvailable() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub PowerModeChanged(sender As Object, e As PowerModeChangedEventArgs)
        Select Case e.Mode
            Case PowerModes.Resume
                Spinner.Visible = True
                Application.DoEvents()
                WaitForNetConnection()
                InitBass()
                Execute(Sub() PlayCurrentSong(), "PowerModeChanged.Resume")
            Case PowerModes.Suspend
                PreSleepActivities()
        End Select
    End Sub

    Private Sub PreSleepActivities()
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
    End Sub

    Private Sub TrayMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TrayMenu.Opening
        If Not IsNothing(Pandora) Then
            If Not IsNothing(Pandora.CurrentSong) And Not IsNothing(Pandora.CurrentStation) Then
                tmiSongTitle.Text = Pandora.CurrentSong.Title.Replace("&", "&&")
                tmiArtistTitle.Text = Pandora.CurrentSong.Artist.Replace("&", "&&")
                tmiStationTitle.Text = Pandora.CurrentStation.Name.Replace("&", "&&")
            End If
            Select Case BASSChannelState()
                Case BASSActive.BASS_ACTIVE_PAUSED
                    tmiPlayPause.Text = "Play"
                Case BASSActive.BASS_ACTIVE_PLAYING
                    tmiPlayPause.Text = "Pause"
                Case Else
                    tmiPlayPause.Text = "Play/Pause"
            End Select
            tmiLikeCurrentSong.Enabled = btnLike.Enabled
            tmiDislikeCurrentSong.Enabled = btnDislike.Enabled
            tmiSkipSong.Enabled = btnSkip.Enabled
            tmiBlockSong.Enabled = btnBlock.Enabled
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
        tbHKBlockSong.Enter

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
        tbHKBlockSong.Leave

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
        tbHKBlockSong.KeyUp

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

        If HKList.Count < 8 Then
            MsgBox("You cannot use the same key for more than one function!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        With My.Settings
            .hkModifier = CType(cbModKey.SelectedValue, Integer)
            .hkBlock = tbHKBlockSong.Tag
            .hkDislike = tbHKDislikeSong.Tag
            .hkGlobalMenu = tbHKGlobalMenu.Tag
            .hkLike = tbHKLikeSong.Tag
            .hkPlayPause = tbHKPlayPause.Tag
            .hkShowHide = tbHKShowHide.Tag
            .hkSkip = tbHKSkipSong.Tag
            .hkSleep = tbHKSleepNow.Tag
        End With
        My.Settings.Save()

        pnlHotKeys.Visible = False

        MsgBox("HotKeys have been saved." + vbCrLf + vbCrLf +
               "New configuration will be active the next time you launch Pandorian...", MsgBoxStyle.Information)


    End Sub
End Class
