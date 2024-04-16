namespace DataLayer.API;

public interface IProduct
{
    string ProductId { get; set; }
    string ProductName { get; set; }
    string ProductDescription { get; set; }
    decimal ProductPrice { get; set; }
    ISupplier Supplier { get; set; }
}