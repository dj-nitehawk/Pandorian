Imports System.Text

Namespace Encryption
    <Serializable()>
    Public Class BlowfishCipher

        Private EncCypher As String
        Private DecCypher As String
        Private Shared BFEnc As BlowFish
        Private Shared BFDec As BlowFish

        Sub New(PandoraPartner As Data.PandoraPartner)
            EncCypher = PandoraPartner.EncryptCipher
            DecCypher = PandoraPartner.DecryptCipher
            BFEnc = New BlowFish(HexString(EncCypher))
            BFDec = New BlowFish(HexString(DecCypher))
        End Sub

        Public Function EnCrypt(Data As String) As String
            If IsNothing(BFEnc) Then
                BFEnc = New BlowFish(HexString(EncCypher))
            End If
            Return BFEnc.Encrypt_ECB(Data)
        End Function

        Public Function DeCrypt(Data As String) As String
            If IsNothing(BFDec) Then
                BFDec = New BlowFish(HexString(DecCypher))
            End If
            Return BFDec.Decrypt_ECB(Data)
        End Function

        Private Function HexString(Text As String) As String
            Dim bytes As Byte() = Encoding.Default.GetBytes(Text)
            Dim hexStr As String = BitConverter.ToString(bytes)
            Return hexStr.Replace("-", "")
        End Function
    End Class
End Namespace

