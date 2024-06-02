using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class ShopTests
{
    [TestMethod]
    public void TestShopConstructor()
    {
        // Arrange
        string shopName = "Test Shop";
        string shopAddress = "Test Address";

        // Act
        IShop shop = new Shop("1", shopName, shopAddress);

        // Assert
        Assert.AreEqual(shopName, shop.ShopName);
        Assert.AreEqual(shopAddress, shop.ShopAddress);
    }

    [TestMethod]
    public void TestShopIdIsNotNull()
    {
        // Arrange
        string shopName = "Test Shop";
        string shopAddress = "Test Address";

        // Act
        IShop shop = new Shop("1", shopName, shopAddress);

        // Assert
        Assert.IsNotNull(shop.ShopId);
    }

}