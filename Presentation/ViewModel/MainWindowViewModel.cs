﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    internal class MainWindowViewModel : IViewModel
    {
        private IViewModel _selectedViewModel { get; set; }


        public MainWindowViewModel()
        {
            this._selectedViewModel = new HomeViewModel();
        }

        public new IViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;

                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
    }
}