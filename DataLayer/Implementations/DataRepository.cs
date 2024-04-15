using DataLayer.API;

namespace DataLayer.Implementations;

public class DataRepository: IDataRepository
{
    private IDataContext _dataContext;

    public DataRepository(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<ISupplier> GetSuppliers()
    {
        return _dataContext.Suppliers;
    }

    public List<IProduct> GetProducts()
    {
        return _dataContext.Products;
    }

    public List<IEvent> GetEvents()
    {
        return _dataContext.Events;
    }

    public List<IOrderStatus> GetOrderStatuses()
    {
        return _dataContext.OrderStatuses;
    }

    public List<IShop> GetShops()
    {
        return _dataContext.Shops;
    }

    public List<IOrder> GetOrders()
    {
        return _dataContext.Orders;
    }

    public IOrderStatus GetOrderStatusByOrder(IOrder order)
    {
        return _dataContext.OrderStatuses.FirstOrDefault(x => x.OrderId == order) ?? throw new Exception("Order status not found");
    }
}