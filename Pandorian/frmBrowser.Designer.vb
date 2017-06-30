<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrowser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBrowser))
        Me.wBrowser = New Pandorian.WebBrowserWithProxy()
        Me.SuspendLayout()
        '
        'wBrowser
        '
        Me.wBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wBrowser.Location = New System.Drawing.Point(0, 0)
        Me.wBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wBrowser.Name = "wBrowser"
        Me.wBrowser.Size = New System.Drawing.Size(1064, 641)
        Me.wBrowser.TabIndex = 0
        '
        'frmBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 641)
        Me.Controls.Add(Me.wBrowser)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBrowser"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pandorian Browser"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents wBrowser As Pandorian.WebBrowserWithProxy
End Class
