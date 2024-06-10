using Microsoft.Extensions.DependencyInjection;

public class AppStartup
{
    public static IServiceProvider ServiceProvider { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        Shared.DependencyConfigurator.ConfigureServices(serviceCollection);

        // Register Views and ViewModels
        serviceCollection.AddTransient<View.LoginPanel>();
        // ... register other views and view models

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var main = ServiceProvider.GetRequiredService<View.LoginPanel>();
        main.Show();

        // Run the application's message loop
        System.Windows.Application app = new System.Windows.Application();
        app.Run();
    }
}
