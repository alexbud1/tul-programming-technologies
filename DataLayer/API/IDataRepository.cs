using DataLayer.Implementations;

namespace DataLayer.API;

public interface IDataRepository
{
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