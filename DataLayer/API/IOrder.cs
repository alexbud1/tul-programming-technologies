namespace DataLayer.API;

public interface IOrder
{
    string OrderId { get; set; }
    string ProductId { get; set; }
    string ShopId { get; set; }
}