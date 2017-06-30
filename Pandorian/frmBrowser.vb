Imports Microsoft.Win32

Public Class frmBrowser

    Private Sub frmBrowser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmMain.btnPlayPause_Click(Nothing, Nothing)
        frmMain.Show()
        wBrowser = Nothing
        GC.Collect()
    End Sub

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles Me.Load

        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                          "Pandorian.exe",
                          11001,
                          RegistryValueKind.DWord)

        If Not Settings.noProxy Then
            Dim p As New InternetProxy
            With p
                .Address = Decrypt(Settings.proxyAddress)
                .UserName = Decrypt(Settings.proyxUsername)
                .Password = Decrypt(Settings.proxyPassword)
            End With
            wBrowser.Proxy = p
        End If

        wBrowser.Goto(frmMain.GetStationURL)
    End Sub

    Private Sub wBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wBrowser.DocumentCompleted
        wBrowser.ScriptErrorsSuppressed = True
        'wBrowser.Document.InvokeScript("eval", {"try { $.jPlayer.pause(); } catch (e) {};"})
    End Sub
End Class