﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModel
{
    public class SupplierViewModel
    {
        private readonly NavigationService _navigationService;

        public RelayCommand NavigateBackCommand { get; }

        public SupplierViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateBackCommand = new RelayCommand(_ => _navigationService.NavigateTo<LoginViewModel>());
        }
    }
}
