
Imports Microsoft.Win32

Namespace Utility.ModifyRegistry
    ''' <summary>
    ''' An useful class to read/write/delete/count registry keys
    ''' </summary>
    Public Class RegistryStore
        Private m_showError As Boolean = False
        ''' <summary>
        ''' A property to show or hide error messages 
        ''' (default = false)
        ''' </summary>
        Public Property ShowError() As Boolean
            Get
                Return m_showError
            End Get
            Set
                m_showError = Value
            End Set
        End Property

        Private m_subKey As String = "SOFTWARE\" + Application.ProductName.ToUpper()
        ''' <summary>
        ''' A property to set the SubKey value
        ''' (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        ''' </summary>
        Public Property SubKey() As String
            Get
                Return m_subKey
            End Get
            Set
                m_subKey = Value
            End Set
        End Property

        Private m_baseRegistryKey As RegistryKey = Registry.CurrentUser
        ''' <summary>
        ''' A property to set the BaseRegistryKey value.
        ''' (default = Registry.CurrentUser)
        ''' </summary>
        Public Property BaseRegistryKey() As RegistryKey
            Get
                Return m_baseRegistryKey
            End Get
            Set
                m_baseRegistryKey = Value
            End Set
        End Property


        ''' <summary>
        ''' To read a registry key.
        ''' input: KeyName (string)
        ''' output: value (object) 
        ''' </summary>
        Public Function Read(KeyName As String) As Object
            Dim rk As RegistryKey = m_baseRegistryKey
            Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
            If sk1 Is Nothing Then
                Return Nothing
            Else
                Try
                    Return sk1.GetValue(KeyName)
                Catch e As Exception
                    ShowErrorMessage(e, "Reading registry " + KeyName)
                    Return Nothing
                End Try
            End If
        End Function

        ''' <summary>
        ''' To write into a registry key.
        ''' input: KeyName (string) , Value (object)
        ''' output: true or false 
        ''' </summary>
        Public Function Write(KeyName As String, Value As Object) As Boolean
            Try
                Dim rk As RegistryKey = m_baseRegistryKey
                Dim sk1 As RegistryKey = rk.CreateSubKey(m_subKey)
                sk1.SetValue(KeyName, Value)
                Return True
            Catch e As Exception
                ShowErrorMessage(e, "Writing registry " + KeyName)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' To delete a registry key.
        ''' input: KeyName (string)
        ''' output: true or false 
        ''' </summary>
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
                ShowErrorMessage(e, Convert.ToString("Deleting SubKey ") & m_subKey)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' To delete a sub key and any child.
        ''' input: void
        ''' output: true or false 
        ''' </summary>
        Public Function DeleteSubKeyTree() As Boolean
            Try
                Dim rk As RegistryKey = m_baseRegistryKey
                Dim sk1 As RegistryKey = rk.OpenSubKey(m_subKey)
                If sk1 IsNot Nothing Then
                    rk.DeleteSubKeyTree(m_subKey)
                End If
                Return True
            Catch e As Exception
                ShowErrorMessage(e, Convert.ToString("Deleting SubKey ") & m_subKey)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Retrive the count of subkeys at the current key.
        ''' input: void
        ''' output: number of subkeys
        ''' </summary>
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
                ShowErrorMessage(e, Convert.ToString("Retriving subkeys of ") & m_subKey)
                Return 0
            End Try
        End Function


        ''' <summary>
        ''' Retrive the count of values in the key.
        ''' input: void
        ''' output: number of keys
        ''' </summary>
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
                ShowErrorMessage(e, Convert.ToString("Retriving keys of ") & m_subKey)
                Return 0
            End Try
        End Function

        Private Sub ShowErrorMessage(e As Exception, Title As String)
            If m_showError = True Then
                MessageBox.Show(e.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If
        End Sub
    End Class
End Namespace

