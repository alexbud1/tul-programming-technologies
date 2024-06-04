using DataLayer.API;
using DataLayer.Implementations;
using LogicLayer.API;
using LogicLayer.Implementations;
using Microsoft.Extensions.DependencyInjection;


public class App
{
    [STAThread]
    public static void Main()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = serviceProvider.GetRequiredService<LoginViewModel>();
        mainWindow.Show();

        // Run the application's message loop
        System.Windows.Application.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register your services and ViewModels here
        services.AddSingleton<IDataRepository, DataRepository>();
        services.AddSingleton<ILoginService, LoginService>();
        services.AddSingleton<LoginViewModel>();
        //services.AddSingleton<MainWindow>();
        // Add other services and ViewModels as needed
    }
}
  