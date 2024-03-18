Imports System.Data
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI

Public Class Login
    Dim signup As New MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        signup.Show()
        Me.Hide()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim conn As New MySqlConnection("host=127.0.0.1; user=root; password=''; database=grader;")
        Dim login As New Login
        Dim datatable As New DataTable()
        Dim pawd As String
        Dim mail As String

        pawd = Pwd.Password
        mail = Email2.Text




        Try
            conn.Open()
            'MsgBox("Connection to database was established successfully")
            If pawd.Length < 8 Then
                MsgBox("password should be atleast 8 characters long")
            ElseIf String.IsNullOrWhiteSpace(mail) Then
                MsgBox("Kindly enter your email address")
            Else
                pawd = PasswordHash.HashPassword(pawd)
                Dim command As New MySqlCommand("SELECT * FROM teachers WHERE email = '" & mail & "' AND password = '" & pawd & "'", conn)
                Dim reader As MySqlDataReader = command.ExecuteReader()
                Dim dashborad As New Dashboard
                If reader.Read() Then

                    'Dim sessionManager As SessionManager
                    SessionManager.SetSession("email", reader("email").ToString())
                    dashborad.Show()
                    Me.Hide()
                Else
                    MsgBox("Invalid credentials. Check your password and email")
                End If
                reader.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
