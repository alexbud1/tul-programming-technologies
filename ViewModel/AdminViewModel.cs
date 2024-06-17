using DataLayer.API;
using System.Collections.ObjectModel;
using ViewModel;
using LogicLayer.API;
using ViewModel;

public class AdminViewModel
{
    private NavigationService _navigationService;
    private readonly ILoginService _loginService;

    public RelayCommand NavigateBackCommand { get; }

    // Dodaj właściwości dla sklepów, produktów i dostawców
    public ObservableCollection<IShop> Shops { get; set; }
    public ObservableCollection<IProduct> Products { get; set; }
    public ObservableCollection<ISupplier> Suppliers { get; set; }

    public AdminViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService)); ;
        NavigateBackCommand = new RelayCommand(_ => _navigationService.NavigateTo<LoginViewModel>());
        _loginService = navigationService.LogicLayer ?? throw new ArgumentNullException("LogicLayer");

        _loginService.Login(ILoginService.LoginChoiceEnum.Admin, "");

        // Inicjalizuj kolekcje
        Shops = new ObservableCollection<IShop>();
        Products = new ObservableCollection<IProduct>();
        Suppliers = new ObservableCollection<ISupplier>();

        // Załaduj dane 
        LoadShops();
        LoadProducts();
        LoadSuppliers();
    }

    // Metody do ładowania danych
    internal void LoadShops()
    {
        // Assuming you have an instance of ILoginService called loginService
        if (_loginService.AdminLogged())
        {
            // Call the method to find the shops from the ILoginService interface
            List<IShop> shops = _loginService.FindShops();

            // Clear the existing collection
            Shops.Clear();

            // Add the retrieved shops to the collection
            foreach (IShop shop in shops)
            {
                Shops.Add(shop);
            }
        }
        else throw new Exception("Admin not logged in");
    }
    internal void LoadProducts()
    {
        if (_loginService.AdminLogged())
        {
            List<IProduct> products = _loginService.FindProducts();
            Products.Clear();
            foreach (IProduct product in products)
            {
                Products.Add(product);
            }
        }
    }
    internal void LoadSuppliers()
    {
        if (_loginService.AdminLogged())
        {
            List<ISupplier> suppliers = _loginService.FindSuppliers();
            Suppliers.Clear();
            foreach (ISupplier supplier in suppliers)
            {
                Suppliers.Add(supplier);
            }
        }
    }
}
