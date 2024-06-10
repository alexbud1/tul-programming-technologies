using DataLayer.API;
using DataLayer.Implementations;
using LogicLayer.API;
using LogicLayer.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class DependencyConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Register your services here
            services.AddSingleton<IDataRepository, DataRepository>();
            services.AddSingleton<ILoginService, LoginService>();

            IDataRepository dataLayer = new DataRepository(new DataContext());
            ILoginService logicLayer = new LoginService(dataLayer);
            NavigationService navigationService = new NavigationService(dataLayer, logicLayer);

            services.AddSingleton<NavigationService>(navigationService);
            services.AddSingleton<ViewModelLocator>();
        }
    }
}
