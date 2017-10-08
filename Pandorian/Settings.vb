
Public Class Settings

    Private Shared Reg As RegistryStore = New RegistryStore()

    Shared Property PositionX As Integer
    Shared Property PositionY As Integer
    Shared Property audioQuality As String
    Shared Property downloadLocation As String
    Shared Property lastStationID As String
    Shared Property launchCount As Integer
    Shared Property audioVolume As Integer
    Shared Property noLiked As Boolean
    Shared Property noProxy As Boolean
    Shared Property noQmix As Boolean
    Shared Property noPrefetch As Boolean
    Shared Property pandoraOne As Boolean
    Shared Property enableBPMCounter As Boolean
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
            Dim val = p.GetValue(s, Nothing)
            If p.PropertyType Is GetType(Boolean) Then
                If val = True Then
                    val = 1
                Else
                    val = 0
                End If
            End If
            Reg.Write(p.Name, val)
        Next
    End Sub

    Shared Sub LoadFromRegistry()
        Dim s As New Settings()
        For Each p In s.GetType.GetProperties
            Dim Val = Reg.Read(p.Name)
            If p.PropertyType Is GetType(Boolean) Then
                If Val = 1 Then
                    Val = True
                Else
                    Val = False
                End If
            End If
            p.SetValue(s, Val, Nothing)
        Next
    End Sub

    Shared Function KeyCount() As Integer
        Return Reg.ValueCount()
    End Function
End Class
