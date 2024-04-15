using DataLayer.Implementations;

namespace DataLayer.API;

public interface IDataRepository
{
    // TODO : Implement constructor with dependency injection
    static IDataRepository Create(IDataContext dataContext)
    {
        return new DataRepository(dataContext);
    }

    List<ISupplier> GetSuppliers();
    List<IProduct> GetProducts();
    List<IEvent> GetEvents();
    List<IOrderStatus> GetOrderStatuses();
    List<IShop> GetShops();
    List<IOrder> GetOrders();

    IOrderStatus GetOrderStatusByOrder(IOrder order);
}