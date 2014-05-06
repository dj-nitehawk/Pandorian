Public Class frmBrowser

    Private Sub frmBrowser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Show()
        wBrowser = Nothing
        GC.Collect()
    End Sub

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not My.Settings.noProxy Then
            Dim p As New InternetProxy
            With p
                .Address = Decrypt(My.Settings.proxyAddress)
                .UserName = Decrypt(My.Settings.proyxUsername)
                .Password = Decrypt(My.Settings.proxyPassword)
            End With
            wBrowser.Proxy = p
        End If
        wBrowser.Goto(frmMain.GetStationURL)
    End Sub

    Private Sub wBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wBrowser.DocumentCompleted
        wBrowser.ScriptErrorsSuppressed = True
        wBrowser.Document.InvokeScript("eval", {"try { $.jPlayer.pause(); } catch (e) {};"})
    End Sub
End Class