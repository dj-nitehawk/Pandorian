Imports System.Text
Module Extensions
    Function Encrypt(ByVal ClearText As String) As String
        Return Convert.ToBase64String(Encoding.UTF8.GetBytes(ClearText))
    End Function
    Function Decrypt(ByVal EncryptedText As String) As String
        Try
            Return Encoding.UTF8.GetString(Convert.FromBase64String(EncryptedText))
        Catch ex As Exception
            Return EncryptedText
        End Try
    End Function
End Module
