namespace DataLayer.API;

public interface IDataContext
{
    // For the purpose of storing data we have chosen to store just lists of objects
    // This is a simple way to store data, although it does not provide us with data persistence

    public List<ISupplier> Suppliers { get; set; }
    public List<IProduct> Products { get; set; }
    public List<IEvent> Events { get; set; }
    public List<IOrderStatus> OrderStatuses { get; set; }
    public List<IShop> Shops { get; set; }
    public List<IOrder> Orders { get; set; }
}