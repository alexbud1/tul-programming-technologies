using DataLayer.API;

namespace DataLayer.Implementations;

internal class Supplier : ISupplier
{
    public string SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }

    public Supplier(string supplierName, string supplierAddress)
    {
        SupplierId = Guid.NewGuid().ToString();
        SupplierName = supplierName;
        SupplierAddress = supplierAddress;
    }
}