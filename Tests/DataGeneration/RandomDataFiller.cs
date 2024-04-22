namespace Tests.DataGeneration;

using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;


public class RandomDataFiller: IDataFiller
{
    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random rnd = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[rnd.Next(s.Length)]).ToArray());
    }

    private int GenerateRandomNumber(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }

    public void FillData(IDataContext dataContext, IDataRepository dataRepository)
    {
        ISupplier supplier = new Supplier(GenerateRandomString(GenerateRandomNumber(1,100)), GenerateRandomString(GenerateRandomNumber(1,100)));
        dataContext.Suppliers.Add(supplier);

        IProduct product = new Product(GenerateRandomString(GenerateRandomNumber(1,100)), GenerateRandomString(GenerateRandomNumber(1,100)), GenerateRandomNumber(1,100), supplier);
        dataContext.Products.Add(product);

        IShop shop = new Shop(GenerateRandomString(GenerateRandomNumber(1,100)), GenerateRandomString(GenerateRandomNumber(1,100)));
        dataContext.Shops.Add(shop);

        IOrder order = new Order(product, shop, dataRepository);
        dataContext.Orders.Add(order);

        ApproveOrder approveOrder = ApproveOrder.ApproveOrderExtra(order, shop, dataRepository);
        dataContext.Events.Add(approveOrder);

        IOrderStatus orderStatus = new OrderStatus(order);
        dataContext.OrderStatuses.Add(orderStatus);
    }
}