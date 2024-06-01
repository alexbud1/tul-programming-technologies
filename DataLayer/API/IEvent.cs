namespace DataLayer.API;

public interface IEvent
{
    string EventId { get; set; }
    string OrderId { get; set; }
    string ShopId { get; set; }
    string Description { get; set; }
    DateTime EventDate { get; set; }

}