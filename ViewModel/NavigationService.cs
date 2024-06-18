using System;
using System.Collections.Generic;
using DataLayer.API;
using LogicLayer.API;
using ViewModel;
using DataLayer.Implementations;
using LogicLayer.Implementations;

namespace ViewModel
{

    public class NavigationService
    {
        private readonly IDataRepository dataLayer;
        private readonly ILoginService logicLayer;

        private object? _currentViewModel;

        public object CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        //public IDataRepository DataLayer { get => dataLayer; }
        public ILoginService LogicLayer { get => logicLayer; }

        // Dictionary to hold the ViewModels
        private Dictionary<Type, object> viewModels = new Dictionary<Type, object>();

        public NavigationService(ILoginService logicLayer)
        {
            //this.dataLayer = dataLayer;
            this.logicLayer = logicLayer;
            _currentViewModel = GetOrCreateViewModel<LoginViewModel>();
        }

        /*public NavigationService()
        {
            dataLayer = new DataRepository(new DataContext());
            logicLayer = new LoginService(dataLayer);
            _currentViewModel = GetOrCreateViewModel<LoginViewModel>();
        }*/

        public void NavigateTo<T>() where T : class
        {
            // Get or create the ViewModel
            var viewModel = GetOrCreateViewModel<T>();
            CurrentViewModel = viewModel;
        }

        private T? GetOrCreateViewModel<T>() where T : class
        {
            if (viewModels.TryGetValue(typeof(T), out var viewModel))
            {
                return viewModel as T;
            }

            viewModel = Activator.CreateInstance(typeof(T), this) as T;

            if (viewModel != null)
            {
                viewModels.Add(typeof(T), viewModel);
            }

            return viewModel as T;
        }



        public event Action? CurrentViewModelChanged;

        protected virtual void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }

}