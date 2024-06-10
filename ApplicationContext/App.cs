using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using DataLayer.API;
using LogicLayer.API;
using DataLayer.Implementations;
using LogicLayer.Implementations;
using ViewModel;
using View;




public class AppStartup
{
    [STAThread]
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var main = serviceProvider.GetRequiredService<View.LoginPanel>();
        main.Show();

        // Run the application's message loop
        System.Windows.Application app = new System.Windows.Application();
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register your services, ViewModels and Views here
        services.AddSingleton<IDataRepository, DataRepository>(); // Can be replaced with another implementation
        services.AddSingleton<ILoginService, LoginService>(); // Can be replaced with another implementation

        IDataRepository dataLayer = new DataRepository(new DataContext());
        ILoginService logicLayer = new LoginService(dataLayer);
        NavigationService navigationService = new NavigationService(dataLayer, logicLayer);

        services.AddSingleton<NavigationService>(navigationService); // Register NavigationService

        // Register Views
        services.AddTransient<View.LoginPanel>();
        services.AddTransient<View.AdminPanel>();
        services.AddTransient<View.ShopPanel>();
        services.AddTransient<View.SupplierPanel>();

        //Add ViewModels
        services.AddTransient<LoginViewModel>(provider => new LoginViewModel(provider.GetRequiredService<NavigationService>()));
        services.AddTransient<AdminViewModel>(provider => new AdminViewModel(provider.GetRequiredService<NavigationService>()));
        services.AddTransient<ShopViewModel>(provider => new ShopViewModel(provider.GetRequiredService<NavigationService>()));
        services.AddTransient<SupplierViewModel>(provider => new SupplierViewModel(provider.GetRequiredService<NavigationService>()));

        // Add other services, ViewModels and Views as needed
    }
}
