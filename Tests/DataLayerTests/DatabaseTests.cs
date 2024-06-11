using DataLayer.API;
using Microsoft.Data.SqlClient;
namespace Tests.DataLayerTests;

[TestClass]
[DeploymentItem("TestDatabase.mdf")]
public class DatabaseTests
{
    private static string? connectionString;

    private IDataRepository? _dataRepository;

    [ClassInitialize]
    public static void ClassInitializeMethod(TestContext context)
    {
        string _DBRelativePath = "TestDatabase.mdf";
        string _DBPath = Path.Combine(Directory.GetCurrentDirectory(), _DBRelativePath);
        FileInfo _databaseFile = new FileInfo(_DBPath);
        if (!_databaseFile.Exists)
        {
            throw new FileNotFoundException($"Database file {_DBPath} not found in current directory {Environment.CurrentDirectory}");
        }
        connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
    }

    [TestInitialize]
    public void TestInitialize()
    {
        IDataContext dataContext = IDataContext.CreateContext(connectionString);
        _dataRepository = IDataRepository.CreateDatabase(dataContext);
    }



    [TestMethod]
    public void TestDatabaseConnection()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Assert.IsTrue(connection.State == System.Data.ConnectionState.Open, "Connection to the database failed.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred while connecting to the database: {ex.Message}");
            }
        }
    }

    [TestMethod]
    public async Task TestSuppliers()
    {
        string supplierId = "1";
        await _dataRepository.AddSupplierAsync(supplierId, "Test Name", "Test Address");

        ISupplier supplier = await _dataRepository.GetSupplierAsync(supplierId);

        Assert.AreEqual("Test Name", supplier.SupplierName);
        Assert.AreEqual("Test Address", supplier.SupplierAddress);
        Assert.AreEqual(supplierId, supplier.SupplierId);

        Assert.IsTrue(await _dataRepository.GetSupplierCountAsync() == 1);

        await _dataRepository.UpdateSupplierAsync(supplierId, "New Name", "New Address");

        ISupplier updatedSupplier = await _dataRepository.GetSupplierAsync(supplierId);

        Assert.AreEqual("New Name", updatedSupplier.SupplierName);
        Assert.AreEqual("New Address", updatedSupplier.SupplierAddress);
        Assert.AreEqual(supplierId, updatedSupplier.SupplierId);
        Assert.AreNotEqual(updatedSupplier, supplier);

        await _dataRepository.DeleteSupplierAsync(supplierId);

        Assert.IsTrue(await _dataRepository.GetSupplierCountAsync() == 0);
    }

    [TestMethod]
    public async Task TestProducts()
    {
        string supplierId = "1";
        await _dataRepository.AddSupplierAsync(supplierId, "Test Name", "Test Address");

        string productId = "1";
        await _dataRepository.AddProductAsync(productId, "Test Product", "Test Description", 10.0m, supplierId);

        IProduct product = await _dataRepository.GetProductAsync(productId);

        Assert.AreEqual("Test Product", product.ProductName);
        Assert.AreEqual("Test Description", product.ProductDescription);
        Assert.AreEqual(10.0m, product.ProductPrice);
        Assert.AreEqual(supplierId, product.SupplierId);
        Assert.AreEqual(productId, product.ProductId);

        Assert.IsTrue(await _dataRepository.GetProductCountAsync() == 1);

        await _dataRepository.UpdateProductAsync(productId, "New Product", "New Description", 20.0m, supplierId);

        IProduct updatedProduct = await _dataRepository.GetProductAsync(productId);

        Assert.AreEqual("New Product", updatedProduct.ProductName);
        Assert.AreEqual("New Description", updatedProduct.ProductDescription);
        Assert.AreEqual(20.0m, updatedProduct.ProductPrice);
        Assert.AreEqual(supplierId, updatedProduct.SupplierId);
        Assert.AreEqual(productId, updatedProduct.ProductId);
        Assert.AreNotEqual(updatedProduct, product);

        await _dataRepository.DeleteProductAsync(productId);

        Assert.IsTrue(await _dataRepository.GetProductCountAsync() == 0);

        await _dataRepository.DeleteSupplierAsync(supplierId);
    }

    [TestMethod]
    public async Task TestShops()
    {
        string shopId = "1";
        await _dataRepository.AddShopAsync(shopId, "Test Name", "Test Address");

        IShop shop = await _dataRepository.GetShopAsync(shopId);

        Assert.AreEqual("Test Name", shop.ShopName);
        Assert.AreEqual("Test Address", shop.ShopAddress);
        Assert.AreEqual(shopId, shop.ShopId);

        Assert.IsTrue(await _dataRepository.GetShopCountAsync() == 1);

        await _dataRepository.UpdateShopAsync(shopId, "New Name", "New Address");

        IShop updatedShop = await _dataRepository.GetShopAsync(shopId);

        Assert.AreEqual("New Name", updatedShop.ShopName);
        Assert.AreEqual("New Address", updatedShop.ShopAddress);
        Assert.AreEqual(shopId, updatedShop.ShopId);
        Assert.AreNotEqual(updatedShop, shop);

        await _dataRepository.DeleteShopAsync(shopId);

        Assert.IsTrue(await _dataRepository.GetShopCountAsync() == 0);
    }

    [TestMethod]
    public async Task TestOrders()
    {
        string supplierId = "1";
        await _dataRepository.AddSupplierAsync(supplierId, "Test Name", "Test Address");

        string productId = "1";
        await _dataRepository.AddProductAsync(productId, "Test Product", "Test Description", 10.0m, supplierId);

        string shopId = "1";
        await _dataRepository.AddShopAsync(shopId, "Test Name", "Test Address");

        string orderId = "1";
        await _dataRepository.AddOrderAsync(orderId, productId, shopId);

        IOrder order = await _dataRepository.GetOrderAsync(orderId);

        Assert.AreEqual(productId, order.ProductId);
        Assert.AreEqual(shopId, order.ShopId);
        Assert.AreEqual(orderId, order.OrderId);

        Assert.IsTrue(await _dataRepository.GetOrderCountAsync() == 1);

        await _dataRepository.UpdateOrderAsync(orderId, productId, shopId);

        IOrder updatedOrder = await _dataRepository.GetOrderAsync(orderId);

        Assert.AreEqual(productId, updatedOrder.ProductId);
        Assert.AreEqual(shopId, updatedOrder.ShopId);
        Assert.AreEqual(orderId, updatedOrder.OrderId);
        Assert.AreNotEqual(updatedOrder, order);

        await _dataRepository.DeleteOrderAsync(orderId);

        Assert.IsTrue(await _dataRepository.GetOrderCountAsync() == 0);

        await _dataRepository.DeleteProductAsync(productId);
        await _dataRepository.DeleteShopAsync(shopId);
        await _dataRepository.DeleteSupplierAsync(supplierId);
    }

    [TestMethod]
    public async Task TestEvents()
    {
        string supplierId = "1";
        await _dataRepository.AddSupplierAsync(supplierId, "Test Name", "Test Address");

        string productId = "1";
        await _dataRepository.AddProductAsync(productId, "Test Product", "Test Description", 10.0m, supplierId);

        string shopId = "1";
        await _dataRepository.AddShopAsync(shopId, "Test Name", "Test Address");

        string orderId = "1";
        await _dataRepository.AddOrderAsync(orderId, productId, shopId);

        string eventId = "1";
        await _dataRepository.AddEventAsync(eventId, orderId, productId);


        IEvent @event = await _dataRepository.GetEventAsync(eventId);

        Assert.AreEqual(orderId, @event.OrderId);
        Assert.AreEqual(shopId, @event.ShopId);
        Assert.AreEqual(eventId, @event.EventId);

        Assert.IsTrue(await _dataRepository.GetEventCountAsync() == 1);

        await _dataRepository.UpdateEventAsync(eventId, orderId, productId);


        IEvent updatedEvent = await _dataRepository.GetEventAsync(eventId);

        Assert.AreEqual(orderId, updatedEvent.OrderId);
        Assert.AreEqual(shopId, updatedEvent.ShopId);
        Assert.AreEqual(eventId, updatedEvent.EventId);
        Assert.AreNotEqual(updatedEvent, @event);

        await _dataRepository.DeleteEventAsync(eventId);

        Assert.IsTrue(await _dataRepository.GetEventCountAsync() == 0);

        await _dataRepository.DeleteOrderAsync(orderId);
        await _dataRepository.DeleteProductAsync(productId);
        await _dataRepository.DeleteShopAsync(shopId);
        await _dataRepository.DeleteSupplierAsync(supplierId);
    }

    [TestMethod]
    public async Task TestOrderStatuses()
    {
        string supplierId = "1";
        await _dataRepository.AddSupplierAsync(supplierId, "Test Name", "Test Address");

        string productId = "1";
        await _dataRepository.AddProductAsync(productId, "Test Product", "Test Description", 10.0m, supplierId);

        string shopId = "1";
        await _dataRepository.AddShopAsync(shopId, "Test Name", "Test Address");

        string orderId = "1";
        await _dataRepository.AddOrderAsync(orderId, productId, shopId);

        string orderStatusId = "1";
        await _dataRepository.AddOrderStatusAsync(orderStatusId, OrderStatusEnum.Pending, orderId);

        IOrderStatus orderStatus = await _dataRepository.GetOrderStatusAsync(orderStatusId);

        Assert.AreEqual(OrderStatusEnum.Pending, orderStatus.Status);
        Assert.AreEqual(orderId, orderStatus.OrderId);
        Assert.AreEqual(orderStatusId, orderStatus.OrderStatusId);

        Assert.IsTrue(await _dataRepository.GetOrderStatusCountAsync() == 1);

        await _dataRepository.UpdateOrderStatusAsync(orderStatusId, OrderStatusEnum.Completed, orderId);

        IOrderStatus updatedOrderStatus = await _dataRepository.GetOrderStatusAsync(orderStatusId);

        Assert.AreEqual(OrderStatusEnum.Pending, updatedOrderStatus.Status);

        Assert.AreEqual(orderId, updatedOrderStatus.OrderId);
        Assert.AreEqual(orderStatusId, updatedOrderStatus.OrderStatusId);
        Assert.AreNotEqual(updatedOrderStatus, orderStatus);

        await _dataRepository.DeleteOrderStatusAsync(orderStatusId);

        Assert.IsTrue(await _dataRepository.GetOrderStatusCountAsync() == 0);

        await _dataRepository.DeleteOrderAsync(orderId);
        await _dataRepository.DeleteProductAsync(productId);
        await _dataRepository.DeleteShopAsync(shopId);
        await _dataRepository.DeleteSupplierAsync(supplierId);
    }
}