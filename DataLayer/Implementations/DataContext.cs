using DataLayer.API;
using DataLayer.Database;

namespace DataLayer.Implementations;

public class DataContext : IDataContext
{
    private readonly string _connectionString;

    public DataContext(string? connectionString = null)
    {
        if (connectionString is null)
        {
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ?? string.Empty;
            string _DBRelativePath = @"DataLayer\Database\Database.mdf";
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            this._connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }
        else
        {
            this._connectionString = connectionString;
        }
    }

    #region Supplier CRUD

    public async Task AddSupplierAsync(ISupplier supplier)
    {
        if (!await CheckIfSupplierExists(supplier.SupplierId))
        {
            using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
            {
                Database.Supplier entity = new Database.Supplier()
                {
                    SupplierId = supplier.SupplierId,
                    SupplierName = supplier.SupplierName,
                    SupplierAddress = supplier.SupplierAddress
                };

                context.Suppliers.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        else
        {
            Console.WriteLine($"Supplier with ID {supplier.SupplierId} already exists.");
        }
    }

    public async Task<ISupplier?> GetSupplierAsync(string supplierId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Supplier? supplier = await Task.Run(() =>
            {
                IQueryable<Database.Supplier> query = context.Suppliers.Where(u => u.SupplierId == supplierId);

                return query.FirstOrDefault();
            });

            return supplier is not null ? new Supplier(supplierId, supplier.SupplierName, supplier.SupplierAddress) : null;
        }
    }

    public async Task UpdateSupplierAsync(ISupplier supplier)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Supplier toUpdate = (from u in context.Suppliers where u.SupplierId == supplier.SupplierId select u).FirstOrDefault()!;

            toUpdate.SupplierName = supplier.SupplierName;
            toUpdate.SupplierAddress = supplier.SupplierAddress;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteSupplierAsync(string supplierId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Supplier toDelete = (from u in context.Suppliers where u.SupplierId == supplierId select u).FirstOrDefault()!;

            context.Suppliers.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, ISupplier>> GetSuppliersAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<ISupplier> suppliersQuery = from u in context.Suppliers
                select
                    new Supplier(u.SupplierId, u.SupplierName, u.SupplierAddress) as ISupplier;

            return await Task.Run(() => suppliersQuery.ToDictionary(k => k.SupplierId));
        }
    }

    public async Task<int> GetSupplierCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Suppliers.Count());
        }
    }

    #endregion

    #region Product CRUD

    public async Task AddProductAsync(IProduct product)
    {
        if (!await CheckIfSupplierExists(product.ProductId))
        {
            using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
            {
                Database.Product entity = new Database.Product()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    SupplierId = product.SupplierId
                };

                context.Products.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        else
        {
            Console.WriteLine($"Product with ID {product.ProductId} already exists.");
        }
    }

    public async Task<IProduct?> GetProductAsync(string productId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product? product = await Task.Run(() =>
            {
                IQueryable<Database.Product> query = context.Products.Where(u => u.ProductId == productId);

                return query.FirstOrDefault();
            });

            return product is not null ? new Product(productId, product.ProductName, product.ProductDescription, product.ProductPrice, product.SupplierId) : null;
        }
    }

    public async Task UpdateProductAsync(IProduct product)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product toUpdate = (from u in context.Products where u.ProductId == product.ProductId select u).FirstOrDefault()!;

            toUpdate.ProductName = product.ProductName;
            toUpdate.ProductDescription = product.ProductDescription;
            toUpdate.ProductPrice = product.ProductPrice;
            toUpdate.SupplierId = product.SupplierId;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteProductAsync(string productId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Product toDelete = (from u in context.Products where u.ProductId == productId select u).FirstOrDefault()!;

            context.Products.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, IProduct>> GetProductsAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IProduct> productsQuery = from u in context.Products
                select
                    new Product(u.ProductId, u.ProductName, u.ProductDescription, u.ProductPrice, u.SupplierId) as IProduct;

            return await Task.Run(() => productsQuery.ToDictionary(k => k.ProductId));
        }
    }

    public async Task<int> GetProductCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Products.Count());
        }
    }

    #endregion

    #region Event CRUD

    public async Task AddEventAsync(IEvent @event)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event entity = new Database.Event()
            {
                EventId = @event.EventId,
                OrderId = @event.OrderId,
                ShopId = @event.ShopId,
                Description = @event.Description,
                EventDate = @event.EventDate,
            };

            context.Events.InsertOnSubmit(entity);
            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<IEvent?> GetEventAsync(string eventId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event? @event = await Task.Run(() =>
            {
                IQueryable<Database.Event> query = context.Events.Where(u => u.EventId == eventId);

                return query.FirstOrDefault();
            });

            return @event is not null ? new Event(eventId, @event.OrderId, @event.ShopId) : null;
        }
    }

    public async Task UpdateEventAsync(IEvent @event)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event toUpdate = (from u in context.Events where u.EventId == @event.EventId select u).FirstOrDefault()!;

            toUpdate.OrderId = @event.OrderId;
            toUpdate.ShopId = @event.ShopId;
            toUpdate.Description = @event.Description;
            toUpdate.EventDate = @event.EventDate;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteEventAsync(string eventId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Event toDelete = (from u in context.Events where u.EventId == eventId select u).FirstOrDefault()!;

            context.Events.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, IEvent>> GetEventsAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IEvent> eventsQuery = from u in context.Events
                select
                    new Event(u.EventId, u.OrderId, u.ShopId) as IEvent;

            return await Task.Run(() => eventsQuery.ToDictionary(k => k.EventId));
        }
    }

    public async Task<int> GetEventCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Events.Count());
        }
    }

    #endregion

    #region OrderStatus CRUD

    public async Task AddOrderStatusAsync(IOrderStatus orderStatus)
    {
        if (!await CheckIfSupplierExists(orderStatus.OrderStatusId))
        {
            using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
            {
                Database.OrderStatus entity = new Database.OrderStatus()
                {
                    OrderStatusId = orderStatus.OrderStatusId,
                    OrderId = orderStatus.OrderId,
                    Status = (int)orderStatus.Status,
                };

                context.OrderStatus.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        else
        {
            Console.WriteLine($"OrderStatus with ID {orderStatus.OrderStatusId} already exists.");
        }
    }

    public async Task<IOrderStatus?> GetOrderStatusAsync(string orderStatusId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.OrderStatus? orderStatus = await Task.Run(() =>
            {
                IQueryable<Database.OrderStatus> query = context.OrderStatus.Where(u => u.OrderStatusId == orderStatusId);

                return query.FirstOrDefault();
            });

            return orderStatus is not null ? new OrderStatus("1", orderStatus.OrderId) : null;
        }
    }

    public async Task UpdateOrderStatusAsync(IOrderStatus orderStatus)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.OrderStatus toUpdate = (from u in context.OrderStatus where u.OrderStatusId == orderStatus.OrderStatusId select u).FirstOrDefault()!;

            toUpdate.OrderId = orderStatus.OrderId;
            toUpdate.Status = (int)orderStatus.Status;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteOrderStatusAsync(string orderStatusId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.OrderStatus toDelete = (from u in context.OrderStatus where u.OrderStatusId == orderStatusId select u).FirstOrDefault()!;

            context.OrderStatus.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, IOrderStatus>> GetOrderStatusesAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IOrderStatus> orderStatusesQuery = from u in context.OrderStatus
                select
                    new OrderStatus("1", u.OrderId) as IOrderStatus;

            return await Task.Run(() => orderStatusesQuery.ToDictionary(k => k.OrderStatusId));
        }
    }

    public async Task<int> GetOrderStatusCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.OrderStatus.Count());
        }
    }

    #endregion

    #region Shop CRUD

    public async Task AddShopAsync(IShop shop)
    {
        if (!await CheckIfSupplierExists(shop.ShopId))
        {
            using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
            {
                Database.Shop entity = new Database.Shop()
                {
                    ShopId = shop.ShopId,
                    ShopName = shop.ShopName,
                    ShopAddress = shop.ShopAddress,
                };

                context.Shops.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        else
        {
            Console.WriteLine($"Shop with ID {shop.ShopId} already exists.");
        }
    }

    public async Task<IShop?> GetShopAsync(string shopId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Shop? shop = await Task.Run(() =>
            {
                IQueryable<Database.Shop> query = context.Shops.Where(u => u.ShopId == shopId);

                return query.FirstOrDefault();
            });

            return shop is not null ? new Shop(shopId, shop.ShopName, shop.ShopAddress) : null;
        }
    }

    public async Task UpdateShopAsync(IShop shop)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Shop toUpdate = (from u in context.Shops where u.ShopId == shop.ShopId select u).FirstOrDefault()!;

            toUpdate.ShopName = shop.ShopName;
            toUpdate.ShopAddress = shop.ShopAddress;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteShopAsync(string shopId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Shop toDelete = (from u in context.Shops where u.ShopId == shopId select u).FirstOrDefault()!;

            context.Shops.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, IShop>> GetShopsAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IShop> shopsQuery = from u in context.Shops
                select
                    new Shop(u.ShopId, u.ShopName, u.ShopAddress) as IShop;

            return await Task.Run(() => shopsQuery.ToDictionary(k => k.ShopId));
        }
    }

    public async Task<int> GetShopCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Shops.Count());
        }
    }

    #endregion

    #region Order CRUD

    public async Task AddOrderAsync(IOrder order)
    {
        if (!await CheckIfOrderExists(order.OrderId))
        {
            using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
            {
                Database.Order entity = new Database.Order()
                {
                    OrderId = order.OrderId,
                    ProductId = order.ProductId,
                    ShopId = order.ShopId,
                };

                context.Orders.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        else
        {
            Console.WriteLine($"Order with ID {order.OrderId} already exists.");
        }
    }

    public async Task<IOrder?> GetOrderAsync(string orderId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Order? order = await Task.Run(() =>
            {
                IQueryable<Database.Order> query = context.Orders.Where(u => u.OrderId == orderId);

                return query.FirstOrDefault();
            });

            return order is not null ? new Order(orderId, order.ProductId, order.ShopId) : null;
        }
    }

    public async Task UpdateOrderAsync(IOrder order)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Order toUpdate = (from u in context.Orders where u.OrderId == order.OrderId select u).FirstOrDefault()!;

            toUpdate.ProductId = order.ProductId;
            toUpdate.ShopId = order.ShopId;

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            Database.Order toDelete = (from u in context.Orders where u.OrderId == orderId select u).FirstOrDefault()!;

            context.Orders.DeleteOnSubmit(toDelete);

            await Task.Run(() => context.SubmitChanges());
        }
    }

    public async Task<Dictionary<string, IOrder>> GetOrdersAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            IQueryable<IOrder> ordersQuery = from u in context.Orders
                select
                    new Order(u.OrderId, u.ProductId, u.ShopId) as IOrder;

            return await Task.Run(() => ordersQuery.ToDictionary(k => k.OrderId));
        }
    }

    public async Task<int> GetOrderCountAsync()
    {
        using (DatabaseDataContext context = new DatabaseDataContext(_connectionString))
        {
            return await Task.Run(() => context.Orders.Count());
        }
    }

    #endregion

    #region Utils

    public async Task<bool> CheckIfSupplierExists(string id)
    {
        return (await GetSupplierAsync(id)) != null;
    }

    public async Task<bool> CheckIfProductExists(string id)
    {
        return (await GetProductAsync(id)) != null;
    }

    public async Task<bool> CheckIfOrderStatusExists(string id)
    {
        return (await GetOrderStatusAsync(id)) != null;
    }

    public async Task<bool> CheckIfShopExists(string id)
    {
        return (await GetShopAsync(id)) != null;
    }

    public async Task<bool> CheckIfOrderExists(string id)
    {
        return (await GetOrderAsync(id)) != null;
    }

    public async Task<bool> CheckIfEventExists(string id)
    {
        return (await GetEventAsync(id)) != null;
    }

    #endregion
}