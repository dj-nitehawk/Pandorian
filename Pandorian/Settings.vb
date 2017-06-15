Imports Pandorian.Utility.ModifyRegistry

Public Class Settings

    Private Shared Reg As RegistryStore = New RegistryStore()

    Shared Property audioQuality As String
    Shared Property downloadLocation As String
    Shared Property lastStationID As String
    Shared Property launchCount As Boolean
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

    Shared Sub SaveToRegistry()
        Reg.Write(NameOf(audioQuality), audioQuality)
        Reg.Write(NameOf(downloadLocation), downloadLocation)
        Reg.Write(NameOf(lastStationID), lastStationID)
        Reg.Write(NameOf(launchCount), launchCount)
        Reg.Write(NameOf(noLiked), noLiked)
        Reg.Write(NameOf(noProxy), noProxy)
        Reg.Write(NameOf(noQmix), noQmix)
        Reg.Write(NameOf(pandoraOne), pandoraOne)
        Reg.Write(NameOf(pandoraPassword), pandoraPassword)
        Reg.Write(NameOf(pandoraUsername), pandoraUsername)
        Reg.Write(NameOf(proxyAddress), proxyAddress)
        Reg.Write(NameOf(proxyPassword), proxyPassword)
        Reg.Write(NameOf(proyxUsername), proyxUsername)
        Reg.Write(NameOf(unlockPassword), unlockPassword)
    End Sub

    Shared Sub LoadFromRegistry()
        audioQuality = Reg.Read(NameOf(audioQuality))
        downloadLocation = Reg.Read(NameOf(downloadLocation))
        lastStationID = Reg.Read(NameOf(lastStationID))
        launchCount = Reg.Read(NameOf(launchCount))
        noLiked = Reg.Read(NameOf(noLiked))
        noProxy = Reg.Read(NameOf(noProxy))
        noQmix = Reg.Read(NameOf(noQmix))
        pandoraOne = Reg.Read(NameOf(pandoraOne))
        pandoraPassword = Reg.Read(NameOf(pandoraPassword))
        pandoraUsername = Reg.Read(NameOf(pandoraUsername))
        proxyAddress = Reg.Read(NameOf(proxyAddress))
        proxyPassword = Reg.Read(NameOf(proxyPassword))
        proyxUsername = Reg.Read(NameOf(proyxUsername))
        unlockPassword = Reg.Read(NameOf(unlockPassword))
    End Sub

    Shared Sub Write(Key As String, Value As Object)
        Reg.Write(Key, Value)
    End Sub

    Shared Function Read(Key As String) As Object
        Return Reg.Read(Key)
    End Function

    Shared Function KeyCount() As Integer
        Return Reg.ValueCount()
    End Function
End Class
