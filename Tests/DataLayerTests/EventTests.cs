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
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name","Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        IEvent @event = new Event("1", order.OrderId, shop.ShopId);

        // Assert
        Assert.AreEqual(order.OrderId, @event.OrderId);
        Assert.AreEqual(shop.ShopId, @event.ShopId);
    }

    [TestMethod]
    public void TestEventIdIsNotNull()
    {
        // Arrange
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name", "Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        IEvent @event = new Event("1", order.OrderId, shop.ShopId);

        // Assert
        Assert.IsNotNull(@event.EventId);
    }
}