using DataLayer.API;

namespace DataLayer.Implementations;

public class Product: IProduct
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    private decimal ProductPrice { get; set; }
    public ISupplier Supplier { get; set; }

    public Product(string productName, string productDescription, decimal productPrice, ISupplier supplier)
    {
        ProductId = Guid.NewGuid().ToString();
        ProductName = productName;
        ProductDescription = productDescription;
        ProductPrice = productPrice;
        Supplier = supplier;
    }
}