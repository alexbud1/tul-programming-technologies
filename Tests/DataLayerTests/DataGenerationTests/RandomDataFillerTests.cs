using DataLayer.DataGeneration;
using DataLayer.API;
using DataLayer.Implementations;

namespace LogicLayer.DataGenerationTests;

[TestClass]
public class RandomDataFillerTests
{
    [TestMethod]
    public void TestFillData()
    {
        // Arrange
        IDataContext dataContext = new DataContext();
        IDataRepository dataRepository = IDataRepository.Create(dataContext);
        IDataFiller dataFiller = new RandomDataFiller();

        // Act
        dataFiller.FillData(dataContext, dataRepository);

        // Assert
        Assert.AreEqual(1, dataContext.Suppliers.Count);
        Assert.AreEqual(1, dataContext.Products.Count);
        Assert.AreEqual(1, dataContext.Events.Count);
        Assert.AreEqual(2, dataContext.OrderStatuses.Count);
        Assert.AreEqual(1, dataContext.Shops.Count);
        Assert.AreEqual(1, dataContext.Orders.Count);
    }
}