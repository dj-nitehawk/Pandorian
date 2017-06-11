Imports Microsoft.Win32
Imports Pandorian.Utility.ModifyRegistry


Public Class frmBrowser

    Private Sub frmBrowser_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmMain.Show()
        wBrowser = Nothing
        GC.Collect()
    End Sub

    Private Sub frmBrowser_Load(sender As Object, e As EventArgs) Handles Me.Load

        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                          "Pandorian.exe",
                          11001,
                          RegistryValueKind.DWord)

        Dim Settings As New RegistryStore

        If Not Settings.Read("noProxy") Then
            Dim p As New InternetProxy
            With p
                .Address = Decrypt(Settings.Read("proxyAddress"))
                .UserName = Decrypt(Settings.Read("proyxUsername"))
                .Password = Decrypt(Settings.Read("proxyPassword"))
            End With
            wBrowser.Proxy = p
        End If

        wBrowser.Goto(frmMain.GetStationURL)
        'wBrowser.Goto("https://pandora.com")
    End Sub

    Private Sub wBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wBrowser.DocumentCompleted
        'wBrowser.ScriptErrorsSuppressed = True
        'wBrowser.Document.InvokeScript("eval", {"try { $.jPlayer.pause(); } catch (e) {};"})
    End Sub
End Class