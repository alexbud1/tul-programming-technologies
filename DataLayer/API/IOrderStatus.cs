namespace DataLayer.API;

public enum OrderStatusEnum
{
    Pending,
    Processing,
    Completed,
    Cancelled
}

public interface IOrderStatus
{
    string OrderStatusId { get; set; }
    string OrderId { get; set; }
    OrderStatusEnum Status { get; set; }
}