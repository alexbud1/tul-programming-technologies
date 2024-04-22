using DataLayer.Implementations;

namespace DataLayer.API;

public interface IDataRepository
{
    static IDataRepository Create(IDataContext dataContext)
    {
        return new DataRepository(dataContext);
    }

    List<ISupplier> GetSuppliers();
    List<IShop> GetShops();

    List <IOrder> GetOrders();

    List <IOrderStatus> GetOrdersStatuses();

    ISupplier GetSupplierById(string id);

    IProduct GetProductById(string id);

    IEvent GetEventById(string id);

    IShop GetShopById(string id);

    IOrder GetOrderById(string id);

    IOrderStatus GetOrderStatusByOrder(IOrder order);

    void AddSupplier(ISupplier supplier);

    void AddShop(IShop shop);

    void AddProduct(IProduct product);

    void AddEvent(IEvent @event);

    IOrder AddOrder(IProduct product, IShop shop);

    void AddOrderStatus(IOrderStatus orderStatus);

    void UpdateOrderStatus(IOrderStatus orderStatus);

}