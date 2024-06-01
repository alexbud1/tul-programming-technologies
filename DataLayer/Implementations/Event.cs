using DataLayer.API;

namespace DataLayer.Implementations;

internal class Event: IEvent
{
    public string EventId { get; set; }
    public string OrderId { get; set; }
    public string ShopId { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }

    public Event(string orderId, string shopId)
    {
        EventId = Guid.NewGuid().ToString();
        OrderId = orderId;
        ShopId = shopId;
        Description = "Order approved";
        EventDate = DateTime.Now;
    }

}