using DataLayer.Implementations;

namespace DataLayer.API;

public interface IDataContext
{
    static IDataContext CreateContext(string? connectionString = null)
    {
        return new DataContext(connectionString);
    }

    #region Supplier CRUD

    Task AddSupplierAsync(ISupplier supplier);
    Task<ISupplier?> GetSupplierAsync(string supplierId);
    Task UpdateSupplierAsync(ISupplier supplier);
    Task DeleteSupplierAsync(string supplierId);
    Task<Dictionary<string, ISupplier>> GetSuppliersAsync();
    Task<int> GetSupplierCountAsync();

    #endregion Supplier CRUD

    #region Product CRUD

    Task AddProductAsync(IProduct product);
    Task<IProduct?> GetProductAsync(string productId);
    Task UpdateProductAsync(IProduct product);
    Task DeleteProductAsync(string productId);
    Task<Dictionary<string, IProduct>> GetProductsAsync();
    Task<int> GetProductCountAsync();

    #endregion Product CRUD

    #region Event CRUD

    Task AddEventAsync(IEvent @event);
    Task<IEvent?> GetEventAsync(string eventId);
    Task UpdateEventAsync(IEvent @event);
    Task DeleteEventAsync(string eventId);
    Task<Dictionary<string, IEvent>> GetEventsAsync();
    Task<int> GetEventCountAsync();

    #endregion Event CRUD

    #region OrderStatus CRUD

    Task AddOrderStatusAsync(IOrderStatus orderStatus);
    Task<IOrderStatus?> GetOrderStatusAsync(string orderStatusId);
    Task UpdateOrderStatusAsync(IOrderStatus orderStatus);
    Task DeleteOrderStatusAsync(string orderStatusId);
    Task<Dictionary<string, IOrderStatus>> GetOrderStatusesAsync();
    Task<int> GetOrderStatusCountAsync();

    #endregion OrderStatus CRUD

    #region Shop CRUD

    Task AddShopAsync(IShop shop);
    Task<IShop?> GetShopAsync(string shopId);
    Task UpdateShopAsync(IShop shop);
    Task DeleteShopAsync(string shopId);
    Task<Dictionary<string, IShop>> GetShopsAsync();
    Task<int> GetShopCountAsync();

    #endregion Shop CRUD

    #region Order CRUD

    Task AddOrderAsync(IOrder order);
    Task<IOrder?> GetOrderAsync(string orderId);
    Task UpdateOrderAsync(IOrder order);
    Task DeleteOrderAsync(string orderId);
    Task<Dictionary<string, IOrder>> GetOrdersAsync();
    Task<int> GetOrderCountAsync();

    #endregion Order CRUD

    #region Utils

    Task<bool> CheckIfSupplierExists(string id);

    Task<bool> CheckIfShopExists(string id);

    Task<bool> CheckIfProductExists(string id);

    Task<bool> CheckIfOrderStatusExists(string id);

    Task<bool> CheckIfOrderExists(string id);

    Task<bool> CheckIfEventExists(string id);

    #endregion
}