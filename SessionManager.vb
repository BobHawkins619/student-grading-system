Imports System.Collections.Generic

Public Class SessionManager
    Private Shared ReadOnly _sessionData As New Dictionary(Of String, Object)()

    Public Shared Sub SetSession(key As String, value As Object)
        _sessionData(key) = value
    End Sub

    Public Shared Function GetSession(key As String) As Object
        If _sessionData.ContainsKey(key) Then
            Return _sessionData(key)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Sub ClearSession()
        _sessionData.Clear()
    End Sub
End Class

