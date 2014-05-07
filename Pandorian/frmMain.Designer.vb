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
        Me.btnLike = New System.Windows.Forms.Button()
        Me.btnDislike = New System.Windows.Forms.Button()
        Me.btnPlayPause = New System.Windows.Forms.Button()
        Me.btnSkip = New System.Windows.Forms.Button()
        Me.prgBar = New System.Windows.Forms.ProgressBar()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.btnBlock = New System.Windows.Forms.Button()
        Me.SongCoverImage = New System.Windows.Forms.PictureBox()
        Me.MenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miManageStation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.miShowSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.miShowHotkeys = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSleepTimer = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.tmiSleepComputer = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlHotKeys = New System.Windows.Forms.Panel()
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
        Me.Spinner = New System.Windows.Forms.PictureBox()
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip.SuspendLayout()
        Me.pnlSleepTimer.SuspendLayout()
        Me.TrayMenu.SuspendLayout()
        Me.pnlHotKeys.SuspendLayout()
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'lblSongName
        '
        Me.lblSongName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSongName.Location = New System.Drawing.Point(-9, 368)
        Me.lblSongName.Name = "lblSongName"
        Me.lblSongName.Size = New System.Drawing.Size(332, 15)
        Me.lblSongName.TabIndex = 3
        Me.lblSongName.Text = "Song Name"
        Me.lblSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArtistName
        '
        Me.lblArtistName.Location = New System.Drawing.Point(-9, 384)
        Me.lblArtistName.Name = "lblArtistName"
        Me.lblArtistName.Size = New System.Drawing.Size(332, 15)
        Me.lblArtistName.TabIndex = 4
        Me.lblArtistName.Text = "Artist Name"
        Me.lblArtistName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAlbumName
        '
        Me.lblAlbumName.Location = New System.Drawing.Point(-6, 400)
        Me.lblAlbumName.Name = "lblAlbumName"
        Me.lblAlbumName.Size = New System.Drawing.Size(329, 15)
        Me.lblAlbumName.TabIndex = 5
        Me.lblAlbumName.Text = "Album Name"
        Me.lblAlbumName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnLike
        '
        Me.btnLike.BackColor = System.Drawing.Color.Azure
        Me.btnLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLike.ForeColor = System.Drawing.Color.Black
        Me.btnLike.Location = New System.Drawing.Point(9, 421)
        Me.btnLike.Name = "btnLike"
        Me.btnLike.Size = New System.Drawing.Size(53, 30)
        Me.btnLike.TabIndex = 6
        Me.btnLike.TabStop = False
        Me.btnLike.Text = "Like"
        Me.btnLike.UseVisualStyleBackColor = False
        '
        'btnDislike
        '
        Me.btnDislike.BackColor = System.Drawing.Color.Azure
        Me.btnDislike.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDislike.ForeColor = System.Drawing.Color.Black
        Me.btnDislike.Location = New System.Drawing.Point(66, 421)
        Me.btnDislike.Name = "btnDislike"
        Me.btnDislike.Size = New System.Drawing.Size(53, 30)
        Me.btnDislike.TabIndex = 7
        Me.btnDislike.TabStop = False
        Me.btnDislike.Text = "Dislike"
        Me.btnDislike.UseVisualStyleBackColor = False
        '
        'btnPlayPause
        '
        Me.btnPlayPause.BackColor = System.Drawing.Color.Azure
        Me.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlayPause.ForeColor = System.Drawing.Color.Black
        Me.btnPlayPause.Location = New System.Drawing.Point(134, 421)
        Me.btnPlayPause.Name = "btnPlayPause"
        Me.btnPlayPause.Size = New System.Drawing.Size(53, 30)
        Me.btnPlayPause.TabIndex = 8
        Me.btnPlayPause.TabStop = False
        Me.btnPlayPause.Text = "Pause"
        Me.btnPlayPause.UseVisualStyleBackColor = False
        '
        'btnSkip
        '
        Me.btnSkip.BackColor = System.Drawing.Color.Azure
        Me.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSkip.ForeColor = System.Drawing.Color.Black
        Me.btnSkip.Location = New System.Drawing.Point(191, 421)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(53, 30)
        Me.btnSkip.TabIndex = 9
        Me.btnSkip.TabStop = False
        Me.btnSkip.Text = "Skip"
        Me.btnSkip.UseVisualStyleBackColor = False
        '
        'prgBar
        '
        Me.prgBar.Location = New System.Drawing.Point(9, 352)
        Me.prgBar.Minimum = 1
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
        'btnBlock
        '
        Me.btnBlock.BackColor = System.Drawing.Color.Azure
        Me.btnBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBlock.ForeColor = System.Drawing.Color.Black
        Me.btnBlock.Location = New System.Drawing.Point(259, 421)
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(53, 30)
        Me.btnBlock.TabIndex = 12
        Me.btnBlock.TabStop = False
        Me.btnBlock.Text = "Block"
        Me.btnBlock.UseVisualStyleBackColor = False
        '
        'SongCoverImage
        '
        Me.SongCoverImage.BackColor = System.Drawing.Color.SteelBlue
        Me.SongCoverImage.ContextMenuStrip = Me.MenuStrip
        Me.SongCoverImage.Location = New System.Drawing.Point(9, 44)
        Me.SongCoverImage.Name = "SongCoverImage"
        Me.SongCoverImage.Size = New System.Drawing.Size(300, 300)
        Me.SongCoverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.SongCoverImage.TabIndex = 2
        Me.SongCoverImage.TabStop = False
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miManageStation, Me.ToolStripSeparator1, Me.miShowSettings, Me.miShowHotkeys, Me.miSleepTimer, Me.miUpdate, Me.ToolStripSeparator2, Me.miVersion})
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(193, 148)
        '
        'miManageStation
        '
        Me.miManageStation.Name = "miManageStation"
        Me.miManageStation.Size = New System.Drawing.Size(192, 22)
        Me.miManageStation.Text = "Manage Station"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(189, 6)
        '
        'miShowSettings
        '
        Me.miShowSettings.Name = "miShowSettings"
        Me.miShowSettings.Size = New System.Drawing.Size(192, 22)
        Me.miShowSettings.Text = "Show Settings"
        '
        'miShowHotkeys
        '
        Me.miShowHotkeys.Name = "miShowHotkeys"
        Me.miShowHotkeys.Size = New System.Drawing.Size(192, 22)
        Me.miShowHotkeys.Text = "Show HotKeys"
        '
        'miSleepTimer
        '
        Me.miSleepTimer.Name = "miSleepTimer"
        Me.miSleepTimer.Size = New System.Drawing.Size(192, 22)
        Me.miSleepTimer.Text = "Sleep Timer"
        '
        'miUpdate
        '
        Me.miUpdate.Name = "miUpdate"
        Me.miUpdate.Size = New System.Drawing.Size(192, 22)
        Me.miUpdate.Text = "Visit Website"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(189, 6)
        '
        'miVersion
        '
        Me.miVersion.Enabled = False
        Me.miVersion.Name = "miVersion"
        Me.miVersion.Size = New System.Drawing.Size(192, 22)
        Me.miVersion.Text = "Current Version: v1.4.0"
        '
        'prgDownload
        '
        Me.prgDownload.Location = New System.Drawing.Point(0, 451)
        Me.prgDownload.Name = "prgDownload"
        Me.prgDownload.Size = New System.Drawing.Size(317, 10)
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
        Me.TrayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiStationTitle, Me.tmiSongTitle, Me.tmiArtistTitle, Me.ToolStripSeparator3, Me.tmiLikeCurrentSong, Me.tmiDislikeCurrentSong, Me.ToolStripSeparator4, Me.tmiPlayPause, Me.tmiSkipSong, Me.ToolStripSeparator5, Me.tmiBlockSong, Me.ToolStripSeparator6, Me.tmiSleepComputer, Me.tmiExit})
        Me.TrayMenu.Name = "TrayMenu"
        Me.TrayMenu.Size = New System.Drawing.Size(224, 248)
        '
        'tmiStationTitle
        '
        Me.tmiStationTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmiStationTitle.Name = "tmiStationTitle"
        Me.tmiStationTitle.Size = New System.Drawing.Size(223, 22)
        Me.tmiStationTitle.Text = "Station Name"
        '
        'tmiSongTitle
        '
        Me.tmiSongTitle.BackColor = System.Drawing.SystemColors.Control
        Me.tmiSongTitle.ForeColor = System.Drawing.Color.DimGray
        Me.tmiSongTitle.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tmiSongTitle.Name = "tmiSongTitle"
        Me.tmiSongTitle.Size = New System.Drawing.Size(223, 22)
        Me.tmiSongTitle.Text = "Name Of Song"
        '
        'tmiArtistTitle
        '
        Me.tmiArtistTitle.ForeColor = System.Drawing.Color.DimGray
        Me.tmiArtistTitle.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tmiArtistTitle.Name = "tmiArtistTitle"
        Me.tmiArtistTitle.Size = New System.Drawing.Size(223, 22)
        Me.tmiArtistTitle.Text = "Artist Name"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(220, 6)
        '
        'tmiLikeCurrentSong
        '
        Me.tmiLikeCurrentSong.Name = "tmiLikeCurrentSong"
        Me.tmiLikeCurrentSong.ShortcutKeyDisplayString = "(ALT+L)"
        Me.tmiLikeCurrentSong.Size = New System.Drawing.Size(223, 22)
        Me.tmiLikeCurrentSong.Text = "Like"
        '
        'tmiDislikeCurrentSong
        '
        Me.tmiDislikeCurrentSong.Name = "tmiDislikeCurrentSong"
        Me.tmiDislikeCurrentSong.ShortcutKeyDisplayString = "(ALT+D)"
        Me.tmiDislikeCurrentSong.Size = New System.Drawing.Size(223, 22)
        Me.tmiDislikeCurrentSong.Text = "Dislike"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(220, 6)
        '
        'tmiPlayPause
        '
        Me.tmiPlayPause.Name = "tmiPlayPause"
        Me.tmiPlayPause.ShortcutKeyDisplayString = "(ALT+SPACE)"
        Me.tmiPlayPause.Size = New System.Drawing.Size(223, 22)
        Me.tmiPlayPause.Text = "Play/Pause"
        '
        'tmiSkipSong
        '
        Me.tmiSkipSong.Name = "tmiSkipSong"
        Me.tmiSkipSong.ShortcutKeyDisplayString = "(ALT+S)"
        Me.tmiSkipSong.Size = New System.Drawing.Size(223, 22)
        Me.tmiSkipSong.Text = "Skip"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(220, 6)
        '
        'tmiBlockSong
        '
        Me.tmiBlockSong.Name = "tmiBlockSong"
        Me.tmiBlockSong.ShortcutKeyDisplayString = "(ALT+B)"
        Me.tmiBlockSong.Size = New System.Drawing.Size(223, 22)
        Me.tmiBlockSong.Text = "Block"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(220, 6)
        '
        'tmiSleepComputer
        '
        Me.tmiSleepComputer.Name = "tmiSleepComputer"
        Me.tmiSleepComputer.ShortcutKeyDisplayString = "(ALT+ESC)"
        Me.tmiSleepComputer.Size = New System.Drawing.Size(223, 22)
        Me.tmiSleepComputer.Text = "Sleep Computer"
        '
        'tmiExit
        '
        Me.tmiExit.Name = "tmiExit"
        Me.tmiExit.Size = New System.Drawing.Size(223, 22)
        Me.tmiExit.Text = "Exit"
        '
        'pnlHotKeys
        '
        Me.pnlHotKeys.BackColor = System.Drawing.Color.SlateGray
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
        'tbHKSleepNow
        '
        Me.tbHKSleepNow.Location = New System.Drawing.Point(160, 234)
        Me.tbHKSleepNow.Name = "tbHKSleepNow"
        Me.tbHKSleepNow.Size = New System.Drawing.Size(72, 20)
        Me.tbHKSleepNow.TabIndex = 26
        Me.tbHKSleepNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKGlobalMenu
        '
        Me.tbHKGlobalMenu.Location = New System.Drawing.Point(160, 211)
        Me.tbHKGlobalMenu.Name = "tbHKGlobalMenu"
        Me.tbHKGlobalMenu.Size = New System.Drawing.Size(72, 20)
        Me.tbHKGlobalMenu.TabIndex = 25
        Me.tbHKGlobalMenu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKShowHide
        '
        Me.tbHKShowHide.Location = New System.Drawing.Point(160, 188)
        Me.tbHKShowHide.Name = "tbHKShowHide"
        Me.tbHKShowHide.Size = New System.Drawing.Size(72, 20)
        Me.tbHKShowHide.TabIndex = 24
        Me.tbHKShowHide.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKBlockSong
        '
        Me.tbHKBlockSong.Location = New System.Drawing.Point(160, 165)
        Me.tbHKBlockSong.Name = "tbHKBlockSong"
        Me.tbHKBlockSong.Size = New System.Drawing.Size(72, 20)
        Me.tbHKBlockSong.TabIndex = 23
        Me.tbHKBlockSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKSkipSong
        '
        Me.tbHKSkipSong.Location = New System.Drawing.Point(160, 142)
        Me.tbHKSkipSong.Name = "tbHKSkipSong"
        Me.tbHKSkipSong.Size = New System.Drawing.Size(72, 20)
        Me.tbHKSkipSong.TabIndex = 22
        Me.tbHKSkipSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKDislikeSong
        '
        Me.tbHKDislikeSong.Location = New System.Drawing.Point(160, 120)
        Me.tbHKDislikeSong.Name = "tbHKDislikeSong"
        Me.tbHKDislikeSong.Size = New System.Drawing.Size(72, 20)
        Me.tbHKDislikeSong.TabIndex = 21
        Me.tbHKDislikeSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKLikeSong
        '
        Me.tbHKLikeSong.Location = New System.Drawing.Point(160, 97)
        Me.tbHKLikeSong.Name = "tbHKLikeSong"
        Me.tbHKLikeSong.Size = New System.Drawing.Size(72, 20)
        Me.tbHKLikeSong.TabIndex = 20
        Me.tbHKLikeSong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbHKPlayPause
        '
        Me.tbHKPlayPause.Location = New System.Drawing.Point(160, 74)
        Me.tbHKPlayPause.Name = "tbHKPlayPause"
        Me.tbHKPlayPause.Size = New System.Drawing.Size(72, 20)
        Me.tbHKPlayPause.TabIndex = 19
        Me.tbHKPlayPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSaveHotkeys
        '
        Me.btnSaveHotkeys.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveHotkeys.ForeColor = System.Drawing.Color.Black
        Me.btnSaveHotkeys.Location = New System.Drawing.Point(113, 267)
        Me.btnSaveHotkeys.Name = "btnSaveHotkeys"
        Me.btnSaveHotkeys.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveHotkeys.TabIndex = 18
        Me.btnSaveHotkeys.Text = "Save"
        Me.btnSaveHotkeys.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(53, 237)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(107, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Sleep Computer Now"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(36, 214)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Show/Hide Global Menu"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(48, 191)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Show/Hide Pandorian"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(97, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Block Song"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(103, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Skip Song"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(93, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Dislike Song"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(104, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Like Song"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(97, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Play/Pause"
        '
        'cbModKey
        '
        Me.cbModKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModKey.FormattingEnabled = True
        Me.cbModKey.Location = New System.Drawing.Point(160, 42)
        Me.cbModKey.Name = "cbModKey"
        Me.cbModKey.Size = New System.Drawing.Size(72, 21)
        Me.cbModKey.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(91, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Modifier Key"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(293, 33)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Hotkey Configuration"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Spinner
        '
        Me.Spinner.BackColor = System.Drawing.Color.Black
        Me.Spinner.Image = Global.Pandorian.My.Resources.Resources.spinner
        Me.Spinner.Location = New System.Drawing.Point(-6, -6)
        Me.Spinner.Name = "Spinner"
        Me.Spinner.Size = New System.Drawing.Size(329, 476)
        Me.Spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Spinner.TabIndex = 17
        Me.Spinner.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(317, 461)
        Me.Controls.Add(Me.Spinner)
        Me.Controls.Add(Me.pnlHotKeys)
        Me.Controls.Add(Me.pnlSleepTimer)
        Me.Controls.Add(Me.prgDownload)
        Me.Controls.Add(Me.btnBlock)
        Me.Controls.Add(Me.prgBar)
        Me.Controls.Add(Me.btnSkip)
        Me.Controls.Add(Me.btnPlayPause)
        Me.Controls.Add(Me.btnDislike)
        Me.Controls.Add(Me.btnLike)
        Me.Controls.Add(Me.lblAlbumName)
        Me.Controls.Add(Me.lblArtistName)
        Me.Controls.Add(Me.lblSongName)
        Me.Controls.Add(Me.SongCoverImage)
        Me.Controls.Add(Me.ddStations)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pandorian By Đĵ ΝιΓΞΗΛψΚ"
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip.ResumeLayout(False)
        Me.pnlSleepTimer.ResumeLayout(False)
        Me.pnlSleepTimer.PerformLayout()
        Me.TrayMenu.ResumeLayout(False)
        Me.pnlHotKeys.ResumeLayout(False)
        Me.pnlHotKeys.PerformLayout()
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ddStations As System.Windows.Forms.ComboBox
    Friend WithEvents SongCoverImage As System.Windows.Forms.PictureBox
    Friend WithEvents lblSongName As System.Windows.Forms.Label
    Friend WithEvents lblArtistName As System.Windows.Forms.Label
    Friend WithEvents lblAlbumName As System.Windows.Forms.Label
    Friend WithEvents btnLike As System.Windows.Forms.Button
    Friend WithEvents btnDislike As System.Windows.Forms.Button
    Friend WithEvents btnPlayPause As System.Windows.Forms.Button
    Friend WithEvents btnSkip As System.Windows.Forms.Button
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

End Class
