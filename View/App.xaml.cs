using System.Configuration;
using System.Data;
using System.Windows;
using View;
using ViewModel;
using AppStartup;

namespace MyApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartupLogic.InitializeApplication(); // Użyj nowej klasy do inicjalizacji aplikacji
        }
    }

}
