using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AdminViewModel
    {
        private NavigationService _navigationService;

        public RelayCommand NavigateBackCommand { get; }

        public AdminViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateBackCommand = new RelayCommand(_ => _navigationService.NavigateTo<LoginViewModel>());
        }
    }
}
