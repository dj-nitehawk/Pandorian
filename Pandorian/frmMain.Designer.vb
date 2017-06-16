<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ddStations = New System.Windows.Forms.ComboBox()
        Me.lblSongName = New System.Windows.Forms.Label()
        Me.lblArtistName = New System.Windows.Forms.Label()
        Me.lblAlbumName = New System.Windows.Forms.Label()
        Me.prgBar = New System.Windows.Forms.ProgressBar()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miManageStation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.miSleepTimer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.miShowSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.miShowHotkeys = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.miSendFeedback = New System.Windows.Forms.ToolStripMenuItem()
        Me.miUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.miVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.prgDownload = New System.Windows.Forms.ProgressBar()
        Me.folderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlSleepTimer = New System.Windows.Forms.Panel()
        Me.btnSTDone = New System.Windows.Forms.Button()
        Me.chkSleep = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ddSleepTimes = New System.Windows.Forms.ComboBox()
        Me.lblSleepStatus = New System.Windows.Forms.Label()
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmiStationTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiSongTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiArtistTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiLikeCurrentSong = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiDislikeCurrentSong = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiPlayPause = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiSkipSong = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiBlockSong = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiLockScreen = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiSleepComputer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlHotKeys = New System.Windows.Forms.Panel()
        Me.tbHKVolUp = New System.Windows.Forms.TextBox()
        Me.tbHKVolDown = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tbHKLockNow = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbHKSleepNow = New System.Windows.Forms.TextBox()
        Me.tbHKGlobalMenu = New System.Windows.Forms.TextBox()
        Me.tbHKShowHide = New System.Windows.Forms.TextBox()
        Me.tbHKBlockSong = New System.Windows.Forms.TextBox()
        Me.tbHKSkipSong = New System.Windows.Forms.TextBox()
        Me.tbHKDislikeSong = New System.Windows.Forms.TextBox()
        Me.tbHKLikeSong = New System.Windows.Forms.TextBox()
        Me.tbHKPlayPause = New System.Windows.Forms.TextBox()
        Me.btnSaveHotkeys = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbModKey = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.volSlider = New System.Windows.Forms.TrackBar()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.bpmTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblBPM = New System.Windows.Forms.Label()
        Me.Spinner = New System.Windows.Forms.PictureBox()
        Me.btnBlock = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPlayPause = New System.Windows.Forms.Button()
        Me.btnDislike = New System.Windows.Forms.Button()
        Me.btnLike = New System.Windows.Forms.Button()
        Me.SongCoverImage = New System.Windows.Forms.PictureBox()
        Me.MenuStrip.SuspendLayout()
        Me.pnlSleepTimer.SuspendLayout()
        Me.TrayMenu.SuspendLayout()
        Me.pnlHotKeys.SuspendLayout()
        CType(Me.volSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ddStations
        '
        Me.ddStations.BackColor = System.Drawing.Color.Azure
        Me.ddStations.DropDownHeight = 400
        Me.ddStations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddStations.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddStations.ForeColor = System.Drawing.Color.DarkBlue
        Me.ddStations.FormattingEnabled = True
        Me.ddStations.IntegralHeight = False
        Me.ddStations.ItemHeight = 20
        Me.ddStations.Location = New System.Drawing.Point(9, 9)
        Me.ddStations.MaxDropDownItems = 20
        Me.ddStations.Name = "ddStations"
        Me.ddStations.Size = New System.Drawing.Size(300, 28)
        Me.ddStations.TabIndex = 1
        Me.ddStations.TabStop = False
        '
        'lblSongName
        '
        Me.lblSongName.AutoEllipsis = True
        Me.lblSongName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSongName.Location = New System.Drawing.Point(0, 368)
        Me.lblSongName.Name = "lblSongName"
        Me.lblSongName.Size = New System.Drawing.Size(317, 15)
        Me.lblSongName.TabIndex = 3
        Me.lblSongName.Text = "Song Name"
        Me.lblSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArtistName
        '
        Me.lblArtistName.AutoEllipsis = True
        Me.lblArtistName.Location = New System.Drawing.Point(-9, 384)
        Me.lblArtistName.Name = "lblArtistName"
        Me.lblArtistName.Size = New System.Drawing.Size(332, 15)
        Me.lblArtistName.TabIndex = 4
        Me.lblArtistName.Text = "Artist Name"
        Me.lblArtistName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAlbumName
        '
        Me.lblAlbumName.AutoEllipsis = True
        Me.lblAlbumName.Location = New System.Drawing.Point(0, 400)
        Me.lblAlbumName.Name = "lblAlbumName"
        Me.lblAlbumName.Size = New System.Drawing.Size(317, 15)
        Me.lblAlbumName.TabIndex = 5
        Me.lblAlbumName.Text = "Album Name"
        Me.lblAlbumName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'prgBar
        '
        Me.prgBar.Location = New System.Drawing.Point(9, 352)
        Me.prgBar.Name = "prgBar"
        Me.prgBar.Size = New System.Drawing.Size(300, 10)
        Me.prgBar.Step = 1
        Me.prgBar.TabIndex = 10
        Me.prgBar.Value = 1
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miManageStation, Me.ToolStripSeparator1, Me.miSleepTimer, Me.ToolStripSeparator8, Me.miShowSettings, Me.miShowHotkeys, Me.ToolStripSeparator9, Me.miSendFeedback, Me.miUpdate, Me.ToolStripSeparator2, Me.miVersion})
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(192, 182)
        '
        'miManageStation
        '
        Me.miManageStation.Name = "miManageStation"
        Me.miManageStation.Size = New System.Drawing.Size(191, 22)
        Me.miManageStation.Text = "Manage Station"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(188, 6)
        '
        'miSleepTimer
        '
        Me.miSleepTimer.Name = "miSleepTimer"
        Me.miSleepTimer.Size = New System.Drawing.Size(191, 22)
        Me.miSleepTimer.Text = "Sleep Timer"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(188, 6)
        '
        'miShowSettings
        '
        Me.miShowSettings.Name = "miShowSettings"
        Me.miShowSettings.Size = New System.Drawing.Size(191, 22)
        Me.miShowSettings.Text = "Show Settings"
        '
        'miShowHotkeys
        '
        Me.miShowHotkeys.Name = "miShowHotkeys"
        Me.miShowHotkeys.Size = New System.Drawing.Size(191, 22)
        Me.miShowHotkeys.Text = "Show HotKeys"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(188, 6)
        '
        'miSendFeedback
        '
        Me.miSendFeedback.Name = "miSendFeedback"
        Me.miSendFeedback.Size = New System.Drawing.Size(191, 22)
        Me.miSendFeedback.Text = "Send Feedback"
        '
        'miUpdate
        '
        Me.miUpdate.Name = "miUpdate"
        Me.miUpdate.Size = New System.Drawing.Size(191, 22)
        Me.miUpdate.Text = "Visit Website"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(188, 6)
        '
        'miVersion
        '
        Me.miVersion.Enabled = False
        Me.miVersion.Name = "miVersion"
        Me.miVersion.Size = New System.Drawing.Size(191, 22)
        Me.miVersion.Text = "Current Version: v1.4.0"
        '
        'prgDownload
        '
        Me.prgDownload.Location = New System.Drawing.Point(0, 460)
        Me.prgDownload.Name = "prgDownload"
        Me.prgDownload.Size = New System.Drawing.Size(317, 3)
        Me.prgDownload.Step = 1
        Me.prgDownload.TabIndex = 14
        Me.prgDownload.Visible = False
        '
        'pnlSleepTimer
        '
        Me.pnlSleepTimer.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlSleepTimer.Controls.Add(Me.btnSTDone)
        Me.pnlSleepTimer.Controls.Add(Me.chkSleep)
        Me.pnlSleepTimer.Controls.Add(Me.Label1)
        Me.pnlSleepTimer.Controls.Add(Me.ddSleepTimes)
        Me.pnlSleepTimer.Controls.Add(Me.lblSleepStatus)
        Me.pnlSleepTimer.Location = New System.Drawing.Point(9, 44)
        Me.pnlSleepTimer.Name = "pnlSleepTimer"
        Me.pnlSleepTimer.Size = New System.Drawing.Size(300, 300)
        Me.pnlSleepTimer.TabIndex = 15
        Me.pnlSleepTimer.Visible = False
        '
        'btnSTDone
        '
        Me.btnSTDone.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSTDone.Location = New System.Drawing.Point(112, 221)
        Me.btnSTDone.Name = "btnSTDone"
        Me.btnSTDone.Size = New System.Drawing.Size(75, 23)
        Me.btnSTDone.TabIndex = 4
        Me.btnSTDone.Text = "Done"
        Me.btnSTDone.UseVisualStyleBackColor = True
        '
        'chkSleep
        '
        Me.chkSleep.AutoSize = True
        Me.chkSleep.Location = New System.Drawing.Point(94, 172)
        Me.chkSleep.Name = "chkSleep"
        Me.chkSleep.Size = New System.Drawing.Size(118, 17)
        Me.chkSleep.TabIndex = 3
        Me.chkSleep.Text = "Enable Sleep Timer"
        Me.chkSleep.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(94, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Put system to sleep in:"
        '
        'ddSleepTimes
        '
        Me.ddSleepTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddSleepTimes.FormattingEnabled = True
        Me.ddSleepTimes.Location = New System.Drawing.Point(105, 135)
        Me.ddSleepTimes.Name = "ddSleepTimes"
        Me.ddSleepTimes.Size = New System.Drawing.Size(89, 21)
        Me.ddSleepTimes.TabIndex = 1
        '
        'lblSleepStatus
        '
        Me.lblSleepStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSleepStatus.Location = New System.Drawing.Point(3, 45)
        Me.lblSleepStatus.Name = "lblSleepStatus"
        Me.lblSleepStatus.Size = New System.Drawing.Size(293, 33)
        Me.lblSleepStatus.TabIndex = 0
        Me.lblSleepStatus.Text = "Sleep Timer Disabled"
        Me.lblSleepStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TrayIcon
        '
        Me.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TrayIcon.ContextMenuStrip = Me.TrayMenu
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "Pandorian"
        '
        'TrayMenu
        '
        Me.TrayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiStationTitle, Me.tmiSongTitle, Me.tmiArtistTitle, Me.ToolStripSeparator3, Me.tmiLikeCurrentSong, Me.tmiDislikeCurrentSong, Me.ToolStripSeparator4, Me.tmiPlayPause, Me.tmiSkipSong, Me.ToolStripSeparator5, Me.tmiBlockSong, Me.ToolStripSeparator6, Me.tmiLockScreen, Me.tmiSleepComputer, Me.ToolStripSeparator7, Me.tmiExit})
        Me.TrayMenu.Name = "TrayMenu"
        Me.TrayMenu.Size = New System.Drawing.Size(160, 276)
        '
        'tmiStationTitle
        '
        Me.tmiStationTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmiStationTitle.Name = "tmiStationTitle"
        Me.tmiStationTitle.Size = New System.Drawing.Size(159, 22)
        Me.tmiStationTitle.Text = "Station Name"
        '
        'tmiSongTitle
        '
        Me.tmiSongTitle.BackColor = System.Drawing.SystemColors.Control
        Me.tmiSongTitle.ForeColor = System.Drawing.Color.DimGray
        Me.tmiSongTitle.Name = "tmiSongTitle"
        Me.tmiSongTitle.Size = New System.Drawing.Size(159, 22)
        Me.tmiSongTitle.Text = "Name Of Song"
        '
        'tmiArtistTitle
        '
        Me.tmiArtistTitle.ForeColor = System.Drawing.Color.DimGray
        Me.tmiArtistTitle.Name = "tmiArtistTitle"
        Me.tmiArtistTitle.Size = New System.Drawing.Size(159, 22)
        Me.tmiArtistTitle.Text = "Artist Name"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(156, 6)
        '
        'tmiLikeCurrentSong
        '
        Me.tmiLikeCurrentSong.Name = "tmiLikeCurrentSong"
        Me.tmiLikeCurrentSong.ShortcutKeyDisplayString = ""
        Me.tmiLikeCurrentSong.Size = New System.Drawing.Size(159, 22)
        Me.tmiLikeCurrentSong.Text = "Like"
        '
        'tmiDislikeCurrentSong
        '
        Me.tmiDislikeCurrentSong.Name = "tmiDislikeCurrentSong"
        Me.tmiDislikeCurrentSong.ShortcutKeyDisplayString = ""
        Me.tmiDislikeCurrentSong.Size = New System.Drawing.Size(159, 22)
        Me.tmiDislikeCurrentSong.Text = "Dislike"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(156, 6)
        '
        'tmiPlayPause
        '
        Me.tmiPlayPause.Name = "tmiPlayPause"
        Me.tmiPlayPause.ShortcutKeyDisplayString = ""
        Me.tmiPlayPause.Size = New System.Drawing.Size(159, 22)
        Me.tmiPlayPause.Text = "Play/Pause"
        '
        'tmiSkipSong
        '
        Me.tmiSkipSong.Name = "tmiSkipSong"
        Me.tmiSkipSong.ShortcutKeyDisplayString = ""
        Me.tmiSkipSong.Size = New System.Drawing.Size(159, 22)
        Me.tmiSkipSong.Text = "Skip"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(156, 6)
        '
        'tmiBlockSong
        '
        Me.tmiBlockSong.Name = "tmiBlockSong"
        Me.tmiBlockSong.ShortcutKeyDisplayString = ""
        Me.tmiBlockSong.Size = New System.Drawing.Size(159, 22)
        Me.tmiBlockSong.Text = "Block"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(156, 6)
        '
        'tmiLockScreen
        '
        Me.tmiLockScreen.Name = "tmiLockScreen"
        Me.tmiLockScreen.Size = New System.Drawing.Size(159, 22)
        Me.tmiLockScreen.Text = "Lock Screen"
        '
        'tmiSleepComputer
        '
        Me.tmiSleepComputer.Name = "tmiSleepComputer"
        Me.tmiSleepComputer.ShortcutKeyDisplayString = ""
        Me.tmiSleepComputer.Size = New System.Drawing.Size(159, 22)
        Me.tmiSleepComputer.Text = "Sleep Computer"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(156, 6)
        '
        'tmiExit
        '
        Me.tmiExit.Name = "tmiExit"
        Me.tmiExit.Size = New System.Drawing.Size(159, 22)
        Me.tmiExit.Text = "Exit"
        '
        'pnlHotKeys
        '
        Me.pnlHotKeys.BackColor = System.Drawing.Color.SlateGray
        Me.pnlHotKeys.Controls.Add(Me.tbHKVolUp)
        Me.pnlHotKeys.Controls.Add(Me.tbHKVolDown)
        Me.pnlHotKeys.Controls.Add(Me.Label13)
        Me.pnlHotKeys.Controls.Add(Me.tbHKLockNow)
        Me.pnlHotKeys.Controls.Add(Me.Label12)
        Me.pnlHotKeys.Controls.Add(Me.tbHKSleepNow)
        Me.pnlHotKeys.Controls.Add(Me.tbHKGlobalMenu)
        Me.pnlHotKeys.Controls.Add(Me.tbHKShowHide)
        Me.pnlHotKeys.Controls.Add(Me.tbHKBlockSong)
        Me.pnlHotKeys.Controls.Add(Me.tbHKSkipSong)
        Me.pnlHotKeys.Controls.Add(Me.tbHKDislikeSong)
        Me.pnlHotKeys.Controls.Add(Me.tbHKLikeSong)
        Me.pnlHotKeys.Controls.Add(Me.tbHKPlayPause)
        Me.pnlHotKeys.Controls.Add(Me.btnSaveHotkeys)
        Me.pnlHotKeys.Controls.Add(Me.Label11)
        Me.pnlHotKeys.Controls.Add(Me.Label10)
        Me.pnlHotKeys.Controls.Add(Me.Label9)
        Me.pnlHotKeys.Controls.Add(Me.Label8)
        Me.pnlHotKeys.Controls.Add(Me.Label7)
        Me.pnlHotKeys.Controls.Add(Me.Label6)
        Me.pnlHotKeys.Controls.Add(Me.Label5)
        Me.pnlHotKeys.Controls.Add(Me.Label4)
        Me.pnlHotKeys.Controls.Add(Me.cbModKey)
        Me.pnlHotKeys.Controls.Add(Me.Label3)
        Me.pnlHotKeys.Controls.Add(Me.Label2)
        Me.pnlHotKeys.Location = New System.Drawing.Point(9, 44)
        Me.pnlHotKeys.Name = "pnlHotKeys"
        Me.pnlHotKeys.Size = New System.Drawing.Size(300, 300)
        Me.pnlHotKeys.TabIndex = 16
        Me.pnlHotKeys.Visible = False
        '
        'tbHKVolUp
        '
        Me.tbHKVolUp.Location = New System.Drawing.Point(199, 158)
        Me.tbHKVolUp.Name = "tbHKVolUp"
        Me.tbHKVolUp.Size = New System.Drawing.Size(50, 20)
        Me.tbHKVolUp.TabIndex = 31
        Me.tbHKVolUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKVolDown
        '
        Me.tbHKVolDown.Location = New System.Drawing.Point(148, 158)
        Me.tbHKVolDown.Name = "tbHKVolDown"
        Me.tbHKVolDown.Size = New System.Drawing.Size(50, 20)
        Me.tbHKVolDown.TabIndex = 30
        Me.tbHKVolDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(56, 163)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Volume Down/Up"
        '
        'tbHKLockNow
        '
        Me.tbHKLockNow.Location = New System.Drawing.Point(148, 242)
        Me.tbHKLockNow.Name = "tbHKLockNow"
        Me.tbHKLockNow.Size = New System.Drawing.Size(101, 20)
        Me.tbHKLockNow.TabIndex = 28
        Me.tbHKLockNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(80, 245)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Lock Screen"
        '
        'tbHKSleepNow
        '
        Me.tbHKSleepNow.Location = New System.Drawing.Point(148, 221)
        Me.tbHKSleepNow.Name = "tbHKSleepNow"
        Me.tbHKSleepNow.Size = New System.Drawing.Size(101, 20)
        Me.tbHKSleepNow.TabIndex = 26
        Me.tbHKSleepNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKGlobalMenu
        '
        Me.tbHKGlobalMenu.Location = New System.Drawing.Point(148, 200)
        Me.tbHKGlobalMenu.Name = "tbHKGlobalMenu"
        Me.tbHKGlobalMenu.Size = New System.Drawing.Size(101, 20)
        Me.tbHKGlobalMenu.TabIndex = 25
        Me.tbHKGlobalMenu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKShowHide
        '
        Me.tbHKShowHide.Location = New System.Drawing.Point(148, 179)
        Me.tbHKShowHide.Name = "tbHKShowHide"
        Me.tbHKShowHide.Size = New System.Drawing.Size(101, 20)
        Me.tbHKShowHide.TabIndex = 24
        Me.tbHKShowHide.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKBlockSong
        '
        Me.tbHKBlockSong.Location = New System.Drawing.Point(148, 137)
        Me.tbHKBlockSong.Name = "tbHKBlockSong"
        Me.tbHKBlockSong.Size = New System.Drawing.Size(101, 20)
        Me.tbHKBlockSong.TabIndex = 23
        Me.tbHKBlockSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKSkipSong
        '
        Me.tbHKSkipSong.Location = New System.Drawing.Point(148, 116)
        Me.tbHKSkipSong.Name = "tbHKSkipSong"
        Me.tbHKSkipSong.Size = New System.Drawing.Size(101, 20)
        Me.tbHKSkipSong.TabIndex = 22
        Me.tbHKSkipSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKDislikeSong
        '
        Me.tbHKDislikeSong.Location = New System.Drawing.Point(148, 95)
        Me.tbHKDislikeSong.Name = "tbHKDislikeSong"
        Me.tbHKDislikeSong.Size = New System.Drawing.Size(101, 20)
        Me.tbHKDislikeSong.TabIndex = 21
        Me.tbHKDislikeSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKLikeSong
        '
        Me.tbHKLikeSong.Location = New System.Drawing.Point(148, 74)
        Me.tbHKLikeSong.Name = "tbHKLikeSong"
        Me.tbHKLikeSong.Size = New System.Drawing.Size(101, 20)
        Me.tbHKLikeSong.TabIndex = 20
        Me.tbHKLikeSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKPlayPause
        '
        Me.tbHKPlayPause.Location = New System.Drawing.Point(148, 53)
        Me.tbHKPlayPause.Name = "tbHKPlayPause"
        Me.tbHKPlayPause.Size = New System.Drawing.Size(101, 20)
        Me.tbHKPlayPause.TabIndex = 19
        Me.tbHKPlayPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSaveHotkeys
        '
        Me.btnSaveHotkeys.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveHotkeys.ForeColor = System.Drawing.Color.Black
        Me.btnSaveHotkeys.Location = New System.Drawing.Point(113, 269)
        Me.btnSaveHotkeys.Name = "btnSaveHotkeys"
        Me.btnSaveHotkeys.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveHotkeys.TabIndex = 18
        Me.btnSaveHotkeys.Text = "Save"
        Me.btnSaveHotkeys.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(41, 224)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(107, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Sleep Computer Now"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(24, 203)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Show/Hide Global Menu"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 182)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Show/Hide Pandorian"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(85, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Block Song"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(91, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Skip Song"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(81, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Dislike Song"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(92, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Like Song"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(85, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Play/Pause"
        '
        'cbModKey
        '
        Me.cbModKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModKey.FormattingEnabled = True
        Me.cbModKey.Location = New System.Drawing.Point(148, 31)
        Me.cbModKey.Name = "cbModKey"
        Me.cbModKey.Size = New System.Drawing.Size(101, 21)
        Me.cbModKey.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(79, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Modifier Key"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, -378)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(293, 33)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Hotkey Configuration"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'volSlider
        '
        Me.volSlider.AutoSize = False
        Me.volSlider.LargeChange = 10
        Me.volSlider.Location = New System.Drawing.Point(2, 348)
        Me.volSlider.Maximum = 100
        Me.volSlider.Name = "volSlider"
        Me.volSlider.Size = New System.Drawing.Size(315, 22)
        Me.volSlider.SmallChange = 5
        Me.volSlider.TabIndex = 18
        Me.volSlider.TabStop = False
        Me.volSlider.TickStyle = System.Windows.Forms.TickStyle.None
        Me.volSlider.Value = 100
        Me.volSlider.Visible = False
        '
        'tbLog
        '
        Me.tbLog.Location = New System.Drawing.Point(7, 8)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.ReadOnly = True
        Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbLog.Size = New System.Drawing.Size(302, 447)
        Me.tbLog.TabIndex = 19
        Me.tbLog.Visible = False
        Me.tbLog.WordWrap = False
        '
        'tip
        '
        Me.tip.BackColor = System.Drawing.Color.White
        Me.tip.ForeColor = System.Drawing.Color.Black
        '
        'btnPrev
        '
        Me.btnPrev.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnPrev.FlatAppearance.BorderSize = 0
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.ForeColor = System.Drawing.Color.Black
        Me.btnPrev.Image = CType(resources.GetObject("btnPrev.Image"), System.Drawing.Image)
        Me.btnPrev.Location = New System.Drawing.Point(111, 421)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(45, 32)
        Me.btnPrev.TabIndex = 21
        Me.btnPrev.TabStop = False
        Me.tip.SetToolTip(Me.btnPrev, "Skip Back")
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'bpmTimer
        '
        Me.bpmTimer.Interval = 20
        '
        'lblBPM
        '
        Me.lblBPM.BackColor = System.Drawing.Color.Black
        Me.lblBPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPM.Location = New System.Drawing.Point(0, 365)
        Me.lblBPM.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBPM.Name = "lblBPM"
        Me.lblBPM.Size = New System.Drawing.Size(317, 49)
        Me.lblBPM.TabIndex = 20
        Me.lblBPM.Text = "000"
        Me.lblBPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBPM.Visible = False
        '
        'Spinner
        '
        Me.Spinner.BackColor = System.Drawing.Color.Black
        Me.Spinner.BackgroundImage = Global.Pandorian.My.Resources.Resources.wait
        Me.Spinner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Spinner.Location = New System.Drawing.Point(0, 0)
        Me.Spinner.Name = "Spinner"
        Me.Spinner.Size = New System.Drawing.Size(317, 463)
        Me.Spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Spinner.TabIndex = 17
        Me.Spinner.TabStop = False
        Me.Spinner.UseWaitCursor = True
        '
        'btnBlock
        '
        Me.btnBlock.BackColor = System.Drawing.SystemColors.Control
        Me.btnBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnBlock.FlatAppearance.BorderSize = 0
        Me.btnBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBlock.ForeColor = System.Drawing.Color.Black
        Me.btnBlock.Image = CType(resources.GetObject("btnBlock.Image"), System.Drawing.Image)
        Me.btnBlock.Location = New System.Drawing.Point(263, 421)
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(45, 32)
        Me.btnBlock.TabIndex = 12
        Me.btnBlock.TabStop = False
        Me.btnBlock.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.Control
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.ForeColor = System.Drawing.Color.Black
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(209, 421)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(45, 32)
        Me.btnNext.TabIndex = 9
        Me.btnNext.TabStop = False
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnPlayPause
        '
        Me.btnPlayPause.BackColor = System.Drawing.SystemColors.Control
        Me.btnPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnPlayPause.FlatAppearance.BorderSize = 0
        Me.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlayPause.ForeColor = System.Drawing.Color.Black
        Me.btnPlayPause.Image = CType(resources.GetObject("btnPlayPause.Image"), System.Drawing.Image)
        Me.btnPlayPause.Location = New System.Drawing.Point(160, 421)
        Me.btnPlayPause.Name = "btnPlayPause"
        Me.btnPlayPause.Size = New System.Drawing.Size(45, 32)
        Me.btnPlayPause.TabIndex = 8
        Me.btnPlayPause.TabStop = False
        Me.btnPlayPause.UseVisualStyleBackColor = False
        '
        'btnDislike
        '
        Me.btnDislike.BackColor = System.Drawing.SystemColors.Control
        Me.btnDislike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnDislike.FlatAppearance.BorderSize = 0
        Me.btnDislike.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDislike.ForeColor = System.Drawing.Color.Black
        Me.btnDislike.Image = CType(resources.GetObject("btnDislike.Image"), System.Drawing.Image)
        Me.btnDislike.Location = New System.Drawing.Point(57, 421)
        Me.btnDislike.Name = "btnDislike"
        Me.btnDislike.Size = New System.Drawing.Size(45, 32)
        Me.btnDislike.TabIndex = 7
        Me.btnDislike.TabStop = False
        Me.btnDislike.UseVisualStyleBackColor = False
        '
        'btnLike
        '
        Me.btnLike.BackColor = System.Drawing.SystemColors.Control
        Me.btnLike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnLike.FlatAppearance.BorderSize = 0
        Me.btnLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLike.ForeColor = System.Drawing.Color.Black
        Me.btnLike.Image = CType(resources.GetObject("btnLike.Image"), System.Drawing.Image)
        Me.btnLike.Location = New System.Drawing.Point(8, 421)
        Me.btnLike.Name = "btnLike"
        Me.btnLike.Size = New System.Drawing.Size(45, 32)
        Me.btnLike.TabIndex = 6
        Me.btnLike.TabStop = False
        Me.btnLike.UseVisualStyleBackColor = False
        '
        'SongCoverImage
        '
        Me.SongCoverImage.BackColor = System.Drawing.Color.Black
        Me.SongCoverImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.SongCoverImage.ContextMenuStrip = Me.MenuStrip
        Me.SongCoverImage.Location = New System.Drawing.Point(9, 44)
        Me.SongCoverImage.Name = "SongCoverImage"
        Me.SongCoverImage.Size = New System.Drawing.Size(300, 300)
        Me.SongCoverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SongCoverImage.TabIndex = 2
        Me.SongCoverImage.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(317, 463)
        Me.Controls.Add(Me.tbLog)
        Me.Controls.Add(Me.Spinner)
        Me.Controls.Add(Me.volSlider)
        Me.Controls.Add(Me.pnlHotKeys)
        Me.Controls.Add(Me.pnlSleepTimer)
        Me.Controls.Add(Me.btnBlock)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPlayPause)
        Me.Controls.Add(Me.btnDislike)
        Me.Controls.Add(Me.btnLike)
        Me.Controls.Add(Me.ddStations)
        Me.Controls.Add(Me.SongCoverImage)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.prgDownload)
        Me.Controls.Add(Me.lblBPM)
        Me.Controls.Add(Me.lblSongName)
        Me.Controls.Add(Me.lblAlbumName)
        Me.Controls.Add(Me.lblArtistName)
        Me.Controls.Add(Me.prgBar)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pandorian By Đĵ ΝιΓΞΗΛψΚ"
        Me.MenuStrip.ResumeLayout(False)
        Me.pnlSleepTimer.ResumeLayout(False)
        Me.pnlSleepTimer.PerformLayout()
        Me.TrayMenu.ResumeLayout(False)
        Me.pnlHotKeys.ResumeLayout(False)
        Me.pnlHotKeys.PerformLayout()
        CType(Me.volSlider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ddStations As System.Windows.Forms.ComboBox
    Friend WithEvents SongCoverImage As System.Windows.Forms.PictureBox
    Friend WithEvents lblSongName As System.Windows.Forms.Label
    Friend WithEvents lblArtistName As System.Windows.Forms.Label
    Friend WithEvents lblAlbumName As System.Windows.Forms.Label
    Friend WithEvents btnLike As System.Windows.Forms.Button
    Friend WithEvents btnDislike As System.Windows.Forms.Button
    Friend WithEvents btnPlayPause As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents prgBar As System.Windows.Forms.ProgressBar
    'Friend WithEvents Player As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents btnBlock As System.Windows.Forms.Button
    Friend WithEvents MenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miShowSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miManageStation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents prgDownload As System.Windows.Forms.ProgressBar
    Friend WithEvents folderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents miShowHotkeys As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miSleepTimer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSleepTimer As System.Windows.Forms.Panel
    Friend WithEvents lblSleepStatus As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddSleepTimes As System.Windows.Forms.ComboBox
    Friend WithEvents chkSleep As System.Windows.Forms.CheckBox
    Friend WithEvents btnSTDone As System.Windows.Forms.Button
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmiStationTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiSongTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiArtistTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiLikeCurrentSong As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiDislikeCurrentSong As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiPlayPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiSkipSong As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiBlockSong As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiSleepComputer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlHotKeys As System.Windows.Forms.Panel
    Friend WithEvents tbHKSleepNow As System.Windows.Forms.TextBox
    Friend WithEvents tbHKGlobalMenu As System.Windows.Forms.TextBox
    Friend WithEvents tbHKShowHide As System.Windows.Forms.TextBox
    Friend WithEvents tbHKBlockSong As System.Windows.Forms.TextBox
    Friend WithEvents tbHKSkipSong As System.Windows.Forms.TextBox
    Friend WithEvents tbHKDislikeSong As System.Windows.Forms.TextBox
    Friend WithEvents tbHKLikeSong As System.Windows.Forms.TextBox
    Friend WithEvents tbHKPlayPause As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveHotkeys As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbModKey As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Spinner As System.Windows.Forms.PictureBox
    Friend WithEvents tbHKLockNow As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tmiLockScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents volSlider As System.Windows.Forms.TrackBar
    Friend WithEvents tbHKVolUp As System.Windows.Forms.TextBox
    Friend WithEvents tbHKVolDown As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents miSendFeedback As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tbLog As System.Windows.Forms.TextBox
    Friend WithEvents tip As System.Windows.Forms.ToolTip
    Friend WithEvents bpmTimer As System.Windows.Forms.Timer
    Friend WithEvents lblBPM As System.Windows.Forms.Label
    Friend WithEvents btnPrev As Button
End Class
