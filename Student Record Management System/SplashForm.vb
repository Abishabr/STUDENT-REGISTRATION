' ============================================================
' Splash Screen Form
' Displays a professional loading screen while the main form
' initializes. Uses custom paint for gradient background and
' animated progress bar.
' ============================================================

Imports System.Drawing.Drawing2D

''' <summary>
''' Professional splash screen with gradient background, app branding,
''' and animated progress bar. Shown via the VB.NET Application Framework
''' while the main form loads.
''' </summary>
Public Class SplashForm
    Inherits Form

    ' Timer for animating the progress bar
    Private WithEvents tmrProgress As New Timer()
    Private progressPos As Single = -200

    Public Sub New()
        ' Form configuration - borderless centered splash
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Size(560, 330)
        Me.DoubleBuffered = True
        Me.BackColor = Color.FromArgb(25, 42, 86)
        Me.ShowInTaskbar = False

        ' Start progress animation (~60 FPS sweep)
        tmrProgress.Interval = 16
        tmrProgress.Start()
    End Sub

    ''' <summary>
    ''' Custom paint handler - draws gradient background, app title,
    ''' version info, progress bar, and copyright text.
    ''' </summary>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

        ' --- Gradient background ---
        Using brush As New LinearGradientBrush(
            ClientRectangle,
            Color.FromArgb(25, 42, 86),
            Color.FromArgb(15, 25, 55),
            LinearGradientMode.ForwardDiagonal)
            g.FillRectangle(brush, ClientRectangle)
        End Using

        ' --- Top accent gradient line ---
        Using brush As New LinearGradientBrush(
            New Rectangle(0, 0, Width, 4),
            Color.FromArgb(52, 152, 219),
            Color.FromArgb(0, 188, 212),
            LinearGradientMode.Horizontal)
            g.FillRectangle(brush, 0, 0, Width, 4)
        End Using

        ' --- Decorative circle accent ---
        Using brush As New SolidBrush(Color.FromArgb(15, 52, 152, 219))
            g.FillEllipse(brush, Width - 200, -80, 300, 300)
        End Using
        Using brush As New SolidBrush(Color.FromArgb(10, 0, 188, 212))
            g.FillEllipse(brush, -80, Height - 150, 250, 250)
        End Using

        ' --- App title line 1 ---
        Using font As New Font("Segoe UI Light", 28, FontStyle.Regular)
            Dim text1 = "Student Record"
            Dim size1 = g.MeasureString(text1, font)
            g.DrawString(text1, font, Brushes.White, (Width - size1.Width) / 2, 45)
        End Using

        ' --- App title line 2 ---
        Using font As New Font("Segoe UI Semibold", 28, FontStyle.Regular)
            Dim text2 = "Management System"
            Dim size2 = g.MeasureString(text2, font)
            g.DrawString(text2, font, Brushes.White, (Width - size2.Width) / 2, 90)
        End Using

        ' --- Version text ---
        Using font As New Font("Segoe UI", 10, FontStyle.Regular)
            Using brush As New SolidBrush(Color.FromArgb(120, 189, 195, 199))
                Dim vText = "Version 1.0"
                Dim vSize = g.MeasureString(vText, font)
                g.DrawString(vText, font, brush, (Width - vSize.Width) / 2, 150)
            End Using
        End Using

        ' --- Loading text ---
        Using font As New Font("Segoe UI", 9, FontStyle.Italic)
            Using brush As New SolidBrush(Color.FromArgb(52, 152, 219))
                g.DrawString("Loading application...", font, brush, 35, 255)
            End Using
        End Using

        ' --- Progress bar background ---
        Using brush As New SolidBrush(Color.FromArgb(40, 55, 100))
            g.FillRectangle(brush, 35, 280, Width - 70, 6)
        End Using

        ' --- Progress bar fill (animated sweep) ---
        Dim trackWidth = Width - 70
        Dim segmentWidth As Integer = 250
        
        Dim segmentRect As New Rectangle(CInt(35 + progressPos), 280, segmentWidth, 6)
        If segmentRect.Width > 0 AndAlso segmentRect.Height > 0 Then
            Using brush As New LinearGradientBrush(
                segmentRect,
                Color.Transparent,
                Color.Transparent,
                LinearGradientMode.Horizontal)
                
                Dim blend As New ColorBlend()
                blend.Positions = {0.0F, 0.5F, 1.0F}
                blend.Colors = {Color.FromArgb(0, 0, 188, 212), Color.FromArgb(255, 0, 255, 255), Color.FromArgb(0, 0, 188, 212)}
                brush.InterpolationColors = blend
                
                g.SetClip(New Rectangle(35, 280, trackWidth, 6))
                g.FillRectangle(brush, segmentRect)
                g.ResetClip()
            End Using
        End If

        ' --- Copyright ---
        Using font As New Font("Segoe UI", 8)
            Using brush As New SolidBrush(Color.FromArgb(80, 189, 195, 199))
                Dim cText = $"© {DateTime.Now.Year} Student Record Management System"
                Dim cSize = g.MeasureString(cText, font)
                g.DrawString(cText, font, brush, (Width - cSize.Width) / 2, 300)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Timer tick handler - advances indeterminate progress bar and triggers repaint.
    ''' </summary>
    Private Sub tmrProgress_Tick(sender As Object, e As EventArgs) Handles tmrProgress.Tick
        progressPos += 7.0F
        If progressPos > Width - 35 Then
            progressPos = -250
        End If
        ' Optimised invalidate to only redraw the loading text and bar
        Invalidate(New Rectangle(30, 250, Width - 60, 40))
    End Sub

    ''' <summary>
    ''' Clean up timer resources when form is disposed.
    ''' </summary>
    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        tmrProgress.Stop()
        tmrProgress.Dispose()
        MyBase.OnFormClosed(e)
    End Sub

End Class
