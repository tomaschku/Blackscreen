Imports System.Runtime.InteropServices

Module special
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As SetWindowPosFlags) As Boolean
    End Function

    <Flags>
    Public Enum SetWindowPosFlags As UInteger
        SynchronousWindowPosition = &H4000
        DeferErase = &H2000
        DrawFrame = &H20
        FrameChanged = &H20
        HideWindow = &H80
        DoNotActivate = &H10
        DoNotCopyBits = &H100
        IgnoreMove = &H2
        DoNotChangeOwnerZOrder = &H200
        DoNotRedraw = &H8
        DoNotReposition = &H200
        DoNotSendChangingEvent = &H400
        IgnoreResize = &H1
        IgnoreZOrder = &H4
        ShowWindow = &H40
    End Enum
End Module

Public Class Form_Main
    Private Sub Form_Main_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Cursor.Hide()
    End Sub

    Private Sub Form_Main_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Cursor.Show()
    End Sub

    Private Sub Form_Main_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.Closing
        Timer_Hide.Stop()
        Cursor.Show()
        Dim window As IntPtr = FindWindow("Shell_traywnd", "")
        SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.ShowWindow)
    End Sub

    Private Sub Form_Main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.Alt And e.KeyCode = Keys.C) Then
            Timer_Hide.Stop()
            Dim window As IntPtr = FindWindow("Shell_traywnd", "")
            SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.ShowWindow)
            Application.Exit()
            e.Handled = True
        End If
        If (e.KeyCode = Keys.LWin Or e.KeyCode = Keys.RWin) Then
            Dim window As IntPtr = FindWindow("Shell_traywnd", "")
            SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.HideWindow)
            e.Handled = True
        End If
    End Sub

    Private Sub Timer_Hide_Tick(sender As Object, e As EventArgs) Handles Timer_Hide.Tick
        Cursor.Hide()
        Dim window As IntPtr = FindWindow("Shell_traywnd", "")
        SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, SetWindowPosFlags.HideWindow)
    End Sub
End Class
