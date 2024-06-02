using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class SupplierTests
{
    [TestMethod]
    public void TestSupplierConstructor()
    {
        // Arrange
        string supplierName = "Test Supplier";
        string supplierAddress = "Test Address";

        // Act
        ISupplier supplier = new Supplier("1", supplierName, supplierAddress);

        // Assert
        Assert.AreEqual(supplierName, supplier.SupplierName);
        Assert.AreEqual(supplierAddress, supplier.SupplierAddress);
    }

    [TestMethod]
    public void TestSupplierIdIsNotNull()
    {
        // Arrange
        string supplierName = "Test Supplier";
        string supplierAddress = "Test Address";

        // Act
        ISupplier supplier = new Supplier("1", supplierName, supplierAddress);

        // Assert
        Assert.IsNotNull(supplier.SupplierId);
    }

}