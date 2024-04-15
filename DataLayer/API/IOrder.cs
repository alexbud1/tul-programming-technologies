namespace DataLayer.API;

public interface IOrder
{
    string OrderId { get; set; }
    IProduct Product { get; set; }
    IShop Shop { get; set; }
}