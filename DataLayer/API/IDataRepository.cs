using DataLayer.Implementations;

namespace DataLayer.API;

public interface IDataRepository
{
    static IDataRepository CreateDatabase(IDataContext? dataContext = null)
    {
        return new DataRepository(dataContext ?? new DataContext());
    }

    #region Supplier CRUD

    Task AddSupplierAsync(string supplierId, string supplierName, string supplierAddress);
    Task DeleteSupplierAsync(string supplierId);
    Task UpdateSupplierAsync(string supplierId, string supplierName, string supplierAddress);
    Task<ISupplier> GetSupplierAsync(string supplierId);
    Task<Dictionary<string, ISupplier>> GetSuppliersAsync();
    Task<int> GetSupplierCountAsync();

    #endregion Supplier CRUD

    #region Product CRUD

    Task AddProductAsync(string productId, string productName, string productDescription, decimal productPrice, string supplierId);
    Task DeleteProductAsync(string productId);
    Task UpdateProductAsync(string productId, string productName, string productDescription, decimal productPrice, string supplierId);
    Task<IProduct> GetProductAsync(string productId);
    Task<Dictionary<string, IProduct>> GetProductsAsync();
    Task<int> GetProductCountAsync();

    #endregion Product CRUD

    #region Event CRUD

    Task AddEventAsync(string eventId, string orderId, string productId);
    Task DeleteEventAsync(string eventId);
    Task UpdateEventAsync(string eventId, string orderId, string productId);
    Task<IEvent> GetEventAsync(string eventId);
    Task<Dictionary<string, IEvent>> GetEventsAsync();
    Task<int> GetEventCountAsync();

    #endregion Event CRUD

    #region OrderStatus CRUD

    Task AddOrderStatusAsync(string orderStatusId, OrderStatusEnum status, string orderId);
    Task DeleteOrderStatusAsync(string orderStatusId);
    Task UpdateOrderStatusAsync(string orderStatusId, OrderStatusEnum status, string orderId);
    Task<IOrderStatus> GetOrderStatusAsync(string orderStatusId);
    Task<Dictionary<string, IOrderStatus>> GetOrderStatusesAsync();
    Task<int> GetOrderStatusCountAsync();

    #endregion OrderStatus CRUD

    #region Shop CRUD

    Task AddShopAsync(string shopId, string shopName, string shopAddress);
    Task DeleteShopAsync(string shopId);
    Task UpdateShopAsync(string shopId, string shopName, string shopAddress);
    Task<IShop> GetShopAsync(string shopId);
    Task<Dictionary<string, IShop>> GetShopsAsync();
    Task<int> GetShopCountAsync();

    #endregion Shop CRUD

    #region Order CRUD

    Task AddOrderAsync(string orderId, string shopId, string orderStatusId);
    Task DeleteOrderAsync(string orderId);
    Task UpdateOrderAsync(string orderId, string shopId, string orderStatusId);
    Task<IOrder> GetOrderAsync(string orderId);
    Task<Dictionary<string, IOrder>> GetOrdersAsync();
    Task<int> GetOrderCountAsync();

    #endregion Order CRUD

}