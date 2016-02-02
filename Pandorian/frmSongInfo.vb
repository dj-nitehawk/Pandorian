Public Class frmSongInfo

    Private Sub frmSongInfo_Click(sender As Object, e As EventArgs) Handles Me.Click, Panel1.Click, Label1.Click, Label2.Click, Label3.Click, track.Click, artist.Click, station.Click
        Me.Hide()
    End Sub

    Private Sub frmSongInfo_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then

            Dim screensize As Rectangle = Screen.GetWorkingArea(Me)
            Me.Location = New Point(screensize.Right - Size.Width, screensize.Bottom - Size.Height)

            timer.Enabled = True
            timer.Stop()
            timer.Start()
        Else
            timer.Enabled = False
        End If
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        Me.Hide()
    End Sub

End Class
