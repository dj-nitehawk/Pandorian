
Imports Microsoft.Win32

Public Class RegistryStore

    Private m_subKey As String = "SOFTWARE\" + Application.ProductName.ToUpper()
    Public Property SubKey() As String
        Get
            Return m_subKey
        End Get
        Set
            m_subKey = Value
        End Set
    End Property

    Private m_baseRegistryKey As RegistryKey = Registry.CurrentUser
    Public Property BaseRegistryKey() As RegistryKey
        Get
            Return m_baseRegistryKey
        End Get
        Set
            m_baseRegistryKey = Value
        End Set
    End Property

    Public Function Read(KeyName As String) As Object
        Dim rk As RegistryKey = m_baseRegistryKey
        Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
        If sk1 Is Nothing Then
            Return Nothing
        Else
            Try
                Return sk1.GetValue(KeyName)
            Catch e As Exception
                Return Nothing
            End Try
        End If
    End Function

    Public Function Write(KeyName As String, Value As Object) As Boolean
        Try
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.CreateSubKey(m_subKey)
            sk1.SetValue(KeyName, Value)
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function

    Public Function DeleteKey(KeyName As String) As Boolean
        Try
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.CreateSubKey(m_subKey)
            If sk1 Is Nothing Then
                Return True
            Else
                sk1.DeleteValue(KeyName)
            End If
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function

    Public Function DeleteSubKeyTree() As Boolean
        Try
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
            If sk1 IsNot Nothing Then
                rk.DeleteSubKeyTree(m_subKey)
            End If
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function

    Public Function SubKeyCount() As Integer
        Try
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
            If sk1 IsNot Nothing Then
                Return sk1.SubKeyCount
            Else
                Return 0
            End If
        Catch e As Exception
            Return 0
        End Try
    End Function

    Public Function ValueCount() As Integer
        Try
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
            If sk1 IsNot Nothing Then
                Return sk1.ValueCount
            Else
                Return 0
            End If
        Catch e As Exception
            Return 0
        End Try
    End Function

End Class

