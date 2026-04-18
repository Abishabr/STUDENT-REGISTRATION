Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication

        ''' <summary>
        ''' Shows the SplashForm while the main form (Form1) is loading.
        ''' This is handled automatically by the VB.NET Application Framework.
        ''' </summary>
        Protected Overrides Sub OnCreateSplashScreen()
            Me.SplashScreen = New SplashForm()
        End Sub

        ''' <summary>
        ''' Sets application-wide defaults including splash screen display time
        ''' and high DPI mode for modern displays.
        ''' </summary>
        Private Sub MyApplication_ApplyApplicationDefaults(
                sender As Object,
                e As ApplyApplicationDefaultsEventArgs
            ) Handles Me.ApplyApplicationDefaults

            ' Show splash screen for at least 3 seconds
            e.MinimumSplashScreenDisplayTime = 3000

            ' Enable high DPI support for sharp rendering on modern monitors
            e.HighDpiMode = HighDpiMode.PerMonitorV2
        End Sub

    End Class

End Namespace
