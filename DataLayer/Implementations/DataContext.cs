using DataLayer.API;

namespace DataLayer.Implementations;

public class DataContext : IDataContext
{
    public List<ISupplier> Suppliers { get; set; }
    public List<IProduct> Products { get; set; }
    public List<IEvent> Events { get; set; }
    public List<IOrderStatus> OrderStatuses { get; set; }
    public List<IShop> Shops { get; set; }
    public List<IOrder> Orders { get; set; }

    public DataContext()
    {
        Suppliers = new List<ISupplier>();
        Products = new List<IProduct>();
        Events = new List<IEvent>();
        OrderStatuses = new List<IOrderStatus>();
        Shops = new List<IShop>();
        Orders = new List<IOrder>();
    }
}