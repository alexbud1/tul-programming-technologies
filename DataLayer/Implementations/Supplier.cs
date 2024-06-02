using DataLayer.API;

namespace DataLayer.Implementations;

internal class Supplier : ISupplier
{
    public string SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }

    public Supplier(string supplierId ,string supplierName, string supplierAddress)
    {
        SupplierId = supplierId;
        SupplierName = supplierName;
        SupplierAddress = supplierAddress;
    }
}