<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLockScreen
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
        Me.lockTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CoverImage = New System.Windows.Forms.PictureBox()
        Me.tbPassword = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblAlbum = New System.Windows.Forms.Label()
        Me.lblArtist = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTimeNow = New System.Windows.Forms.Label()
        Me.lblDateNow = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CoverImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lockTimer
        '
        Me.lockTimer.Interval = 1000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CoverImage, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbPassword, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTimeNow, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDateNow, 1, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(978, 722)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CoverImage
        '
        Me.CoverImage.BackColor = System.Drawing.Color.Black
        Me.CoverImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CoverImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CoverImage.Location = New System.Drawing.Point(239, 86)
        Me.CoverImage.MaximumSize = New System.Drawing.Size(500, 500)
        Me.CoverImage.MinimumSize = New System.Drawing.Size(500, 500)
        Me.CoverImage.Name = "CoverImage"
        Me.CoverImage.Size = New System.Drawing.Size(500, 500)
        Me.CoverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.CoverImage.TabIndex = 0
        Me.CoverImage.TabStop = False
        '
        'tbPassword
        '
        Me.tbPassword.AcceptsReturn = True
        Me.tbPassword.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tbPassword.Location = New System.Drawing.Point(389, 609)
        Me.tbPassword.Margin = New System.Windows.Forms.Padding(3, 20, 3, 10)
        Me.tbPassword.MaxLength = 100
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.ShortcutsEnabled = False
        Me.tbPassword.Size = New System.Drawing.Size(200, 20)
        Me.tbPassword.TabIndex = 1
        Me.tbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbPassword.UseSystemPasswordChar = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Panel1.Controls.Add(Me.lblAlbum)
        Me.Panel1.Controls.Add(Me.lblArtist)
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Location = New System.Drawing.Point(242, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(494, 65)
        Me.Panel1.TabIndex = 5
        '
        'lblAlbum
        '
        Me.lblAlbum.AutoEllipsis = True
        Me.lblAlbum.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlbum.ForeColor = System.Drawing.Color.White
        Me.lblAlbum.Location = New System.Drawing.Point(3, 44)
        Me.lblAlbum.Name = "lblAlbum"
        Me.lblAlbum.Size = New System.Drawing.Size(488, 17)
        Me.lblAlbum.TabIndex = 4
        Me.lblAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArtist
        '
        Me.lblArtist.AutoEllipsis = True
        Me.lblArtist.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArtist.ForeColor = System.Drawing.Color.White
        Me.lblArtist.Location = New System.Drawing.Point(3, 24)
        Me.lblArtist.Name = "lblArtist"
        Me.lblArtist.Size = New System.Drawing.Size(488, 17)
        Me.lblArtist.TabIndex = 3
        Me.lblArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.AutoEllipsis = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(3, 4)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(488, 17)
        Me.lblTitle.TabIndex = 2
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTimeNow
        '
        Me.lblTimeNow.AutoSize = True
        Me.lblTimeNow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTimeNow.Font = New System.Drawing.Font("Calibri", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeNow.ForeColor = System.Drawing.Color.White
        Me.lblTimeNow.Location = New System.Drawing.Point(239, 639)
        Me.lblTimeNow.Name = "lblTimeNow"
        Me.lblTimeNow.Size = New System.Drawing.Size(500, 27)
        Me.lblTimeNow.TabIndex = 6
        Me.lblTimeNow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblDateNow
        '
        Me.lblDateNow.AutoSize = True
        Me.lblDateNow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDateNow.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateNow.ForeColor = System.Drawing.Color.White
        Me.lblDateNow.Location = New System.Drawing.Point(239, 666)
        Me.lblDateNow.Name = "lblDateNow"
        Me.lblDateNow.Size = New System.Drawing.Size(500, 20)
        Me.lblDateNow.TabIndex = 7
        Me.lblDateNow.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmLockScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(978, 722)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLockScreen"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.CoverImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lockTimer As System.Windows.Forms.Timer
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CoverImage As System.Windows.Forms.PictureBox
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblArtist As System.Windows.Forms.Label
    Friend WithEvents lblAlbum As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTimeNow As System.Windows.Forms.Label
    Friend WithEvents lblDateNow As System.Windows.Forms.Label

End Class
