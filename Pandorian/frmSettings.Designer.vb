﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.lblProxyAddre = New System.Windows.Forms.Label()
        Me.lblProxyUser = New System.Windows.Forms.Label()
        Me.lblProxyPass = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnPassword = New System.Windows.Forms.TextBox()
        Me.pnUsername = New System.Windows.Forms.TextBox()
        Me.prxPassword = New System.Windows.Forms.TextBox()
        Me.prxUserName = New System.Windows.Forms.TextBox()
        Me.prxAddress = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnPayPal = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ddQuality = New System.Windows.Forms.ComboBox()
        Me.chkPandoraOne = New System.Windows.Forms.CheckBox()
        Me.chkNoProxy = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.unlockCode = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.chkNoQMix = New System.Windows.Forms.CheckBox()
        Me.chkNoLiked = New System.Windows.Forms.CheckBox()
        Me.chkBPMCounter = New System.Windows.Forms.CheckBox()
        Me.chkNoPrefetch = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblProxyAddre
        '
        Me.lblProxyAddre.AutoSize = True
        Me.lblProxyAddre.Location = New System.Drawing.Point(12, 112)
        Me.lblProxyAddre.Name = "lblProxyAddre"
        Me.lblProxyAddre.Size = New System.Drawing.Size(74, 13)
        Me.lblProxyAddre.TabIndex = 0
        Me.lblProxyAddre.Text = "Proxy Address"
        '
        'lblProxyUser
        '
        Me.lblProxyUser.AutoSize = True
        Me.lblProxyUser.Location = New System.Drawing.Point(12, 134)
        Me.lblProxyUser.Name = "lblProxyUser"
        Me.lblProxyUser.Size = New System.Drawing.Size(84, 13)
        Me.lblProxyUser.TabIndex = 0
        Me.lblProxyUser.Text = "Proxy Username"
        '
        'lblProxyPass
        '
        Me.lblProxyPass.AutoSize = True
        Me.lblProxyPass.Location = New System.Drawing.Point(12, 156)
        Me.lblProxyPass.Name = "lblProxyPass"
        Me.lblProxyPass.Size = New System.Drawing.Size(82, 13)
        Me.lblProxyPass.TabIndex = 0
        Me.lblProxyPass.Text = "Proxy Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Pandora Username"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Pandora Password"
        '
        'pnPassword
        '
        Me.pnPassword.BackColor = System.Drawing.Color.White
        Me.pnPassword.Location = New System.Drawing.Point(115, 28)
        Me.pnPassword.Name = "pnPassword"
        Me.pnPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.pnPassword.Size = New System.Drawing.Size(157, 20)
        Me.pnPassword.TabIndex = 2
        Me.pnPassword.UseSystemPasswordChar = True
        '
        'pnUsername
        '
        Me.pnUsername.BackColor = System.Drawing.Color.White
        Me.pnUsername.Location = New System.Drawing.Point(115, 6)
        Me.pnUsername.Name = "pnUsername"
        Me.pnUsername.Size = New System.Drawing.Size(157, 20)
        Me.pnUsername.TabIndex = 1
        '
        'prxPassword
        '
        Me.prxPassword.BackColor = System.Drawing.Color.White
        Me.prxPassword.Location = New System.Drawing.Point(114, 153)
        Me.prxPassword.Name = "prxPassword"
        Me.prxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.prxPassword.Size = New System.Drawing.Size(157, 20)
        Me.prxPassword.TabIndex = 8
        Me.prxPassword.UseSystemPasswordChar = True
        '
        'prxUserName
        '
        Me.prxUserName.BackColor = System.Drawing.Color.White
        Me.prxUserName.Location = New System.Drawing.Point(114, 131)
        Me.prxUserName.Name = "prxUserName"
        Me.prxUserName.Size = New System.Drawing.Size(157, 20)
        Me.prxUserName.TabIndex = 7
        '
        'prxAddress
        '
        Me.prxAddress.BackColor = System.Drawing.Color.White
        Me.prxAddress.Location = New System.Drawing.Point(114, 109)
        Me.prxAddress.Name = "prxAddress"
        Me.prxAddress.Size = New System.Drawing.Size(157, 20)
        Me.prxAddress.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(50, 390)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 26)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Apply"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnPayPal
        '
        Me.btnPayPal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPayPal.FlatAppearance.BorderSize = 0
        Me.btnPayPal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayPal.Image = Global.Pandorian.My.Resources.Resources.paypal
        Me.btnPayPal.Location = New System.Drawing.Point(59, 326)
        Me.btnPayPal.Name = "btnPayPal"
        Me.btnPayPal.Size = New System.Drawing.Size(155, 56)
        Me.btnPayPal.TabIndex = 12
        Me.btnPayPal.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Audio Quality"
        '
        'ddQuality
        '
        Me.ddQuality.BackColor = System.Drawing.Color.White
        Me.ddQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddQuality.FormattingEnabled = True
        Me.ddQuality.Location = New System.Drawing.Point(115, 77)
        Me.ddQuality.Name = "ddQuality"
        Me.ddQuality.Size = New System.Drawing.Size(120, 21)
        Me.ddQuality.TabIndex = 5
        '
        'chkPandoraOne
        '
        Me.chkPandoraOne.AutoSize = True
        Me.chkPandoraOne.Location = New System.Drawing.Point(115, 54)
        Me.chkPandoraOne.Name = "chkPandoraOne"
        Me.chkPandoraOne.Size = New System.Drawing.Size(111, 17)
        Me.chkPandoraOne.TabIndex = 4
        Me.chkPandoraOne.Text = "Use Pandora Plus"
        Me.chkPandoraOne.UseVisualStyleBackColor = True
        '
        'chkNoProxy
        '
        Me.chkNoProxy.AutoSize = True
        Me.chkNoProxy.Location = New System.Drawing.Point(114, 179)
        Me.chkNoProxy.Name = "chkNoProxy"
        Me.chkNoProxy.Size = New System.Drawing.Size(112, 17)
        Me.chkNoProxy.TabIndex = 10
        Me.chkNoProxy.Text = "Don't Use A Proxy"
        Me.chkNoProxy.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 206)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Unlock Password"
        '
        'unlockCode
        '
        Me.unlockCode.BackColor = System.Drawing.Color.White
        Me.unlockCode.Location = New System.Drawing.Point(114, 203)
        Me.unlockCode.Name = "unlockCode"
        Me.unlockCode.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.unlockCode.Size = New System.Drawing.Size(157, 20)
        Me.unlockCode.TabIndex = 11
        Me.unlockCode.UseSystemPasswordChar = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(144, 390)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(82, 26)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'chkNoQMix
        '
        Me.chkNoQMix.AutoSize = True
        Me.chkNoQMix.Location = New System.Drawing.Point(25, 231)
        Me.chkNoQMix.Name = "chkNoQMix"
        Me.chkNoQMix.Size = New System.Drawing.Size(226, 17)
        Me.chkNoQMix.TabIndex = 15
        Me.chkNoQMix.Text = "Don't indicate QuickMix members with a ✪"
        Me.chkNoQMix.UseVisualStyleBackColor = True
        '
        'chkNoLiked
        '
        Me.chkNoLiked.AutoSize = True
        Me.chkNoLiked.Location = New System.Drawing.Point(25, 254)
        Me.chkNoLiked.Name = "chkNoLiked"
        Me.chkNoLiked.Size = New System.Drawing.Size(190, 17)
        Me.chkNoLiked.TabIndex = 16
        Me.chkNoLiked.Text = "Don't indicate liked songs with a ✔"
        Me.chkNoLiked.UseVisualStyleBackColor = True
        '
        'chkBPMCounter
        '
        Me.chkBPMCounter.AutoSize = True
        Me.chkBPMCounter.Location = New System.Drawing.Point(25, 302)
        Me.chkBPMCounter.Name = "chkBPMCounter"
        Me.chkBPMCounter.Size = New System.Drawing.Size(239, 17)
        Me.chkBPMCounter.TabIndex = 17
        Me.chkBPMCounter.Text = "Show BPM of song when album art is clicked"
        Me.chkBPMCounter.UseVisualStyleBackColor = True
        '
        'chkNoPrefetch
        '
        Me.chkNoPrefetch.AutoSize = True
        Me.chkNoPrefetch.Location = New System.Drawing.Point(25, 278)
        Me.chkNoPrefetch.Name = "chkNoPrefetch"
        Me.chkNoPrefetch.Size = New System.Drawing.Size(145, 17)
        Me.chkNoPrefetch.TabIndex = 18
        Me.chkNoPrefetch.Text = "Don't pre-fetch next song"
        Me.chkNoPrefetch.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 427)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkNoPrefetch)
        Me.Controls.Add(Me.chkBPMCounter)
        Me.Controls.Add(Me.chkNoLiked)
        Me.Controls.Add(Me.chkNoQMix)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkNoProxy)
        Me.Controls.Add(Me.chkPandoraOne)
        Me.Controls.Add(Me.ddQuality)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnPayPal)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.prxAddress)
        Me.Controls.Add(Me.prxUserName)
        Me.Controls.Add(Me.unlockCode)
        Me.Controls.Add(Me.prxPassword)
        Me.Controls.Add(Me.pnUsername)
        Me.Controls.Add(Me.pnPassword)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblProxyPass)
        Me.Controls.Add(Me.lblProxyUser)
        Me.Controls.Add(Me.lblProxyAddre)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pandorian Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblProxyAddre As System.Windows.Forms.Label
    Friend WithEvents lblProxyUser As System.Windows.Forms.Label
    Friend WithEvents lblProxyPass As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnPassword As System.Windows.Forms.TextBox
    Friend WithEvents pnUsername As System.Windows.Forms.TextBox
    Friend WithEvents prxPassword As System.Windows.Forms.TextBox
    Friend WithEvents prxUserName As System.Windows.Forms.TextBox
    Friend WithEvents prxAddress As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnPayPal As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ddQuality As System.Windows.Forms.ComboBox
    Friend WithEvents chkPandoraOne As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoProxy As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents unlockCode As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkNoQMix As CheckBox
    Friend WithEvents chkNoLiked As CheckBox
    Friend WithEvents chkBPMCounter As CheckBox
    Friend WithEvents chkNoPrefetch As CheckBox
End Class
