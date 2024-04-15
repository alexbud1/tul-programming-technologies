using DataLayer.API;

namespace DataLayer.Implementations;

public class Order: IOrder
{
    public string OrderId { get; set; }
    public IProduct Product { get; set; }
    public IShop Shop { get; set; }

    public Order(IProduct product, IShop shop)
    {
        OrderId = Guid.NewGuid().ToString();
        Product = product;
        Shop = shop;
    }
}