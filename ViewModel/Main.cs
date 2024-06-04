using DataLayer.API;
using DataLayer.Implementations;
using LogicLayer.API;
using LogicLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModel
{
    public class MainViewModel
    {
        public static void Main()
        {
            IDataContext datacontext = new DataLayer.Implementations.DataContext(null);
            IDataRepository _dataRepository = new DataRepository(datacontext);
            ILoginService _loginService = new LoginService(_dataRepository);
            NavigationService _navigationService = new NavigationService(_dataRepository, _loginService);
            LoginViewModel loginViewModel = new LoginViewModel(_navigationService);
        }
    }
}
