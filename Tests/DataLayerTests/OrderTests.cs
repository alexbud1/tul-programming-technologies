using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class OrderTests
{
    [TestMethod]
    public void TestOrderConstructor()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IProduct product = new Product("Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("Test Shop", "Test Address");
        IOrder order = new Order(product.ProductId, shop.ShopId);

        // Act
        // Assert
        Assert.AreEqual(product.ProductId, order.ProductId);
        Assert.AreEqual(shop.ShopId, order.ShopId);
    }

    [TestMethod]
    public void TestOrderIdIsNotNull()
    {
        // Arrange
        ISupplier supplier = new Supplier("Test Supplier", "Test Address");
        IProduct product = new Product("Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("Test Shop", "Test Address");
        IOrder order = new Order(product.ProductId, shop.ShopId);

        // Act
        // Assert
        Assert.IsNotNull(order.OrderId);
    }
}