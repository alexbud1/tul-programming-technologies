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
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), new Shop("Test Shop", "Test Address"), dataRepository);

        // Act
        IOrderStatus orderStatus = new OrderStatus(order);

        // Assert
        Assert.AreEqual(order, orderStatus.Order);
        Assert.AreEqual(OrderStatusEnum.Pending, orderStatus.Status);
    }

    [TestMethod]
    public void TestOrderStatusIdIsNotNull()
    {
        // Arrange
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), new Shop("Test Shop", "Test Address"), dataRepository);

        // Act
        IOrderStatus orderStatus = new OrderStatus(order);

        // Assert
        Assert.IsNotNull(orderStatus.OrderStatusId);
    }
}