' ============================================================
' LoginForm.vb - Login Screen
' Student Record Management System
'
' Professional login form shown AFTER the splash screen.
' Uses email + password authentication against SQLite.
' Default: admin@gmail.com / 12345678
' ============================================================

Imports System.Drawing.Drawing2D

Public Class LoginForm
    Inherits Form

#Region "State"
    <ComponentModel.DesignerSerializationVisibility(ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property AuthenticatedUser As String = ""
    Private loginAttempts As Integer = 0
    Private Const MAX_ATTEMPTS As Integer = 5
#End Region

#Region "Controls"
    Private txtEmail As TextBox
    Private txtPassword As TextBox
    Private btnLogin As Button
    Private lblError As Label
    Private chkShowPassword As CheckBox
#End Region

    Public Sub New()
        Me.Text = "Login — Student Record Management System"
        Me.ClientSize = New Size(460, 620)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.FromArgb(240, 243, 247)
        Me.DoubleBuffered = True
        Me.Font = New Font("Segoe UI", 10F)
        Me.KeyPreview = True

        DatabaseHelper.InitializeDatabase()
        BuildUI()
    End Sub

    Private Sub BuildUI()
        ' ===== TOP HEADER PANEL (gradient) =====
        Dim pnlTop As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 140,
            .BackColor = Color.FromArgb(25, 42, 86)
        }
        AddHandler pnlTop.Paint, Sub(s, e)
            Dim g = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            Using br As New LinearGradientBrush(pnlTop.ClientRectangle,
                Color.FromArgb(25, 42, 86), Color.FromArgb(41, 128, 185),
                LinearGradientMode.ForwardDiagonal)
                g.FillRectangle(br, pnlTop.ClientRectangle)
            End Using
            ' Decorative circle
            Using br As New SolidBrush(Color.FromArgb(20, 255, 255, 255))
                g.FillEllipse(br, pnlTop.Width - 100, -40, 160, 160)
            End Using
        End Sub
        Me.Controls.Add(pnlTop)

        ' App title
        Dim lblAppName As New Label() With {
            .Text = "SRMS",
            .Font = New Font("Segoe UI", 28, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .AutoSize = True,
            .Location = New Point(30, 20)
        }
        pnlTop.Controls.Add(lblAppName)

        ' App subtitle
        Dim lblAppSub As New Label() With {
            .Text = "Student Record Management System",
            .Font = New Font("Segoe UI", 11),
            .ForeColor = Color.FromArgb(200, 220, 240),
            .BackColor = Color.Transparent,
            .AutoSize = True,
            .Location = New Point(32, 85)
        }
        pnlTop.Controls.Add(lblAppSub)

        ' Accent line under header
        Dim pnlAccent As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 3,
            .BackColor = Color.FromArgb(0, 188, 212)
        }
        Me.Controls.Add(pnlAccent)
        pnlAccent.BringToFront()

        ' ===== WHITE CARD for the form =====
        Dim pnlCard As New Panel() With {
            .Size = New Size(400, 410),
            .Location = New Point(30, 160),
            .BackColor = Color.White
        }
        ' Add subtle shadow effect via border paint
        AddHandler pnlCard.Paint, Sub(s, e)
            Dim rect = New Rectangle(0, 0, pnlCard.Width - 1, pnlCard.Height - 1)
            Using pen As New Pen(Color.FromArgb(40, 0, 0, 0))
                e.Graphics.DrawRectangle(pen, rect)
            End Using
        End Sub
        Me.Controls.Add(pnlCard)

        ' "Sign In" title
        Dim lblSignIn As New Label() With {
            .Text = "Sign In",
            .Font = New Font("Segoe UI Semibold", 20),
            .ForeColor = Color.FromArgb(25, 42, 86),
            .AutoSize = True,
            .Location = New Point(25, 20)
        }
        pnlCard.Controls.Add(lblSignIn)

        ' Subtitle
        Dim lblDesc As New Label() With {
            .Text = "Enter your credentials to continue",
            .Font = New Font("Segoe UI", 10),
            .ForeColor = Color.FromArgb(140, 150, 160),
            .AutoSize = True,
            .Location = New Point(27, 58)
        }
        pnlCard.Controls.Add(lblDesc)

        ' --- Email Label ---
        Dim lblEmail As New Label() With {
            .Text = "Email Address",
            .Font = New Font("Segoe UI Semibold", 10),
            .ForeColor = Color.FromArgb(60, 70, 80),
            .AutoSize = True,
            .Location = New Point(25, 100)
        }
        pnlCard.Controls.Add(lblEmail)

        ' --- Email TextBox ---
        txtEmail = New TextBox() With {
            .Font = New Font("Segoe UI", 12),
            .Size = New Size(350, 34),
            .Location = New Point(25, 126),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.FromArgb(248, 250, 252),
            .MaxLength = 100
        }
        pnlCard.Controls.Add(txtEmail)

        ' --- Password Label ---
        Dim lblPass As New Label() With {
            .Text = "Password",
            .Font = New Font("Segoe UI Semibold", 10),
            .ForeColor = Color.FromArgb(60, 70, 80),
            .AutoSize = True,
            .Location = New Point(25, 172)
        }
        pnlCard.Controls.Add(lblPass)

        ' --- Password TextBox ---
        txtPassword = New TextBox() With {
            .Font = New Font("Segoe UI", 12),
            .Size = New Size(350, 34),
            .Location = New Point(25, 198),
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.FromArgb(248, 250, 252),
            .UseSystemPasswordChar = True,
            .MaxLength = 50
        }
        pnlCard.Controls.Add(txtPassword)

        ' --- Show Password ---
        chkShowPassword = New CheckBox() With {
            .Text = "Show Password",
            .Font = New Font("Segoe UI", 9),
            .ForeColor = Color.FromArgb(130, 140, 150),
            .AutoSize = True,
            .Location = New Point(25, 238),
            .BackColor = Color.Transparent
        }
        AddHandler chkShowPassword.CheckedChanged, Sub(s, ev)
            txtPassword.UseSystemPasswordChar = Not chkShowPassword.Checked
        End Sub
        pnlCard.Controls.Add(chkShowPassword)

        ' --- Error Label ---
        lblError = New Label() With {
            .Text = "",
            .Font = New Font("Segoe UI", 9.5F),
            .ForeColor = Color.FromArgb(231, 76, 60),
            .Size = New Size(350, 22),
            .Location = New Point(25, 290),
            .Visible = False
        }
        pnlCard.Controls.Add(lblError)

        ' --- Login Button ---
        btnLogin = New Button() With {
            .Text = "Sign In",
            .Font = New Font("Segoe UI Semibold", 12),
            .Size = New Size(350, 45),
            .Location = New Point(25, 320),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(41, 128, 185),
            .ForeColor = Color.White,
            .Cursor = Cursors.Hand
        }
        btnLogin.FlatAppearance.BorderSize = 0
        btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 152, 219)
        btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 100, 160)
        AddHandler btnLogin.Click, AddressOf AttemptLogin
        pnlCard.Controls.Add(btnLogin)

        ' ===== Default credentials hint at bottom =====
        Dim lblHint As New Label() With {
            .Text = "Default: admin@gmail.com  /  12345678",
            .Font = New Font("Segoe UI", 8.5F, FontStyle.Italic),
            .ForeColor = Color.FromArgb(160, 170, 180),
            .AutoSize = True,
            .Location = New Point(115, 585)
        }
        Me.Controls.Add(lblHint)

        ' --- Keyboard events ---
        AddHandler txtEmail.KeyDown, Sub(s, ev)
            If ev.KeyCode = Keys.Enter Then
                txtPassword.Focus()
                ev.SuppressKeyPress = True
            End If
        End Sub
        AddHandler txtPassword.KeyDown, Sub(s, ev)
            If ev.KeyCode = Keys.Enter Then
                AttemptLogin(Nothing, EventArgs.Empty)
                ev.SuppressKeyPress = True
            End If
        End Sub
        AddHandler txtEmail.TextChanged, Sub(s, ev) HideError()
        AddHandler txtPassword.TextChanged, Sub(s, ev) HideError()

        ' Focus email on load
        AddHandler Me.Shown, Sub(s, ev) txtEmail.Focus()
    End Sub

#Region "Login Logic"
    Private Sub AttemptLogin(sender As Object, e As EventArgs)
        Dim email = txtEmail.Text.Trim()
        Dim password = txtPassword.Text

        If String.IsNullOrEmpty(email) Then
            ShowError("Please enter your email address.")
            txtEmail.Focus()
            Return
        End If
        If String.IsNullOrEmpty(password) Then
            ShowError("Please enter your password.")
            txtPassword.Focus()
            Return
        End If

        If loginAttempts >= MAX_ATTEMPTS Then
            ShowError("Too many failed attempts. Please restart.")
            btnLogin.Enabled = False
            Return
        End If

        If DatabaseHelper.AuthenticateUser(email, password) Then
            AuthenticatedUser = email
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            loginAttempts += 1
            Dim remaining = MAX_ATTEMPTS - loginAttempts
            If remaining > 0 Then
                ShowError($"Invalid email or password. ({remaining} attempts left)")
            Else
                ShowError("Too many failed attempts. Please restart.")
                btnLogin.Enabled = False
            End If
            txtPassword.Clear()
            txtPassword.Focus()
        End If
    End Sub

    Private Sub ShowError(msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

    Private Sub HideError()
        If lblError.Visible Then
            lblError.Visible = False
            lblError.Text = ""
        End If
    End Sub
#End Region

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If Me.DialogResult <> DialogResult.OK Then
            Me.DialogResult = DialogResult.Cancel
        End If
        MyBase.OnFormClosing(e)
    End Sub

End Class
