using DataLayer.API;

namespace DataLayer.Implementations.Events;

internal class DeliverOrder: IEvent
{
    public string EventId { get; set; }
    public IOrder Order { get; set; }
    public IShop Shop { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }

    public static DeliverOrder DeliverOrderExtra(IOrder order, IShop shop, IDataRepository dataRepository)
    {
        var deliverOrder = new DeliverOrder(order, shop);
        var orderStatus = dataRepository.GetOrderStatusByOrder(order);
        orderStatus.Status = OrderStatusEnum.Completed;
        dataRepository.UpdateOrderStatus(orderStatus);
        return deliverOrder;
    }

    private DeliverOrder(IOrder order, IShop shop)
    {
        EventId = Guid.NewGuid().ToString();
        Order = order;
        Shop = shop;
        Description = "Order delivered";
        EventDate = DateTime.Now;
    }
}