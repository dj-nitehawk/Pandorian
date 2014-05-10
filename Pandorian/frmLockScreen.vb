Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public NotInheritable Class frmLockScreen

    Private Sub frmLockScreen_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.TopMost = True
    End Sub

    Private Sub tbPassword_TextChanged(sender As Object, e As EventArgs) Handles tbPassword.TextChanged
        If tbPassword.Text = "5284" Then
            Me.Close()
        End If
    End Sub

    Private Sub frmLockScreen_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        KeyboardJammer.Jam()
    End Sub

    Private Sub frmLockScreen_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        KeyboardJammer.UnJam()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        timer.Enabled = False
        For Each p In Process.GetProcessesByName("taskmgr")
            p.Kill()
        Next
        timer.Enabled = True
        Me.TopMost = True
    End Sub

End Class
