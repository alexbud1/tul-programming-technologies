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
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name", "Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        // Assert
        Assert.AreEqual(product.ProductId, order.ProductId);
        Assert.AreEqual(shop.ShopId, order.ShopId);
    }

    [TestMethod]
    public void TestOrderIdIsNotNull()
    {
        // Arrange
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name", "Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        // Assert
        Assert.IsNotNull(order.OrderId);
    }
}