using DataLayer.API;

namespace DataLayer.Implementations;

public class OrderStatus: IOrderStatus
{
    public string OrderStatusId { get; set; }
    public IOrder Order { get; set; }
    public OrderStatusEnum Status { get; set; }

    public OrderStatus(IOrder orderId)
    {
        OrderStatusId = Guid.NewGuid().ToString();
        Order = orderId;
        Status = OrderStatusEnum.Pending; // Default status
    }
}