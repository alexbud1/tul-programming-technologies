namespace DataLayer.API;

public interface IEvent
{
    DateTime EventDate { get; set; }
    string EventId { get; set; }
    string Description { get; set; }
}