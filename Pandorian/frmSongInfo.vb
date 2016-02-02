Public Class frmSongInfo

    Private Sub frmSongInfo_Click(sender As Object, e As EventArgs) Handles Me.Click, Label1.Click, Label2.Click, Label3.Click, artist.Click, track.Click, station.Click
        Me.Hide()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        Me.Hide()
    End Sub

    Private Sub track_VisibleChanged(sender As Object, e As EventArgs) Handles track.VisibleChanged
        If Me.Visible Then

            Dim screensize As Rectangle = Screen.GetWorkingArea(Me)
            Me.Location = New Point((screensize.Right - Size.Width) - 10, (screensize.Bottom - Size.Height) - 10)

            timer.Enabled = True
            timer.Stop()
            timer.Start()
        Else
            timer.Enabled = False
        End If
    End Sub
End Class
