Imports System.Windows
Imports Microsoft.Extensions.DependencyInjection
Imports ViewModel

Namespace View
    Partial Public Class App
        Sub Main()
            Dim navigationService As New NavigationService()

            navigationService.NavigateTo(Of LoginViewModel)()

            Dim loginPanel As New LoginPanel(navigationService)
            loginPanel.Show()
        End Sub
    End Class
End Namespace
