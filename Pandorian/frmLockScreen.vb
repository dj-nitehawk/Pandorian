﻿
Public Class frmLockScreen

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
        If Not String.IsNullOrEmpty(My.Settings.unlockPassword) Then
            If Not getMD5Hash(tbPassword.Text) = Decrypt(My.Settings.unlockPassword) Then
                Exit Sub
            End If
        End If
        tbPassword.Text = ""
        Me.Hide()
    End Sub

    Private Sub frmLockScreen_Enter(sender As Object, e As EventArgs) Handles Me.Enter
        tbPassword.Focus()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles lockTimer.Tick
        If Me.Visible Then
            For Each p In Process.GetProcessesByName("taskmgr")
                p.Kill()
            Next

            Me.TopMost = True
            Me.Activate()

            If Not tbPassword.Focused Then
                tbPassword.Focus()
            End If

            lblTimeNow.Text = Now.ToString("t")
            lblDateNow.Text = Now.DayOfWeek.ToString + ", " + MonthName(Now.Month) + " " + Now.Day.ToString + " " + Now.Year.ToString

            If Not lblTitle.Text = frmMain.lblSongName.Text Then
                lblTitle.Text = frmMain.lblSongName.Text
                lblArtist.Text = frmMain.lblArtistName.Text
                lblAlbum.Text = frmMain.lblAlbumName.Text
                CoverImage.Image = frmMain.SongCoverImage.Image
            End If

        End If
    End Sub

    Private Sub frmLockScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        tbPassword.Focus()
        lblAlbum.UseMnemonic = False
        lblArtist.UseMnemonic = False
        lblTitle.UseMnemonic = False
    End Sub

    Private Sub frmLockScreen_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Select Case Me.Visible
            Case True
                lockTimer.Enabled = True
                KeyboardJammer.Jam()
                Windows.Forms.Cursor.Hide()
            Case False
                lockTimer.Enabled = False
                KeyboardJammer.UnJam()
                Windows.Forms.Cursor.Show()
        End Select
    End Sub

End Class
