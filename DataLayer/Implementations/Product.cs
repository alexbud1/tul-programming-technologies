using DataLayer.API;

namespace DataLayer.Implementations;

internal class Product: IProduct
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public string SupplierId { get; set; }

    public Product(string productName, string productDescription, decimal productPrice, string supplierId)
    {
        ProductId = Guid.NewGuid().ToString();
        ProductName = productName;
        ProductDescription = productDescription;
        ProductPrice = productPrice;
        SupplierId = supplierId;
    }
}