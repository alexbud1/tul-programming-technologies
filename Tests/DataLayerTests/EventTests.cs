using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class EventTests
{
    [TestMethod]
    public void TestEventConstructor()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IProduct product = new Product("Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("Test Shop", "Test Address");
        IOrder order = new Order(product.ProductId, shop.ShopId);

        // Act
        IEvent @event = new Event(order.OrderId, shop.ShopId);

        // Assert
        Assert.AreEqual(order.OrderId, @event.OrderId);
        Assert.AreEqual(shop.ShopId, @event.ShopId);
    }

    [TestMethod]
    public void TestEventIdIsNotNull()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IProduct product = new Product("Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("Test Shop", "Test Address");
        IOrder order = new Order(product.ProductId, shop.ShopId);

        // Act
        IEvent @event = new Event(order.OrderId, shop.ShopId);

        // Assert
        Assert.IsNotNull(@event.EventId);
    }
}