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
    IOrder OrderId { get; set; }
    OrderStatusEnum Status { get; set; }
}