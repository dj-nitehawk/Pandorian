Public NotInheritable Class frmSongInfo
    Private Sub frmSongInfo_Click(sender As Object, e As EventArgs) Handles Me.Click, Label1.Click, Label2.Click, Label3.Click, artist.Click, track.Click, station.Click
        Me.Hide()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        timer.Enabled = False
        Me.Hide()
    End Sub
End Class
