using System.Configuration;
using System.Data;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dataLayer = new DataLayer.Implementations.DataRepository(new DataLayer.Implementations.DataContext());
            var logicLayer = new LogicLayer.Implementations.LoginService(dataLayer);
            var navigationService = new NavigationService(dataLayer, logicLayer);

            navigationService.NavigateTo<LoginViewModel>();

            var loginPanel = new LoginPanel(navigationService);
            loginPanel.Show();
        }
    }

}
