<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        formErrorProvider = New ErrorProvider(components)
        pnlHeader = New Panel()
        lblTitle = New Label()
        accentLine = New Panel()
        pnlNav = New Panel()
        btnNavExit = New Button()
        btnNavPrint = New Button()
        btnNavExport = New Button()
        separator = New Panel()
        btnNavView = New Button()
        btnNavAdd = New Button()
        btnNavDashboard = New Button()
        navHeader = New Panel()
        navAccent = New Panel()
        lblNavBrand = New Label()
        pnlContent = New Panel()
        pnlDashboard = New Panel()
        pnlQuickActions = New Panel()
        lblQADesc = New Label()
        lblQATitle = New Label()
        pnlSpacing2 = New Panel()
        pnlCards = New FlowLayoutPanel()
        cardTotal = New Panel()
        lblTotalCount = New Label()
        lblTotalTitle = New Label()
        cardMale = New Panel()
        lblMaleCount = New Label()
        lblMaleTitle = New Label()
        cardFemale = New Panel()
        lblFemaleCount = New Label()
        lblFemaleTitle = New Label()
        pnlSpacing1 = New Panel()
        pnlHero = New Panel()
        lblHeroTitle = New Label()
        lblHeroSubtitle = New Label()
        pnlAddEditStudent = New Panel()
        pnlFormCard = New Panel()
        pnlFormFields = New Panel()
        tblForm = New TableLayoutPanel()
        lblFirstName = New Label()
        lblLastName = New Label()
        lblGender = New Label()
        txtFirstName = New TextBox()
        txtLastName = New TextBox()
        cboGender = New ComboBox()
        lblDOB = New Label()
        lblDepartment = New Label()
        lblPhone = New Label()
        dtpDateOfBirth = New DateTimePicker()
        cboDepartment = New ComboBox()
        txtPhone = New TextBox()
        lblEmail = New Label()
        lblAddress = New Label()
        txtEmail = New TextBox()
        txtAddress = New TextBox()
        pnlActions = New Panel()
        actionBorder = New Panel()
        flowActions = New FlowLayoutPanel()
        btnSaveMode = New Button()
        marginBtn = New Panel()
        btnCancelMode = New Button()
        pnlFormHeader = New Panel()
        lblAddEditTitle = New Label()
        lblIdWrapper = New Panel()
        lblSelectedID = New Label()
        headerLine = New Panel()
        pnlViewStudents = New Panel()
        pnlGridCard = New Panel()
        gridWrapper = New Panel()
        dgvStudents = New DataGridView()
        pnlToolbar = New Panel()
        btnEditSelected = New Button()
        marginPanel1 = New Panel()
        btnDeleteSelected = New Button()
        pnlSearchContainer = New Panel()
        pnlSearchInner = New Panel()
        lblSearchIcon = New Label()
        lblSearchPlaceholder = New Label()
        txtSearch = New TextBox()
        pnlViewHeader = New Panel()
        lblViewTitle = New Label()
        viewHeaderLine = New Panel()
        mainStatusStrip = New StatusStrip()
        statusLabel = New ToolStripStatusLabel()
        lblShortcuts = New ToolStripStatusLabel()
        CType(formErrorProvider, ComponentModel.ISupportInitialize).BeginInit()
        pnlHeader.SuspendLayout()
        pnlNav.SuspendLayout()
        navHeader.SuspendLayout()
        pnlContent.SuspendLayout()
        pnlDashboard.SuspendLayout()
        pnlQuickActions.SuspendLayout()
        pnlCards.SuspendLayout()
        cardTotal.SuspendLayout()
        cardMale.SuspendLayout()
        cardFemale.SuspendLayout()
        pnlHero.SuspendLayout()
        pnlAddEditStudent.SuspendLayout()
        pnlFormCard.SuspendLayout()
        pnlFormFields.SuspendLayout()
        tblForm.SuspendLayout()
        pnlActions.SuspendLayout()
        flowActions.SuspendLayout()
        pnlFormHeader.SuspendLayout()
        lblIdWrapper.SuspendLayout()
        pnlViewStudents.SuspendLayout()
        pnlGridCard.SuspendLayout()
        gridWrapper.SuspendLayout()
        CType(dgvStudents, ComponentModel.ISupportInitialize).BeginInit()
        pnlToolbar.SuspendLayout()
        pnlSearchContainer.SuspendLayout()
        pnlSearchInner.SuspendLayout()
        pnlViewHeader.SuspendLayout()
        mainStatusStrip.SuspendLayout()
        SuspendLayout()
        ' 
        ' formErrorProvider
        ' 
        formErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
        formErrorProvider.ContainerControl = Me
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.FromArgb(CByte(25), CByte(42), CByte(86))
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.Controls.Add(accentLine)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(1400, 60)
        pnlHeader.TabIndex = 2
        ' 
        ' lblTitle
        ' 
        lblTitle.BackColor = Color.Transparent
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Font = New Font("Segoe UI Semibold", 16F)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(0, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Padding = New Padding(15, 0, 0, 0)
        lblTitle.Size = New Size(1400, 57)
        lblTitle.TabIndex = 0
        lblTitle.Text = "  Student Record Management System"
        lblTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' accentLine
        ' 
        accentLine.BackColor = Color.FromArgb(CByte(0), CByte(188), CByte(212))
        accentLine.Dock = DockStyle.Bottom
        accentLine.Location = New Point(0, 57)
        accentLine.Name = "accentLine"
        accentLine.Size = New Size(1400, 3)
        accentLine.TabIndex = 1
        ' 
        ' pnlNav
        ' 
        pnlNav.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        pnlNav.Controls.Add(btnNavExit)
        pnlNav.Controls.Add(btnNavPrint)
        pnlNav.Controls.Add(btnNavExport)
        pnlNav.Controls.Add(separator)
        pnlNav.Controls.Add(btnNavView)
        pnlNav.Controls.Add(btnNavAdd)
        pnlNav.Controls.Add(btnNavDashboard)
        pnlNav.Controls.Add(navHeader)
        pnlNav.Dock = DockStyle.Left
        pnlNav.Location = New Point(0, 60)
        pnlNav.Name = "pnlNav"
        pnlNav.Size = New Size(230, 764)
        pnlNav.TabIndex = 1
        ' 
        ' btnNavExit
        ' 
        btnNavExit.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavExit.Cursor = Cursors.Hand
        btnNavExit.Dock = DockStyle.Bottom
        btnNavExit.FlatAppearance.BorderSize = 0
        btnNavExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavExit.FlatStyle = FlatStyle.Flat
        btnNavExit.Font = New Font("Segoe UI", 10.5F)
        btnNavExit.ForeColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnNavExit.Location = New Point(0, 716)
        btnNavExit.Name = "btnNavExit"
        btnNavExit.Padding = New Padding(20, 0, 0, 0)
        btnNavExit.Size = New Size(230, 48)
        btnNavExit.TabIndex = 0
        btnNavExit.Text = "    Exit Application"
        btnNavExit.TextAlign = ContentAlignment.MiddleLeft
        btnNavExit.UseVisualStyleBackColor = False
        ' 
        ' btnNavPrint
        ' 
        btnNavPrint.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavPrint.Cursor = Cursors.Hand
        btnNavPrint.Dock = DockStyle.Top
        btnNavPrint.FlatAppearance.BorderSize = 0
        btnNavPrint.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavPrint.FlatStyle = FlatStyle.Flat
        btnNavPrint.Font = New Font("Segoe UI", 10.5F)
        btnNavPrint.ForeColor = Color.FromArgb(CByte(189), CByte(195), CByte(199))
        btnNavPrint.Location = New Point(0, 248)
        btnNavPrint.Name = "btnNavPrint"
        btnNavPrint.Padding = New Padding(20, 0, 0, 0)
        btnNavPrint.Size = New Size(230, 48)
        btnNavPrint.TabIndex = 1
        btnNavPrint.Text = "    Print Report"
        btnNavPrint.TextAlign = ContentAlignment.MiddleLeft
        btnNavPrint.UseVisualStyleBackColor = False
        ' 
        ' btnNavExport
        ' 
        btnNavExport.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavExport.Cursor = Cursors.Hand
        btnNavExport.Dock = DockStyle.Top
        btnNavExport.FlatAppearance.BorderSize = 0
        btnNavExport.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavExport.FlatStyle = FlatStyle.Flat
        btnNavExport.Font = New Font("Segoe UI", 10.5F)
        btnNavExport.ForeColor = Color.FromArgb(CByte(189), CByte(195), CByte(199))
        btnNavExport.Location = New Point(0, 200)
        btnNavExport.Name = "btnNavExport"
        btnNavExport.Padding = New Padding(20, 0, 0, 0)
        btnNavExport.Size = New Size(230, 48)
        btnNavExport.TabIndex = 2
        btnNavExport.Text = "    Export to CSV"
        btnNavExport.TextAlign = ContentAlignment.MiddleLeft
        btnNavExport.UseVisualStyleBackColor = False
        ' 
        ' separator
        ' 
        separator.BackColor = Color.FromArgb(CByte(50), CByte(65), CByte(110))
        separator.Dock = DockStyle.Top
        separator.Location = New Point(0, 199)
        separator.Margin = New Padding(15, 5, 15, 5)
        separator.Name = "separator"
        separator.Size = New Size(230, 1)
        separator.TabIndex = 3
        ' 
        ' btnNavView
        ' 
        btnNavView.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavView.Cursor = Cursors.Hand
        btnNavView.Dock = DockStyle.Top
        btnNavView.FlatAppearance.BorderSize = 0
        btnNavView.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavView.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavView.FlatStyle = FlatStyle.Flat
        btnNavView.Font = New Font("Segoe UI", 10.5F)
        btnNavView.ForeColor = Color.FromArgb(CByte(189), CByte(195), CByte(199))
        btnNavView.Location = New Point(0, 151)
        btnNavView.Name = "btnNavView"
        btnNavView.Padding = New Padding(20, 0, 0, 0)
        btnNavView.Size = New Size(230, 48)
        btnNavView.TabIndex = 4
        btnNavView.Text = "    View All Students"
        btnNavView.TextAlign = ContentAlignment.MiddleLeft
        btnNavView.UseVisualStyleBackColor = False
        ' 
        ' btnNavAdd
        ' 
        btnNavAdd.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavAdd.Cursor = Cursors.Hand
        btnNavAdd.Dock = DockStyle.Top
        btnNavAdd.FlatAppearance.BorderSize = 0
        btnNavAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavAdd.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavAdd.FlatStyle = FlatStyle.Flat
        btnNavAdd.Font = New Font("Segoe UI", 10.5F)
        btnNavAdd.ForeColor = Color.FromArgb(CByte(189), CByte(195), CByte(199))
        btnNavAdd.Location = New Point(0, 103)
        btnNavAdd.Name = "btnNavAdd"
        btnNavAdd.Padding = New Padding(20, 0, 0, 0)
        btnNavAdd.Size = New Size(230, 48)
        btnNavAdd.TabIndex = 5
        btnNavAdd.Text = "    Add New Student"
        btnNavAdd.TextAlign = ContentAlignment.MiddleLeft
        btnNavAdd.UseVisualStyleBackColor = False
        ' 
        ' btnNavDashboard
        ' 
        btnNavDashboard.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(73))
        btnNavDashboard.Cursor = Cursors.Hand
        btnNavDashboard.Dock = DockStyle.Top
        btnNavDashboard.FlatAppearance.BorderSize = 0
        btnNavDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnNavDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(45), CByte(60), CByte(110))
        btnNavDashboard.FlatStyle = FlatStyle.Flat
        btnNavDashboard.Font = New Font("Segoe UI", 10.5F)
        btnNavDashboard.ForeColor = Color.FromArgb(CByte(189), CByte(195), CByte(199))
        btnNavDashboard.Location = New Point(0, 55)
        btnNavDashboard.Name = "btnNavDashboard"
        btnNavDashboard.Padding = New Padding(20, 0, 0, 0)
        btnNavDashboard.Size = New Size(230, 48)
        btnNavDashboard.TabIndex = 6
        btnNavDashboard.Text = "    Dashboard"
        btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft
        btnNavDashboard.UseVisualStyleBackColor = False
        ' 
        ' navHeader
        ' 
        navHeader.BackColor = Color.FromArgb(CByte(22), CByte(33), CByte(62))
        navHeader.Controls.Add(navAccent)
        navHeader.Controls.Add(lblNavBrand)
        navHeader.Dock = DockStyle.Top
        navHeader.Location = New Point(0, 0)
        navHeader.Name = "navHeader"
        navHeader.Size = New Size(230, 55)
        navHeader.TabIndex = 7
        ' 
        ' navAccent
        ' 
        navAccent.BackColor = Color.FromArgb(CByte(50), CByte(65), CByte(110))
        navAccent.Dock = DockStyle.Bottom
        navAccent.Location = New Point(0, 54)
        navAccent.Name = "navAccent"
        navAccent.Size = New Size(230, 1)
        navAccent.TabIndex = 0
        ' 
        ' lblNavBrand
        ' 
        lblNavBrand.BackColor = Color.Transparent
        lblNavBrand.Dock = DockStyle.Fill
        lblNavBrand.Font = New Font("Segoe UI Semibold", 15F)
        lblNavBrand.ForeColor = Color.FromArgb(CByte(0), CByte(188), CByte(212))
        lblNavBrand.Location = New Point(0, 0)
        lblNavBrand.Name = "lblNavBrand"
        lblNavBrand.Size = New Size(230, 55)
        lblNavBrand.TabIndex = 1
        lblNavBrand.Text = "  SRMS"
        lblNavBrand.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlContent
        ' 
        pnlContent.BackColor = Color.FromArgb(CByte(240), CByte(243), CByte(247))
        pnlContent.Controls.Add(pnlDashboard)
        pnlContent.Controls.Add(pnlAddEditStudent)
        pnlContent.Controls.Add(pnlViewStudents)
        pnlContent.Dock = DockStyle.Fill
        pnlContent.Location = New Point(230, 60)
        pnlContent.Name = "pnlContent"
        pnlContent.Padding = New Padding(20)
        pnlContent.Size = New Size(1170, 764)
        pnlContent.TabIndex = 0
        ' 
        ' pnlDashboard
        ' 
        pnlDashboard.BackColor = Color.FromArgb(CByte(240), CByte(243), CByte(247))
        pnlDashboard.Controls.Add(pnlQuickActions)
        pnlDashboard.Controls.Add(pnlSpacing2)
        pnlDashboard.Controls.Add(pnlCards)
        pnlDashboard.Controls.Add(pnlSpacing1)
        pnlDashboard.Controls.Add(pnlHero)
        pnlDashboard.Dock = DockStyle.Fill
        pnlDashboard.Location = New Point(20, 20)
        pnlDashboard.Name = "pnlDashboard"
        pnlDashboard.Padding = New Padding(10)
        pnlDashboard.Size = New Size(1130, 724)
        pnlDashboard.TabIndex = 0
        ' 
        ' pnlQuickActions
        ' 
        pnlQuickActions.BackColor = Color.White
        pnlQuickActions.Controls.Add(lblQADesc)
        pnlQuickActions.Controls.Add(lblQATitle)
        pnlQuickActions.Dock = DockStyle.Fill
        pnlQuickActions.Location = New Point(10, 345)
        pnlQuickActions.Name = "pnlQuickActions"
        pnlQuickActions.Padding = New Padding(30)
        pnlQuickActions.Size = New Size(1110, 369)
        pnlQuickActions.TabIndex = 0
        ' 
        ' lblQADesc
        ' 
        lblQADesc.BackColor = Color.Transparent
        lblQADesc.Dock = DockStyle.Fill
        lblQADesc.Font = New Font("Segoe UI", 11F)
        lblQADesc.ForeColor = Color.FromArgb(CByte(100), CByte(110), CByte(120))
        lblQADesc.Location = New Point(30, 70)
        lblQADesc.Name = "lblQADesc"
        lblQADesc.Padding = New Padding(10, 10, 0, 0)
        lblQADesc.Size = New Size(1050, 269)
        lblQADesc.TabIndex = 0
        lblQADesc.Text = resources.GetString("lblQADesc.Text")
        ' 
        ' lblQATitle
        ' 
        lblQATitle.BackColor = Color.Transparent
        lblQATitle.Dock = DockStyle.Top
        lblQATitle.Font = New Font("Segoe UI Semibold", 14F)
        lblQATitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblQATitle.Location = New Point(30, 30)
        lblQATitle.Name = "lblQATitle"
        lblQATitle.Size = New Size(1050, 40)
        lblQATitle.TabIndex = 1
        lblQATitle.Text = "System Shortcuts & Information"
        lblQATitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlSpacing2
        ' 
        pnlSpacing2.BackColor = Color.Transparent
        pnlSpacing2.Dock = DockStyle.Top
        pnlSpacing2.Location = New Point(10, 335)
        pnlSpacing2.Name = "pnlSpacing2"
        pnlSpacing2.Size = New Size(1110, 10)
        pnlSpacing2.TabIndex = 1
        ' 
        ' pnlCards
        ' 
        pnlCards.BackColor = Color.Transparent
        pnlCards.Controls.Add(cardTotal)
        pnlCards.Controls.Add(cardMale)
        pnlCards.Controls.Add(cardFemale)
        pnlCards.Dock = DockStyle.Top
        pnlCards.Location = New Point(10, 165)
        pnlCards.Name = "pnlCards"
        pnlCards.Padding = New Padding(5, 5, 0, 10)
        pnlCards.Size = New Size(1110, 170)
        pnlCards.TabIndex = 2
        pnlCards.WrapContents = False
        ' 
        ' cardTotal
        ' 
        cardTotal.BackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        cardTotal.Controls.Add(lblTotalCount)
        cardTotal.Controls.Add(lblTotalTitle)
        cardTotal.Location = New Point(5, 5)
        cardTotal.Margin = New Padding(0, 0, 25, 0)
        cardTotal.Name = "cardTotal"
        cardTotal.Size = New Size(280, 130)
        cardTotal.TabIndex = 0
        ' 
        ' lblTotalCount
        ' 
        lblTotalCount.AutoSize = True
        lblTotalCount.BackColor = Color.Transparent
        lblTotalCount.Font = New Font("Segoe UI", 36F, FontStyle.Bold)
        lblTotalCount.ForeColor = Color.White
        lblTotalCount.Location = New Point(25, 15)
        lblTotalCount.Name = "lblTotalCount"
        lblTotalCount.Size = New Size(70, 81)
        lblTotalCount.TabIndex = 0
        lblTotalCount.Text = "0"
        ' 
        ' lblTotalTitle
        ' 
        lblTotalTitle.AutoSize = True
        lblTotalTitle.BackColor = Color.Transparent
        lblTotalTitle.Font = New Font("Segoe UI Semibold", 11F)
        lblTotalTitle.ForeColor = Color.FromArgb(CByte(230), CByte(255), CByte(255), CByte(255))
        lblTotalTitle.Location = New Point(30, 96)
        lblTotalTitle.Name = "lblTotalTitle"
        lblTotalTitle.Size = New Size(162, 25)
        lblTotalTitle.TabIndex = 1
        lblTotalTitle.Text = "TOTAL STUDENTS"
        ' 
        ' cardMale
        ' 
        cardMale.BackColor = Color.FromArgb(CByte(39), CByte(174), CByte(96))
        cardMale.Controls.Add(lblMaleCount)
        cardMale.Controls.Add(lblMaleTitle)
        cardMale.Location = New Point(310, 5)
        cardMale.Margin = New Padding(0, 0, 25, 0)
        cardMale.Name = "cardMale"
        cardMale.Size = New Size(280, 130)
        cardMale.TabIndex = 1
        ' 
        ' lblMaleCount
        ' 
        lblMaleCount.AutoSize = True
        lblMaleCount.BackColor = Color.Transparent
        lblMaleCount.Font = New Font("Segoe UI", 36F, FontStyle.Bold)
        lblMaleCount.ForeColor = Color.White
        lblMaleCount.Location = New Point(25, 15)
        lblMaleCount.Name = "lblMaleCount"
        lblMaleCount.Size = New Size(70, 81)
        lblMaleCount.TabIndex = 0
        lblMaleCount.Text = "0"
        ' 
        ' lblMaleTitle
        ' 
        lblMaleTitle.AutoSize = True
        lblMaleTitle.BackColor = Color.Transparent
        lblMaleTitle.Font = New Font("Segoe UI Semibold", 11F)
        lblMaleTitle.ForeColor = Color.FromArgb(CByte(230), CByte(255), CByte(255), CByte(255))
        lblMaleTitle.Location = New Point(25, 100)
        lblMaleTitle.Name = "lblMaleTitle"
        lblMaleTitle.Size = New Size(159, 25)
        lblMaleTitle.TabIndex = 1
        lblMaleTitle.Text = "MALE STUDENTS"
        ' 
        ' cardFemale
        ' 
        cardFemale.BackColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        cardFemale.Controls.Add(lblFemaleCount)
        cardFemale.Controls.Add(lblFemaleTitle)
        cardFemale.Location = New Point(615, 5)
        cardFemale.Margin = New Padding(0, 0, 25, 0)
        cardFemale.Name = "cardFemale"
        cardFemale.Size = New Size(280, 130)
        cardFemale.TabIndex = 2
        ' 
        ' lblFemaleCount
        ' 
        lblFemaleCount.AutoSize = True
        lblFemaleCount.BackColor = Color.Transparent
        lblFemaleCount.Font = New Font("Segoe UI", 36F, FontStyle.Bold)
        lblFemaleCount.ForeColor = Color.White
        lblFemaleCount.Location = New Point(25, 15)
        lblFemaleCount.Name = "lblFemaleCount"
        lblFemaleCount.Size = New Size(70, 81)
        lblFemaleCount.TabIndex = 0
        lblFemaleCount.Text = "0"
        ' 
        ' lblFemaleTitle
        ' 
        lblFemaleTitle.AutoSize = True
        lblFemaleTitle.BackColor = Color.Transparent
        lblFemaleTitle.Font = New Font("Segoe UI Semibold", 11F)
        lblFemaleTitle.ForeColor = Color.FromArgb(CByte(230), CByte(255), CByte(255), CByte(255))
        lblFemaleTitle.Location = New Point(25, 96)
        lblFemaleTitle.Name = "lblFemaleTitle"
        lblFemaleTitle.Size = New Size(179, 25)
        lblFemaleTitle.TabIndex = 1
        lblFemaleTitle.Text = "FEMALE STUDENTS"
        ' 
        ' pnlSpacing1
        ' 
        pnlSpacing1.BackColor = Color.Transparent
        pnlSpacing1.Dock = DockStyle.Top
        pnlSpacing1.Location = New Point(10, 140)
        pnlSpacing1.Name = "pnlSpacing1"
        pnlSpacing1.Size = New Size(1110, 25)
        pnlSpacing1.TabIndex = 3
        ' 
        ' pnlHero
        ' 
        pnlHero.BackColor = Color.FromArgb(CByte(25), CByte(42), CByte(86))
        pnlHero.Controls.Add(lblHeroTitle)
        pnlHero.Controls.Add(lblHeroSubtitle)
        pnlHero.Dock = DockStyle.Top
        pnlHero.Location = New Point(10, 10)
        pnlHero.Name = "pnlHero"
        pnlHero.Size = New Size(1110, 130)
        pnlHero.TabIndex = 4
        ' 
        ' lblHeroTitle
        ' 
        lblHeroTitle.AutoSize = True
        lblHeroTitle.BackColor = Color.Transparent
        lblHeroTitle.Font = New Font("Segoe UI Semibold", 22F)
        lblHeroTitle.ForeColor = Color.White
        lblHeroTitle.Location = New Point(30, 25)
        lblHeroTitle.Name = "lblHeroTitle"
        lblHeroTitle.Size = New Size(519, 50)
        lblHeroTitle.TabIndex = 0
        lblHeroTitle.Text = "Welcome Back, Administrator"
        ' 
        ' lblHeroSubtitle
        ' 
        lblHeroSubtitle.AutoSize = True
        lblHeroSubtitle.BackColor = Color.Transparent
        lblHeroSubtitle.Font = New Font("Segoe UI", 11F)
        lblHeroSubtitle.ForeColor = Color.FromArgb(CByte(220), CByte(255), CByte(255), CByte(255))
        lblHeroSubtitle.Location = New Point(35, 75)
        lblHeroSubtitle.Name = "lblHeroSubtitle"
        lblHeroSubtitle.Size = New Size(564, 25)
        lblHeroSubtitle.TabIndex = 1
        lblHeroSubtitle.Text = "Manage your institution's student records efficiently and securely."
        ' 
        ' pnlAddEditStudent
        ' 
        pnlAddEditStudent.BackColor = Color.FromArgb(CByte(240), CByte(243), CByte(247))
        pnlAddEditStudent.Controls.Add(pnlFormCard)
        pnlAddEditStudent.Dock = DockStyle.Fill
        pnlAddEditStudent.Location = New Point(20, 20)
        pnlAddEditStudent.Name = "pnlAddEditStudent"
        pnlAddEditStudent.Padding = New Padding(40, 30, 40, 30)
        pnlAddEditStudent.Size = New Size(1130, 724)
        pnlAddEditStudent.TabIndex = 1
        pnlAddEditStudent.Visible = False
        ' 
        ' pnlFormCard
        ' 
        pnlFormCard.BackColor = Color.White
        pnlFormCard.Controls.Add(pnlFormFields)
        pnlFormCard.Controls.Add(pnlActions)
        pnlFormCard.Controls.Add(pnlFormHeader)
        pnlFormCard.Dock = DockStyle.Top
        pnlFormCard.Location = New Point(40, 30)
        pnlFormCard.Name = "pnlFormCard"
        pnlFormCard.Size = New Size(1050, 520)
        pnlFormCard.TabIndex = 0
        ' 
        ' pnlFormFields
        ' 
        pnlFormFields.BackColor = Color.White
        pnlFormFields.Controls.Add(tblForm)
        pnlFormFields.Dock = DockStyle.Fill
        pnlFormFields.Location = New Point(0, 75)
        pnlFormFields.Name = "pnlFormFields"
        pnlFormFields.Padding = New Padding(10, 20, 10, 20)
        pnlFormFields.Size = New Size(1050, 360)
        pnlFormFields.TabIndex = 0
        ' 
        ' tblForm
        ' 
        tblForm.ColumnCount = 3
        tblForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tblForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.33F))
        tblForm.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.34F))
        tblForm.Controls.Add(lblFirstName, 0, 0)
        tblForm.Controls.Add(lblLastName, 1, 0)
        tblForm.Controls.Add(lblGender, 2, 0)
        tblForm.Controls.Add(txtFirstName, 0, 1)
        tblForm.Controls.Add(txtLastName, 1, 1)
        tblForm.Controls.Add(cboGender, 2, 1)
        tblForm.Controls.Add(lblDOB, 0, 2)
        tblForm.Controls.Add(lblDepartment, 1, 2)
        tblForm.Controls.Add(lblPhone, 2, 2)
        tblForm.Controls.Add(dtpDateOfBirth, 0, 3)
        tblForm.Controls.Add(cboDepartment, 1, 3)
        tblForm.Controls.Add(txtPhone, 2, 3)
        tblForm.Controls.Add(lblEmail, 0, 4)
        tblForm.Controls.Add(lblAddress, 1, 4)
        tblForm.Controls.Add(txtEmail, 0, 5)
        tblForm.Controls.Add(txtAddress, 1, 5)
        tblForm.Dock = DockStyle.Fill
        tblForm.Location = New Point(10, 20)
        tblForm.Name = "tblForm"
        tblForm.Padding = New Padding(20, 0, 20, 0)
        tblForm.RowCount = 6
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 55F))
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 55F))
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tblForm.RowStyles.Add(New RowStyle(SizeType.Absolute, 55F))
        tblForm.Size = New Size(1030, 320)
        tblForm.TabIndex = 0
        ' 
        ' lblFirstName
        ' 
        lblFirstName.BackColor = Color.Transparent
        lblFirstName.Dock = DockStyle.Fill
        lblFirstName.Font = New Font("Segoe UI Semibold", 9.5F)
        lblFirstName.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblFirstName.Location = New Point(23, 0)
        lblFirstName.Name = "lblFirstName"
        lblFirstName.Padding = New Padding(8, 0, 0, 5)
        lblFirstName.Size = New Size(323, 30)
        lblFirstName.TabIndex = 0
        lblFirstName.Text = "First Name *"
        lblFirstName.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblLastName
        ' 
        lblLastName.BackColor = Color.Transparent
        lblLastName.Dock = DockStyle.Fill
        lblLastName.Font = New Font("Segoe UI Semibold", 9.5F)
        lblLastName.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblLastName.Location = New Point(352, 0)
        lblLastName.Name = "lblLastName"
        lblLastName.Padding = New Padding(8, 0, 0, 5)
        lblLastName.Size = New Size(323, 30)
        lblLastName.TabIndex = 1
        lblLastName.Text = "Last Name *"
        lblLastName.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblGender
        ' 
        lblGender.BackColor = Color.Transparent
        lblGender.Dock = DockStyle.Fill
        lblGender.Font = New Font("Segoe UI Semibold", 9.5F)
        lblGender.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblGender.Location = New Point(681, 0)
        lblGender.Name = "lblGender"
        lblGender.Padding = New Padding(8, 0, 0, 5)
        lblGender.Size = New Size(326, 30)
        lblGender.TabIndex = 2
        lblGender.Text = "Gender *"
        lblGender.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' txtFirstName
        ' 
        txtFirstName.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        txtFirstName.BorderStyle = BorderStyle.FixedSingle
        txtFirstName.Dock = DockStyle.Fill
        txtFirstName.Font = New Font("Segoe UI", 11F)
        txtFirstName.Location = New Point(28, 35)
        txtFirstName.Margin = New Padding(8, 5, 20, 5)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(301, 32)
        txtFirstName.TabIndex = 3
        ' 
        ' txtLastName
        ' 
        txtLastName.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        txtLastName.BorderStyle = BorderStyle.FixedSingle
        txtLastName.Dock = DockStyle.Fill
        txtLastName.Font = New Font("Segoe UI", 11F)
        txtLastName.Location = New Point(357, 35)
        txtLastName.Margin = New Padding(8, 5, 20, 5)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(301, 32)
        txtLastName.TabIndex = 4
        ' 
        ' cboGender
        ' 
        cboGender.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        cboGender.Dock = DockStyle.Fill
        cboGender.DropDownStyle = ComboBoxStyle.DropDownList
        cboGender.Font = New Font("Segoe UI", 11F)
        cboGender.Items.AddRange(New Object() {"Male", "Female"})
        cboGender.Location = New Point(686, 35)
        cboGender.Margin = New Padding(8, 5, 20, 5)
        cboGender.Name = "cboGender"
        cboGender.Size = New Size(304, 33)
        cboGender.TabIndex = 5
        ' 
        ' lblDOB
        ' 
        lblDOB.BackColor = Color.Transparent
        lblDOB.Dock = DockStyle.Fill
        lblDOB.Font = New Font("Segoe UI Semibold", 9.5F)
        lblDOB.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblDOB.Location = New Point(23, 85)
        lblDOB.Name = "lblDOB"
        lblDOB.Padding = New Padding(8, 0, 0, 5)
        lblDOB.Size = New Size(323, 30)
        lblDOB.TabIndex = 6
        lblDOB.Text = "Date of Birth *"
        lblDOB.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblDepartment
        ' 
        lblDepartment.BackColor = Color.Transparent
        lblDepartment.Dock = DockStyle.Fill
        lblDepartment.Font = New Font("Segoe UI Semibold", 9.5F)
        lblDepartment.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblDepartment.Location = New Point(352, 85)
        lblDepartment.Name = "lblDepartment"
        lblDepartment.Padding = New Padding(8, 0, 0, 5)
        lblDepartment.Size = New Size(323, 30)
        lblDepartment.TabIndex = 7
        lblDepartment.Text = "Department *"
        lblDepartment.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblPhone
        ' 
        lblPhone.BackColor = Color.Transparent
        lblPhone.Dock = DockStyle.Fill
        lblPhone.Font = New Font("Segoe UI Semibold", 9.5F)
        lblPhone.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblPhone.Location = New Point(681, 85)
        lblPhone.Name = "lblPhone"
        lblPhone.Padding = New Padding(8, 0, 0, 5)
        lblPhone.Size = New Size(326, 30)
        lblPhone.TabIndex = 8
        lblPhone.Text = "Phone Number"
        lblPhone.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' dtpDateOfBirth
        ' 
        dtpDateOfBirth.Dock = DockStyle.Fill
        dtpDateOfBirth.Font = New Font("Segoe UI", 11F)
        dtpDateOfBirth.Format = DateTimePickerFormat.Short
        dtpDateOfBirth.Location = New Point(28, 120)
        dtpDateOfBirth.Margin = New Padding(8, 5, 20, 5)
        dtpDateOfBirth.Name = "dtpDateOfBirth"
        dtpDateOfBirth.Size = New Size(301, 32)
        dtpDateOfBirth.TabIndex = 9
        ' 
        ' cboDepartment
        ' 
        cboDepartment.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        cboDepartment.Dock = DockStyle.Fill
        cboDepartment.DropDownStyle = ComboBoxStyle.DropDownList
        cboDepartment.Font = New Font("Segoe UI", 11F)
        cboDepartment.Items.AddRange(New Object() {"Computer Science", "Information Technology", "Electrical Engineering", "Mechanical Engineering", "Civil Engineering", "Business Administration", "Mathematics", "Physics", "Biology", "Chemistry"})
        cboDepartment.Location = New Point(357, 120)
        cboDepartment.Margin = New Padding(8, 5, 20, 5)
        cboDepartment.Name = "cboDepartment"
        cboDepartment.Size = New Size(301, 33)
        cboDepartment.TabIndex = 10
        ' 
        ' txtPhone
        ' 
        txtPhone.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        txtPhone.BorderStyle = BorderStyle.FixedSingle
        txtPhone.Dock = DockStyle.Fill
        txtPhone.Font = New Font("Segoe UI", 11F)
        txtPhone.Location = New Point(686, 120)
        txtPhone.Margin = New Padding(8, 5, 20, 5)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(304, 32)
        txtPhone.TabIndex = 11
        ' 
        ' lblEmail
        ' 
        lblEmail.BackColor = Color.Transparent
        lblEmail.Dock = DockStyle.Fill
        lblEmail.Font = New Font("Segoe UI Semibold", 9.5F)
        lblEmail.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblEmail.Location = New Point(23, 170)
        lblEmail.Name = "lblEmail"
        lblEmail.Padding = New Padding(8, 0, 0, 5)
        lblEmail.Size = New Size(323, 30)
        lblEmail.TabIndex = 12
        lblEmail.Text = "Email Address"
        lblEmail.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblAddress
        ' 
        lblAddress.BackColor = Color.Transparent
        lblAddress.Dock = DockStyle.Fill
        lblAddress.Font = New Font("Segoe UI Semibold", 9.5F)
        lblAddress.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        lblAddress.Location = New Point(352, 170)
        lblAddress.Name = "lblAddress"
        lblAddress.Padding = New Padding(8, 0, 0, 5)
        lblAddress.Size = New Size(323, 30)
        lblAddress.TabIndex = 13
        lblAddress.Text = "Address"
        lblAddress.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' txtEmail
        ' 
        txtEmail.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Dock = DockStyle.Fill
        txtEmail.Font = New Font("Segoe UI", 11F)
        txtEmail.Location = New Point(28, 205)
        txtEmail.Margin = New Padding(8, 5, 20, 5)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(301, 32)
        txtEmail.TabIndex = 14
        ' 
        ' txtAddress
        ' 
        txtAddress.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        txtAddress.BorderStyle = BorderStyle.FixedSingle
        tblForm.SetColumnSpan(txtAddress, 2)
        txtAddress.Dock = DockStyle.Fill
        txtAddress.Font = New Font("Segoe UI", 11F)
        txtAddress.Location = New Point(357, 205)
        txtAddress.Margin = New Padding(8, 5, 20, 5)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(633, 32)
        txtAddress.TabIndex = 15
        ' 
        ' pnlActions
        ' 
        pnlActions.BackColor = Color.FromArgb(CByte(250), CByte(251), CByte(252))
        pnlActions.Controls.Add(actionBorder)
        pnlActions.Controls.Add(flowActions)
        pnlActions.Dock = DockStyle.Bottom
        pnlActions.Location = New Point(0, 435)
        pnlActions.Name = "pnlActions"
        pnlActions.Padding = New Padding(30, 20, 30, 20)
        pnlActions.Size = New Size(1050, 85)
        pnlActions.TabIndex = 1
        ' 
        ' actionBorder
        ' 
        actionBorder.BackColor = Color.FromArgb(CByte(230), CByte(235), CByte(240))
        actionBorder.Dock = DockStyle.Top
        actionBorder.Location = New Point(30, 20)
        actionBorder.Name = "actionBorder"
        actionBorder.Size = New Size(590, 1)
        actionBorder.TabIndex = 0
        ' 
        ' flowActions
        ' 
        flowActions.Controls.Add(btnSaveMode)
        flowActions.Controls.Add(marginBtn)
        flowActions.Controls.Add(btnCancelMode)
        flowActions.Dock = DockStyle.Right
        flowActions.FlowDirection = FlowDirection.RightToLeft
        flowActions.Location = New Point(620, 20)
        flowActions.Name = "flowActions"
        flowActions.Size = New Size(400, 45)
        flowActions.TabIndex = 1
        flowActions.WrapContents = False
        ' 
        ' btnSaveMode
        ' 
        btnSaveMode.BackColor = Color.FromArgb(CByte(39), CByte(174), CByte(96))
        btnSaveMode.Cursor = Cursors.Hand
        btnSaveMode.FlatAppearance.BorderSize = 0
        btnSaveMode.FlatStyle = FlatStyle.Flat
        btnSaveMode.Font = New Font("Segoe UI Semibold", 10.5F)
        btnSaveMode.ForeColor = Color.White
        btnSaveMode.Location = New Point(235, 3)
        btnSaveMode.Margin = New Padding(5, 3, 5, 3)
        btnSaveMode.Name = "btnSaveMode"
        btnSaveMode.Size = New Size(160, 42)
        btnSaveMode.TabIndex = 0
        btnSaveMode.Text = "Save Record"
        btnSaveMode.UseVisualStyleBackColor = False
        ' 
        ' marginBtn
        ' 
        marginBtn.BackColor = Color.Transparent
        marginBtn.Location = New Point(212, 3)
        marginBtn.Name = "marginBtn"
        marginBtn.Size = New Size(15, 10)
        marginBtn.TabIndex = 1
        ' 
        ' btnCancelMode
        ' 
        btnCancelMode.BackColor = Color.White
        btnCancelMode.Cursor = Cursors.Hand
        btnCancelMode.FlatAppearance.BorderColor = Color.FromArgb(CByte(200), CByte(205), CByte(210))
        btnCancelMode.FlatStyle = FlatStyle.Flat
        btnCancelMode.Font = New Font("Segoe UI Semibold", 10.5F)
        btnCancelMode.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        btnCancelMode.Location = New Point(64, 3)
        btnCancelMode.Margin = New Padding(5, 3, 5, 3)
        btnCancelMode.Name = "btnCancelMode"
        btnCancelMode.Size = New Size(140, 42)
        btnCancelMode.TabIndex = 2
        btnCancelMode.Text = "Clear Form"
        btnCancelMode.UseVisualStyleBackColor = False
        ' 
        ' pnlFormHeader
        ' 
        pnlFormHeader.BackColor = Color.Transparent
        pnlFormHeader.Controls.Add(lblAddEditTitle)
        pnlFormHeader.Controls.Add(lblIdWrapper)
        pnlFormHeader.Controls.Add(headerLine)
        pnlFormHeader.Dock = DockStyle.Top
        pnlFormHeader.Location = New Point(0, 0)
        pnlFormHeader.Name = "pnlFormHeader"
        pnlFormHeader.Size = New Size(1050, 75)
        pnlFormHeader.TabIndex = 2
        ' 
        ' lblAddEditTitle
        ' 
        lblAddEditTitle.AutoSize = True
        lblAddEditTitle.BackColor = Color.Transparent
        lblAddEditTitle.Dock = DockStyle.Left
        lblAddEditTitle.Font = New Font("Segoe UI Semibold", 18F)
        lblAddEditTitle.ForeColor = Color.FromArgb(CByte(25), CByte(42), CByte(86))
        lblAddEditTitle.Location = New Point(0, 0)
        lblAddEditTitle.Name = "lblAddEditTitle"
        lblAddEditTitle.Padding = New Padding(30, 0, 0, 0)
        lblAddEditTitle.Size = New Size(392, 41)
        lblAddEditTitle.TabIndex = 0
        lblAddEditTitle.Text = "Add New Student Record"
        lblAddEditTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblIdWrapper
        ' 
        lblIdWrapper.Controls.Add(lblSelectedID)
        lblIdWrapper.Dock = DockStyle.Right
        lblIdWrapper.Location = New Point(870, 0)
        lblIdWrapper.Name = "lblIdWrapper"
        lblIdWrapper.Padding = New Padding(20)
        lblIdWrapper.Size = New Size(180, 74)
        lblIdWrapper.TabIndex = 1
        ' 
        ' lblSelectedID
        ' 
        lblSelectedID.BackColor = Color.FromArgb(CByte(240), CByte(248), CByte(255))
        lblSelectedID.Dock = DockStyle.Fill
        lblSelectedID.Font = New Font("Segoe UI Semibold", 11F)
        lblSelectedID.ForeColor = Color.FromArgb(CByte(0), CByte(188), CByte(212))
        lblSelectedID.Location = New Point(20, 20)
        lblSelectedID.Name = "lblSelectedID"
        lblSelectedID.Size = New Size(140, 34)
        lblSelectedID.TabIndex = 0
        lblSelectedID.Text = "New Record"
        lblSelectedID.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' headerLine
        ' 
        headerLine.BackColor = Color.FromArgb(CByte(230), CByte(235), CByte(240))
        headerLine.Dock = DockStyle.Bottom
        headerLine.Location = New Point(0, 74)
        headerLine.Name = "headerLine"
        headerLine.Size = New Size(1050, 1)
        headerLine.TabIndex = 2
        ' 
        ' pnlViewStudents
        ' 
        pnlViewStudents.BackColor = Color.FromArgb(CByte(240), CByte(243), CByte(247))
        pnlViewStudents.Controls.Add(pnlGridCard)
        pnlViewStudents.Dock = DockStyle.Fill
        pnlViewStudents.Location = New Point(20, 20)
        pnlViewStudents.Name = "pnlViewStudents"
        pnlViewStudents.Padding = New Padding(40, 30, 40, 30)
        pnlViewStudents.Size = New Size(1130, 724)
        pnlViewStudents.TabIndex = 2
        pnlViewStudents.Visible = False
        ' 
        ' pnlGridCard
        ' 
        pnlGridCard.BackColor = Color.White
        pnlGridCard.Controls.Add(gridWrapper)
        pnlGridCard.Controls.Add(pnlToolbar)
        pnlGridCard.Controls.Add(pnlViewHeader)
        pnlGridCard.Dock = DockStyle.Fill
        pnlGridCard.Location = New Point(40, 30)
        pnlGridCard.Name = "pnlGridCard"
        pnlGridCard.Size = New Size(1050, 664)
        pnlGridCard.TabIndex = 0
        ' 
        ' gridWrapper
        ' 
        gridWrapper.Controls.Add(dgvStudents)
        gridWrapper.Dock = DockStyle.Fill
        gridWrapper.Location = New Point(0, 140)
        gridWrapper.Name = "gridWrapper"
        gridWrapper.Padding = New Padding(0, 0, 0, 20)
        gridWrapper.Size = New Size(1050, 524)
        gridWrapper.TabIndex = 0
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = Color.White
        dgvStudents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvStudents.BackgroundColor = Color.White
        dgvStudents.BorderStyle = BorderStyle.None
        dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(245), CByte(248), CByte(252))
        DataGridViewCellStyle2.Font = New Font("Segoe UI Semibold", 10.5F)
        DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(80), CByte(90), CByte(100))
        DataGridViewCellStyle2.Padding = New Padding(15, 5, 10, 5)
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvStudents.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvStudents.ColumnHeadersHeight = 45
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 10F)
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.Padding = New Padding(15, 2, 10, 2)
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(235), CByte(245), CByte(255))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(30), CByte(40), CByte(50))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvStudents.DefaultCellStyle = DataGridViewCellStyle3
        dgvStudents.Dock = DockStyle.Fill
        dgvStudents.EnableHeadersVisualStyles = False
        dgvStudents.GridColor = Color.FromArgb(CByte(235), CByte(238), CByte(242))
        dgvStudents.Location = New Point(0, 0)
        dgvStudents.MultiSelect = False
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersVisible = False
        dgvStudents.RowHeadersWidth = 51
        dgvStudents.RowTemplate.Height = 45
        dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStudents.Size = New Size(1050, 504)
        dgvStudents.TabIndex = 0
        ' 
        ' pnlToolbar
        ' 
        pnlToolbar.BackColor = Color.White
        pnlToolbar.Controls.Add(btnEditSelected)
        pnlToolbar.Controls.Add(marginPanel1)
        pnlToolbar.Controls.Add(btnDeleteSelected)
        pnlToolbar.Controls.Add(pnlSearchContainer)
        pnlToolbar.Dock = DockStyle.Top
        pnlToolbar.Location = New Point(0, 70)
        pnlToolbar.Name = "pnlToolbar"
        pnlToolbar.Padding = New Padding(20, 15, 20, 15)
        pnlToolbar.Size = New Size(1050, 70)
        pnlToolbar.TabIndex = 1
        ' 
        ' btnEditSelected
        ' 
        btnEditSelected.BackColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnEditSelected.Cursor = Cursors.Hand
        btnEditSelected.Dock = DockStyle.Right
        btnEditSelected.FlatAppearance.BorderSize = 0
        btnEditSelected.FlatStyle = FlatStyle.Flat
        btnEditSelected.Font = New Font("Segoe UI Semibold", 9.5F)
        btnEditSelected.ForeColor = Color.White
        btnEditSelected.Location = New Point(740, 15)
        btnEditSelected.Margin = New Padding(5, 3, 5, 3)
        btnEditSelected.Name = "btnEditSelected"
        btnEditSelected.Size = New Size(140, 40)
        btnEditSelected.TabIndex = 0
        btnEditSelected.Text = "Edit Record"
        btnEditSelected.UseVisualStyleBackColor = False
        ' 
        ' marginPanel1
        ' 
        marginPanel1.Dock = DockStyle.Right
        marginPanel1.Location = New Point(880, 15)
        marginPanel1.Name = "marginPanel1"
        marginPanel1.Size = New Size(10, 40)
        marginPanel1.TabIndex = 1
        ' 
        ' btnDeleteSelected
        ' 
        btnDeleteSelected.BackColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnDeleteSelected.Cursor = Cursors.Hand
        btnDeleteSelected.Dock = DockStyle.Right
        btnDeleteSelected.FlatAppearance.BorderSize = 0
        btnDeleteSelected.FlatStyle = FlatStyle.Flat
        btnDeleteSelected.Font = New Font("Segoe UI Semibold", 9.5F)
        btnDeleteSelected.ForeColor = Color.White
        btnDeleteSelected.Location = New Point(890, 15)
        btnDeleteSelected.Margin = New Padding(5, 3, 5, 3)
        btnDeleteSelected.Name = "btnDeleteSelected"
        btnDeleteSelected.Size = New Size(140, 40)
        btnDeleteSelected.TabIndex = 2
        btnDeleteSelected.Text = "Delete Record"
        btnDeleteSelected.UseVisualStyleBackColor = False
        ' 
        ' pnlSearchContainer
        ' 
        pnlSearchContainer.Controls.Add(pnlSearchInner)
        pnlSearchContainer.Dock = DockStyle.Left
        pnlSearchContainer.Location = New Point(20, 15)
        pnlSearchContainer.Name = "pnlSearchContainer"
        pnlSearchContainer.Padding = New Padding(0, 1, 0, 1)
        pnlSearchContainer.Size = New Size(400, 40)
        pnlSearchContainer.TabIndex = 3
        ' 
        ' pnlSearchInner
        ' 
        pnlSearchInner.BackColor = Color.White
        pnlSearchInner.Controls.Add(lblSearchIcon)
        pnlSearchInner.Controls.Add(lblSearchPlaceholder)
        pnlSearchInner.Controls.Add(txtSearch)
        pnlSearchInner.Cursor = Cursors.IBeam
        pnlSearchInner.Location = New Point(0, 1)
        pnlSearchInner.Name = "pnlSearchInner"
        pnlSearchInner.Padding = New Padding(10, 6, 10, 6)
        pnlSearchInner.Size = New Size(400, 38)
        pnlSearchInner.TabIndex = 0
        ' 
        ' lblSearchIcon
        ' 
        lblSearchIcon.Font = New Font("Segoe UI", 10F)
        lblSearchIcon.ForeColor = Color.FromArgb(CByte(170), CByte(180), CByte(190))
        lblSearchIcon.Location = New Point(12, 8)
        lblSearchIcon.Name = "lblSearchIcon"
        lblSearchIcon.Padding = New Padding(0, 2, 0, 0)
        lblSearchIcon.Size = New Size(28, 24)
        lblSearchIcon.TabIndex = 0
        lblSearchIcon.Text = "🔍"
        lblSearchIcon.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblSearchPlaceholder
        ' 
        lblSearchPlaceholder.AutoSize = True
        lblSearchPlaceholder.BackColor = Color.Transparent
        lblSearchPlaceholder.Font = New Font("Segoe UI", 10.5F)
        lblSearchPlaceholder.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblSearchPlaceholder.Location = New Point(42, 9)
        lblSearchPlaceholder.Name = "lblSearchPlaceholder"
        lblSearchPlaceholder.Size = New Size(306, 25)
        lblSearchPlaceholder.TabIndex = 2
        lblSearchPlaceholder.Text = "Search by Name, ID, or Department..."
        ' 
        ' txtSearch
        ' 
        txtSearch.BackColor = Color.White
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.Font = New Font("Segoe UI", 10.5F)
        txtSearch.Location = New Point(42, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(325, 31)
        txtSearch.TabIndex = 1
        ' 
        ' pnlViewHeader
        ' 
        pnlViewHeader.BackColor = Color.Transparent
        pnlViewHeader.Controls.Add(lblViewTitle)
        pnlViewHeader.Controls.Add(viewHeaderLine)
        pnlViewHeader.Dock = DockStyle.Top
        pnlViewHeader.Location = New Point(0, 0)
        pnlViewHeader.Name = "pnlViewHeader"
        pnlViewHeader.Size = New Size(1050, 70)
        pnlViewHeader.TabIndex = 2
        ' 
        ' lblViewTitle
        ' 
        lblViewTitle.Dock = DockStyle.Fill
        lblViewTitle.Font = New Font("Segoe UI Semibold", 18F)
        lblViewTitle.ForeColor = Color.FromArgb(CByte(25), CByte(42), CByte(86))
        lblViewTitle.Location = New Point(0, 0)
        lblViewTitle.Name = "lblViewTitle"
        lblViewTitle.Padding = New Padding(20, 0, 0, 0)
        lblViewTitle.Size = New Size(1050, 69)
        lblViewTitle.TabIndex = 0
        lblViewTitle.Text = "Manage Student Records"
        lblViewTitle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' viewHeaderLine
        ' 
        viewHeaderLine.BackColor = Color.FromArgb(CByte(230), CByte(235), CByte(240))
        viewHeaderLine.Dock = DockStyle.Bottom
        viewHeaderLine.Location = New Point(0, 69)
        viewHeaderLine.Name = "viewHeaderLine"
        viewHeaderLine.Size = New Size(1050, 1)
        viewHeaderLine.TabIndex = 1
        ' 
        ' mainStatusStrip
        ' 
        mainStatusStrip.BackColor = Color.FromArgb(CByte(25), CByte(42), CByte(86))
        mainStatusStrip.ImageScalingSize = New Size(20, 20)
        mainStatusStrip.Items.AddRange(New ToolStripItem() {statusLabel, lblShortcuts})
        mainStatusStrip.Location = New Point(0, 824)
        mainStatusStrip.Name = "mainStatusStrip"
        mainStatusStrip.Size = New Size(1400, 26)
        mainStatusStrip.SizingGrip = False
        mainStatusStrip.TabIndex = 3
        ' 
        ' statusLabel
        ' 
        statusLabel.Font = New Font("Segoe UI", 9F)
        statusLabel.ForeColor = Color.White
        statusLabel.Name = "statusLabel"
        statusLabel.Size = New Size(58, 20)
        statusLabel.Text = "  Ready"
        ' 
        ' lblShortcuts
        ' 
        lblShortcuts.Font = New Font("Segoe UI", 8F)
        lblShortcuts.ForeColor = Color.FromArgb(CByte(140), CByte(189), CByte(195), CByte(199))
        lblShortcuts.Name = "lblShortcuts"
        lblShortcuts.Size = New Size(1327, 20)
        lblShortcuts.Spring = True
        lblShortcuts.Text = "Ctrl+S Save/Add | Ctrl+U Update | Ctrl+F Search | Ctrl+E Export | Ctrl+P Print"
        lblShortcuts.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(9F, 23F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1400, 850)
        Controls.Add(pnlContent)
        Controls.Add(pnlNav)
        Controls.Add(pnlHeader)
        Controls.Add(mainStatusStrip)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        KeyPreview = True
        MinimumSize = New Size(1200, 750)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Student Record Management System"
        CType(formErrorProvider, ComponentModel.ISupportInitialize).EndInit()
        pnlHeader.ResumeLayout(False)
        pnlNav.ResumeLayout(False)
        navHeader.ResumeLayout(False)
        pnlContent.ResumeLayout(False)
        pnlDashboard.ResumeLayout(False)
        pnlQuickActions.ResumeLayout(False)
        pnlCards.ResumeLayout(False)
        cardTotal.ResumeLayout(False)
        cardTotal.PerformLayout()
        cardMale.ResumeLayout(False)
        cardMale.PerformLayout()
        cardFemale.ResumeLayout(False)
        cardFemale.PerformLayout()
        pnlHero.ResumeLayout(False)
        pnlHero.PerformLayout()
        pnlAddEditStudent.ResumeLayout(False)
        pnlFormCard.ResumeLayout(False)
        pnlFormFields.ResumeLayout(False)
        tblForm.ResumeLayout(False)
        tblForm.PerformLayout()
        pnlActions.ResumeLayout(False)
        flowActions.ResumeLayout(False)
        pnlFormHeader.ResumeLayout(False)
        pnlFormHeader.PerformLayout()
        lblIdWrapper.ResumeLayout(False)
        pnlViewStudents.ResumeLayout(False)
        pnlGridCard.ResumeLayout(False)
        gridWrapper.ResumeLayout(False)
        CType(dgvStudents, ComponentModel.ISupportInitialize).EndInit()
        pnlToolbar.ResumeLayout(False)
        pnlSearchContainer.ResumeLayout(False)
        pnlSearchInner.ResumeLayout(False)
        pnlSearchInner.PerformLayout()
        pnlViewHeader.ResumeLayout(False)
        mainStatusStrip.ResumeLayout(False)
        mainStatusStrip.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlQuickActions As Panel
    Friend WithEvents lblQADesc As Label
    Friend WithEvents lblQATitle As Label
    Friend WithEvents pnlSpacing2 As Panel
    Friend WithEvents pnlCards As FlowLayoutPanel
    Friend WithEvents cardTotal As Panel
    Friend WithEvents lblTotalTitle As Label
    Friend WithEvents cardMale As Panel
    Friend WithEvents lblMaleTitle As Label
    Friend WithEvents cardFemale As Panel
    Friend WithEvents lblFemaleTitle As Label
    Friend WithEvents pnlSpacing1 As Panel
    Friend WithEvents pnlHero As Panel
    Friend WithEvents lblHeroTitle As Label
    Friend WithEvents lblHeroSubtitle As Label
    Friend WithEvents pnlFormCard As Panel
    Friend WithEvents pnlFormFields As Panel
    Friend WithEvents tblForm As TableLayoutPanel
    Friend WithEvents lblFirstName As Label
    Friend WithEvents lblLastName As Label
    Friend WithEvents lblGender As Label
    Friend WithEvents lblDOB As Label
    Friend WithEvents lblDepartment As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblAddress As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents actionBorder As Panel
    Friend WithEvents flowActions As FlowLayoutPanel
    Friend WithEvents marginBtn As Panel
    Friend WithEvents pnlFormHeader As Panel
    Friend WithEvents lblIdWrapper As Panel
    Friend WithEvents headerLine As Panel
    Friend WithEvents pnlGridCard As Panel
    Friend WithEvents gridWrapper As Panel
    Friend WithEvents pnlToolbar As Panel
    Friend WithEvents marginPanel1 As Panel
    Friend WithEvents pnlSearchContainer As Panel
    Friend WithEvents pnlSearchInner As Panel
    Friend WithEvents lblSearchIcon As Label
    Friend WithEvents pnlViewHeader As Panel
    Friend WithEvents lblViewTitle As Label
    Friend WithEvents viewHeaderLine As Panel
    Friend WithEvents separator As Panel
    Friend WithEvents navHeader As Panel
    Friend WithEvents navAccent As Panel
    Friend WithEvents lblNavBrand As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents accentLine As Panel
    Friend WithEvents lblShortcuts As ToolStripStatusLabel

End Class