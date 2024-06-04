Imports System.Windows
Imports System.Windows.Navigation
Imports DataLayer.API
Imports DataLayer.Implementations
Imports LogicLayer.API
Imports LogicLayer.Implementations
Imports ViewModel
Imports ViewModels

Namespace View
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)

            'Dim datacontext As IDataContext = New DataLayer.Implementations.DataContext(Nothing)
            'Dim _dataRepository As IDataRepository = New DataRepository(datacontext)
            'Dim _loginService As ILoginService = New LoginService(_dataRepository)
            'Dim _navigationService As NavigationService = New NavigationService(_dataRepository, _loginService)
            'Dim loginViewModel As LoginViewModel = New LoginViewModel(_navigationService)

            MainViewModel.Main()
        End Sub
    End Class
End Namespace
