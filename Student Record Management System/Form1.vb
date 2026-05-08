' ============================================================
' Form1.vb - Main Application Form
' Student Record Management System
'
' This is the primary form containing:
'   - Professional dashboard with statistics cards
'   - Separate pages for Add/Edit and View/Manage records
'   - Complete student CRUD operations
'   - Real-time search functionality
'   - Modern styled DataGridView
'   - Export to CSV and Print Report features
'   - Keyboard shortcuts and status bar
'
' Architecture: All UI controls are created programmatically
' for cleaner code and easier maintenance.
' ============================================================

Imports System.Drawing.Drawing2D

Public Class Form1

#Region "Theme Colors - Professional Color Palette"
    Private ClrPrimaryDark As Color = Color.FromArgb(25, 42, 86)
    Private ClrPrimary As Color = Color.FromArgb(41, 128, 185)
    Private ClrPrimaryLight As Color = Color.FromArgb(52, 152, 219)
    Private ClrAccent As Color = Color.FromArgb(0, 188, 212)
    Private ClrSuccess As Color = Color.FromArgb(39, 174, 96)
    Private ClrDanger As Color = Color.FromArgb(231, 76, 60)
    Private ClrWarning As Color = Color.FromArgb(243, 156, 18)
    Private ClrBgLight As Color = Color.FromArgb(240, 243, 247)
    Private ClrCardBg As Color = Color.White
    Private ClrTextDark As Color = Color.FromArgb(44, 62, 80)
    Private ClrTextMuted As Color = Color.FromArgb(127, 140, 141)
    Private ClrNavBg As Color = Color.FromArgb(30, 39, 73)
    Private ClrNavHover As Color = Color.FromArgb(45, 60, 110)
#End Region

#Region "Control Declarations"
    ' --- Main Layout Panels ---
    Private pnlHeader As Panel
    Private pnlNav As Panel
    Private pnlContent As Panel

    ' --- Switchable Content Panels (Pages) ---
    Private pnlDashboard As Panel
    Private pnlAddEditStudent As Panel
    Private pnlViewStudents As Panel

    ' --- Navigation Buttons ---
    Private btnNavDashboard As Button
    Private btnNavAdd As Button
    Private btnNavView As Button
    Private btnNavExport As Button
    Private btnNavPrint As Button
    Private btnNavExit As Button

    ' --- Dashboard Statistics Labels ---
    Private lblTotalCount As Label
    Private lblMaleCount As Label
    Private lblFemaleCount As Label

    ' --- Student Form Input Controls ---
    Private txtFirstName As TextBox
    Private txtLastName As TextBox
    Private cboGender As ComboBox
    Private dtpDateOfBirth As DateTimePicker
    Private cboDepartment As ComboBox
    Private txtPhone As TextBox
    Private txtEmail As TextBox
    Private txtAddress As TextBox

    ' --- Header Labels in AddEdit ---
    Private lblAddEditTitle As Label
    Private lblSelectedID As Label

    ' --- Action Buttons in AddEdit panel ---
    Private btnSaveMode As Button
    Private btnCancelMode As Button

    ' --- Action Buttons in View panel ---
    Private btnEditSelected As Button
    Private btnDeleteSelected As Button
    Private txtSearch As TextBox
    Private lblSearchPlaceholder As Label
    Private dgvStudents As DataGridView

    ' --- Status Strip ---
    Private mainStatusStrip As StatusStrip
    Private statusLabel As ToolStripStatusLabel

    ' --- Error Provider ---
    Private formErrorProvider As ErrorProvider
#End Region

#Region "State Variables"
    Private isEditMode As Boolean = False
    Private selectedStudentID As Integer = -1
    Private activeNavButton As Button = Nothing
    Private printPageIndex As Integer = 0
#End Region

#Region "Constructor & Form Events"
    Public Sub New()
        InitializeComponent()
        SetupEventHandlers()
        ' Start invisible — login form will show first, then we restore visibility
        Me.Opacity = 0
    End Sub

    Private Sub SetupEventHandlers()
        AddHandler btnNavDashboard.Click, Sub(s, ev) ShowDashboard()
        AddHandler btnNavAdd.Click, Sub(s, ev) ShowAddStudent()
        AddHandler btnNavView.Click, Sub(s, ev) ShowViewStudents()
        AddHandler btnNavExport.Click, Sub(s, ev) ExportToCSV()
        AddHandler btnNavPrint.Click, Sub(s, ev) PrintReport()
        AddHandler btnNavExit.Click, Sub(s, ev) Me.Close()

        ' Restrict phone number to digits only (and backspace)
        AddHandler txtPhone.KeyPress, Sub(s, ev)
            If Not Char.IsDigit(ev.KeyChar) AndAlso Not Char.IsControl(ev.KeyChar) Then
                ev.Handled = True
            End If
        End Sub

        AddHandler btnSaveMode.Click, Sub(s, ev)
            If isEditMode Then UpdateStudentRecord() Else AddStudent()
        End Sub
        AddHandler btnCancelMode.Click, Sub(s, ev)
            If isEditMode Then ShowViewStudents() Else ClearFields()
        End Sub

        AddHandler btnEditSelected.Click, Sub(s, ev) EditSelectedStudent()
        AddHandler btnDeleteSelected.Click, Sub(s, ev) DeleteSelectedStudent()
        AddHandler txtSearch.TextChanged, Sub(s, ev)
            If txtSearch.Text.Length > 0 Then
                lblSearchPlaceholder.Visible = False
            Else
                lblSearchPlaceholder.Visible = True
            End If
            SearchStudents()
        End Sub
        AddHandler txtSearch.GotFocus, Sub(s, ev)
            If txtSearch.Text.Length = 0 Then lblSearchPlaceholder.Visible = False
        End Sub
        AddHandler txtSearch.LostFocus, Sub(s, ev)
            If txtSearch.Text.Length = 0 Then lblSearchPlaceholder.Visible = True
        End Sub
        AddHandler lblSearchPlaceholder.Click, Sub(s, ev) txtSearch.Focus()
        AddHandler lblSearchIcon.Click, Sub(s, ev) txtSearch.Focus()

        AddHandler dgvStudents.CellDoubleClick, Sub(s, ev)
            If ev.RowIndex >= 0 Then EditSelectedStudent()
        End Sub
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DatabaseHelper.InitializeDatabase()
            ShowDashboard()
        Catch ex As Exception
            MessageBox.Show($"Error initializing application: {ex.Message}", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private loginShown As Boolean = False

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' Only show login once (Shown can fire again after Hide/Show)
        If loginShown Then Return
        loginShown = True

        Try
            ' Form is invisible (Opacity=0) — show login dialog
            Dim loginForm As New LoginForm()
            Dim loginResult = loginForm.ShowDialog()
            Dim authenticatedEmail = loginForm.AuthenticatedUser
            loginForm.Dispose()

            If loginResult <> DialogResult.OK Then
                ' User cancelled login — exit without confirmation prompt
                RemoveHandler Me.FormClosing, AddressOf Form1_FormClosing
                Application.Exit()
                Return
            End If

            ' Store authenticated user and show personalized welcome
            My.Application.LoggedInUser = authenticatedEmail
            Dim fullName = DatabaseHelper.GetUserFullName(authenticatedEmail)

            ' Update the hero title on the dashboard
            For Each ctrl As Control In pnlDashboard.Controls
                If TypeOf ctrl Is Panel Then
                    For Each child As Control In ctrl.Controls
                        If TypeOf child Is Label AndAlso child.Name = "lblHeroTitle" Then
                            child.Text = $"Welcome Back, {fullName}"
                        End If
                    Next
                End If
            Next

            ' Now make the main form visible
            Me.Opacity = 1
            UpdateStatus($"Logged in as {fullName}")
        Catch ex As Exception
            MessageBox.Show($"Error during login: {ex.Message}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.S
                    If pnlAddEditStudent.Visible Then
                        If isEditMode Then UpdateStudentRecord() Else AddStudent()
                    Else
                        ShowAddStudent()
                    End If
                    e.SuppressKeyPress = True
                Case Keys.U
                    ShowViewStudents()
                    e.SuppressKeyPress = True
                Case Keys.F
                    ShowViewStudents()
                    txtSearch.Focus()
                    txtSearch.SelectAll()
                    e.SuppressKeyPress = True
                Case Keys.N
                    ShowAddStudent()
                    e.SuppressKeyPress = True
                Case Keys.E
                    ExportToCSV()
                    e.SuppressKeyPress = True
                Case Keys.P
                    PrintReport()
                    e.SuppressKeyPress = True
            End Select
        End If
    End Sub
#End Region

#Region "Navigation - Page Routing"
    Private Sub HideAllPanels()
        pnlDashboard.Visible = False
        pnlAddEditStudent.Visible = False
        pnlViewStudents.Visible = False
    End Sub

    Private Sub ShowDashboard()
        HideAllPanels()
        pnlDashboard.Visible = True
        pnlDashboard.BringToFront()
        UpdateDashboardStats()
        SetActiveNavButton(btnNavDashboard)
        UpdateStatus("Dashboard view")
    End Sub

    Private Sub ShowViewStudents()
        HideAllPanels()
        pnlViewStudents.Visible = True
        pnlViewStudents.BringToFront()
        LoadStudents()
        SetActiveNavButton(btnNavView)
        UpdateStatus("Viewing all student records")
    End Sub

    Private Sub ShowAddStudent()
        HideAllPanels()
        pnlAddEditStudent.Visible = True
        pnlAddEditStudent.BringToFront()
        
        isEditMode = False
        selectedStudentID = -1
        lblAddEditTitle.Text = "  Add New Student"
        lblSelectedID.Text = "New Record"
        lblSelectedID.ForeColor = ClrAccent
        
        btnSaveMode.Text = "  Save Student"
        btnSaveMode.BackColor = ClrSuccess
        btnCancelMode.Text = "  Clear Fields"
        
        ClearFields()
        txtFirstName.Focus()
        SetActiveNavButton(btnNavAdd)
        UpdateStatus("Ready to add a new student")
    End Sub

    Private Sub ShowEditStudent()
        HideAllPanels()
        pnlAddEditStudent.Visible = True
        pnlAddEditStudent.BringToFront()
        
        isEditMode = True
        lblAddEditTitle.Text = "  Edit Student Record"
        lblSelectedID.Text = $"Editing ID: {selectedStudentID}"
        lblSelectedID.ForeColor = ClrWarning
        
        btnSaveMode.Text = "  Update Student"
        btnSaveMode.BackColor = ClrPrimary
        btnCancelMode.Text = "  Cancel Edit"
        
        txtFirstName.Focus()
        SetActiveNavButton(btnNavView) ' Stay highlighted on View because we came from there
        UpdateStatus($"Editing student ID: {selectedStudentID}")
    End Sub

    Private Sub SetActiveNavButton(btn As Button)
        For Each ctrl As Control In pnlNav.Controls
            If TypeOf ctrl Is Button Then
                Dim navBtn = DirectCast(ctrl, Button)
                navBtn.BackColor = ClrNavBg
                If navBtn Is btnNavExit Then
                    navBtn.ForeColor = Color.FromArgb(231, 76, 60)
                Else
                    navBtn.ForeColor = Color.FromArgb(189, 195, 199)
                End If
            End If
        Next
        If btn IsNot Nothing Then
            btn.BackColor = ClrPrimary
            btn.ForeColor = Color.White
            activeNavButton = btn
        End If
    End Sub
#End Region

#Region "Data Operations - CRUD"
    Private Sub LoadStudents()
        Try
            dgvStudents.DataSource = DatabaseHelper.GetAllStudents()
            FormatDataGridView()
            UpdateDashboardStats()
        Catch ex As Exception
            MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddStudent()
        If Not ValidateForm() Then Return

        Dim student As New Student() With {
            .FirstName = txtFirstName.Text.Trim(), .LastName = txtLastName.Text.Trim(),
            .Gender = cboGender.SelectedItem.ToString(), .DateOfBirth = dtpDateOfBirth.Value.Date,
            .Department = cboDepartment.SelectedItem.ToString(), .PhoneNumber = txtPhone.Text.Trim(),
            .Email = txtEmail.Text.Trim(), .Address = txtAddress.Text.Trim(), .RegistrationDate = DateTime.Now
        }

        If DatabaseHelper.AddStudent(student) Then
            MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            UpdateStatus("New student record added successfully")
        End If
    End Sub

    Private Sub EditSelectedStudent()
        If dgvStudents.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student from the list to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row = dgvStudents.SelectedRows(0)
        selectedStudentID = Convert.ToInt32(row.Cells("StudentID").Value)

        txtFirstName.Text = row.Cells("FirstName").Value?.ToString()
        txtLastName.Text = row.Cells("LastName").Value?.ToString()
        
        Dim genderVal = row.Cells("Gender").Value?.ToString()
        If cboGender.Items.Contains(genderVal) Then cboGender.SelectedItem = genderVal

        Dim dobStr = row.Cells("DateOfBirth").Value?.ToString()
        If Not String.IsNullOrEmpty(dobStr) Then
            Dim parsedDate As Date
            If Date.TryParse(dobStr, parsedDate) Then dtpDateOfBirth.Value = parsedDate
        End If

        Dim deptVal = row.Cells("Department").Value?.ToString()
        If cboDepartment.Items.Contains(deptVal) Then cboDepartment.SelectedItem = deptVal

        txtPhone.Text = row.Cells("PhoneNumber").Value?.ToString()
        txtEmail.Text = row.Cells("Email").Value?.ToString()
        txtAddress.Text = row.Cells("Address").Value?.ToString()

        ' Switch to the Add/Edit page in Edit mode
        ShowEditStudent()
    End Sub

    Private Sub UpdateStudentRecord()
        If selectedStudentID = -1 Then Return
        If Not ValidateForm() Then Return

        If MessageBox.Show("Are you sure you want to update this student record?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Dim student As New Student() With {
            .StudentID = selectedStudentID, .FirstName = txtFirstName.Text.Trim(),
            .LastName = txtLastName.Text.Trim(), .Gender = cboGender.SelectedItem.ToString(),
            .DateOfBirth = dtpDateOfBirth.Value.Date, .Department = cboDepartment.SelectedItem.ToString(),
            .PhoneNumber = txtPhone.Text.Trim(), .Email = txtEmail.Text.Trim(), .Address = txtAddress.Text.Trim()
        }

        If DatabaseHelper.UpdateStudent(student) Then
            MessageBox.Show("Student record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            selectedStudentID = -1
            ShowViewStudents()
            UpdateStatus($"Student ID {student.StudentID} updated successfully")
        End If
    End Sub

    Private Sub DeleteSelectedStudent()
        If dgvStudents.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row = dgvStudents.SelectedRows(0)
        Dim idToDelete = Convert.ToInt32(row.Cells("StudentID").Value)

        If MessageBox.Show("Are you sure you want to permanently delete this student record?" & vbCrLf & "This action cannot be undone.", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then Return

        If DatabaseHelper.DeleteStudent(idToDelete) Then
            MessageBox.Show("Student record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStudents()
            UpdateStatus($"Student ID {idToDelete} deleted successfully")
        End If
    End Sub

    Private Sub SearchStudents()
        Try
            Dim query = txtSearch.Text.Trim()
            If String.IsNullOrEmpty(query) Then
                dgvStudents.DataSource = DatabaseHelper.GetAllStudents()
            Else
                dgvStudents.DataSource = DatabaseHelper.SearchStudents(query)
            End If
            FormatDataGridView()
            UpdateStatus($"Found {dgvStudents.Rows.Count} matching records")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateDashboardStats()
        Try
            If lblTotalCount IsNot Nothing Then lblTotalCount.Text = DatabaseHelper.GetTotalCount().ToString()
            If lblMaleCount IsNot Nothing Then lblMaleCount.Text = DatabaseHelper.GetGenderCount("Male").ToString()
            If lblFemaleCount IsNot Nothing Then lblFemaleCount.Text = DatabaseHelper.GetGenderCount("Female").ToString()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Form Helpers"
    Private Function ValidateForm() As Boolean
        formErrorProvider.Clear()
        Dim isValid As Boolean = True

        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            formErrorProvider.SetError(txtFirstName, "First Name is required")
            isValid = False
        End If
        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            formErrorProvider.SetError(txtLastName, "Last Name is required")
            isValid = False
        End If
        If cboGender.SelectedIndex = -1 Then
            formErrorProvider.SetError(cboGender, "Please select a gender")
            isValid = False
        End If
        If cboDepartment.SelectedIndex = -1 Then
            formErrorProvider.SetError(cboDepartment, "Please select a department")
            isValid = False
        End If

        ' Email is required and must be valid format
        If String.IsNullOrWhiteSpace(txtEmail.Text) Then
            formErrorProvider.SetError(txtEmail, "Email is required")
            isValid = False
        Else
            Dim emailError = ValidationHelper.ValidateEmail(txtEmail.Text)
            If Not String.IsNullOrEmpty(emailError) Then
                formErrorProvider.SetError(txtEmail, emailError)
                isValid = False
            Else
                ' Check if email already exists in database
                Dim excludeId = If(isEditMode, selectedStudentID, -1)
                If DatabaseHelper.IsEmailExists(txtEmail.Text.Trim(), excludeId) Then
                    formErrorProvider.SetError(txtEmail, "This email is already registered")
                    isValid = False
                End If
            End If
        End If

        ' Phone number must be digits only and valid format
        If Not String.IsNullOrWhiteSpace(txtPhone.Text) Then
            Dim phoneError = ValidationHelper.ValidatePhone(txtPhone.Text)
            If Not String.IsNullOrEmpty(phoneError) Then
                formErrorProvider.SetError(txtPhone, phoneError)
                isValid = False
            Else
                ' Check if phone already exists in database
                Dim excludeId = If(isEditMode, selectedStudentID, -1)
                If DatabaseHelper.IsPhoneExists(txtPhone.Text.Trim(), excludeId) Then
                    formErrorProvider.SetError(txtPhone, "This phone number is already registered")
                    isValid = False
                End If
            End If
        End If

        If Not isValid Then UpdateStatus("Please correct the highlighted validation errors")
        Return isValid
    End Function

    Private Sub ClearFields()
        txtFirstName.Clear()
        txtLastName.Clear()
        cboGender.SelectedIndex = -1
        dtpDateOfBirth.Value = Date.Now
        cboDepartment.SelectedIndex = -1
        txtPhone.Clear()
        txtEmail.Clear()
        txtAddress.Clear()
        formErrorProvider.Clear()
        
        If Not isEditMode AndAlso lblSelectedID IsNot Nothing Then
            lblSelectedID.Text = "New Record"
        End If
    End Sub

    Private Sub FormatDataGridView()
        If dgvStudents.Columns.Count = 0 Then Return
        Try
            If dgvStudents.Columns.Contains("StudentID") Then
                dgvStudents.Columns("StudentID").HeaderText = "ID"
                dgvStudents.Columns("StudentID").Width = 55
            End If
            If dgvStudents.Columns.Contains("FirstName") Then dgvStudents.Columns("FirstName").HeaderText = "First Name"
            If dgvStudents.Columns.Contains("LastName") Then dgvStudents.Columns("LastName").HeaderText = "Last Name"
            If dgvStudents.Columns.Contains("DateOfBirth") Then dgvStudents.Columns("DateOfBirth").HeaderText = "Date of Birth"
            If dgvStudents.Columns.Contains("PhoneNumber") Then dgvStudents.Columns("PhoneNumber").HeaderText = "Phone"
            If dgvStudents.Columns.Contains("RegistrationDate") Then dgvStudents.Columns("RegistrationDate").HeaderText = "Registered"
            If dgvStudents.Columns.Contains("Address") Then dgvStudents.Columns("Address").Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateStatus(message As String)
        If statusLabel IsNot Nothing Then statusLabel.Text = $"  {message}  |  {DateTime.Now:HH:mm:ss}"
    End Sub
#End Region

#Region "Export & Print"
    Private Sub ExportToCSV()
        Try
            If Not pnlViewStudents.Visible Then ShowViewStudents()
            If dgvStudents.Rows.Count = 0 Then
                MessageBox.Show("No records to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog() With {.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*", .FileName = $"Students_{DateTime.Now:yyyyMMdd_HHmmss}.csv", .Title ="Export Student Records"}
                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim sb As New System.Text.StringBuilder()
                    Dim headers As New List(Of String)
                    For Each col As DataGridViewColumn In dgvStudents.Columns
                        If col.Visible Then headers.Add($"""{col.HeaderText}""")
                    Next
                    sb.AppendLine(String.Join(",", headers))

                    For Each row As DataGridViewRow In dgvStudents.Rows
                        Dim cells As New List(Of String)
                        For Each col As DataGridViewColumn In dgvStudents.Columns
                            If col.Visible Then cells.Add($"""{If(row.Cells(col.Index).Value?.ToString(), "").Replace("""", """""")}""")
                        Next
                        sb.AppendLine(String.Join(",", cells))
                    Next
                    IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8)
                    MessageBox.Show("Exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Export error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintReport()
        Try
            If Not pnlViewStudents.Visible Then ShowViewStudents()
            If dgvStudents.Rows.Count = 0 Then Return

            printPageIndex = 0
            Dim printDoc As New Printing.PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintDoc_PrintPage

            Using ppd As New PrintPreviewDialog() With {.Document = printDoc, .Width = 950, .Height = 650}
                ppd.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show($"Print error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        Dim g = e.Graphics
        Dim yPos As Single = 50
        Dim leftMargin As Single = 50
        Dim pageWidth As Single = e.PageBounds.Width - 100

        If printPageIndex = 0 Then
            Using titleFont As New Font("Segoe UI", 16, FontStyle.Bold)
                g.DrawString("Student Record Management System", titleFont, Brushes.DarkSlateBlue, leftMargin, yPos)
            End Using
            yPos += 30
            Using subFont As New Font("Segoe UI", 11)
                g.DrawString("Student Report", subFont, Brushes.Gray, leftMargin, yPos)
            End Using
            yPos += 25
            Using dateFont As New Font("Segoe UI", 9)
                g.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm} | Total: {dgvStudents.Rows.Count}", dateFont, Brushes.Gray, leftMargin, yPos)
            End Using
            yPos += 25
            Using pen As New Pen(Color.DarkSlateBlue, 2)
                g.DrawLine(pen, leftMargin, yPos, leftMargin + pageWidth, yPos)
            End Using
            yPos += 15
        End If

        Dim cols = {"ID", "First Name", "Last Name", "Gender", "Department", "Phone", "Email"}
        Dim colWidths = {45, 95, 95, 60, 130, 95, 160}

        Using headerFont As New Font("Segoe UI Semibold", 9)
            Using headerBrush As New SolidBrush(Color.DarkSlateBlue)
                Dim xPos As Single = leftMargin
                For i = 0 To cols.Length - 1
                    g.DrawString(cols(i), headerFont, headerBrush, xPos, yPos)
                    xPos += colWidths(i)
                Next
            End Using
        End Using
        yPos += 22

        Using pen As New Pen(Color.LightGray, 1)
            g.DrawLine(pen, leftMargin, yPos, leftMargin + pageWidth, yPos)
        End Using
        yPos += 5

        Using dataFont As New Font("Segoe UI", 8.5F)
            While printPageIndex < dgvStudents.Rows.Count
                If yPos > e.PageBounds.Height - 60 Then
                    e.HasMorePages = True
                    Return
                End If

                Dim row = dgvStudents.Rows(printPageIndex)
                Dim xPos As Single = leftMargin
                Dim values = {row.Cells("StudentID").Value?.ToString(), row.Cells("FirstName").Value?.ToString(), row.Cells("LastName").Value?.ToString(), row.Cells("Gender").Value?.ToString(), row.Cells("Department").Value?.ToString(), row.Cells("PhoneNumber").Value?.ToString(), row.Cells("Email").Value?.ToString()}

                For i = 0 To cols.Length - 1
                    g.DrawString(If(values(i), ""), dataFont, Brushes.Black, xPos, yPos)
                    xPos += colWidths(i)
                Next
                yPos += 18
                printPageIndex += 1
            End While
        End Using

        Using footerFont As New Font("Segoe UI", 8)
            g.DrawString($"Total Students: {dgvStudents.Rows.Count} | Page", footerFont, Brushes.Gray, leftMargin, e.PageBounds.Height - 40)
        End Using

        printPageIndex = 0
        e.HasMorePages = False
    End Sub
#End Region

#Region "UI Helper Factories - Control Creation Methods"
    Private Function CreateNavButton(text As String) As Button
        Dim btn As New Button() With {.Text = text, .TextAlign = ContentAlignment.MiddleLeft, .Dock = DockStyle.Top, .Height = 48, .FlatStyle = FlatStyle.Flat, .BackColor = ClrNavBg, .ForeColor = Color.FromArgb(189, 195, 199), .Font = New Font("Segoe UI", 10.5F), .Cursor = Cursors.Hand, .Padding = New Padding(20, 0, 0, 0)}
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = ClrNavHover
        btn.FlatAppearance.MouseDownBackColor = ClrPrimary
        Return btn
    End Function

    Private Function CreateActionButton(text As String, bgColor As Color) As Button
        Dim btn As New Button() With {.Text = text, .Size = New Size(155, 38), .Margin = New Padding(5, 3, 5, 3), .FlatStyle = FlatStyle.Flat, .BackColor = bgColor, .ForeColor = Color.White, .Font = New Font("Segoe UI Semibold", 9.5F), .Cursor = Cursors.Hand}
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(bgColor, 0.15F)
        btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(bgColor, 0.1F)
        Return btn
    End Function

    Private Function CreateFieldLabel(text As String) As Label
        Return New Label() With {.Text = text, .Font = New Font("Segoe UI Semibold", 9.5F), .ForeColor = Color.FromArgb(80, 90, 100), .BackColor = Color.Transparent, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.BottomLeft, .Padding = New Padding(8, 0, 0, 5)}
    End Function

    Private Function CreateStyledTextBox() As TextBox
        Return New TextBox() With {.Font = New Font("Segoe UI", 11.0F), .Dock = DockStyle.Fill, .BorderStyle = BorderStyle.FixedSingle, .Margin = New Padding(8, 5, 20, 5), .BackColor = Color.FromArgb(250, 251, 252)}
    End Function

    Private Function CreateStyledComboBox(items As String()) As ComboBox
        Dim cbo As New ComboBox() With {.Font = New Font("Segoe UI", 11.0F), .Dock = DockStyle.Fill, .DropDownStyle = ComboBoxStyle.DropDownList, .Margin = New Padding(8, 5, 20, 5), .BackColor = Color.FromArgb(250, 251, 252)}
        cbo.Items.AddRange(items)
        Return cbo
    End Function

    Private Function CreateStatCard(title As String, colorStart As Color, colorEnd As Color) As Panel
        Dim card As New Panel() With {.Size = New Size(280, 130), .Margin = New Padding(0, 0, 25, 0), .BackColor = Color.White}
        
        Dim lblCount As New Label() With {.Visible = False, .Text = "0"}
        AddHandler lblCount.TextChanged, Sub(s, e) card.Invalidate()
        card.Controls.Add(lblCount)

        AddHandler card.Paint, Sub(s, e)
              Dim g = e.Graphics
              g.SmoothingMode = SmoothingMode.AntiAlias
              g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
              Dim rect As New Rectangle(0, 0, card.Width, card.Height)
              Using brush As New LinearGradientBrush(rect, colorStart, colorEnd, LinearGradientMode.ForwardDiagonal)
                  g.FillRectangle(brush, rect)
              End Using
              Using brush As New SolidBrush(Color.FromArgb(20, 255, 255, 255))
                  g.FillEllipse(brush, card.Width - 70, -20, 120, 120)
                  g.FillEllipse(brush, card.Width - 110, card.Height - 50, 70, 70)
              End Using
              Using font As New Font("Segoe UI", 36.0F, FontStyle.Bold)
                  g.DrawString(lblCount.Text, font, Brushes.White, 25, 15)
              End Using
              Using font As New Font("Segoe UI Semibold", 11.0F)
                  Using tBrush As New SolidBrush(Color.FromArgb(230, 255, 255, 255))
                      g.DrawString(title, font, tBrush, 30, 85)
                  End Using
              End Using
          End Sub
        
        card.Tag = lblCount
        Return card
    End Function
#End Region

End Class
