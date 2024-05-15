using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;

namespace Tests.DataLayerTests;

[TestClass]
public class DeliverOrderTests
{
    [TestMethod]
    public void TestDeliverOrderConstructor()
    {
        // Arrange
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address")), shop, dataRepository);
        // Act
        DeliverOrder deliverOrder = DeliverOrder.DeliverOrderExtra(order, shop, dataRepository);

        // Assert
        Assert.AreEqual(order, deliverOrder.Order);
        var orderStatus = dataRepository.GetOrderStatusByOrder(order);
        Assert.AreEqual(OrderStatusEnum.Completed, orderStatus.Status);
    }

}