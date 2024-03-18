Imports System.IO
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient


Public Class Dashboard
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim conn As New MySqlConnection("host=127.0.0.1; user=root; password=''; database=grader;")
        Dim regno As String
        Dim name As String
        Dim course As String
        Dim unit As String
        Dim semster As String
        Dim ct1 As Integer
        Dim ct2 As Integer
        Dim egx As Integer
        Dim grad As String




        'MsgBox(SessionManager.GetSession("email"))

        Try
            conn.Open()
            If String.IsNullOrEmpty(SNo.Text) OrElse
   String.IsNullOrEmpty(SName.Text) OrElse
   String.IsNullOrEmpty(Course1.Text) OrElse
   String.IsNullOrEmpty(SUnit.Text) OrElse
   String.IsNullOrEmpty(Semester.Text) OrElse
   String.IsNullOrEmpty(Cat1.Text) OrElse
   String.IsNullOrEmpty(Cat2.Text) OrElse
   String.IsNullOrEmpty(Exam.Text) Then
                MsgBox("All fields must be filled!")
            Else
                regno = SNo.Text
                name = SName.Text
                course = Course1.Text
                unit = SUnit.Text
                semster = Semester.Text
                ct1 = CInt(Cat1.Text)
                ct2 = CInt(Cat2.Text)
                egx = CInt(Exam.Text)

                Dim totalScore As Double = (((ct1 + ct2) / 60) * 30) + egx
                If totalScore >= 70 AndAlso totalScore <= 100 Then
                    grad = "A"
                ElseIf totalScore >= 60 AndAlso totalScore <= 69 Then
                    grad = "B"
                ElseIf totalScore >= 50 AndAlso totalScore <= 59 Then
                    grad = "C"
                ElseIf totalScore >= 40 AndAlso totalScore <= 49 Then
                    grad = "D"
                Else
                    grad = "Fail"
                End If
                Grade.Text = grad
                Dim query As String = "INSERT INTO students (registration_no, name, course, unit, semester, cat1, cat2, exam, grade)  VALUES ('" & regno & "', '" & name & "', '" & course & "', '" & unit & "', '" & semster & "', '" & ct1 & "', '" & ct2 & "', '" & egx & "', '" & grad & "')"


                Dim command As New MySqlCommand(query, conn)

                command.ExecuteNonQuery()
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

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim connectionString As String = "host=127.0.0.1; user=root; password=''; database=grader;"

        ' SQL query to fetch data from MySQL
        Dim query As String = "SELECT * FROM students"

        Try
            ' Open database connection
            Using connection As New MySqlConnection(connectionString)
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        ' Create a StreamWriter to write to CSV file
                        Using writer As New StreamWriter("output.csv")
                            ' Write column headers
                            For i As Integer = 0 To reader.FieldCount - 1
                                writer.Write("""" & reader.GetName(i) & """")
                                If i < reader.FieldCount - 1 Then
                                    writer.Write(",")
                                End If
                            Next
                            writer.WriteLine()

                            ' Write data
                            While reader.Read()
                                For i As Integer = 0 To reader.FieldCount - 1
                                    writer.Write("""" & reader.GetValue(i) & """")
                                    If i < reader.FieldCount - 1 Then
                                        writer.Write(",")
                                    End If
                                Next
                                writer.WriteLine()
                            End While
                        End Using
                    End Using
                End Using
            End Using

            MessageBox.Show("Export to CSV completed successfully. The file has been saved in Debug/ folder")
        Catch ex As Exception
            MessageBox.Show("Error exporting to CSV: " & ex.Message)
        End Try
    End Sub
End Class
