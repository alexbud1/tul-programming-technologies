using DataLayer.API;

namespace DataLayer.Implementations;

internal class OrderStatus: IOrderStatus
{
    public string OrderStatusId { get; set; }
    public string OrderId { get; set; }
    public OrderStatusEnum Status { get; set; }

    public OrderStatus(string orderId)
    {
        OrderStatusId = Guid.NewGuid().ToString();
        OrderId = orderId;
        Status = OrderStatusEnum.Pending; // Default status
    }
}