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
        IProduct product = new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address"));
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(product, shop, dataRepository);

        // Act
        // Assert
        Assert.AreEqual(product, order.Product);
        Assert.AreEqual(shop, order.Shop);
    }

    [TestMethod]
    public void TestOrderIdIsNotNull()
    {
        // Arrange
        IProduct product = new Product("Test Product", "Test Description", 10.0m, new Supplier("Test Supplier", "Test Address"));
        IShop shop = new Shop("Test Shop", "Test Address");
        IDataRepository dataRepository = IDataRepository.Create(new DataContext());
        IOrder order = new Order(product, shop, dataRepository);

        // Act
        // Assert
        Assert.IsNotNull(order.OrderId);
    }
}