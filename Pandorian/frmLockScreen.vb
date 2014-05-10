Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public NotInheritable Class frmLockScreen
    Private CurrentToken As String

    Private Sub frmLockScreen_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.TopMost = True
    End Sub

    Private Sub tbPassword_KeyUp(sender As Object, e As KeyEventArgs) Handles tbPassword.KeyUp
        If e.KeyCode = Keys.Enter Then
            tbPassword_TextChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub tbPassword_Leave(sender As Object, e As EventArgs) Handles tbPassword.Leave
        tbPassword.Focus()
    End Sub

    Private Sub tbPassword_TextChanged(sender As Object, e As EventArgs) Handles tbPassword.TextChanged
        If tbPassword.Text = Decrypt(My.Settings.lockScreenPassword) Then
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
        UpdateInfo()
    End Sub

    Private Sub frmLockScreen_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        tbPassword.Focus()
    End Sub

    Private Sub UpdateInfo()
        If Not CurrentToken = frmMain.SongToken Then
            CurrentToken = frmMain.SongToken
            lblTitle.Text = frmMain.lblSongName.Text
            lblArtist.Text = frmMain.lblArtistName.Text
            lblAlbum.Text = frmMain.lblAlbumName.Text
            CoverImage.Image = frmMain.SongCoverImage.Image
        End If
    End Sub
End Class
