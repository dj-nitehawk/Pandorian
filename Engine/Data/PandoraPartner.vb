Namespace Data

    Public Class PandoraPartner
        Inherits PandoraData

        Property API_URL As String
        Property Username As String
        Property Password As String
        Property DeviceID As String
        Property EncryptCipher As String
        Property DecryptCipher As String
        Property AccountType As Data.AccountType
        Property API_Version As String

        Public Sub New(AccountType As AccountType)
            Select Case AccountType
                Case Data.AccountType.FREE_USER
                    API_URL = "tuner.pandora.com"
                    Username = "android"
                    Password = "AC7IBG09A3DTSYM4R41UJWL07VLN8JI7"
                    DeviceID = "android-generic"
                    EncryptCipher = "6#26FRL$ZWD"
                    DecryptCipher = "R=U!LH$O2B#"
                Case Data.AccountType.PANDORA_ONE_USER
                    API_URL = "internal-tuner.pandora.com"
                    Username = "pandora one"
                    Password = "TVCKIBGS9AO9TSYLNNFUML0743LH82D"
                    DeviceID = "D01"
                    EncryptCipher = "2%3WCL*JU$MP]4"
                    DecryptCipher = "U#IO$RZPAB%VX2"
            End Select
            Me.AccountType = AccountType
            Me.API_Version = "5"
        End Sub

    End Class

End Namespace

