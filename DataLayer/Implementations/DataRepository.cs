using DataLayer.API;

namespace DataLayer.Implementations;

internal class DataRepository: IDataRepository
{
    private IDataContext _context;

    public DataRepository(IDataContext context)
    {
        _context = context;
    }

    #region Supplier CRUD

    public async Task AddSupplierAsync(string supplierId, string supplierName, string supplierAddress)
    {
        ISupplier supplier = new Supplier(supplierName, supplierAddress);

        await _context.AddSupplierAsync(supplier);
    }

    public async Task<ISupplier> GetSupplierAsync(string supplierId)
    {
        ISupplier? supplier = await _context.GetSupplierAsync(supplierId);

        if (supplier is null)
            throw new Exception("This supplier does not exist!");

        return supplier;
    }

    public async Task UpdateSupplierAsync(string supplierId, string supplierName, string supplierAddress)
    {
        ISupplier supplier = new Supplier(supplierName, supplierAddress);

        if (!await CheckIfSupplierExists(supplierId))
            throw new Exception("This supplier does not exist");

        await _context.UpdateSupplierAsync(supplier);
    }

    public async Task DeleteSupplierAsync(string supplierId)
    {
        if (!await CheckIfSupplierExists(supplierId))
            throw new Exception("This supplier does not exist");

        await _context.DeleteSupplierAsync(supplierId);
    }

    public async Task <Dictionary<string, ISupplier>> GetSuppliersAsync()
    {
        return await _context.GetSuppliersAsync();
    }

    public async Task<int> GetSupplierCountAsync()
    {
        return await _context.GetSupplierCountAsync();
    }

    #endregion Supplier CRUD

    #region Product CRUD

    public async Task AddProductAsync(string productId, string productName, string productDescription, decimal productPrice, string supplierId)
    {
        IProduct product = new Product(productName, productDescription, productPrice, supplierId);

        await _context.AddProductAsync(product);
    }

    public async Task<IProduct> GetProductAsync(string productId)
    {
        IProduct? product = await _context.GetProductAsync(productId);

        if (product is null)
            throw new Exception("This product does not exist!");

        return product;
    }

    public async Task UpdateProductAsync(string productId, string productName, string productDescription, decimal productPrice, string supplierId)
    {
        IProduct product = new Product(productName, productDescription, productPrice, supplierId);

        if (!await CheckIfProductExists(productId))
            throw new Exception("This product does not exist");

        await _context.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(string productId)
    {
        if (!await CheckIfProductExists(productId))
            throw new Exception("This product does not exist");

        await _context.DeleteProductAsync(productId);
    }

    public async Task<Dictionary<string, IProduct>> GetProductsAsync()
    {
        return await _context.GetProductsAsync();
    }

    public async Task<int> GetProductCountAsync()
    {
        return await _context.GetProductCountAsync();
    }

    #endregion

    #region Event CRUD

    public async Task AddEventAsync(string eventId, string shopId)
    {
        IEvent @event = new Event(shopId, shopId);

        await _context.AddEventAsync(@event);
    }

    public async Task<IEvent> GetEventAsync(string eventId)
    {
        IEvent? @event = await _context.GetEventAsync(eventId);

        if (@event is null)
            throw new Exception("This event does not exist!");

        return @event;
    }

    public async Task UpdateEventAsync(string eventId, string shopId)
    {
        IEvent @event = new Event(shopId, shopId);

        if (!await CheckIfEventExists(eventId))
            throw new Exception("This event does not exist");

        await _context.UpdateEventAsync(@event);
    }

    public async Task DeleteEventAsync(string eventId)
    {
        if (!await CheckIfEventExists(eventId))
            throw new Exception("This event does not exist");

        await _context.DeleteEventAsync(eventId);
    }

    public async Task<Dictionary<string, IEvent>> GetEventsAsync()
    {
        return await _context.GetEventsAsync();
    }

    public async Task<int> GetEventCountAsync()
    {
        return await _context.GetEventCountAsync();
    }

    #endregion

    #region OrderStatus CRUD

    public async Task AddOrderStatusAsync(string orderStatusId, OrderStatusEnum status, string orderId)
    {
        IOrderStatus orderStatus = new OrderStatus(orderId);

        await _context.AddOrderStatusAsync(orderStatus);
    }

    public async Task<IOrderStatus> GetOrderStatusAsync(string orderStatusId)
    {
        IOrderStatus? orderStatus = await _context.GetOrderStatusAsync(orderStatusId);

        if (orderStatus is null)
            throw new Exception("This order status does not exist!");

        return orderStatus;
    }

    public async Task UpdateOrderStatusAsync(string orderStatusId, OrderStatusEnum status, string orderId)
    {
        IOrderStatus orderStatus = new OrderStatus(orderId);

        if (!await CheckIfOrderStatusExists(orderStatusId))
            throw new Exception("This order status does not exist");

        await _context.UpdateOrderStatusAsync(orderStatus);
    }

    public async Task DeleteOrderStatusAsync(string orderStatusId)
    {
        if (!await CheckIfOrderStatusExists(orderStatusId))
            throw new Exception("This order status does not exist");

        await _context.DeleteOrderStatusAsync(orderStatusId);
    }

    public async Task<Dictionary<string, IOrderStatus>> GetOrderStatusesAsync()
    {
        return await _context.GetOrderStatusesAsync();
    }

    public async Task<int> GetOrderStatusCountAsync()
    {
        return await _context.GetOrderStatusCountAsync();
    }

    #endregion

    #region Shop CRUD

    public async Task AddShopAsync(string shopId, string shopName, string shopAddress)
    {
        IShop shop = new Shop(shopName, shopAddress);

        await _context.AddShopAsync(shop);
    }

    public async Task<IShop> GetShopAsync(string shopId)
    {
        IShop? shop = await _context.GetShopAsync(shopId);

        if (shop is null)
            throw new Exception("This shop does not exist!");

        return shop;
    }

    public async Task UpdateShopAsync(string shopId, string shopName, string shopAddress)
    {
        IShop shop = new Shop(shopName, shopAddress);

        if (!await CheckIfShopExists(shopId))
            throw new Exception("This shop does not exist");

        await _context.UpdateShopAsync(shop);
    }

    public async Task DeleteShopAsync(string shopId)
    {
        if (!await CheckIfShopExists(shopId))
            throw new Exception("This shop does not exist");

        await _context.DeleteShopAsync(shopId);
    }

    public async Task<Dictionary<string, IShop>> GetShopsAsync()
    {
        return await _context.GetShopsAsync();
    }

    public async Task<int> GetShopCountAsync()
    {
        return await _context.GetShopCountAsync();
    }

    #endregion

    #region Order CRUD

    public async Task AddOrderAsync(string orderId, string shopId, string orderStatusId)
    {
        IOrder order = new Order(shopId, orderStatusId);

        await _context.AddOrderAsync(order);
    }

    public async Task<IOrder> GetOrderAsync(string orderId)
    {
        IOrder? order = await _context.GetOrderAsync(orderId);

        if (order is null)
            throw new Exception("This order does not exist!");

        return order;
    }

    public async Task UpdateOrderAsync(string orderId, string shopId, string orderStatusId)
    {
        IOrder order = new Order(shopId, orderStatusId);

        if (!await CheckIfOrderExists(orderId))
            throw new Exception("This order does not exist");

        await _context.UpdateOrderAsync(order);
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        if (!await CheckIfOrderExists(orderId))
            throw new Exception("This order does not exist");

        await _context.DeleteOrderAsync(orderId);
    }

    public async Task<Dictionary<string, IOrder>> GetOrdersAsync()
    {
        return await _context.GetOrdersAsync();
    }

    public async Task<int> GetOrderCountAsync()
    {
        return await _context.GetOrderCountAsync();
    }

    #endregion

    #region Utils

    public async Task<bool> CheckIfSupplierExists(string supplierId)
    {
        return await _context.CheckIfSupplierExists(supplierId);
    }

    public async Task<bool> CheckIfProductExists(string productId)
    {
        return await _context.CheckIfProductExists(productId);
    }

    public async Task<bool> CheckIfEventExists(string eventId)
    {
        return await _context.CheckIfEventExists(eventId);
    }

    public async Task<bool> CheckIfOrderStatusExists(string orderStatusId)
    {
        return await _context.CheckIfOrderStatusExists(orderStatusId);
    }

    public async Task<bool> CheckIfShopExists(string shopId)
    {
        return await _context.CheckIfShopExists(shopId);
    }

    public async Task<bool> CheckIfOrderExists(string orderId)
    {
        return await _context.CheckIfOrderExists(orderId);
    }

    #endregion
}