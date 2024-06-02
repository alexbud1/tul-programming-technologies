using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class OrderStatusTests
{
    [TestMethod]
    public void TestOrderStatusConstructor()
    {
        // Arrange
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name", "Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        IOrderStatus orderStatus = new OrderStatus("1", order.OrderId);

        // Assert
        Assert.AreEqual(order.OrderId, orderStatus.OrderId);
        Assert.AreEqual(OrderStatusEnum.Pending, orderStatus.Status);
    }

    [TestMethod]
    public void TestOrderStatusIdIsNotNull()
    {
        // Arrange
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");
        IProduct product = new Product("1", "Test Product", "Test Description", 10.0m, supplier.SupplierId);
        IShop shop = new Shop("1", "Test name", "Test Address");
        IOrder order = new Order("1", product.ProductId, shop.ShopId);

        // Act
        IOrderStatus orderStatus = new OrderStatus("1", order.OrderId);

        // Assert
        Assert.IsNotNull(orderStatus.OrderStatusId);
    }
}