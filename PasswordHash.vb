Imports System.Text
Imports System.Security.Cryptography
Public Class PasswordHash
    Public Shared Function HashPassword(password As String) As String

        Dim passwordBytes As Byte() = Encoding.UTF8.GetBytes(password)


        Using sha256 As SHA256 = sha256.Create()

            Dim hashedBytes As Byte() = sha256.ComputeHash(passwordBytes)


            Dim stringBuilder As New StringBuilder()
            For Each hashedByte As Byte In hashedBytes
                stringBuilder.Append(hashedByte.ToString("x2"))
            Next


            Return stringBuilder.ToString()
        End Using
    End Function

    Public Shared Function VerifyPassword(password As String, hashedPassword As String) As Boolean
        Dim inputHashedPassword As String = HashPassword(password)


        Return inputHashedPassword = hashedPassword
    End Function
End Class
