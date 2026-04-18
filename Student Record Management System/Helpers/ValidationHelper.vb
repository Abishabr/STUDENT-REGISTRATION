' ============================================================
' Validation Helper Module
' Provides reusable input validation methods for form fields.
' ============================================================

Imports System.Text.RegularExpressions

''' <summary>
''' Module containing shared validation methods for student
''' form data. Returns error messages or empty strings on success.
''' </summary>
Public Module ValidationHelper

    ''' <summary>
    ''' Validates that a required field is not empty or whitespace.
    ''' </summary>
    ''' <param name="value">The input value to validate</param>
    ''' <param name="fieldName">Display name of the field for error messages</param>
    ''' <returns>Error message if invalid, empty string if valid</returns>
    Public Function ValidateRequired(value As String, fieldName As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return $"{fieldName} is required."
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Validates email format using a standard regex pattern.
    ''' Empty values are considered valid (email is optional).
    ''' </summary>
    ''' <param name="email">The email address to validate</param>
    ''' <returns>Error message if invalid, empty string if valid</returns>
    Public Function ValidateEmail(email As String) As String
        If String.IsNullOrWhiteSpace(email) Then Return String.Empty
        Dim pattern As String = "^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$"
        If Not Regex.IsMatch(email.Trim(), pattern) Then
            Return "Invalid email format."
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Validates phone number format (7-15 digits with optional separators).
    ''' Empty values are considered valid (phone is optional).
    ''' </summary>
    ''' <param name="phone">The phone number to validate</param>
    ''' <returns>Error message if invalid, empty string if valid</returns>
    Public Function ValidatePhone(phone As String) As String
        If String.IsNullOrWhiteSpace(phone) Then Return String.Empty
        Dim pattern As String = "^[\d\s\-\+\(\)]{7,15}$"
        If Not Regex.IsMatch(phone.Trim(), pattern) Then
            Return "Invalid phone number (7-15 digits, may include +, -, spaces)."
        End If
        Return String.Empty
    End Function

End Module
