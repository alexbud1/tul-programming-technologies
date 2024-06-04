using System;
using DataLayer.API;
using LogicLayer.API; 

public class NavigationService
{
    private object _currentViewModel;

    public object CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public IDataRepository DataLayer { get; }
    public ILoginService LogicLayer { get; }

    public NavigationService(IDataRepository dataLayer, ILoginService logicLayer)
    {
        DataLayer = dataLayer;
        LogicLayer = logicLayer;
    }

    public void NavigateTo(object viewModel)
    {
        CurrentViewModel = viewModel;
    }

    public event Action CurrentViewModelChanged;

    protected virtual void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
