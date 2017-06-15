
Public Class Settings

    Private Shared Reg As RegistryStore = New RegistryStore()

    Shared Property audioQuality As String
    Shared Property downloadLocation As String
    Shared Property lastStationID As String
    Shared Property launchCount As Integer
    Shared Property noLiked As Boolean
    Shared Property noProxy As Boolean
    Shared Property noQmix As Boolean
    Shared Property pandoraOne As Boolean
    Shared Property pandoraPassword As String
    Shared Property pandoraUsername As String
    Shared Property proxyAddress As String
    Shared Property proxyPassword As String
    Shared Property proyxUsername As String
    Shared Property unlockPassword As String
    Shared Property hkPlayPause As Integer
    Shared Property hkLike As Integer
    Shared Property hkDislike As Integer
    Shared Property hkSkip As Integer
    Shared Property hkBlock As Integer
    Shared Property hkShowHide As Integer
    Shared Property hkGlobalMenu As Integer
    Shared Property hkSleep As Integer
    Shared Property hkLock As Integer
    Shared Property hkVolDown As Integer
    Shared Property hkVolUp As Integer
    Shared Property hkModifier As Integer

    Shared Sub SaveToRegistry()
        Dim s As New Settings()
        For Each p In s.GetType.GetProperties
            Reg.Write(p.Name, p.GetValue(s, Nothing))
        Next
    End Sub

    Shared Sub LoadFromRegistry()
        Dim s As New Settings()
        For Each p In s.GetType.GetProperties
            Try
                p.SetValue(s, Reg.Read(p.Name), Nothing)
            Catch ex As Exception
                p.SetValue(s, Nothing, Nothing)
            End Try
        Next
    End Sub

    Shared Function KeyCount() As Integer
        Return Reg.ValueCount()
    End Function
End Class
