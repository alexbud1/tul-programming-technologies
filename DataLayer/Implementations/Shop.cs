using DataLayer.API;

namespace DataLayer.Implementations;

internal class Shop: IShop
{
    public string ShopId { get; set; }
    public string ShopName { get; set; }
    public string ShopAddress { get; set; }

    public Shop(string shopName, string shopAddress)
    {
        ShopId = Guid.NewGuid().ToString();
        ShopName = shopName;
        ShopAddress = shopAddress;
    }
}