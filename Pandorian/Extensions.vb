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

    Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = md5Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""
        Dim b As Byte

        For Each b In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function
End Module
