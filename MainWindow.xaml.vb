Imports MySql.Data.MySqlClient
Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim conn As New MySqlConnection("host=127.0.0.1; user=root; password=''; database=grader;")
        Dim login As New Login
        Dim pwd As String
        Dim nam As String
        Dim mail As String

        pwd = Password.Password
        nam = Name.Text
        mail = Email.Text




        Try
            conn.Open()
            'MsgBox("Connection to database was established successfully")
            If pwd.Length < 8 Then
                MsgBox("password should be atleast 8 characters long")
            ElseIf String.IsNullOrWhiteSpace(nam) Then
                MsgBox("enter your name")
            ElseIf String.IsNullOrWhiteSpace(mail) Then
                MsgBox("Kindly enter your email address")
            Else
                pwd = PasswordHash.HashPassword(pwd)
                Dim command As New MySqlCommand("INSERT INTO teachers(name, email, password) VALUES('" & nam & "', '" & mail & "', '" & pwd & "')", conn)
                command.ExecuteNonQuery()
                MsgBox("Your registration was successful. You can now login")
                login.Show()
                Me.Hide()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim login As New Login
        login.Show()
        Me.Hide()
    End Sub
End Class
