using DataLayer.Implementations;
using DataLayer.API;

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
        IShop shop = new Shop(shopName, shopAddress);

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
        IShop shop = new Shop(shopName, shopAddress);

        // Assert
        Assert.IsNotNull(shop.ShopId);
    }

}