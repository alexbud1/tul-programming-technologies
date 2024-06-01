using DataLayer.API;

namespace DataLayer.Implementations;

internal class Order: IOrder
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public string ShopId { get; set; }

    public Order(string productId, string shopId)
    {
        OrderId = Guid.NewGuid().ToString();
        ProductId = productId;
        ShopId = shopId;

        // IOrderStatus status = new OrderStatus(this);
        // dataRepository.AddOrderStatus(status);
    }
}