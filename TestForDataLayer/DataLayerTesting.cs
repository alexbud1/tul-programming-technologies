namespace TestForDataLayer;

[TestClass]
public class DataLayerTesting
{
    [TestMethod]
    public void MainTest()
    {
    }

    [TestMethod]
    public void SupplierTesting()
    {
        Supplier supplier1 = new Supplier("DC Wooden Furniture", "Warsaw"); //class Supplier(name, city);

        #region get&set
        Assert.AreEqual("DC Wooden Furniture", supplier1.getName()); //compare names
        Assert.AreEqual("Warsaw", supplier1.getLoc()); //compare locations

        supplier1.setName("New Furniture sp.z.o.o");
        Assert.AreEqual("New Furniture sp.z.o.o", supplier1.getName());

        supplier1.setLoc("Lodz");
        Assert.AreEqual("Lodz", supplier1.getLoc());
        
        #endregion get&set
        
        #region products
        
        Furniture furniture1 = new Furniture(129.99, "desk");
        supplier1.addProduct(furniture1); //adding a product to supplier stock
        Assert.AreEqual(1, supplier1.getStockSize());
        Assert.AreEqual(furniture1, supplier1.getProduct(0));

        supplier1.removeProduct(0); //remove a product from the stock with index = 0
        Assert.AreEqual(0, supplier1.getStockSize());
        
        #endregion products
    }

    [TestMethod]
    public void FurnitureTesting()
    {
        Furniture furniture1 = new Furniture(195.81,"wooden"); //Furniture : IProduct <- interface
        Assert.AreEqual("wooden", furniture1.getMaterial());
        Assert.AreEqual(195.81, furniture1.getPrice()); //Derived from IProduct class
        
    }

    [TestMethod]
    public void OrdersQueue()
    { 
        Furniture furniture1 = new Furniture(15.60, "product_name");
        Order order1 = new Order(1, furniture1);
        Order order2 = new Order(2, furniture1);
        OrdersQueue queue1 = new OrdersQueue(10);
        queue1.enqueue(order1);
        queue1.enqueue(order2);
        
        Assert.AreEqual(order1, queue1.dequeue());
        Assert.IsFalse(queue1.isEmpty());
        Assert.AreEqual(order2, queue1.dequeue());
        Assert.IsTrue(queue1.isEmpty());
    }

    [TestMethod]
    public void OrderTesting()
    {
        Furniture furniture1 = new Furniture(15.60, "product_name");
        Order order1 = new Order(1, furniture1); //Order(order_id, IProduct);
        Assert.AreEqual(1, order1.getId());
        Assert.AreEqual(furniture1, order1.getProduct());
    }

    [TestMethod]
    public void ShopTesting() {
        Shop ikea = new Shop("Ikea", "Warsaw"); //class Shop(name, city)
    }
}