using DataLayer.Implementations;
using DataLayer.API;
using DataLayer.Implementations.Events;

namespace Tests.DataLayerTests;

[TestClass]
public class DataRepositoryTests
{
    [TestMethod]
    public void TestDataRepositoryConstructor()
    {
        // Arrange
        // Act
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());

        // Assert
        Assert.IsNotNull(dataRepository);
    }

    [TestMethod]
    public void TestGetShopById()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        dataRepository.AddShop(shop);

        // Act
        IShop shopById = dataRepository.GetShopById(shop.ShopId);

        // Assert
        Assert.AreEqual(shop, shopById);
    }

    [TestMethod]
    public void TestGetProductById()
    {
        // Arrange
        IProduct product = new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address"));
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        dataRepository.AddProduct(product);

        // Act
        IProduct productById = dataRepository.GetProductById(product.ProductId);

        // Assert
        Assert.AreEqual(product, productById);
    }

    [TestMethod]
    public void TestGetSupplierById()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        dataRepository.AddSupplier(supplier);

        // Act
        ISupplier supplierById = dataRepository.GetSupplierById(supplier.SupplierId);

        // Assert
        Assert.AreEqual(supplier, supplierById);
    }

    [TestMethod]
    public void TestGetOrderById()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order =
            new Order(
                new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")),
                shop, dataRepository);
        dataRepository.AddOrder(order);

        // Act
        IOrder orderById = dataRepository.GetOrderById(order.OrderId);


        // Assert
        Assert.AreEqual(order, orderById);
    }

    [TestMethod]
    public void TestGetOrderStatusByOrder()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order =
            new Order(
                new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")),
                shop, dataRepository);
        dataRepository.AddOrder(order);

        // Act
        IOrderStatus orderStatus = dataRepository.GetOrderStatusByOrder(order);

        // Assert
        Assert.IsNotNull(orderStatus);
    }

    [TestMethod]
    public void TestGetEventById()
    {
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);

        ApproveOrder approveOrder = ApproveOrder.ApproveOrderExtra(order, shop, dataRepository);
        dataRepository.AddEvent(approveOrder);

        // Act
        IEvent eventById = dataRepository.GetEventById(approveOrder.EventId);

        // Assert
        Assert.AreEqual(approveOrder, eventById);
    }

    [TestMethod]
    public void TestListShops()
    {
        // Arrange
        IShop shop1 = new Shop("Test Shop 1", "Test Address 1");
        IShop shop2 = new Shop("Test Shop 2", "Test Address 2");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        dataRepository.AddShop(shop1);
        dataRepository.AddShop(shop2);

        // Assert
        Assert.AreEqual(dataRepository.GetShops().Count, 2);
    }

    [TestMethod]
    public void TestListSuppliers()
    {
        // Arrange
        ISupplier supplier1 = new Supplier("Test Supplier 1", "Test Address 1");
        ISupplier supplier2 = new Supplier("Test Supplier 2", "Test Address 2");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        dataRepository.AddSupplier(supplier1);
        dataRepository.AddSupplier(supplier2);

        // Assert
        Assert.AreEqual(dataRepository.GetSuppliers().Count, 2);
    }

    [TestMethod]
    public void TestListOrders()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order1 = new Order(new Product("Test Product 1", "Test Description 1", 10.0m, new Supplier("Test Supplier 1", "Test Address 1")), shop, dataRepository);
        IOrder order2 = new Order(new Product("Test Product 2", "Test Description 2", 20.0m, new Supplier("Test Supplier 2", "Test Address 2")), shop, dataRepository);
        dataRepository.AddOrder(order1);
        dataRepository.AddOrder(order2);

        // Assert
        Assert.AreEqual(dataRepository.GetOrders().Count, 2);
    }

    [TestMethod]
    public void TestAddShop()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());

        // Act
        dataRepository.AddShop(shop);

        // Assert
        Assert.AreEqual(dataRepository.GetShops().Count, 1);
    }

    [TestMethod]
    public void TestAddProduct()
    {
        // Arrange
        IProduct product = new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address"));
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());

        // Act
        dataRepository.AddProduct(product);

        // Assert
        Assert.AreEqual(dataRepository.GetProductById(product.ProductId), product);
    }

    [TestMethod]
    public void TestAddSupplier()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());

        // Act
        dataRepository.AddSupplier(supplier);

        // Assert
        Assert.AreEqual(dataRepository.GetSupplierById(supplier.SupplierId), supplier);
    }

    [TestMethod]
    public void TestAddOrder()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);

        // Act
        dataRepository.AddOrder(order);

        // Assert
        Assert.AreEqual(dataRepository.GetOrderById(order.OrderId), order);
    }

    [TestMethod]
    public void TestAddEvent()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);
        ApproveOrder approveOrder = ApproveOrder.ApproveOrderExtra(order, shop, dataRepository);

        // Act
        dataRepository.AddEvent(approveOrder);

        // Assert
        Assert.AreEqual(dataRepository.GetEventById(approveOrder.EventId), approveOrder);
    }

    [TestMethod]
    public void TestUpdateOrderStatus()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);
        IOrderStatus orderStatus = dataRepository.GetOrderStatusByOrder(order);
        orderStatus.Status = OrderStatusEnum.Processing;

        // Act
        dataRepository.UpdateOrderStatus(orderStatus);

        // Assert
        Assert.AreEqual(dataRepository.GetOrderStatusByOrder(order).Status, OrderStatusEnum.Processing);
    }

}