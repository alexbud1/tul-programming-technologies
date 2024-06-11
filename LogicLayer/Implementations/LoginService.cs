using DataLayer.API;
using LogicLayer.API;
using System.Collections.Immutable;

namespace LogicLayer.Implementations;

public class LoginService : ILoginService
{
    #region private attributes

    private IShop? _shop;
    private ISupplier? _supplier;
    private bool _shopLogged = false;
    private bool _supplierLogged = false;
    private bool _adminLogged = false;

    private readonly IDataRepository _dataRepository;

    #endregion private attributes

    public LoginService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    #region public functions

    #region Login
    public bool Login(ILoginService.LoginChoiceEnum loginChoice, string? id = "")
    {
        switch (loginChoice)
        {
            case ILoginService.LoginChoiceEnum.Admin:
                _adminLogged = true;
                break;
            case ILoginService.LoginChoiceEnum.Shop:
                _shop = _dataRepository.GetShopAsync(id).Result;
                if (_shop == null) throw new NullReferenceException("Shop not found" + _shop.ShopId);
                _shopLogged = true;
                break;
            case ILoginService.LoginChoiceEnum.Supplier:
                _supplier = _dataRepository.GetSupplierAsync(id).Result;
                if (_supplier == null) throw new NullReferenceException("Supplier not found" + _supplier.SupplierId);
                _supplierLogged = true;
                break;
            default:
                throw new ArgumentException("Not existing logging account");
        }

        return _shopLogged || _supplierLogged || _adminLogged;
    }

    public bool Login(int loginChoice, string id)
    {
        if (Enum.IsDefined(typeof(ILoginService.LoginChoiceEnum), loginChoice))
        {
            return Login((ILoginService.LoginChoiceEnum)loginChoice, id);
        }
        else throw new ArgumentException("Login choice does not exist");
    }

    public void Logout()
    {
        _supplierLogged = false;
        _shopLogged = false;
        _adminLogged = false;
    }

    public bool AdminLogged()
    {
        return _adminLogged;
    }

    public bool ShopLogged()
    {
        return _shopLogged;
    }

    public bool SupplierLogged()
    {
        return _supplierLogged;
    }

    #endregion Login

    #region Placing order logic

    //Shop access
    public void PlaceOrder(string orderId, string productId)
    {
        if (_shopLogged)
        {
            var product = _dataRepository.GetProductAsync(productId).Result;

            if (product == null) throw new NullReferenceException("No product found with id: " + productId);

            string orderStatusId = Guid.NewGuid().ToString();
            while (orderStatusId == _dataRepository.GetOrderStatusAsync(orderStatusId).Result.OrderStatusId)
                orderStatusId = Guid.NewGuid().ToString();

            // Create order and associate with product
            _dataRepository.AddOrderAsync(product.ProductId, _shop.ShopId, orderStatusId);
        }
        else throw new Exception("Not logged to a shop");
    }

    //Shop access
    public void CancelOrder(string orderId)
    {
        if (_shopLogged)
        {
            SetCancelled(orderId);
        }
        else throw new Exception("Not logged to a shop");
    }

    #endregion Placing order logic

    #region Receiving order logic

    //Supplier access
    public List<IOrderStatus> FindOrders()
    {
        if (_supplierLogged)
        {
            var orders = _dataRepository.GetOrderStatusesAsync().Result.
                Where(p => _dataRepository.GetProductAsync(
                        _dataRepository.GetOrderAsync(p.Value.OrderId)
                        .Result.ProductId
                    ).Result.
                    SupplierId == _supplier.SupplierId);
            return orders.Select(p => p.Value).ToList();
        }
        else if (_shopLogged)
        {
            var orders = _dataRepository.GetOrderStatusesAsync().Result.
                Where(p => _dataRepository.GetOrderAsync(p.Value.OrderId).Result.
                ShopId == _shop.ShopId);

            return orders.Select(p => p.Value).ToList();
        }
        else throw new Exception("Not logged to a supplier or shop, or admin");
    }

    private List<IOrderStatus> FindOrderStatusesForShop(string shopId)
    {
        var orders = _dataRepository.GetOrderStatusesAsync().Result.
            Where(p => _dataRepository.GetOrderAsync(p.Value.OrderId).Result.
            ShopId == shopId);
        return orders.Select(p => p.Value).ToList();
    }

    private List<IOrderStatus> FindOrderStatusesForSupplier(string supplierId)
    {
        var orders = _dataRepository.GetOrderStatusesAsync().Result.
            Where(p => _dataRepository.GetProductAsync(
                _dataRepository.GetOrderAsync(p.Value.OrderId).Result.
                ProductId).Result.
                SupplierId == supplierId);
        return orders.Select(p => p.Value).ToList();
    }

    public IOrderStatus FindOrderById(string id)
    {
        if (_supplierLogged)
        {
            var order = FindOrderStatusesForSupplier(_supplier?.SupplierId).Find(p => p.OrderId == id);
            if (order == null) throw new NullReferenceException("Order not found.");
            return order;
        }
        else if (_shopLogged)
        {
            var order = FindOrderStatusesForShop(_shop?.ShopId).Find(p => p.OrderId == id);
            if (order == null) throw new NullReferenceException("Order not found.");
            return order;
        }
        else if (_adminLogged)
        {
            var order = _dataRepository.GetOrderStatusesAsync().Result.
                Values.ToList().Find(p => p.OrderId == id);
            if (order == null) throw new NullReferenceException("Order not found.");
            return order;
        }
        else
            throw new ApplicationException("Unknown user");
    }

    public List<IOrderStatus> FindOrdersByStatus(OrderStatusEnum status)
    {
        if (_supplierLogged)
        {
            var orders = FindOrderStatusesForSupplier(_supplier.SupplierId).FindAll(p => p.Status == status);
            if (orders == null) throw new NullReferenceException("No order found");
            return orders;
        }
        else throw new Exception("Not logged to supplier");
    }

    #endregion Receiving order logic

    #region Status setting

    public void SetStatus(string orderStatusId, OrderStatusEnum status)
    {
        // Set order status using the provided order and status
        var order = _dataRepository.GetOrderStatusAsync(orderStatusId).Result;
        if (order != null) _dataRepository.UpdateOrderStatusAsync(orderStatusId, status, order.OrderId);
        else throw new NullReferenceException("No order found");
    }

    public void SetStatus(string orderStatusId, string status)
    {
        // Set order status using order ID and status string
        SetStatus(orderStatusId, (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), status));
    }

    public void SetPending(string orderStatusId)
    {
        // Set order status to pending
        SetStatus(orderStatusId, OrderStatusEnum.Pending);
    }

    public void SetProcessing(string orderStatusId)
    {
        // Set order status to processing
        SetStatus(orderStatusId, OrderStatusEnum.Processing);
    }

    public void SetCompleted(string orderStatusId)
    {
        // Set order status to completed
        SetStatus(orderStatusId, OrderStatusEnum.Completed);

    }

    public void SetCancelled(string orderStatusId)
    {
        // Set order status to cancelled
        SetStatus(orderStatusId, OrderStatusEnum.Cancelled);
    }

    #endregion Status setting

    #region AdminOnlyFunctions

    public List<IShop> FindShops()
    {
        if (_adminLogged)
        {
            return _dataRepository.GetShopsAsync().Result.Values.ToList();
        }
        else throw new Exception("Not logged as admin");
    }

    public List<ISupplier> FindSuppliers()
    {
        if (_adminLogged)
        {
            return _dataRepository.GetSuppliersAsync().Result.Values.ToList();
        }
        else throw new Exception("Not logged as admin");
    }

    public List<IProduct> FindProducts()
    {
        if (_adminLogged)
        {
            return _dataRepository.GetProductsAsync().Result.Values.ToList();
        }
        else throw new Exception("Not logged as admin");
    }

    #endregion AdminOnlyFunctions

    #endregion public functions
}
