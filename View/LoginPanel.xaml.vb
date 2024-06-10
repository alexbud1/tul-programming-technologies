Imports Microsoft.Extensions.DependencyInjection
Imports ViewModel
Imports [Shared]
Imports System.Windows.Navigation

Partial Public Class LoginPanel
    Public Sub New()
        InitializeComponent()
        Dim navigationService = New NavigationService()
        DataContext = New LoginViewModel(navigationService)
    End Sub
End Class
