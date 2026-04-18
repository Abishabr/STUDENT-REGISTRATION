' ============================================================
' Database Helper Class
' Handles all SQLite database operations for the application.
' Uses Microsoft.Data.Sqlite for lightweight, file-based storage.
' ============================================================

Imports Microsoft.Data.Sqlite

''' <summary>
''' Provides all database CRUD operations and queries for the 
''' Student Record Management System using SQLite.
''' The database file is created automatically in the application directory.
''' </summary>
Public Class DatabaseHelper

    ' Database file stored alongside the executable
    Private Shared ReadOnly DbPath As String = IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "StudentRecords.db")
    Private Shared ReadOnly ConnStr As String = $"Data Source={DbPath}"

#Region "Database Initialization"

    ''' <summary>
    ''' Creates the Students table if it does not already exist.
    ''' Called once at application startup.
    ''' </summary>
    Public Shared Sub InitializeDatabase()
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "CREATE TABLE IF NOT EXISTS Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Gender TEXT NOT NULL,
                        DateOfBirth TEXT NOT NULL,
                        Department TEXT NOT NULL,
                        PhoneNumber TEXT DEFAULT '',
                        Email TEXT DEFAULT '',
                        Address TEXT DEFAULT '',
                        RegistrationDate TEXT NOT NULL
                    )", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Database initialization error: {ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Create Operations"

    ''' <summary>
    ''' Inserts a new student record into the database.
    ''' </summary>
    ''' <param name="student">Student object with all fields populated</param>
    ''' <returns>True if the record was inserted successfully</returns>
    Public Shared Function AddStudent(student As Student) As Boolean
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "INSERT INTO Students (FirstName, LastName, Gender, DateOfBirth, " &
                    "Department, PhoneNumber, Email, Address, RegistrationDate) " &
                    "VALUES (@fn, @ln, @g, @dob, @dept, @phone, @email, @addr, @regdate)", conn)

                    cmd.Parameters.AddWithValue("@fn", student.FirstName)
                    cmd.Parameters.AddWithValue("@ln", student.LastName)
                    cmd.Parameters.AddWithValue("@g", student.Gender)
                    cmd.Parameters.AddWithValue("@dob", student.DateOfBirth.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@dept", student.Department)
                    cmd.Parameters.AddWithValue("@phone", If(student.PhoneNumber, ""))
                    cmd.Parameters.AddWithValue("@email", If(student.Email, ""))
                    cmd.Parameters.AddWithValue("@addr", If(student.Address, ""))
                    cmd.Parameters.AddWithValue("@regdate", student.RegistrationDate.ToString("yyyy-MM-dd HH:mm:ss"))

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error adding student: {ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

#Region "Read Operations"

    ''' <summary>
    ''' Retrieves all student records ordered by ID descending (newest first).
    ''' </summary>
    ''' <returns>DataTable containing all student records</returns>
    Public Shared Function GetAllStudents() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "SELECT * FROM Students ORDER BY StudentID DESC", conn)
                    Using reader = cmd.ExecuteReader()
                        dt.Load(reader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error loading students: {ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' Searches students by keyword across StudentID, FirstName, LastName, and Department.
    ''' </summary>
    ''' <param name="query">Search keyword to match</param>
    ''' <returns>DataTable containing matching student records</returns>
    Public Shared Function SearchStudents(query As String) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "SELECT * FROM Students WHERE " &
                    "CAST(StudentID AS TEXT) LIKE @q OR " &
                    "FirstName LIKE @q OR " &
                    "LastName LIKE @q OR " &
                    "Department LIKE @q " &
                    "ORDER BY StudentID DESC", conn)

                    cmd.Parameters.AddWithValue("@q", $"%{query}%")
                    Using reader = cmd.ExecuteReader()
                        dt.Load(reader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Silently handle search errors during rapid typing
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' Gets the total count of all student records.
    ''' </summary>
    Public Shared Function GetTotalCount() As Integer
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand("SELECT COUNT(*) FROM Students", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Gets the count of students filtered by gender.
    ''' </summary>
    ''' <param name="gender">Gender value to filter by (e.g., "Male", "Female")</param>
    Public Shared Function GetGenderCount(gender As String) As Integer
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "SELECT COUNT(*) FROM Students WHERE Gender = @g", conn)
                    cmd.Parameters.AddWithValue("@g", gender)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region

#Region "Update Operations"

    ''' <summary>
    ''' Updates an existing student record identified by StudentID.
    ''' </summary>
    ''' <param name="student">Student object with updated fields and valid StudentID</param>
    ''' <returns>True if the record was updated successfully</returns>
    Public Shared Function UpdateStudent(student As Student) As Boolean
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "UPDATE Students SET FirstName=@fn, LastName=@ln, Gender=@g, " &
                    "DateOfBirth=@dob, Department=@dept, PhoneNumber=@phone, " &
                    "Email=@email, Address=@addr WHERE StudentID=@id", conn)

                    cmd.Parameters.AddWithValue("@id", student.StudentID)
                    cmd.Parameters.AddWithValue("@fn", student.FirstName)
                    cmd.Parameters.AddWithValue("@ln", student.LastName)
                    cmd.Parameters.AddWithValue("@g", student.Gender)
                    cmd.Parameters.AddWithValue("@dob", student.DateOfBirth.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@dept", student.Department)
                    cmd.Parameters.AddWithValue("@phone", If(student.PhoneNumber, ""))
                    cmd.Parameters.AddWithValue("@email", If(student.Email, ""))
                    cmd.Parameters.AddWithValue("@addr", If(student.Address, ""))

                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error updating student: {ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

#Region "Delete Operations"

    ''' <summary>
    ''' Deletes a student record by StudentID.
    ''' </summary>
    ''' <param name="studentId">The ID of the student to delete</param>
    ''' <returns>True if the record was deleted successfully</returns>
    Public Shared Function DeleteStudent(studentId As Integer) As Boolean
        Try
            Using conn As New SqliteConnection(ConnStr)
                conn.Open()
                Using cmd As New SqliteCommand(
                    "DELETE FROM Students WHERE StudentID = @id", conn)
                    cmd.Parameters.AddWithValue("@id", studentId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error deleting student: {ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

End Class
