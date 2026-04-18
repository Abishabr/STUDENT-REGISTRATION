' ============================================================
' Student Model Class
' Represents a student record in the database
' ============================================================

''' <summary>
''' Data model representing a student record with all fields
''' matching the Students database table schema.
''' </summary>
Public Class Student

    ''' <summary>Auto-generated unique identifier for the student</summary>
    Public Property StudentID As Integer

    ''' <summary>Student's first name (required)</summary>
    Public Property FirstName As String

    ''' <summary>Student's last name (required)</summary>
    Public Property LastName As String

    ''' <summary>Student's gender: Male, Female, or Other (required)</summary>
    Public Property Gender As String

    ''' <summary>Student's date of birth (required)</summary>
    Public Property DateOfBirth As Date

    ''' <summary>Academic department the student is enrolled in (required)</summary>
    Public Property Department As String

    ''' <summary>Contact phone number (optional)</summary>
    Public Property PhoneNumber As String

    ''' <summary>Contact email address (optional)</summary>
    Public Property Email As String

    ''' <summary>Physical address (optional)</summary>
    Public Property Address As String

    ''' <summary>Date and time the student was registered in the system</summary>
    Public Property RegistrationDate As Date

    ''' <summary>
    ''' Creates a new Student instance with RegistrationDate set to current time.
    ''' </summary>
    Public Sub New()
        RegistrationDate = Date.Now
        FirstName = String.Empty
        LastName = String.Empty
        Gender = String.Empty
        Department = String.Empty
        PhoneNumber = String.Empty
        Email = String.Empty
        Address = String.Empty
    End Sub

End Class
