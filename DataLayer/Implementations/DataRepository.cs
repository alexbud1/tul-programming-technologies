using System.Drawing;
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

    public List<IShop> GetShops()
    {
        return _dataContext.Shops;
    }

    public IOrderStatus GetOrderStatusByOrder(IOrder order)
    {
        return _dataContext.OrderStatuses.FirstOrDefault(x => x.Order == order) ?? new OrderStatus(order);
    }

    public ISupplier GetSupplierById(string id)
    {
        return _dataContext.Suppliers.FirstOrDefault(x => x.SupplierId == id) ?? throw new Exception("Supplier not found");
    }

    public IProduct GetProductById(string id)
    {
        return _dataContext.Products.FirstOrDefault(x => x.ProductId == id) ?? throw new Exception("Product not found");
    }

    public IEvent GetEventById(string id)
    {
        return _dataContext.Events.FirstOrDefault(x => x.EventId == id) ?? throw new Exception("Event not found");
    }

    public IShop GetShopById(string id)
    {
        return _dataContext.Shops.FirstOrDefault(x => x.ShopId == id) ?? throw new Exception("Shop not found");
    }

    public IOrder GetOrderById(string id)
    {
        return _dataContext.Orders.FirstOrDefault(x => x.OrderId == id) ?? throw new Exception("Order not found");
    }

    public void AddSupplier(ISupplier supplier)
    {
        _dataContext.Suppliers.Add(supplier);
    }

    public void AddShop(IShop shop)
    {
        _dataContext.Shops.Add(shop);
    }

    public void AddProduct(IProduct product)
    {
        _dataContext.Products.Add(product);
    }

    public void AddEvent(IEvent @event)
    {
        _dataContext.Events.Add(@event);
        System.Console.WriteLine("Events count: " + _dataContext.Events.Count);
    }

    public void AddOrder(IOrder order)
    {
        _dataContext.Orders.Add(order);
    }

    public void AddOrderStatus(IOrderStatus orderStatus)
    {
        _dataContext.OrderStatuses.Add(orderStatus);
    }

    public void UpdateOrderStatus(IOrderStatus orderStatus)
    {
        var orderStatusToUpdate = _dataContext.OrderStatuses.FirstOrDefault(x => x.Order == orderStatus.Order);
        if (orderStatusToUpdate != null)
        {
            orderStatusToUpdate.Status = orderStatus.Status;
        }
    }

    public ISupplier GetSupplierById(string id)
    {
        return _dataContext.Suppliers.FirstOrDefault(x => x.SupplierId == id) ?? throw new Exception("Supplier not found");
    }

    public IProduct GetProductById(string id)
    {
        return _dataContext.Products.FirstOrDefault(x => x.ProductId == id) ?? throw new Exception("Product not found");
    }

    public IEvent GetEventById(string id)
    {
        return _dataContext.Events.FirstOrDefault(x => x.EventId == id) ?? throw new Exception("Event not found");
    }

    public IShop GetShopById(string id)
    {
        return _dataContext.Shops.FirstOrDefault(x => x.ShopId == id) ?? throw new Exception("Shop not found");
    }

    public IOrder GetOrderById(string id)
    {
        return _dataContext.Orders.FirstOrDefault(x => x.OrderId == id) ?? throw new Exception("Order not found");
    }

    public void AddSupplier(ISupplier supplier)
    {
        _dataContext.Suppliers.Add(supplier);
    }

    public void AddShop(IShop shop)
    {
        _dataContext.Shops.Add(shop);
    }

    public void AddProduct(IProduct product)
    {
        _dataContext.Products.Add(product);
    }

    public void AddEvent(IEvent @event)
    {
        _dataContext.Events.Add(@event);
    }

    public void AddOrder(IOrder order)
    {
        _dataContext.Orders.Add(order);
    }

    public void AddOrderStatus(IOrderStatus orderStatus)
    {
        _dataContext.OrderStatuses.Add(orderStatus);
    }
}