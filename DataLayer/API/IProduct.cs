namespace DataLayer.API;

public interface IProduct
{
    string ProductId { get; set; }
    string ProductName { get; set; }
    string ProductDescription { get; set; }
    decimal ProductPrice { get; set; }
    string SupplierId { get; set; }
}