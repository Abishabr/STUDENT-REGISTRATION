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
    Private Shared ReadOnly ClrPrimaryDark As Color = Color.FromArgb(25, 42, 86)
    Private Shared ReadOnly ClrPrimary As Color = Color.FromArgb(41, 128, 185)
    Private Shared ReadOnly ClrPrimaryLight As Color = Color.FromArgb(52, 152, 219)
    Private Shared ReadOnly ClrAccent As Color = Color.FromArgb(0, 188, 212)
    Private Shared ReadOnly ClrSuccess As Color = Color.FromArgb(39, 174, 96)
    Private Shared ReadOnly ClrDanger As Color = Color.FromArgb(231, 76, 60)
    Private Shared ReadOnly ClrWarning As Color = Color.FromArgb(243, 156, 18)
    Private Shared ReadOnly ClrBgLight As Color = Color.FromArgb(240, 243, 247)
    Private Shared ReadOnly ClrCardBg As Color = Color.White
    Private Shared ReadOnly ClrTextDark As Color = Color.FromArgb(44, 62, 80)
    Private Shared ReadOnly ClrTextMuted As Color = Color.FromArgb(127, 140, 141)
    Private Shared ReadOnly ClrNavBg As Color = Color.FromArgb(30, 39, 73)
    Private Shared ReadOnly ClrNavHover As Color = Color.FromArgb(45, 60, 110)
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
        SetupUI()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DatabaseHelper.InitializeDatabase()
            ShowDashboard()
            Me.KeyPreview = True
            UpdateStatus("Application loaded successfully")
        Catch ex As Exception
            MessageBox.Show($"Error initializing application: {ex.Message}", "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Case Keys.S ' Save or Add
                    If pnlAddEditStudent.Visible Then
                        If isEditMode Then UpdateStudentRecord() Else AddStudent()
                    Else
                        ShowAddStudent()
                    End If
                    e.SuppressKeyPress = True
                Case Keys.U ' Update (switch to view)
                    ShowViewStudents()
                    e.SuppressKeyPress = True
                Case Keys.F ' Focus Search
                    ShowViewStudents()
                    txtSearch.Focus()
                    txtSearch.SelectAll()
                    e.SuppressKeyPress = True
                Case Keys.N ' New 
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
    Private Sub SetupUI()
        formErrorProvider = New ErrorProvider()
        formErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        SetupContentPanel()
        SetupNavPanel()
        SetupHeaderPanel()
        SetupStatusStrip()

        ' Build the pages
        SetupDashboardPanel()
        SetupAddEditPanel()
        SetupViewPanel()
    End Sub

    Private Sub SetupHeaderPanel()
        pnlHeader = New Panel() With {.Dock = DockStyle.Top, .Height = 60, .BackColor = ClrPrimaryDark}
        Dim accentLine As New Panel() With {.Dock = DockStyle.Bottom, .Height = 3, .BackColor = ClrAccent}
        pnlHeader.Controls.Add(accentLine)
        Dim lblTitle As New Label() With {.Text = "  Student Record Management System", .Font = New Font("Segoe UI Semibold", 16.0F), .ForeColor = Color.White, .BackColor = Color.Transparent, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft, .Padding = New Padding(15, 0, 0, 0)}
        pnlHeader.Controls.Add(lblTitle)
        Me.Controls.Add(pnlHeader)
    End Sub

    Private Sub SetupNavPanel()
        pnlNav = New Panel() With {.Dock = DockStyle.Left, .Width = 230, .BackColor = ClrNavBg}

        btnNavDashboard = CreateNavButton("    Dashboard")
        btnNavAdd = CreateNavButton("    Add New Student")
        btnNavView = CreateNavButton("    View All Students")
        btnNavSearch = CreateNavButton("    Search Students")
        btnNavExport = CreateNavButton("    Export to CSV")
        btnNavPrint = CreateNavButton("    Print Report")
        btnNavExit = CreateNavButton("    Exit Application")
        btnNavExit.ForeColor = Color.FromArgb(231, 76, 60)

        Dim separator As New Panel() With {.Dock = DockStyle.Top, .Height = 1, .BackColor = Color.FromArgb(50, 65, 110), .Margin = New Padding(15, 5, 15, 5)}

        ' Click Handlers
        AddHandler btnNavDashboard.Click, Sub(s, ev) ShowDashboard()
        AddHandler btnNavAdd.Click, Sub(s, ev) ShowAddStudent()
        AddHandler btnNavView.Click, Sub(s, ev) ShowViewStudents()
        AddHandler btnNavSearch.Click, Sub(s, ev)
                                           ShowViewStudents()
                                           txtSearch.Focus()
                                           txtSearch.SelectAll()
                                       End Sub
        AddHandler btnNavExport.Click, Sub(s, ev) ExportToCSV()
        AddHandler btnNavPrint.Click, Sub(s, ev) PrintReport()
        AddHandler btnNavExit.Click, Sub(s, ev) Me.Close()

        ' Dock buttons
        btnNavExit.Dock = DockStyle.Bottom
        pnlNav.Controls.Add(btnNavExit)
        pnlNav.Controls.Add(btnNavPrint)
        pnlNav.Controls.Add(btnNavExport)
        pnlNav.Controls.Add(separator)
        pnlNav.Controls.Add(btnNavSearch)
        pnlNav.Controls.Add(btnNavView)
        pnlNav.Controls.Add(btnNavAdd)
        pnlNav.Controls.Add(btnNavDashboard)

        Dim navHeader As New Panel() With {.Dock = DockStyle.Top, .Height = 55, .BackColor = Color.FromArgb(22, 33, 62)}
        Dim lblNavBrand As New Label() With {.Text = "  SRMS", .Font = New Font("Segoe UI Semibold", 15.0F), .ForeColor = ClrAccent, .BackColor = Color.Transparent, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}
        navHeader.Controls.Add(lblNavBrand)
        Dim navAccent As New Panel() With {.Dock = DockStyle.Bottom, .Height = 1, .BackColor = Color.FromArgb(50, 65, 110)}
        navHeader.Controls.Add(navAccent)
        pnlNav.Controls.Add(navHeader)
        Me.Controls.Add(pnlNav)
    End Sub

    Private Sub SetupContentPanel()
        pnlContent = New Panel() With {.Dock = DockStyle.Fill, .BackColor = ClrBgLight, .Padding = New Padding(20)}
        Me.Controls.Add(pnlContent)
    End Sub

    Private Sub SetupStatusStrip()
        mainStatusStrip = New StatusStrip() With {.BackColor = ClrPrimaryDark, .SizingGrip = False}
        statusLabel = New ToolStripStatusLabel() With {.Text = "  Ready", .ForeColor = Color.White, .Font = New Font("Segoe UI", 9.0F)}
        mainStatusStrip.Items.Add(statusLabel)
        Dim lblShortcuts As New ToolStripStatusLabel() With {.Text = "Ctrl+S Save/Add | Ctrl+U Update | Ctrl+F Search | Ctrl+E Export | Ctrl+P Print", .ForeColor = Color.FromArgb(140, 189, 195, 199), .Font = New Font("Segoe UI", 8.0F), .Spring = True, .TextAlign = ContentAlignment.MiddleRight}
        mainStatusStrip.Items.Add(lblShortcuts)
        Me.Controls.Add(mainStatusStrip)
    End Sub
#End Region

#Region "UI Setup - Dashboard Panel"
    Private Sub SetupDashboardPanel()
        pnlDashboard = New Panel() With {.Dock = DockStyle.Fill, .BackColor = ClrBgLight, .Visible = True, .Padding = New Padding(10)}

        ' Hero Banner
        Dim pnlHero As New Panel() With {.Dock = DockStyle.Top, .Height = 130}
        AddHandler pnlHero.Paint, Sub(s, e)
              Dim g = e.Graphics
              g.SmoothingMode = SmoothingMode.AntiAlias
              g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
              Dim rect As New Rectangle(0, 0, pnlHero.Width, pnlHero.Height)
              Using brush As New LinearGradientBrush(rect, Color.FromArgb(30, 39, 73), Color.FromArgb(41, 128, 185), LinearGradientMode.Horizontal)
                  g.FillRectangle(brush, rect)
              End Using
              Using brush As New SolidBrush(Color.FromArgb(15, 255, 255, 255))
                  g.FillEllipse(brush, pnlHero.Width - 150, -50, 250, 250)
                  g.FillEllipse(brush, pnlHero.Width - 300, 20, 100, 100)
              End Using
              Using font As New Font("Segoe UI Semibold", 22.0F)
                  g.DrawString("Welcome Back, Administrator", font, Brushes.White, 30, 25)
              End Using
              Using font As New Font("Segoe UI", 11.0F)
                  Using sBrush As New SolidBrush(Color.FromArgb(220, 255, 255, 255))
                      g.DrawString("Manage your institution's student records efficiently and securely.", font, sBrush, 35, 75)
                  End Using
              End Using
          End Sub

        ' Spacing
        Dim pnlSpacing1 As New Panel() With {.Dock = DockStyle.Top, .Height = 25, .BackColor = Color.Transparent}

        ' Action Cards Container
        Dim pnlCards As New FlowLayoutPanel() With {.Dock = DockStyle.Top, .Height = 170, .FlowDirection = FlowDirection.LeftToRight, .WrapContents = False, .BackColor = Color.Transparent, .Padding = New Padding(5, 5, 0, 10)}
        
        Dim cardTotal = CreateStatCard("TOTAL STUDENTS", Color.FromArgb(41, 128, 185), Color.FromArgb(0, 188, 212))
        Dim cardMale = CreateStatCard("MALE STUDENTS", Color.FromArgb(39, 174, 96), Color.FromArgb(46, 204, 113))
        Dim cardFemale = CreateStatCard("FEMALE STUDENTS", Color.FromArgb(211, 84, 0), Color.FromArgb(243, 156, 18))

        lblTotalCount = DirectCast(cardTotal.Tag, Label)
        lblMaleCount = DirectCast(cardMale.Tag, Label)
        lblFemaleCount = DirectCast(cardFemale.Tag, Label)

        pnlCards.Controls.Add(cardTotal)
        pnlCards.Controls.Add(cardMale)
        pnlCards.Controls.Add(cardFemale)

        ' System Shortcuts container
        Dim pnlSpacing2 As New Panel() With {.Dock = DockStyle.Top, .Height = 10, .BackColor = Color.Transparent}
        Dim pnlQuickActions As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .Padding = New Padding(30)}
        
        Dim lblQATitle As New Label() With {.Text = "System Shortcuts & Information", .Font = New Font("Segoe UI Semibold", 14.0F), .ForeColor = ClrTextDark, .BackColor = Color.Transparent, .Dock = DockStyle.Top, .Height = 40, .TextAlign = ContentAlignment.MiddleLeft}
        Dim lblQADesc As New Label() With {
            .Text = "Manage records with a full suite of highly responsive tools built for educational administrators." & vbCrLf & vbCrLf &
            "   •  Add New Student  —  Register new admissions with complete profile details" & vbCrLf &
            "   •  View & Manage    —  Access the student grid to edit or remove records" & vbCrLf &
            "   •  Export & Print   —  Create physical backups or share spreadsheets instantly" & vbCrLf & vbCrLf &
            "PRO TIP: Use keyboard shortcuts like Ctrl+S (Save), Ctrl+F (Search), or Ctrl+E (Export) to speed up your workflow.",
            .Font = New Font("Segoe UI", 11.0F), .ForeColor = Color.FromArgb(100, 110, 120), .BackColor = Color.Transparent, .Dock = DockStyle.Fill, .Padding = New Padding(10, 10, 0, 0)}
        
        pnlQuickActions.Controls.Add(lblQADesc)
        pnlQuickActions.Controls.Add(lblQATitle)

        ' Reverse dock order for Top
        pnlDashboard.Controls.Add(pnlQuickActions)
        pnlDashboard.Controls.Add(pnlSpacing2)
        pnlDashboard.Controls.Add(pnlCards)
        pnlDashboard.Controls.Add(pnlSpacing1)
        pnlDashboard.Controls.Add(pnlHero)

        pnlContent.Controls.Add(pnlDashboard)
    End Sub
#End Region

#Region "UI Setup - Add/Edit Student Page"
    Private Sub SetupAddEditPanel()
        pnlAddEditStudent = New Panel() With {.Dock = DockStyle.Fill, .BackColor = ClrBgLight, .Visible = False, .Padding = New Padding(40, 30, 40, 30)}

        ' Premium Form Card
        Dim pnlFormCard As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 520,
            .BackColor = Color.White
        }
        
        AddHandler pnlFormCard.Paint, Sub(s, e)
             Dim g = e.Graphics
             Using pen As New Pen(Color.FromArgb(220, 225, 230), 1)
                 g.DrawRectangle(pen, 0, 0, pnlFormCard.Width - 1, pnlFormCard.Height - 1)
             End Using
        End Sub

        ' 1. Header inside the Card
        Dim pnlFormHeader As New Panel() With {.Dock = DockStyle.Top, .Height = 75, .BackColor = Color.Transparent}

        lblAddEditTitle = New Label() With {.Text = "Add New Student Record", .Font = New Font("Segoe UI Semibold", 18.0F), .ForeColor = ClrPrimaryDark, .BackColor = Color.Transparent, .Dock = DockStyle.Left, .TextAlign = ContentAlignment.MiddleLeft, .Padding = New Padding(30, 0, 0, 0), .AutoSize = True}
        pnlFormHeader.Controls.Add(lblAddEditTitle)

        lblSelectedID = New Label() With {.Text = "New Record", .Font = New Font("Segoe UI Semibold", 11.0F), .ForeColor = ClrAccent, .BackColor = Color.FromArgb(240, 248, 255), .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter, .AutoSize = False}
        ' Wrap label to simulate padding/margin alignment
        Dim lblIdWrapper As New Panel() With {.Dock = DockStyle.Right, .Width = 180, .Padding = New Padding(20, 20, 20, 20)}
        lblIdWrapper.Controls.Add(lblSelectedID)
        pnlFormHeader.Controls.Add(lblIdWrapper)

        Dim headerLine As New Panel() With {.Dock = DockStyle.Bottom, .Height = 1, .BackColor = Color.FromArgb(230, 235, 240)}
        pnlFormHeader.Controls.Add(headerLine)

        ' 2. Form Fields Grid
        Dim pnlFormFields = CreateFormFieldsPanel()

        ' 3. Action Buttons Section (Bottom bordered area)
        Dim pnlActions As New Panel() With {.Dock = DockStyle.Bottom, .Height = 85, .BackColor = Color.FromArgb(250, 251, 252), .Padding = New Padding(30, 20, 30, 20)}
        Dim actionBorder As New Panel() With {.Dock = DockStyle.Top, .Height = 1, .BackColor = Color.FromArgb(230, 235, 240)}
        pnlActions.Controls.Add(actionBorder)
        
        Dim flowActions As New FlowLayoutPanel() With {.Dock = DockStyle.Right, .Width = 400, .FlowDirection = FlowDirection.RightToLeft, .WrapContents = False}
        
        btnSaveMode = CreateActionButton("Save Record", ClrSuccess)
        btnSaveMode.Size = New Size(160, 42)
        btnSaveMode.Font = New Font("Segoe UI Semibold", 10.5F)
        
        btnCancelMode = CreateActionButton("Clear Form", ClrTextMuted)
        btnCancelMode.Size = New Size(140, 42)
        btnCancelMode.Font = New Font("Segoe UI Semibold", 10.5F)
        btnCancelMode.BackColor = Color.White
        btnCancelMode.ForeColor = ClrTextDark
        btnCancelMode.FlatAppearance.BorderColor = Color.FromArgb(200, 205, 210)
        btnCancelMode.FlatAppearance.BorderSize = 1
        
        AddHandler btnSaveMode.Click, Sub(s, ev)
                                          If isEditMode Then UpdateStudentRecord() Else AddStudent()
                                      End Sub
        AddHandler btnCancelMode.Click, Sub(s, ev)
                                            If isEditMode Then ShowViewStudents() Else ClearFields()
                                        End Sub

        flowActions.Controls.Add(btnSaveMode)
        flowActions.Controls.Add(New Panel() With {.Width = 15, .Height = 10, .BackColor = Color.Transparent})
        flowActions.Controls.Add(btnCancelMode)
        pnlActions.Controls.Add(flowActions)

        ' Combine all into Card
        pnlFormCard.Controls.Add(pnlFormFields) ' Fill
        pnlFormCard.Controls.Add(pnlActions)    ' Bottom
        pnlFormCard.Controls.Add(pnlFormHeader) ' Top

        pnlAddEditStudent.Controls.Add(pnlFormCard)
        pnlContent.Controls.Add(pnlAddEditStudent)
    End Sub

    Private Function CreateFormFieldsPanel() As Panel
        Dim pnl As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White, .Padding = New Padding(10, 20, 10, 20)}
        
        Dim tbl As New TableLayoutPanel() With {.Dock = DockStyle.Fill, .ColumnCount = 3, .RowCount = 6, .Padding = New Padding(20, 0, 20, 0)}
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.34F))
        
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 55))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 55))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 30))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 55))

        tbl.Controls.Add(CreateFieldLabel("First Name *"), 0, 0)
        tbl.Controls.Add(CreateFieldLabel("Last Name *"), 1, 0)
        tbl.Controls.Add(CreateFieldLabel("Gender *"), 2, 0)

        txtFirstName = CreateStyledTextBox()
        tbl.Controls.Add(txtFirstName, 0, 1)
        txtLastName = CreateStyledTextBox()
        tbl.Controls.Add(txtLastName, 1, 1)
        cboGender = CreateStyledComboBox({"Male", "Female", "Other"})
        tbl.Controls.Add(cboGender, 2, 1)

        tbl.Controls.Add(CreateFieldLabel("Date of Birth *"), 0, 2)
        tbl.Controls.Add(CreateFieldLabel("Department *"), 1, 2)
        tbl.Controls.Add(CreateFieldLabel("Phone Number"), 2, 2)

        dtpDateOfBirth = New DateTimePicker() With {.Format = DateTimePickerFormat.Short, .Font = New Font("Segoe UI", 11.0F), .Dock = DockStyle.Fill, .Margin = New Padding(8, 5, 20, 5)}
        tbl.Controls.Add(dtpDateOfBirth, 0, 3)
        cboDepartment = CreateStyledComboBox({"Computer Science", "Information Technology", "Electrical Engineering", "Mechanical Engineering", "Civil Engineering", "Business Administration", "Mathematics", "Physics", "Biology", "Chemistry"})
        tbl.Controls.Add(cboDepartment, 1, 3)
        txtPhone = CreateStyledTextBox()
        tbl.Controls.Add(txtPhone, 2, 3)

        tbl.Controls.Add(CreateFieldLabel("Email Address"), 0, 4)
        tbl.Controls.Add(CreateFieldLabel("Address"), 1, 4)

        txtEmail = CreateStyledTextBox()
        tbl.Controls.Add(txtEmail, 0, 5)
        txtAddress = CreateStyledTextBox()
        tbl.SetColumnSpan(txtAddress, 2)
        tbl.Controls.Add(txtAddress, 1, 5)

        pnl.Controls.Add(tbl)
        Return pnl
    End Function
#End Region

#Region "UI Setup - View Students Page"
    Private Sub SetupViewPanel()
        pnlViewStudents = New Panel() With {.Dock = DockStyle.Fill, .BackColor = ClrBgLight, .Visible = False, .Padding = New Padding(40, 30, 40, 30)}

        Dim pnlGridCard As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        AddHandler pnlGridCard.Paint, Sub(s, e)
             Dim g = e.Graphics
             Using pen As New Pen(Color.FromArgb(220, 225, 230), 1)
                 g.DrawRectangle(pen, 0, 0, pnlGridCard.Width - 1, pnlGridCard.Height - 1)
             End Using
        End Sub

        ' 1. Title Header inside the Card
        Dim pnlHeader As New Panel() With {.Dock = DockStyle.Top, .Height = 70, .BackColor = Color.Transparent}
        Dim lblTitle As New Label() With {.Text = "Manage Student Records", .Font = New Font("Segoe UI Semibold", 18.0F), .ForeColor = ClrPrimaryDark, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft, .Padding = New Padding(20, 0, 0, 0)}
        pnlHeader.Controls.Add(lblTitle)
        
        Dim headerLine As New Panel() With {.Dock = DockStyle.Bottom, .Height = 1, .BackColor = Color.FromArgb(230, 235, 240)}
        pnlHeader.Controls.Add(headerLine)

        ' 2. Toolbar
        Dim pnlToolbar As New Panel() With {.Dock = DockStyle.Top, .Height = 70, .BackColor = Color.White, .Padding = New Padding(20, 15, 20, 15)}
        
        btnDeleteSelected = CreateActionButton("Delete Record", ClrDanger)
        btnDeleteSelected.Size = New Size(140, 38)
        btnDeleteSelected.Dock = DockStyle.Right
        AddHandler btnDeleteSelected.Click, Sub(s, ev) DeleteSelectedStudent()
        
        Dim marginPanel1 As New Panel() With {.Width = 10, .Dock = DockStyle.Right}

        btnEditSelected = CreateActionButton("Edit Record", ClrPrimary)
        btnEditSelected.Size = New Size(140, 38)
        btnEditSelected.Dock = DockStyle.Right
        AddHandler btnEditSelected.Click, Sub(s, ev) EditSelectedStudent()

        ' Modern Search Box
        Dim pnlSearchContainer As New Panel() With {.Dock = DockStyle.Left, .Width = 400, .Padding = New Padding(0, 1, 0, 1)}
        Dim pnlSearchInner As New Panel() With {.Dock = DockStyle.Fill, .BackColor = Color.FromArgb(248, 249, 250), .Padding = New Padding(12, 7, 10, 5), .Cursor = Cursors.IBeam}
        AddHandler pnlSearchInner.Paint, Sub(s, e)
              Dim g = e.Graphics
              Using pen As New Pen(Color.FromArgb(215, 220, 225), 1)
                  g.DrawRectangle(pen, 0, 0, pnlSearchInner.Width - 1, pnlSearchInner.Height - 1)
              End Using
        End Sub

        Dim lblIcon As New Label() With {.Text = "🔍", .Dock = DockStyle.Left, .Width = 28, .Font = New Font("Segoe UI", 10.0F), .ForeColor = Color.FromArgb(170, 180, 190), .BackColor = Color.Transparent, .TextAlign = ContentAlignment.TopCenter, .Padding = New Padding(0, 2, 0, 0)}
        AddHandler lblIcon.Click, Sub(s, e) txtSearch.Focus()
        
        txtSearch = New TextBox() With {
            .Font = New Font("Segoe UI", 10.5F), 
            .BorderStyle = BorderStyle.None, 
            .Dock = DockStyle.Fill, 
            .BackColor = Color.FromArgb(248, 249, 250),
            .PlaceholderText = "Search by Name, ID, or Department..."
        }
        
        AddHandler pnlSearchInner.Click, Sub(s, e) txtSearch.Focus()
        AddHandler txtSearch.TextChanged, Sub(s, ev) SearchStudents()
        
        pnlSearchInner.Controls.Add(lblIcon)
        pnlSearchInner.Controls.Add(txtSearch)
        txtSearch.BringToFront()
        
        pnlSearchContainer.Controls.Add(pnlSearchInner)

        pnlToolbar.Controls.Add(btnEditSelected)
        pnlToolbar.Controls.Add(marginPanel1)
        pnlToolbar.Controls.Add(btnDeleteSelected)
        pnlToolbar.Controls.Add(pnlSearchContainer)

        ' 3. Data Grid
        SetupDataGridView()
        Dim gridWrapper As New Panel() With {.Dock = DockStyle.Fill, .Padding = New Padding(0, 0, 0, 20)}
        gridWrapper.Controls.Add(dgvStudents)

        pnlGridCard.Controls.Add(gridWrapper)
        pnlGridCard.Controls.Add(pnlToolbar)
        pnlGridCard.Controls.Add(pnlHeader)

        pnlViewStudents.Controls.Add(pnlGridCard)
        pnlContent.Controls.Add(pnlViewStudents)
    End Sub

    Private Sub SetupDataGridView()
        dgvStudents = New DataGridView() With {.Dock = DockStyle.Fill, .BackgroundColor = Color.White, .BorderStyle = BorderStyle.None, .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal, .GridColor = Color.FromArgb(235, 238, 242)}
        dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252)
        dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 90, 100)
        dgvStudents.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.5F)
        dgvStudents.ColumnHeadersDefaultCellStyle.Padding = New Padding(15, 5, 10, 5)
        dgvStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dgvStudents.ColumnHeadersHeight = 45
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvStudents.EnableHeadersVisualStyles = False
        dgvStudents.DefaultCellStyle.Font = New Font("Segoe UI", 10.0F)
        dgvStudents.DefaultCellStyle.Padding = New Padding(15, 2, 10, 2)
        dgvStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 245, 255)
        dgvStudents.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 40, 50)
        dgvStudents.RowTemplate.Height = 45
        dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvStudents.ReadOnly = True
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.AllowUserToResizeRows = False
        dgvStudents.MultiSelect = False
        dgvStudents.RowHeadersVisible = False

        ' Double click opens Edit mode
        AddHandler dgvStudents.CellDoubleClick, Sub(s, ev)
                                                    If ev.RowIndex >= 0 Then EditSelectedStudent()
                                                End Sub
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

        Dim emailError = ValidationHelper.ValidateEmail(txtEmail.Text)
        If Not String.IsNullOrEmpty(emailError) Then
            formErrorProvider.SetError(txtEmail, emailError)
            isValid = False
        End If

        Dim phoneError = ValidationHelper.ValidatePhone(txtPhone.Text)
        If Not String.IsNullOrEmpty(phoneError) Then
            formErrorProvider.SetError(txtPhone, phoneError)
            isValid = False
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
