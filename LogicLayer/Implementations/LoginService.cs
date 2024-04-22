using DataLayer.API;
using LogicLayer.API;

namespace LogicLayer.Implementations;

public class LoginService: ILoginService
{
    #region private attributes

    private IShop? _shop ;
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
    public bool Login(ILoginService.LoginChoiceEnum loginChoice, string id="")
    {
        switch (loginChoice)
        {
            case ILoginService.LoginChoiceEnum.Admin:
                _adminLogged = true;
                break;
            case ILoginService.LoginChoiceEnum.Shop:
                _shop = _dataRepository.GetShopById(id);
                if (_shop == null) throw new NullReferenceException("Shop not found");
                _shopLogged = true;
                break;
            case ILoginService.LoginChoiceEnum.Supplier:
                _supplier = _dataRepository.GetSupplierById(id);
                if (_supplier == null) throw new NullReferenceException("Supplier not found");
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
        } else throw new ArgumentException("Login choice does not exist");
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
            var product = _dataRepository.GetProductById(productId);

            if (product == null) throw new NullReferenceException("No product found with id: " + productId);

            // Create order and associate with product
            _dataRepository.AddOrder(product, _shop);
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
    public List <IOrderStatus> FindOrders()
    {
        if (_supplierLogged)
        {
            var orders = _dataRepository.GetOrdersStatuses().FindAll(p => p.Order.Product.Supplier == _supplier);
            return orders;
        } else if (_shopLogged)
        {
            var orders = _dataRepository.GetOrdersStatuses().FindAll(p => p.Order.Shop == _shop);
            return orders;
        }
        else throw new Exception("Not logged to a supplier");
    }

    public IOrderStatus FindOrderById(string id)
    {
        if (_supplierLogged)
        {
            var order = _dataRepository.GetOrdersStatuses().Find(p => p.Order.OrderId == id
                                                                      && _supplier == p.Order.Product.Supplier);
            if (order == null) throw new NullReferenceException("Order not found.");
            return order;
        } else if (_shopLogged)
        {
            var order = _dataRepository.GetOrdersStatuses().Find(p => p.Order.OrderId == id && p.Order.Shop == _shop);
            if (order == null) throw new NullReferenceException("Order not found.");
            return order;
        }
        else if (_adminLogged)
        {
            var order = _dataRepository.GetOrdersStatuses().Find(p => p.Order.OrderId == id);
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
            var orders = _dataRepository.GetOrdersStatuses().FindAll(p => p.Status == status);
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
        var newOrderStatus = _dataRepository.GetOrdersStatuses().Find(p => p.OrderStatusId == orderStatusId);
        if (newOrderStatus != null) newOrderStatus.Status = status;
        else throw new NullReferenceException("No order found");
    }

    public void SetStatus(string orderStatusId, string status)
    {
        // Set order status using order ID and status string
        var order = _dataRepository.GetOrdersStatuses().Find(p => p.OrderStatusId == orderStatusId);
        if (order != null)
        {
            order.Status = Enum.Parse<OrderStatusEnum>(status);
        }
        else throw new NullReferenceException("No order found");
    }

    public void SetPending(string orderStatusId)
    {
        // Set order status to pending
        var orderStatus = FindOrderById(orderStatusId);
        if (orderStatus != null)
        {
            orderStatus.Status = OrderStatusEnum.Pending;
            _dataRepository.UpdateOrderStatus(orderStatus);
        }
        else throw new NullReferenceException("Cannot find the order.");
    }

    public void SetProcessing(string orderStatusId)
    {
        // Set order status to processing
        var orderStatus = FindOrderById(orderStatusId);
        if (orderStatus != null)
        {
            orderStatus.Status = OrderStatusEnum.Processing;
            _dataRepository.UpdateOrderStatus(orderStatus);
        }
        else throw new NullReferenceException("Cannot find the order.");
    }

    public void SetCompleted(string orderStatusId)
    {
        // Set order status to completed
        var orderStatus = FindOrderById(orderStatusId);
        if (orderStatus != null)
        {
            orderStatus.Status = OrderStatusEnum.Completed;
            _dataRepository.UpdateOrderStatus(orderStatus);
        }
        else throw new NullReferenceException("Cannot find the order");

    }

    public void SetCancelled(string orderStatusId)
    {
        // Set order status to cancelled
        var orderStatus = FindOrderById(orderStatusId);
        if (orderStatus != null)
        {
            orderStatus.Status = OrderStatusEnum.Cancelled;
            _dataRepository.UpdateOrderStatus(orderStatus);
        }
        else throw new NullReferenceException("Cannot find the order");
    }

    #endregion Status setting

    #endregion public functions
}