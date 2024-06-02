﻿using DataLayer.API;
using DataLayer.Implementations;

namespace Tests.DataLayerTests;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void TestProductConstructor()
    {
        // Arrange
        string productName = "Test Product";
        string productDescription = "Test Description";
        decimal productPrice = 10.0m;
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");

        // Act
        IProduct product = new Product("1", productName, productDescription, productPrice, supplier.SupplierId);

        // Assert
        Assert.AreEqual(productName, product.ProductName);
        Assert.AreEqual(productDescription, product.ProductDescription);
        Assert.AreEqual(productPrice, product.ProductPrice);
        Assert.AreEqual(supplier.SupplierId, product.SupplierId);
    }

    [TestMethod]
    public void TestProductIdIsNotNull()
    {
        // Arrange
        string productName = "Test Product";
        string productDescription = "Test Description";
        decimal productPrice = 10.0m;
        ISupplier supplier = new Supplier("1", "Test Supplier", "Test Address");

        // Act
        IProduct product = new Product("1", productName, productDescription, productPrice, supplier.SupplierId);

        // Assert
        Assert.IsNotNull(product.ProductId);
    }
}