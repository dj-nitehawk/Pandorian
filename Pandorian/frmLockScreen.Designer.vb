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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CoverImage = New System.Windows.Forms.PictureBox()
        Me.tbPassword = New System.Windows.Forms.TextBox()
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CoverImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CoverImage, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbPassword, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(770, 570)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CoverImage
        '
        Me.CoverImage.BackColor = System.Drawing.Color.White
        Me.CoverImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CoverImage.Location = New System.Drawing.Point(138, 45)
        Me.CoverImage.Name = "CoverImage"
        Me.CoverImage.Size = New System.Drawing.Size(494, 494)
        Me.CoverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.CoverImage.TabIndex = 0
        Me.CoverImage.TabStop = False
        '
        'tbPassword
        '
        Me.tbPassword.AcceptsReturn = True
        Me.tbPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.tbPassword.Location = New System.Drawing.Point(285, 546)
        Me.tbPassword.MaxLength = 100
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.ShortcutsEnabled = False
        Me.tbPassword.Size = New System.Drawing.Size(200, 20)
        Me.tbPassword.TabIndex = 1
        Me.tbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbPassword.UseSystemPasswordChar = True
        '
        'timer
        '
        Me.timer.Enabled = True
        '
        'frmLockScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(770, 570)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLockScreen"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.CoverImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CoverImage As System.Windows.Forms.PictureBox
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents timer As System.Windows.Forms.Timer

End Class
