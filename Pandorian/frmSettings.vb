﻿Public Class frmSettings

    Private Qualities As New Dictionary(Of String, String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not chkNoProxy.Checked And prxAddress.Text = "" Then
            MsgBox("Please specify a proxy server and port." + vbCrLf +
                   "Or check 'Don't Use A Proxy'", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not pnUsername.Text = "" And Not pnPassword.Text = "" Then

            My.Settings.proxyAddress = prxAddress.Text
            My.Settings.proxyPassword = prxPassword.Text
            My.Settings.proyxUsername = prxUserName.Text
            My.Settings.noProxy = chkNoProxy.Checked
            My.Settings.pandoraUsername = pnUsername.Text
            My.Settings.pandoraPassword = pnPassword.Text
            My.Settings.pandoraOne = chkPandoraOne.Checked
            My.Settings.audioQuality = ddQuality.SelectedValue
            My.Settings.Save()

            Me.Hide()
            frmMain.Show()

            If frmMain.IsLoggedIn Then
                MsgBox("Settings saved. Restart app to use new settings", MsgBoxStyle.Information)
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
            MsgBox("All the fields are required.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        prxAddress.Text = My.Settings.proxyAddress
        prxUserName.Text = My.Settings.proyxUsername
        prxPassword.Text = My.Settings.proxyPassword
        chkNoProxy.Checked = My.Settings.noProxy
        pnUsername.Text = My.Settings.pandoraUsername
        pnPassword.Text = My.Settings.pandoraPassword
        chkPandoraOne.Checked = My.Settings.pandoraOne
        chkNoProxy.Checked = My.Settings.noProxy

        PopulateQualityList(My.Settings.pandoraOne)

        For Each i As KeyValuePair(Of String, String) In ddQuality.Items
            If i.Value = My.Settings.audioQuality Then
                ddQuality.SelectedIndex = ddQuality.FindStringExact(i.Key)
                Exit For
            End If
        Next
    End Sub

    Private Sub lnkPandora_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPandora.LinkClicked
        Process.Start("http://www.pandora.com")
    End Sub

    Private Sub lnkProxy_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkProxy.LinkClicked
        Process.Start("http://instantproxies.com/billing/aff.php?aff=120")
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
        Else
            Qualities.Add("Medium (64k AAC+)", "mediumQuality")
            Qualities.Add("Low (32k AAC+)", "lowQuality")
        End If
        ddQuality.ValueMember = "Value"
        ddQuality.DisplayMember = "Key"
        ddQuality.DataSource = New BindingSource(Qualities, Nothing)
    End Sub

    Private Sub chkNoProxy_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoProxy.CheckedChanged
        If chkNoProxy.Checked Then
            My.Settings.noProxy = True
            lblProxyAddre.Enabled = False
            lblProxyPass.Enabled = False
            lblProxyUser.Enabled = False
            prxAddress.Enabled = False
            prxPassword.Enabled = False
            prxUserName.Enabled = False
            lnkProxy.Enabled = False
        Else
            My.Settings.noProxy = False
            lblProxyAddre.Enabled = True
            lblProxyPass.Enabled = True
            lblProxyUser.Enabled = True
            prxAddress.Enabled = True
            prxPassword.Enabled = True
            prxUserName.Enabled = True
            lnkProxy.Enabled = True
        End If
    End Sub
End Class