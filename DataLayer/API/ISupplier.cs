namespace DataLayer.API;

public interface ISupplier
{
    string SupplierId { get; set; }
    string SupplierName { get; set; }
    string SupplierAddress { get; set; }
}