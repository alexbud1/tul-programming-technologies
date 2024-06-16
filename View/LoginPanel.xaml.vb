Imports Microsoft.Extensions.DependencyInjection
Imports ViewModel
Imports System.Windows.Navigation
Imports System.Windows

Partial Public Class LoginPanel
    Inherits Window

    Private ReadOnly _navigationService As NavigationService

    Public Sub New(navigationService As NavigationService)
        InitializeComponent()
        _navigationService = navigationService

        Me.DataContext = _navigationService.CurrentViewModel
    End Sub
End Class
