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
        Me.Spinner = New System.Windows.Forms.PictureBox()
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
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip.SuspendLayout()
        Me.pnlSleepTimer.SuspendLayout()
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
        'Spinner
        '
        Me.Spinner.Image = Global.Pandorian.My.Resources.Resources.spinner
        Me.Spinner.Location = New System.Drawing.Point(2, 1)
        Me.Spinner.Name = "Spinner"
        Me.Spinner.Size = New System.Drawing.Size(312, 460)
        Me.Spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Spinner.TabIndex = 13
        Me.Spinner.TabStop = False
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
        Me.MenuStrip.ShowImageMargin = False
        Me.MenuStrip.Size = New System.Drawing.Size(168, 148)
        '
        'miManageStation
        '
        Me.miManageStation.Name = "miManageStation"
        Me.miManageStation.Size = New System.Drawing.Size(167, 22)
        Me.miManageStation.Text = "Manage Station"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'miShowSettings
        '
        Me.miShowSettings.Name = "miShowSettings"
        Me.miShowSettings.Size = New System.Drawing.Size(167, 22)
        Me.miShowSettings.Text = "Show Settings"
        '
        'miShowHotkeys
        '
        Me.miShowHotkeys.Name = "miShowHotkeys"
        Me.miShowHotkeys.Size = New System.Drawing.Size(167, 22)
        Me.miShowHotkeys.Text = "Show HotKeys"
        '
        'miSleepTimer
        '
        Me.miSleepTimer.Name = "miSleepTimer"
        Me.miSleepTimer.Size = New System.Drawing.Size(167, 22)
        Me.miSleepTimer.Text = "Sleep Timer"
        '
        'miUpdate
        '
        Me.miUpdate.Name = "miUpdate"
        Me.miUpdate.Size = New System.Drawing.Size(167, 22)
        Me.miUpdate.Text = "Check For An Update"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(164, 6)
        '
        'miVersion
        '
        Me.miVersion.Enabled = False
        Me.miVersion.Name = "miVersion"
        Me.miVersion.Size = New System.Drawing.Size(167, 22)
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
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(317, 461)
        Me.Controls.Add(Me.Spinner)
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
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pandorian By Đĵ ΝιΓΞΗΛψΚ"
        CType(Me.Spinner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SongCoverImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip.ResumeLayout(False)
        Me.pnlSleepTimer.ResumeLayout(False)
        Me.pnlSleepTimer.PerformLayout()
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
    Friend WithEvents Spinner As System.Windows.Forms.PictureBox
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

End Class
