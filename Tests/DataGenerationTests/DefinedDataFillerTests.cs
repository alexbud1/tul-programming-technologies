namespace Tests.DataGenerationTests;

using DataLayer.API;
using DataLayer.Implementations;
using Tests.DataGeneration;

[TestClass]
public class DefinedDataFillerTests
{
    [TestMethod]
    public void TestFillData()
    {
        // Arrange
        IDataContext dataContext = new DataContext();
        IDataRepository dataRepository = IDataRepository.Create(dataContext);
        IDataFiller dataFiller = new DefinedDataFiller();

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