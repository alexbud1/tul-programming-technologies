using DataLayer.API;

namespace DataLayer.Implementations;

internal class Product: IProduct
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public string SupplierId { get; set; }

    public Product(string productId , string productName, string productDescription, decimal productPrice, string supplierId)
    {
        ProductId = productId;
        ProductName = productName;
        ProductDescription = productDescription;
        ProductPrice = productPrice;
        SupplierId = supplierId;
    }
}