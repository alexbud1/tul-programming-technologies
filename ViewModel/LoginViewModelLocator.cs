using ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ViewModel { 
    public class LoginViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public LoginViewModel LoginViewModel => _serviceProvider.GetRequiredService<LoginViewModel>();
    }
}