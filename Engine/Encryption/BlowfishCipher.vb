Imports System.Text
Namespace Encryption
    Public Class BlowfishCipher

        Private Shared BFEnc As BlowFish
        Private Shared BFDec As BlowFish

        Sub New(PandoraPartner As Data.PandoraPartner)
            BFEnc = New BlowFish(HexString(PandoraPartner.EncryptCipher))
            BFDec = New BlowFish(HexString(PandoraPartner.DecryptCipher))
        End Sub

        Public Function EnCrypt(Data As String) As String
            Return BFEnc.Encrypt_ECB(Data)
        End Function

        Public Function DeCrypt(Data As String) As String
            Return BFDec.Decrypt_ECB(Data)
        End Function

        Private Shared Function HexString(Text As String) As String
            Dim bytes As Byte() = Encoding.Default.GetBytes(Text)
            Dim hexStr As String = BitConverter.ToString(bytes)
            Return hexStr.Replace("-", "")
        End Function
    End Class
End Namespace

