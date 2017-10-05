
Public Class frmSettings

    Private Qualities As New Dictionary(Of String, String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not chkNoProxy.Checked Then

            If String.IsNullOrEmpty(prxAddress.Text) Or prxAddress.Text = "http://server:port" Then
                MsgBox("Please specify a proxy server and port." + vbCrLf +
                                   "Or check 'Don't Use A Proxy'", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

        End If

        If Not String.IsNullOrEmpty(pnUsername.Text) And Not String.IsNullOrEmpty(pnPassword.Text) Then

            Settings.proxyAddress = Encrypt(prxAddress.Text)
            Settings.proxyPassword = Encrypt(prxPassword.Text)
            Settings.proyxUsername = Encrypt(prxUserName.Text)
            Settings.noProxy = chkNoProxy.Checked
            Settings.pandoraUsername = Encrypt(pnUsername.Text)
            Settings.pandoraPassword = Encrypt(pnPassword.Text)
            Settings.pandoraOne = chkPandoraOne.Checked
            Settings.noQmix = chkNoQMix.Checked
            Settings.noLiked = chkNoLiked.Checked
            Settings.noPrefetch = chkNoPrefetch.Checked
            Settings.enableBPMCounter = chkBPMCounter.Checked
            Settings.audioQuality = ddQuality.SelectedValue
            If Not String.IsNullOrEmpty(unlockCode.Text) And Not unlockCode.Text = "secret" Then
                Settings.unlockPassword = Encrypt(getMD5Hash(unlockCode.Text))
            End If
            Settings.SaveToRegistry()
            frmMain.ClearSession()

            Me.Hide()
            frmMain.Show()

            If frmMain.IsLoggedIn Then
                MsgBox("Settings saved. Restart app to use new settings.", MsgBoxStyle.Information)
            Else
                Try
                    frmMain.RunNow()
                Catch ex As Exception
                    MsgBox("SetingsRunNow: " + ex.Message, MsgBoxStyle.Critical)
                    frmMain.Hide()
                    Me.Show()
                End Try
            End If

        Else
            MsgBox("Pandora A/C details are required!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        prxAddress.Text = Decrypt(Settings.proxyAddress)
        prxUserName.Text = Decrypt(Settings.proyxUsername)
        prxPassword.Text = Decrypt(Settings.proxyPassword)
        chkNoProxy.Checked = Settings.noProxy
        pnUsername.Text = Decrypt(Settings.pandoraUsername)
        pnPassword.Text = Decrypt(Settings.pandoraPassword)
        chkPandoraOne.Checked = Settings.pandoraOne
        chkNoProxy.Checked = Settings.noProxy
        chkNoQMix.Checked = Settings.noQmix
        chkNoLiked.Checked = Settings.noLiked
        chkNoPrefetch.Checked = Settings.noPrefetch
        chkBPMCounter.Checked = Settings.enableBPMCounter
        If Not String.IsNullOrEmpty(Settings.unlockPassword) Then
            unlockCode.Text = "secret"
        End If

        PopulateQualityList(Settings.pandoraOne)

        For Each i As KeyValuePair(Of String, String) In ddQuality.Items
            If i.Value = Settings.audioQuality Then
                ddQuality.SelectedIndex = ddQuality.FindStringExact(i.Key)
                Exit For
            End If
        Next
    End Sub

    Private Sub btnPayPal_Click(sender As Object, e As EventArgs) Handles btnPayPal.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=JFH9VRVFAYZAW")
    End Sub

    Private Sub chkPandoraOne_CheckedChanged(sender As Object, e As EventArgs) Handles chkPandoraOne.CheckedChanged
        PopulateQualityList(chkPandoraOne.Checked)
    End Sub

    Private Sub PopulateQualityList(IsPandoraOne As Boolean)
        Qualities.Clear()
        If IsPandoraOne Then
            Qualities.Add("High (192k MP3)", "highQuality")
            Qualities.Add("Medium (64k AAC+)", "mediumQuality")
            Qualities.Add("Medium (128k MP3)", "128mp3")
        Else
            Qualities.Add("Medium (64k AAC+)", "mediumQuality")
            Qualities.Add("Medium (128k MP3)", "128mp3")
            Qualities.Add("Low (32k AAC+)", "lowQuality")
        End If
        ddQuality.ValueMember = "Value"
        ddQuality.DisplayMember = "Key"
        ddQuality.DataSource = New BindingSource(Qualities, Nothing)
    End Sub

    Private Sub chkNoProxy_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoProxy.CheckedChanged
        If chkNoProxy.Checked Then
            Settings.noProxy = True
            lblProxyAddre.Enabled = False
            lblProxyPass.Enabled = False
            lblProxyUser.Enabled = False
            prxAddress.Enabled = False
            prxPassword.Enabled = False
            prxUserName.Enabled = False
        Else
            Settings.noProxy = 0
            lblProxyAddre.Enabled = True
            lblProxyPass.Enabled = True
            lblProxyUser.Enabled = True
            prxAddress.Enabled = True
            prxPassword.Enabled = True
            prxUserName.Enabled = True
        End If
    End Sub

    Private Sub SelectText(sender As Object, e As EventArgs) Handles pnUsername.Click, pnPassword.Click, prxAddress.Click, prxUserName.Click, prxPassword.Click, unlockCode.Click
        Dim tb As TextBox = DirectCast(sender, TextBox)
        tb.SelectAll()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If frmMain.HasSettings() Then
            frmMain.Show()
            Me.Close()
        Else
            frmMain.Close()
        End If
    End Sub

    Private Sub ddQuality_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ddQuality.SelectionChangeCommitted
        Settings.audioQuality = ddQuality.SelectedValue
        Settings.SaveToRegistry()
    End Sub

    Private Sub noPrefetch_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoPrefetch.CheckedChanged
        Settings.noPrefetch = chkNoPrefetch.Checked
        Settings.SaveToRegistry()
    End Sub
End Class