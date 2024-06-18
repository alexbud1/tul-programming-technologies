using System.Windows;
using ViewModel;
using DataLayer.Implementations;
using LogicLayer.Implementations;

namespace AppStartup
{
    public class StartupLogic
    {
        public static void InitializeApplication()
        {
            var dataLayer = new DataRepository(new DataContext());
            var logicLayer = new LoginService(dataLayer);
            var navigationService = new NavigationService(logicLayer);

            navigationService.NavigateTo<LoginViewModel>();

            //var loginPanel = new LoginPanel(navigationService);
            //loginPanel.Show();
        }
    }
}
