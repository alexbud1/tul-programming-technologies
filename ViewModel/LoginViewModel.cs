using LogicLayer.API;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ViewModel;

namespace ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //private readonly IDataRepository _dataRepository;
        private readonly ILoginService _loginService;
        private readonly NavigationService NavigationService;

        public RelayCommand AdminLoginCommand { get; }
        public RelayCommand ShopLoginCommand { get; }
        public RelayCommand SupplierLoginCommand { get; }

        private string _shopId;
        private string _supplierId;

        public string ShopId
        {
            get => _shopId;
            set
            {
                _shopId = value;
                OnPropertyChanged();
            }
        }

        public string SupplierId
        {
            get => _supplierId;
            set
            {
                _supplierId = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
            _loginService = NavigationService.LogicLayer;

            AdminLoginCommand = new RelayCommand(ExecuteAdminLogin, CanExecuteAdminLogin);
            ShopLoginCommand = new RelayCommand(ExecuteShopLogin, CanExecuteShopLogin);
            SupplierLoginCommand = new RelayCommand(ExecuteSupplierLogin, CanExecuteSupplierLogin);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteAdminLogin(object parameter)
        {
            _loginService.Login(ILoginService.LoginChoiceEnum.Admin, "");
            NavigationService.NavigateTo<AdminViewModel>();
        }

        private void ExecuteShopLogin(object parameter)
        {
            _loginService.Login(ILoginService.LoginChoiceEnum.Shop, ShopId);
            NavigationService.NavigateTo<ShopViewModel>();
        }

        private void ExecuteSupplierLogin(object parameter)
        {
            _loginService.Login(ILoginService.LoginChoiceEnum.Supplier, SupplierId);
            NavigationService.NavigateTo<SupplierViewModel>();
        }


        private bool CanExecuteAdminLogin(object parameter)
        {
            return true; // Define logic to determine if command can execute
        }

        private bool CanExecuteShopLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(ShopId); // Enable button only if ShopId is not empty
        }

        private bool CanExecuteSupplierLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(SupplierId); // Enable button only if SupplierId is not empty
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}