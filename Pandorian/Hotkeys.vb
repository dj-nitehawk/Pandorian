Public Class Hotkeys

    Private Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const WM_HOTKEY As Integer = &H312
    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum

    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal hotkeyID As Integer, ByVal triggerKey As Keys, ByVal modifier As KeyModifier)
        RegisterHotKey(sourceForm.Handle, hotkeyID, modifier, CInt(triggerKey))
    End Sub
    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form, hotkeyID As Integer)
        UnregisterHotKey(sourceForm.Handle, hotkeyID)
    End Sub
    
End Class