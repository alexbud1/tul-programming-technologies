using DataLayer.Implementations;
using DataLayer.API;

namespace Tests.DataLayerTests;

[TestClass]
public class DataContextTests
{
    [TestMethod]
    public void TestDataContextConstructor()
    {
        // Arrange
        // Act
        DataContext dataContext = new DataContext();

        // Assert
        Assert.IsNotNull(dataContext);
        Assert.IsNotNull(dataContext.Shops);
        Assert.IsNotNull(dataContext.Products);
        Assert.IsNotNull(dataContext.Suppliers);
        Assert.IsNotNull(dataContext.Orders);
        Assert.IsNotNull(dataContext.OrderStatuses);
    }
}