using DataLayer.API;

namespace DataLayer.Implementations.Events;

public class CancelOrder: IEvent
{
    public string EventId { get; set; }
    public IOrder Order { get; set; }
    public IShop Shop { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }

    public static CancelOrder CancelOrderExtra(IOrder order, IShop shop, IDataRepository dataRepository)
    {
        var cancelOrder =  new CancelOrder(order, shop);
        var orderStatus = dataRepository.GetOrderStatusByOrder(order);
        orderStatus.Status = OrderStatusEnum.Cancelled;
        dataRepository.UpdateOrderStatus(orderStatus);

        return cancelOrder;
    }

    private CancelOrder(IOrder order, IShop shop)
    {
        EventId = Guid.NewGuid().ToString();
        Order = order;
        Shop = shop;
        Description = "Order cancelled";
        EventDate = DateTime.Now;
    }
}