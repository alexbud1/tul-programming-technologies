using DataLayer.Implementations;
using DataLayer.API;
using DataLayer.Implementations.Events;

namespace Tests.DataLayerTests;

[TestClass]
public class CancelOrderTests
{
    [TestMethod]
    public void TestCancelOrderConstructor()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);
        // Act
        CancelOrder cancelOrder = CancelOrder.CancelOrderExtra(order, shop, dataRepository);

        // Assert
        Assert.AreEqual(order, cancelOrder.Order);
        var orderStatus = dataRepository.GetOrderStatusByOrder(order);
        Assert.AreEqual(OrderStatusEnum.Cancelled, orderStatus.Status);
    }
}