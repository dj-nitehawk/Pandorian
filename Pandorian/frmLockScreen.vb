Public NotInheritable Class frmLockScreen

    Private Sub frmLockScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.TopMost = True
    End Sub

End Class
