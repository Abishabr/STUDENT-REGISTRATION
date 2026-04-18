' ============================================================
' Form1.vb - Main Application Form
' Student Record Management System
'
' This is the primary form containing:
'   - Professional dashboard with statistics cards
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

    ' Primary colors
    Private Shared ReadOnly ClrPrimaryDark As Color = Color.FromArgb(25, 42, 86)
    Private Shared ReadOnly ClrPrimary As Color = Color.FromArgb(41, 128, 185)
    Private Shared ReadOnly ClrPrimaryLight As Color = Color.FromArgb(52, 152, 219)
    Private Shared ReadOnly ClrAccent As Color = Color.FromArgb(0, 188, 212)

    ' Semantic colors
    Private Shared ReadOnly ClrSuccess As Color = Color.FromArgb(39, 174, 96)
    Private Shared ReadOnly ClrDanger As Color = Color.FromArgb(231, 76, 60)
    Private Shared ReadOnly ClrWarning As Color = Color.FromArgb(243, 156, 18)
    Private Shared ReadOnly ClrInfo As Color = Color.FromArgb(142, 68, 173)

    ' Background and surface colors
    Private Shared ReadOnly ClrBgLight As Color = Color.FromArgb(240, 243, 247)
    Private Shared ReadOnly ClrCardBg As Color = Color.White

    ' Text colors
    Private Shared ReadOnly ClrTextDark As Color = Color.FromArgb(44, 62, 80)
    Private Shared ReadOnly ClrTextMuted As Color = Color.FromArgb(127, 140, 141)

    ' Navigation colors
    Private Shared ReadOnly ClrNavBg As Color = Color.FromArgb(30, 39, 73)
    Private Shared ReadOnly ClrNavHover As Color = Color.FromArgb(45, 60, 110)

#End Region

#Region "Control Declarations"

    ' --- Main Layout Panels ---
    Private pnlHeader As Panel
    Private pnlNav As Panel
    Private pnlContent As Panel

    ' --- Switchable Content Panels ---
    Private pnlDashboard As Panel
    Private pnlStudentMgmt As Panel

    ' --- Navigation Buttons ---
    Private btnNavDashboard As Button
    Private btnNavAdd As Button
    Private btnNavView As Button
    Private btnNavSearch As Button
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

    ' --- Student ID Display Label ---
    Private lblSelectedID As Label

    ' --- Action Buttons ---
    Private btnAdd As Button
    Private btnUpdate As Button
    Private btnDelete As Button
    Private btnClear As Button

    ' --- Search ---
    Private txtSearch As TextBox

    ' --- DataGridView ---
    Private dgvStudents As DataGridView

    ' --- Status Strip ---
    Private mainStatusStrip As StatusStrip
    Private statusLabel As ToolStripStatusLabel

    ' --- Error Provider ---
    Private formErrorProvider As ErrorProvider

#End Region

#Region "State Variables"

    ' Tracks the currently selected student for update/delete operations
    Private selectedStudentID As Integer = -1

    ' Tracks the currently highlighted nav button
    Private activeNavButton As Button = Nothing

    ' Used for print pagination
    Private printPageIndex As Integer = 0

#End Region

#Region "Constructor & Form Events"

    ''' <summary>
    ''' Form constructor - initializes designer components then builds all UI programmatically.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        SetupUI()
    End Sub

    ''' <summary>
    ''' Form Load - initializes database and loads the dashboard view.
    ''' </summary>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize SQLite database (creates table if not exists)
            DatabaseHelper.InitializeDatabase()

            ' Show dashboard view by default
            ShowDashboard()

            ' Enable keyboard shortcut detection
            Me.KeyPreview = True

            UpdateStatus("Application loaded successfully")
        Catch ex As Exception
            MessageBox.Show($"Error initializing application: {ex.Message}",
                            "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Form Closing - shows confirmation dialog before exit.
    ''' </summary>
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If MessageBox.Show("Are you sure you want to exit the application?",
                          "Exit Confirmation",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Keyboard shortcut handler for rapid access to common operations.
    ''' </summary>
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.S ' Save / Add Student
                    ShowStudentManagement()
                    AddStudent()
                    e.SuppressKeyPress = True
                Case Keys.U ' Update Student
                    UpdateStudentRecord()
                    e.SuppressKeyPress = True
                Case Keys.D ' Delete Student
                    DeleteStudentRecord()
                    e.SuppressKeyPress = True
                Case Keys.F ' Focus Search
                    ShowStudentManagement()
                    txtSearch.Focus()
                    txtSearch.SelectAll()
                    e.SuppressKeyPress = True
                Case Keys.N ' New / Clear fields
                    ShowAddStudent()
                    e.SuppressKeyPress = True
                Case Keys.E ' Export
                    ExportToCSV()
                    e.SuppressKeyPress = True
                Case Keys.P ' Print
                    PrintReport()
                    e.SuppressKeyPress = True
            End Select
        End If
    End Sub

#End Region

#Region "UI Setup - Main Layout"

    ''' <summary>
    ''' Master setup method - creates all UI components in correct docking order.
    ''' </summary>
    Private Sub SetupUI()
        ' Initialize error provider
        formErrorProvider = New ErrorProvider()
        formErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        ' Build layout panels (order matters for correct docking!)
        ' Fill panel must be added FIRST, then Left, Top, Bottom
        SetupContentPanel()
        SetupNavPanel()
        SetupHeaderPanel()
        SetupStatusStrip()

        ' Build the two content views
        SetupDashboardPanel()
        SetupStudentManagementPanel()
    End Sub

    ''' <summary>
    ''' Creates the top header bar with app title and accent line.
    ''' </summary>
    Private Sub SetupHeaderPanel()
        pnlHeader = New Panel()
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 60
        pnlHeader.BackColor = ClrPrimaryDark

        ' Accent gradient line at bottom of header
        Dim accentLine As New Panel()
        accentLine.Dock = DockStyle.Bottom
        accentLine.Height = 3
        accentLine.BackColor = ClrAccent
        pnlHeader.Controls.Add(accentLine)

        ' App title label
        Dim lblTitle As New Label()
        lblTitle.Text = "  Student Record Management System"
        lblTitle.Font = New Font("Segoe UI Semibold", 16.0F)
        lblTitle.ForeColor = Color.White
        lblTitle.BackColor = Color.Transparent
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.MiddleLeft
        lblTitle.Padding = New Padding(15, 0, 0, 0)
        pnlHeader.Controls.Add(lblTitle)

        Me.Controls.Add(pnlHeader)
    End Sub

    ''' <summary>
    ''' Creates the left navigation sidebar with styled buttons.
    ''' </summary>
    Private Sub SetupNavPanel()
        pnlNav = New Panel()
        pnlNav.Dock = DockStyle.Left
        pnlNav.Width = 230
        pnlNav.BackColor = ClrNavBg

        ' --- Create navigation buttons ---
        btnNavDashboard = CreateNavButton("    Dashboard")
        btnNavAdd = CreateNavButton("    Add New Student")
        btnNavView = CreateNavButton("    View All Students")
        btnNavSearch = CreateNavButton("    Search Students")
        btnNavExport = CreateNavButton("    Export to CSV")
        btnNavPrint = CreateNavButton("    Print Report")
        btnNavExit = CreateNavButton("    Exit Application")
        btnNavExit.ForeColor = Color.FromArgb(231, 76, 60)

        ' Navigation separator line
        Dim separator As New Panel()
        separator.Dock = DockStyle.Top
        separator.Height = 1
        separator.BackColor = Color.FromArgb(50, 65, 110)
        separator.Margin = New Padding(15, 5, 15, 5)

        ' Wire up click events
        AddHandler btnNavDashboard.Click, Sub(s, ev) ShowDashboard()
        AddHandler btnNavAdd.Click, Sub(s, ev) ShowAddStudent()
        AddHandler btnNavView.Click, Sub(s, ev) ShowStudentManagement()
        AddHandler btnNavSearch.Click, Sub(s, ev)
                                           ShowStudentManagement()
                                           txtSearch.Focus()
                                           txtSearch.SelectAll()
                                       End Sub
        AddHandler btnNavExport.Click, Sub(s, ev) ExportToCSV()
        AddHandler btnNavPrint.Click, Sub(s, ev) PrintReport()
        AddHandler btnNavExit.Click, Sub(s, ev) Me.Close()

        ' Add buttons to nav panel
        ' Dock.Top: controls added LAST appear at the visual TOP
        ' Dock.Bottom: exit button pinned to bottom
        btnNavExit.Dock = DockStyle.Bottom
        pnlNav.Controls.Add(btnNavExit)

        ' Add separator and top-docked buttons (reverse visual order for Dock.Top)
        pnlNav.Controls.Add(btnNavPrint)
        pnlNav.Controls.Add(btnNavExport)
        pnlNav.Controls.Add(separator)
        pnlNav.Controls.Add(btnNavSearch)
        pnlNav.Controls.Add(btnNavView)
        pnlNav.Controls.Add(btnNavAdd)
        pnlNav.Controls.Add(btnNavDashboard)

        ' Nav header with branding
        Dim navHeader As New Panel()
        navHeader.Dock = DockStyle.Top
        navHeader.Height = 55
        navHeader.BackColor = Color.FromArgb(22, 33, 62)

        Dim lblNavBrand As New Label()
        lblNavBrand.Text = "  SRMS"
        lblNavBrand.Font = New Font("Segoe UI Semibold", 15.0F)
        lblNavBrand.ForeColor = ClrAccent
        lblNavBrand.BackColor = Color.Transparent
        lblNavBrand.Dock = DockStyle.Fill
        lblNavBrand.TextAlign = ContentAlignment.MiddleCenter
        navHeader.Controls.Add(lblNavBrand)

        ' Branding panel accent
        Dim navAccent As New Panel()
        navAccent.Dock = DockStyle.Bottom
        navAccent.Height = 1
        navAccent.BackColor = Color.FromArgb(50, 65, 110)
        navHeader.Controls.Add(navAccent)

        pnlNav.Controls.Add(navHeader)

        Me.Controls.Add(pnlNav)
    End Sub

    ''' <summary>
    ''' Creates the main content area that hosts Dashboard and Student Management panels.
    ''' </summary>
    Private Sub SetupContentPanel()
        pnlContent = New Panel()
        pnlContent.Dock = DockStyle.Fill
        pnlContent.BackColor = ClrBgLight
        pnlContent.Padding = New Padding(20)
        Me.Controls.Add(pnlContent)
    End Sub

    ''' <summary>
    ''' Creates the bottom status strip with status messages and keyboard shortcut hints.
    ''' </summary>
    Private Sub SetupStatusStrip()
        mainStatusStrip = New StatusStrip()
        mainStatusStrip.BackColor = ClrPrimaryDark
        mainStatusStrip.SizingGrip = False

        ' Status message label
        statusLabel = New ToolStripStatusLabel()
        statusLabel.Text = "  Ready"
        statusLabel.ForeColor = Color.White
        statusLabel.Font = New Font("Segoe UI", 9.0F)
        mainStatusStrip.Items.Add(statusLabel)

        ' Keyboard shortcuts hint (right-aligned)
        Dim lblShortcuts As New ToolStripStatusLabel()
        lblShortcuts.Text = "Ctrl+S Save | Ctrl+U Update | Ctrl+D Delete | Ctrl+F Search | Ctrl+E Export | Ctrl+P Print"
        lblShortcuts.ForeColor = Color.FromArgb(140, 189, 195, 199)
        lblShortcuts.Font = New Font("Segoe UI", 8.0F)
        lblShortcuts.Spring = True
        lblShortcuts.TextAlign = ContentAlignment.MiddleRight
        mainStatusStrip.Items.Add(lblShortcuts)

        Me.Controls.Add(mainStatusStrip)
    End Sub

#End Region

#Region "UI Setup - Dashboard Panel"

    ''' <summary>
    ''' Creates the dashboard view with statistics cards and welcome info.
    ''' </summary>
    Private Sub SetupDashboardPanel()
        pnlDashboard = New Panel()
        pnlDashboard.Dock = DockStyle.Fill
        pnlDashboard.BackColor = ClrBgLight
        pnlDashboard.Visible = True

        ' --- Dashboard Title ---
        Dim lblDashTitle As New Label()
        lblDashTitle.Text = "  Dashboard Overview"
        lblDashTitle.Font = New Font("Segoe UI Semibold", 18.0F)
        lblDashTitle.ForeColor = ClrTextDark
        lblDashTitle.BackColor = Color.Transparent
        lblDashTitle.Dock = DockStyle.Top
        lblDashTitle.Height = 50
        lblDashTitle.TextAlign = ContentAlignment.MiddleLeft

        ' --- Statistics Cards Container ---
        Dim pnlCards As New FlowLayoutPanel()
        pnlCards.Dock = DockStyle.Top
        pnlCards.Height = 170
        pnlCards.FlowDirection = FlowDirection.LeftToRight
        pnlCards.WrapContents = False
        pnlCards.BackColor = Color.Transparent
        pnlCards.Padding = New Padding(0, 5, 0, 10)

        ' Create the three stat cards
        Dim cardTotal = CreateStatCard("Total Students", ClrPrimary)
        Dim cardMale = CreateStatCard("Male Students", ClrSuccess)
        Dim cardFemale = CreateStatCard("Female Students", ClrWarning)

        ' Store references to the count labels for live updates
        lblTotalCount = DirectCast(cardTotal.Tag, Label)
        lblMaleCount = DirectCast(cardMale.Tag, Label)
        lblFemaleCount = DirectCast(cardFemale.Tag, Label)

        pnlCards.Controls.Add(cardTotal)
        pnlCards.Controls.Add(cardMale)
        pnlCards.Controls.Add(cardFemale)

        ' --- Welcome / Info Panel ---
        Dim pnlWelcome As New Panel()
        pnlWelcome.Dock = DockStyle.Fill
        pnlWelcome.BackColor = ClrCardBg
        pnlWelcome.Padding = New Padding(30)

        ' Welcome title
        Dim lblWelcomeTitle As New Label()
        lblWelcomeTitle.Text = "Welcome to Student Record Management System"
        lblWelcomeTitle.Font = New Font("Segoe UI Semibold", 16.0F)
        lblWelcomeTitle.ForeColor = ClrTextDark
        lblWelcomeTitle.BackColor = Color.Transparent
        lblWelcomeTitle.Dock = DockStyle.Top
        lblWelcomeTitle.Height = 45
        lblWelcomeTitle.TextAlign = ContentAlignment.MiddleLeft
        pnlWelcome.Controls.Add(lblWelcomeTitle)

        ' Welcome description
        Dim lblWelcomeDesc As New Label()
        lblWelcomeDesc.Text =
            "Use the navigation panel on the left to manage student records." & vbCrLf & vbCrLf &
            "Quick Actions:" & vbCrLf &
            "   Add New Student — Register a new student record" & vbCrLf &
            "   View All Students — Browse and manage all student records" & vbCrLf &
            "   Search Students — Find students by name, ID, or department" & vbCrLf &
            "   Export to CSV — Download student data as a spreadsheet" & vbCrLf &
            "   Print Report — Generate a printable student report" & vbCrLf & vbCrLf &
            "Keyboard Shortcuts:" & vbCrLf &
            "   Ctrl+S  Save new student          Ctrl+U  Update selected student" & vbCrLf &
            "   Ctrl+D  Delete selected student   Ctrl+F  Focus search field" & vbCrLf &
            "   Ctrl+N  Clear form / New record    Ctrl+E  Export to CSV" & vbCrLf &
            "   Ctrl+P  Print report"
        lblWelcomeDesc.Font = New Font("Segoe UI", 10.5F)
        lblWelcomeDesc.ForeColor = ClrTextMuted
        lblWelcomeDesc.BackColor = Color.Transparent
        lblWelcomeDesc.Dock = DockStyle.Fill
        lblWelcomeDesc.Padding = New Padding(0, 10, 0, 0)
        pnlWelcome.Controls.Add(lblWelcomeDesc)

        ' Assemble dashboard panel (Fill first, then Top in reverse visual order)
        pnlDashboard.Controls.Add(pnlWelcome)
        pnlDashboard.Controls.Add(pnlCards)
        pnlDashboard.Controls.Add(lblDashTitle)

        pnlContent.Controls.Add(pnlDashboard)
    End Sub

#End Region

#Region "UI Setup - Student Management Panel"

    ''' <summary>
    ''' Creates the student management panel with form fields, action buttons,
    ''' search bar, and DataGridView.
    ''' </summary>
    Private Sub SetupStudentManagementPanel()
        pnlStudentMgmt = New Panel()
        pnlStudentMgmt.Dock = DockStyle.Fill
        pnlStudentMgmt.BackColor = ClrBgLight
        pnlStudentMgmt.Visible = False

        ' Build sub-sections (each is a Dock.Top or Dock.Fill panel)
        ' Add in reverse visual order: Fill first, then Top items bottom-to-top

        ' --- DataGridView (Dock.Fill - takes remaining space) ---
        SetupDataGridView()
        pnlStudentMgmt.Controls.Add(dgvStudents)

        ' --- Search Bar (Dock.Top) ---
        Dim pnlSearchBar = CreateSearchBar()
        pnlStudentMgmt.Controls.Add(pnlSearchBar)

        ' --- Action Buttons (Dock.Top) ---
        Dim pnlActions = CreateActionButtonsPanel()
        pnlStudentMgmt.Controls.Add(pnlActions)

        ' --- Form Fields (Dock.Top) ---
        Dim pnlFormFields = CreateFormFieldsPanel()
        pnlStudentMgmt.Controls.Add(pnlFormFields)

        ' --- Form Section Header (Dock.Top) ---
        Dim pnlFormHeader As New Panel()
        pnlFormHeader.Dock = DockStyle.Top
        pnlFormHeader.Height = 42
        pnlFormHeader.BackColor = ClrCardBg
        pnlFormHeader.Padding = New Padding(15, 0, 15, 0)

        Dim lblFormTitle As New Label()
        lblFormTitle.Text = "  Student Information"
        lblFormTitle.Font = New Font("Segoe UI Semibold", 13.0F)
        lblFormTitle.ForeColor = ClrTextDark
        lblFormTitle.BackColor = Color.Transparent
        lblFormTitle.Dock = DockStyle.Left
        lblFormTitle.AutoSize = True
        lblFormTitle.TextAlign = ContentAlignment.MiddleLeft
        pnlFormHeader.Controls.Add(lblFormTitle)

        ' Selected student indicator
        lblSelectedID = New Label()
        lblSelectedID.Text = "New Record"
        lblSelectedID.Font = New Font("Segoe UI", 10.0F, FontStyle.Italic)
        lblSelectedID.ForeColor = ClrAccent
        lblSelectedID.BackColor = Color.Transparent
        lblSelectedID.Dock = DockStyle.Right
        lblSelectedID.AutoSize = True
        lblSelectedID.TextAlign = ContentAlignment.MiddleRight
        pnlFormHeader.Controls.Add(lblSelectedID)

        ' Header accent line
        Dim headerLine As New Panel()
        headerLine.Dock = DockStyle.Bottom
        headerLine.Height = 2
        headerLine.BackColor = ClrPrimary
        pnlFormHeader.Controls.Add(headerLine)

        pnlStudentMgmt.Controls.Add(pnlFormHeader)

        pnlContent.Controls.Add(pnlStudentMgmt)
    End Sub

    ''' <summary>
    ''' Creates the form fields panel with a TableLayoutPanel for responsive input layout.
    ''' 3 columns x 3 rows of label+input pairs.
    ''' </summary>
    Private Function CreateFormFieldsPanel() As Panel
        Dim pnl As New Panel()
        pnl.Dock = DockStyle.Top
        pnl.Height = 215
        pnl.BackColor = ClrCardBg
        pnl.Padding = New Padding(10, 5, 10, 5)

        ' TableLayoutPanel for responsive grid layout
        Dim tbl As New TableLayoutPanel()
        tbl.Dock = DockStyle.Fill
        tbl.ColumnCount = 3
        tbl.RowCount = 6
        tbl.Padding = New Padding(5)

        ' 3 equal-width columns
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.34F))

        ' Row heights: alternating Label (25px) and Control (38px) rows
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 25))  ' Labels row 1
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 38))  ' Controls row 1
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 25))  ' Labels row 2
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 38))  ' Controls row 2
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 25))  ' Labels row 3
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 38))  ' Controls row 3

        ' === Row 0: Labels ===
        tbl.Controls.Add(CreateFieldLabel("First Name *"), 0, 0)
        tbl.Controls.Add(CreateFieldLabel("Last Name *"), 1, 0)
        tbl.Controls.Add(CreateFieldLabel("Gender *"), 2, 0)

        ' === Row 1: Controls ===
        txtFirstName = CreateStyledTextBox()
        tbl.Controls.Add(txtFirstName, 0, 1)

        txtLastName = CreateStyledTextBox()
        tbl.Controls.Add(txtLastName, 1, 1)

        cboGender = CreateStyledComboBox({"Male", "Female", "Other"})
        tbl.Controls.Add(cboGender, 2, 1)

        ' === Row 2: Labels ===
        tbl.Controls.Add(CreateFieldLabel("Date of Birth *"), 0, 2)
        tbl.Controls.Add(CreateFieldLabel("Department *"), 1, 2)
        tbl.Controls.Add(CreateFieldLabel("Phone Number"), 2, 2)

        ' === Row 3: Controls ===
        dtpDateOfBirth = New DateTimePicker()
        dtpDateOfBirth.Format = DateTimePickerFormat.Short
        dtpDateOfBirth.Font = New Font("Segoe UI", 10.0F)
        dtpDateOfBirth.Dock = DockStyle.Fill
        dtpDateOfBirth.Margin = New Padding(5, 3, 5, 3)
        tbl.Controls.Add(dtpDateOfBirth, 0, 3)

        cboDepartment = CreateStyledComboBox({
            "Computer Science",
            "Information Technology",
            "Electrical Engineering",
            "Mechanical Engineering",
            "Civil Engineering",
            "Business Administration",
            "Mathematics",
            "Physics",
            "Biology",
            "Chemistry"
        })
        tbl.Controls.Add(cboDepartment, 1, 3)

        txtPhone = CreateStyledTextBox()
        tbl.Controls.Add(txtPhone, 2, 3)

        ' === Row 4: Labels ===
        tbl.Controls.Add(CreateFieldLabel("Email Address"), 0, 4)
        tbl.Controls.Add(CreateFieldLabel("Address"), 1, 4)

        ' === Row 5: Controls ===
        txtEmail = CreateStyledTextBox()
        tbl.Controls.Add(txtEmail, 0, 5)

        txtAddress = CreateStyledTextBox()
        tbl.SetColumnSpan(txtAddress, 2)
        tbl.Controls.Add(txtAddress, 1, 5)

        pnl.Controls.Add(tbl)
        Return pnl
    End Function

    ''' <summary>
    ''' Creates the action buttons panel with Add, Update, Delete, and Clear buttons.
    ''' </summary>
    Private Function CreateActionButtonsPanel() As Panel
        Dim pnl As New Panel()
        pnl.Dock = DockStyle.Top
        pnl.Height = 55
        pnl.BackColor = ClrCardBg
        pnl.Padding = New Padding(12, 5, 12, 5)

        Dim flow As New FlowLayoutPanel()
        flow.Dock = DockStyle.Fill
        flow.FlowDirection = FlowDirection.LeftToRight
        flow.WrapContents = False

        ' Create styled action buttons
        btnAdd = CreateActionButton("  Add Student", ClrSuccess)
        btnUpdate = CreateActionButton("  Update", ClrPrimary)
        btnDelete = CreateActionButton("  Delete", ClrDanger)
        btnClear = CreateActionButton("  Clear Fields", ClrWarning)

        ' Wire click events
        AddHandler btnAdd.Click, Sub(s, ev) AddStudent()
        AddHandler btnUpdate.Click, Sub(s, ev) UpdateStudentRecord()
        AddHandler btnDelete.Click, Sub(s, ev) DeleteStudentRecord()
        AddHandler btnClear.Click, Sub(s, ev) ClearFields()

        flow.Controls.Add(btnAdd)
        flow.Controls.Add(btnUpdate)
        flow.Controls.Add(btnDelete)
        flow.Controls.Add(btnClear)

        pnl.Controls.Add(flow)
        Return pnl
    End Function

    ''' <summary>
    ''' Creates the search bar panel with real-time search textbox.
    ''' </summary>
    Private Function CreateSearchBar() As Panel
        Dim pnl As New Panel()
        pnl.Dock = DockStyle.Top
        pnl.Height = 48
        pnl.BackColor = ClrBgLight
        pnl.Padding = New Padding(12, 8, 12, 8)

        ' Search textbox (Dock.Fill, added first)
        txtSearch = New TextBox()
        txtSearch.Font = New Font("Segoe UI", 10.5F)
        txtSearch.Dock = DockStyle.Fill
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.BackColor = Color.White
        AddHandler txtSearch.TextChanged, Sub(s, ev) SearchStudents()

        ' Search label (Dock.Left, added second)
        Dim lblSearch As New Label()
        lblSearch.Text = "  Search:  "
        lblSearch.Font = New Font("Segoe UI Semibold", 10.0F)
        lblSearch.ForeColor = ClrTextDark
        lblSearch.BackColor = Color.Transparent
        lblSearch.Dock = DockStyle.Left
        lblSearch.AutoSize = True
        lblSearch.TextAlign = ContentAlignment.MiddleLeft

        pnl.Controls.Add(txtSearch)
        pnl.Controls.Add(lblSearch)
        Return pnl
    End Function

    ''' <summary>
    ''' Creates and configures the main DataGridView with professional styling.
    ''' </summary>
    Private Sub SetupDataGridView()
        dgvStudents = New DataGridView()
        dgvStudents.Dock = DockStyle.Fill

        ' --- Visual styling ---
        dgvStudents.BackgroundColor = Color.White
        dgvStudents.BorderStyle = BorderStyle.None
        dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvStudents.GridColor = Color.FromArgb(230, 235, 240)

        ' Header styling
        dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = ClrPrimaryDark
        dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvStudents.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.0F)
        dgvStudents.ColumnHeadersDefaultCellStyle.Padding = New Padding(5)
        dgvStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvStudents.ColumnHeadersHeight = 42
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvStudents.EnableHeadersVisualStyles = False

        ' Cell styling
        dgvStudents.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        dgvStudents.DefaultCellStyle.Padding = New Padding(5, 2, 5, 2)
        dgvStudents.DefaultCellStyle.SelectionBackColor = ClrPrimaryLight
        dgvStudents.DefaultCellStyle.SelectionForeColor = Color.White
        dgvStudents.RowTemplate.Height = 36

        ' Alternating row colors for readability
        dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252)

        ' Behavior settings
        dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvStudents.ReadOnly = True
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.AllowUserToResizeRows = False
        dgvStudents.MultiSelect = False
        dgvStudents.RowHeadersVisible = False

        ' Row click event - loads selected student data into form fields
        AddHandler dgvStudents.CellClick, AddressOf DgvStudents_CellClick
    End Sub

#End Region

#Region "Navigation - Panel Switching"

    ''' <summary>
    ''' Switches to the Dashboard view and refreshes statistics.
    ''' </summary>
    Private Sub ShowDashboard()
        pnlStudentMgmt.Visible = False
        pnlDashboard.Visible = True
        pnlDashboard.BringToFront()
        UpdateDashboardStats()
        SetActiveNavButton(btnNavDashboard)
        UpdateStatus("Dashboard view")
    End Sub

    ''' <summary>
    ''' Switches to the Student Management view and loads all students.
    ''' </summary>
    Private Sub ShowStudentManagement()
        pnlDashboard.Visible = False
        pnlStudentMgmt.Visible = True
        pnlStudentMgmt.BringToFront()
        LoadStudents()
        SetActiveNavButton(btnNavView)
        UpdateStatus("Student management view - All records loaded")
    End Sub

    ''' <summary>
    ''' Switches to Student Management with cleared fields for adding a new student.
    ''' </summary>
    Private Sub ShowAddStudent()
        ShowStudentManagement()
        ClearFields()
        txtFirstName.Focus()
        SetActiveNavButton(btnNavAdd)
        UpdateStatus("Ready to add a new student")
    End Sub

    ''' <summary>
    ''' Highlights the currently active navigation button and resets others.
    ''' </summary>
    Private Sub SetActiveNavButton(btn As Button)
        ' Reset all nav buttons to default appearance
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

        ' Highlight the active button
        If btn IsNot Nothing Then
            btn.BackColor = ClrPrimary
            btn.ForeColor = Color.White
            activeNavButton = btn
        End If
    End Sub

#End Region

#Region "Data Operations - CRUD"

    ''' <summary>
    ''' Loads all student records into the DataGridView.
    ''' </summary>
    Private Sub LoadStudents()
        Try
            dgvStudents.DataSource = DatabaseHelper.GetAllStudents()
            FormatDataGridView()
            UpdateDashboardStats()
            UpdateStatus($"Loaded {dgvStudents.Rows.Count} student records")
        Catch ex As Exception
            MessageBox.Show($"Error loading students: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Validates form and inserts a new student record.
    ''' Clears the form and refreshes the DataGridView on success.
    ''' </summary>
    Private Sub AddStudent()
        If Not ValidateForm() Then Return

        Dim student As New Student()
        student.FirstName = txtFirstName.Text.Trim()
        student.LastName = txtLastName.Text.Trim()
        student.Gender = cboGender.SelectedItem.ToString()
        student.DateOfBirth = dtpDateOfBirth.Value.Date
        student.Department = cboDepartment.SelectedItem.ToString()
        student.PhoneNumber = txtPhone.Text.Trim()
        student.Email = txtEmail.Text.Trim()
        student.Address = txtAddress.Text.Trim()
        student.RegistrationDate = DateTime.Now

        If DatabaseHelper.AddStudent(student) Then
            MessageBox.Show("Student added successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadStudents()
            UpdateStatus("New student record added successfully")
        End If
    End Sub

    ''' <summary>
    ''' Validates form, confirms with user, and updates the selected student record.
    ''' </summary>
    Private Sub UpdateStudentRecord()
        If selectedStudentID = -1 Then
            MessageBox.Show("Please select a student from the table to update.",
                            "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateForm() Then Return

        If MessageBox.Show("Are you sure you want to update this student record?",
                          "Update Confirmation",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim student As New Student()
        student.StudentID = selectedStudentID
        student.FirstName = txtFirstName.Text.Trim()
        student.LastName = txtLastName.Text.Trim()
        student.Gender = cboGender.SelectedItem.ToString()
        student.DateOfBirth = dtpDateOfBirth.Value.Date
        student.Department = cboDepartment.SelectedItem.ToString()
        student.PhoneNumber = txtPhone.Text.Trim()
        student.Email = txtEmail.Text.Trim()
        student.Address = txtAddress.Text.Trim()

        If DatabaseHelper.UpdateStudent(student) Then
            MessageBox.Show("Student record updated successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadStudents()
            UpdateStatus($"Student ID {selectedStudentID} updated successfully")
            selectedStudentID = -1
        End If
    End Sub

    ''' <summary>
    ''' Confirms with user and deletes the selected student record.
    ''' </summary>
    Private Sub DeleteStudentRecord()
        If selectedStudentID = -1 Then
            MessageBox.Show("Please select a student from the table to delete.",
                            "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to permanently delete this student record?" &
                          vbCrLf & "This action cannot be undone.",
                          "Delete Confirmation",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Warning) = DialogResult.No Then
            Return
        End If

        Dim deletedId = selectedStudentID
        If DatabaseHelper.DeleteStudent(selectedStudentID) Then
            MessageBox.Show("Student record deleted successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadStudents()
            selectedStudentID = -1
            UpdateStatus($"Student ID {deletedId} deleted successfully")
        End If
    End Sub

    ''' <summary>
    ''' Performs real-time search as the user types. Filters the DataGridView.
    ''' </summary>
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
            ' Silently handle errors during rapid typing
        End Try
    End Sub

    ''' <summary>
    ''' Updates the dashboard statistics cards with current counts.
    ''' </summary>
    Private Sub UpdateDashboardStats()
        Try
            If lblTotalCount IsNot Nothing Then
                lblTotalCount.Text = DatabaseHelper.GetTotalCount().ToString()
            End If
            If lblMaleCount IsNot Nothing Then
                lblMaleCount.Text = DatabaseHelper.GetGenderCount("Male").ToString()
            End If
            If lblFemaleCount IsNot Nothing Then
                lblFemaleCount.Text = DatabaseHelper.GetGenderCount("Female").ToString()
            End If
        Catch ex As Exception
            ' Silently handle stats update errors
        End Try
    End Sub

#End Region

#Region "DataGridView Events"

    ''' <summary>
    ''' Handles row selection in the DataGridView - loads the selected
    ''' student's data into the form fields for editing.
    ''' </summary>
    Private Sub DgvStudents_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        Try
            Dim row = dgvStudents.Rows(e.RowIndex)

            ' Load student data into form fields
            selectedStudentID = Convert.ToInt32(row.Cells("StudentID").Value)
            txtFirstName.Text = row.Cells("FirstName").Value?.ToString()
            txtLastName.Text = row.Cells("LastName").Value?.ToString()

            ' Set ComboBox selections (handle case where value might not match)
            Dim genderVal = row.Cells("Gender").Value?.ToString()
            If cboGender.Items.Contains(genderVal) Then
                cboGender.SelectedItem = genderVal
            End If

            ' Parse date from string
            Dim dobStr = row.Cells("DateOfBirth").Value?.ToString()
            If Not String.IsNullOrEmpty(dobStr) Then
                Dim parsedDate As Date
                If Date.TryParse(dobStr, parsedDate) Then
                    dtpDateOfBirth.Value = parsedDate
                End If
            End If

            Dim deptVal = row.Cells("Department").Value?.ToString()
            If cboDepartment.Items.Contains(deptVal) Then
                cboDepartment.SelectedItem = deptVal
            End If

            txtPhone.Text = row.Cells("PhoneNumber").Value?.ToString()
            txtEmail.Text = row.Cells("Email").Value?.ToString()
            txtAddress.Text = row.Cells("Address").Value?.ToString()

            ' Update selected ID indicator
            lblSelectedID.Text = $"Editing Student ID: {selectedStudentID}"
            lblSelectedID.ForeColor = ClrPrimary

            UpdateStatus($"Selected Student ID: {selectedStudentID} - " &
                         $"{txtFirstName.Text} {txtLastName.Text}")
        Catch ex As Exception
            MessageBox.Show($"Error loading student data: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Form Helpers - Validation, Clear, Format"

    ''' <summary>
    ''' Validates all required form fields and shows ErrorProvider icons
    ''' next to invalid fields.
    ''' </summary>
    ''' <returns>True if all fields are valid</returns>
    Private Function ValidateForm() As Boolean
        formErrorProvider.Clear()
        Dim isValid As Boolean = True

        ' First Name (required)
        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            formErrorProvider.SetError(txtFirstName, "First Name is required")
            isValid = False
        End If

        ' Last Name (required)
        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            formErrorProvider.SetError(txtLastName, "Last Name is required")
            isValid = False
        End If

        ' Gender (required)
        If cboGender.SelectedIndex = -1 Then
            formErrorProvider.SetError(cboGender, "Please select a gender")
            isValid = False
        End If

        ' Department (required)
        If cboDepartment.SelectedIndex = -1 Then
            formErrorProvider.SetError(cboDepartment, "Please select a department")
            isValid = False
        End If

        ' Email (optional, but validate format if provided)
        Dim emailError = ValidationHelper.ValidateEmail(txtEmail.Text)
        If Not String.IsNullOrEmpty(emailError) Then
            formErrorProvider.SetError(txtEmail, emailError)
            isValid = False
        End If

        ' Phone (optional, but validate format if provided)
        Dim phoneError = ValidationHelper.ValidatePhone(txtPhone.Text)
        If Not String.IsNullOrEmpty(phoneError) Then
            formErrorProvider.SetError(txtPhone, phoneError)
            isValid = False
        End If

        If Not isValid Then
            UpdateStatus("Please correct the highlighted validation errors")
        End If

        Return isValid
    End Function

    ''' <summary>
    ''' Clears all form input fields and resets state to 'New Record' mode.
    ''' </summary>
    Private Sub ClearFields()
        txtFirstName.Clear()
        txtLastName.Clear()
        cboGender.SelectedIndex = -1
        dtpDateOfBirth.Value = Date.Now
        cboDepartment.SelectedIndex = -1
        txtPhone.Clear()
        txtEmail.Clear()
        txtAddress.Clear()
        txtSearch.Clear()

        selectedStudentID = -1
        formErrorProvider.Clear()

        ' Reset selected ID indicator
        If lblSelectedID IsNot Nothing Then
            lblSelectedID.Text = "New Record"
            lblSelectedID.ForeColor = ClrAccent
        End If

        UpdateStatus("Fields cleared - Ready for new entry")
    End Sub

    ''' <summary>
    ''' Formats DataGridView column headers and widths after data binding.
    ''' </summary>
    Private Sub FormatDataGridView()
        If dgvStudents.Columns.Count = 0 Then Return

        Try
            ' Rename columns for professional display
            If dgvStudents.Columns.Contains("StudentID") Then
                dgvStudents.Columns("StudentID").HeaderText = "ID"
                dgvStudents.Columns("StudentID").Width = 55
                dgvStudents.Columns("StudentID").MinimumWidth = 50
            End If
            If dgvStudents.Columns.Contains("FirstName") Then
                dgvStudents.Columns("FirstName").HeaderText = "First Name"
            End If
            If dgvStudents.Columns.Contains("LastName") Then
                dgvStudents.Columns("LastName").HeaderText = "Last Name"
            End If
            If dgvStudents.Columns.Contains("DateOfBirth") Then
                dgvStudents.Columns("DateOfBirth").HeaderText = "Date of Birth"
            End If
            If dgvStudents.Columns.Contains("PhoneNumber") Then
                dgvStudents.Columns("PhoneNumber").HeaderText = "Phone"
            End If
            If dgvStudents.Columns.Contains("RegistrationDate") Then
                dgvStudents.Columns("RegistrationDate").HeaderText = "Registered"
            End If
            ' Hide Address column to save horizontal space
            If dgvStudents.Columns.Contains("Address") Then
                dgvStudents.Columns("Address").Visible = False
            End If
        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    ''' <summary>
    ''' Updates the status bar message with a timestamp.
    ''' </summary>
    Private Sub UpdateStatus(message As String)
        If statusLabel IsNot Nothing Then
            statusLabel.Text = $"  {message}  |  {DateTime.Now:HH:mm:ss}"
        End If
    End Sub

#End Region

#Region "Export & Print"

    ''' <summary>
    ''' Exports the current DataGridView data to a CSV file.
    ''' Opens a SaveFileDialog for the user to choose the file location.
    ''' </summary>
    Private Sub ExportToCSV()
        Try
            ' Ensure we have data to export
            If pnlStudentMgmt.Visible = False Then
                ShowStudentManagement()
            End If

            If dgvStudents.Rows.Count = 0 Then
                MessageBox.Show("No student records to export.",
                                "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                sfd.FileName = $"StudentRecords_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                sfd.Title = "Export Student Records to CSV"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim sb As New System.Text.StringBuilder()

                    ' Write column headers
                    Dim headers As New List(Of String)
                    For Each col As DataGridViewColumn In dgvStudents.Columns
                        If col.Visible Then headers.Add($"""{col.HeaderText}""")
                    Next
                    sb.AppendLine(String.Join(",", headers))

                    ' Write data rows
                    For Each row As DataGridViewRow In dgvStudents.Rows
                        Dim cells As New List(Of String)
                        For Each col As DataGridViewColumn In dgvStudents.Columns
                            If col.Visible Then
                                Dim value = If(row.Cells(col.Index).Value?.ToString(), "")
                                ' Escape double quotes and wrap in quotes
                                value = $"""{value.Replace("""", """""")}"""
                                cells.Add(value)
                            End If
                        Next
                        sb.AppendLine(String.Join(",", cells))
                    Next

                    IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8)

                    MessageBox.Show($"Records exported successfully!" & vbCrLf &
                                   $"File: {sfd.FileName}" & vbCrLf &
                                   $"Total records: {dgvStudents.Rows.Count}",
                                   "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UpdateStatus($"Exported {dgvStudents.Rows.Count} records to CSV")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error exporting records: {ex.Message}",
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Opens a Print Preview dialog with a formatted student report.
    ''' </summary>
    Private Sub PrintReport()
        Try
            ' Ensure we have data to print
            If pnlStudentMgmt.Visible = False Then
                ShowStudentManagement()
            End If

            If dgvStudents.Rows.Count = 0 Then
                MessageBox.Show("No student records to print.",
                                "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            printPageIndex = 0
            Dim printDoc As New Printing.PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintDoc_PrintPage

            Using ppd As New PrintPreviewDialog()
                ppd.Document = printDoc
                ppd.Width = 950
                ppd.Height = 650
                ppd.ShowDialog()
            End Using

            UpdateStatus("Print preview displayed")
        Catch ex As Exception
            MessageBox.Show($"Error generating print report: {ex.Message}",
                            "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' PrintPage event handler - draws the student report page by page.
    ''' Handles pagination for large datasets.
    ''' </summary>
    Private Sub PrintDoc_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        Dim g = e.Graphics
        Dim yPos As Single = 50
        Dim leftMargin As Single = 50
        Dim pageWidth As Single = e.PageBounds.Width - 100

        ' --- Page Header (only on first page or if continuing) ---
        If printPageIndex = 0 Then
            ' Report title
            Using titleFont As New Font("Segoe UI", 16, FontStyle.Bold)
                g.DrawString("Student Record Management System", titleFont,
                             Brushes.DarkSlateBlue, leftMargin, yPos)
            End Using
            yPos += 30

            ' Subtitle
            Using subFont As New Font("Segoe UI", 11)
                g.DrawString("Student Report", subFont, Brushes.Gray, leftMargin, yPos)
            End Using
            yPos += 25

            ' Generation date
            Using dateFont As New Font("Segoe UI", 9)
                g.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}  |  " &
                             $"Total Students: {dgvStudents.Rows.Count}",
                             dateFont, Brushes.Gray, leftMargin, yPos)
            End Using
            yPos += 25

            ' Horizontal rule
            Using pen As New Pen(Color.DarkSlateBlue, 2)
                g.DrawLine(pen, leftMargin, yPos, leftMargin + pageWidth, yPos)
            End Using
            yPos += 15
        End If

        ' --- Column Headers ---
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

        ' Header line
        Using pen As New Pen(Color.LightGray, 1)
            g.DrawLine(pen, leftMargin, yPos, leftMargin + pageWidth, yPos)
        End Using
        yPos += 5

        ' --- Data Rows ---
        Using dataFont As New Font("Segoe UI", 8.5F)
            While printPageIndex < dgvStudents.Rows.Count
                ' Check for page break
                If yPos > e.PageBounds.Height - 60 Then
                    e.HasMorePages = True
                    Return
                End If

                Dim row = dgvStudents.Rows(printPageIndex)
                Dim xPos As Single = leftMargin

                Dim values = {
                    row.Cells("StudentID").Value?.ToString(),
                    row.Cells("FirstName").Value?.ToString(),
                    row.Cells("LastName").Value?.ToString(),
                    row.Cells("Gender").Value?.ToString(),
                    row.Cells("Department").Value?.ToString(),
                    row.Cells("PhoneNumber").Value?.ToString(),
                    row.Cells("Email").Value?.ToString()
                }

                For i = 0 To Math.Min(values.Length, colWidths.Length) - 1
                    g.DrawString(If(values(i), ""), dataFont, Brushes.Black, xPos, yPos)
                    xPos += colWidths(i)
                Next

                yPos += 18
                printPageIndex += 1
            End While
        End Using

        ' --- Page Footer ---
        Using footerFont As New Font("Segoe UI", 8)
            g.DrawString($"Total Students: {dgvStudents.Rows.Count}  |  Page",
                         footerFont, Brushes.Gray, leftMargin, e.PageBounds.Height - 40)
        End Using

        ' Reset for potential reprint
        printPageIndex = 0
        e.HasMorePages = False
    End Sub

#End Region

#Region "UI Helper Factories - Control Creation Methods"

    ''' <summary>
    ''' Creates a styled navigation sidebar button with hover effects.
    ''' </summary>
    Private Function CreateNavButton(text As String) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.TextAlign = ContentAlignment.MiddleLeft
        btn.Dock = DockStyle.Top
        btn.Height = 48
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = ClrNavHover
        btn.FlatAppearance.MouseDownBackColor = ClrPrimary
        btn.BackColor = ClrNavBg
        btn.ForeColor = Color.FromArgb(189, 195, 199)
        btn.Font = New Font("Segoe UI", 10.5F, FontStyle.Regular)
        btn.Cursor = Cursors.Hand
        btn.Padding = New Padding(20, 0, 0, 0)
        Return btn
    End Function

    ''' <summary>
    ''' Creates a styled action button (Add, Update, Delete, Clear) with
    ''' flat design, hover effects, and consistent sizing.
    ''' </summary>
    Private Function CreateActionButton(text As String, bgColor As Color) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Size = New Size(155, 38)
        btn.Margin = New Padding(5, 3, 5, 3)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(bgColor, 0.15F)
        btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(bgColor, 0.1F)
        btn.BackColor = bgColor
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI Semibold", 9.5F)
        btn.Cursor = Cursors.Hand
        Return btn
    End Function

    ''' <summary>
    ''' Creates a styled label for form field captions.
    ''' </summary>
    Private Function CreateFieldLabel(text As String) As Label
        Dim lbl As New Label()
        lbl.Text = text
        lbl.Font = New Font("Segoe UI Semibold", 9.0F)
        lbl.ForeColor = ClrTextDark
        lbl.BackColor = Color.Transparent
        lbl.Dock = DockStyle.Fill
        lbl.TextAlign = ContentAlignment.BottomLeft
        lbl.Padding = New Padding(5, 0, 0, 0)
        lbl.Margin = New Padding(3)
        Return lbl
    End Function

    ''' <summary>
    ''' Creates a consistently styled TextBox for form inputs.
    ''' </summary>
    Private Function CreateStyledTextBox() As TextBox
        Dim txt As New TextBox()
        txt.Font = New Font("Segoe UI", 10.0F)
        txt.Dock = DockStyle.Fill
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.Margin = New Padding(5, 3, 5, 3)
        txt.BackColor = Color.White
        Return txt
    End Function

    ''' <summary>
    ''' Creates a consistently styled ComboBox with predefined items.
    ''' </summary>
    Private Function CreateStyledComboBox(items As String()) As ComboBox
        Dim cbo As New ComboBox()
        cbo.Font = New Font("Segoe UI", 10.0F)
        cbo.Dock = DockStyle.Fill
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.Margin = New Padding(5, 3, 5, 3)
        cbo.BackColor = Color.White
        cbo.Items.AddRange(items)
        Return cbo
    End Function

    ''' <summary>
    ''' Creates a dashboard statistics card panel with accent color,
    ''' count display, and title label. The count Label is stored in
    ''' Panel.Tag for external reference.
    ''' </summary>
    Private Function CreateStatCard(title As String, accentColor As Color) As Panel
        Dim card As New Panel()
        card.Size = New Size(250, 140)
        card.Margin = New Padding(0, 0, 20, 0)
        card.BackColor = ClrCardBg
        card.Padding = New Padding(15)

        ' Top accent line
        Dim accent As New Panel()
        accent.Dock = DockStyle.Top
        accent.Height = 4
        accent.BackColor = accentColor
        card.Controls.Add(accent)

        ' Count number (large, colored)
        Dim lblCount As New Label()
        lblCount.Text = "0"
        lblCount.Font = New Font("Segoe UI", 32.0F, FontStyle.Bold)
        lblCount.ForeColor = accentColor
        lblCount.BackColor = Color.Transparent
        lblCount.Location = New Point(20, 18)
        lblCount.AutoSize = True
        card.Controls.Add(lblCount)

        ' Title label
        Dim lblTitle As New Label()
        lblTitle.Text = title
        lblTitle.Font = New Font("Segoe UI", 10.0F)
        lblTitle.ForeColor = ClrTextMuted
        lblTitle.BackColor = Color.Transparent
        lblTitle.Location = New Point(20, 95)
        lblTitle.AutoSize = True
        card.Controls.Add(lblTitle)

        ' Store count label reference in Tag for external access
        card.Tag = lblCount

        Return card
    End Function

#End Region

End Class
