using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace View
{
    public partial class LoginPanel : Window
    {
        private readonly NavigationService _navigationService;

        public LoginPanel(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;

            this.DataContext = _navigationService.CurrentViewModel;
        }
    }
}
