using DataLayer.API;

namespace DataLayer.Implementations.Events;

public class ApproveOrder: IEvent
{
    public string EventId { get; set; }
    public IOrder Order { get; set; }
    public IShop Shop { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }

    public static ApproveOrder ApproveOrderExtra(IOrder order, IShop shop, IDataRepository dataRepository)
    {
        var approveOrder = new ApproveOrder(order, shop);
        var orderStatus = dataRepository.GetOrderStatusByOrder(order);
        orderStatus.Status = OrderStatusEnum.Processing;
        dataRepository.UpdateOrderStatus(orderStatus);

        return approveOrder;
    }

    private ApproveOrder(IOrder order, IShop shop)
    {
        EventId = Guid.NewGuid().ToString();
        Order = order;
        Shop = shop;
        Description = "Order approved";
        EventDate = DateTime.Now;
    }

}