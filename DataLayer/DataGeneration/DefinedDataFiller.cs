// using DataLayer.API;
// using DataLayer.Implementations;
// using DataLayer.Implementations.Events;
//
// namespace DataLayer.DataGeneration;
//
// public class DefinedDataFiller : IDataFiller
// {
//     public void FillData(IDataContext dataContext, IDataRepository dataRepository)
//     {
//         ISupplier supplier = new Supplier("Test Supplier", "Test Address");
//         dataContext.Suppliers.Add(supplier);
//
//         IProduct product = new Product("Test Product", "Test Description", 10.0m, supplier);
//         dataContext.Products.Add(product);
//
//         IShop shop = new Shop("Test Shop", "Test Address");
//         dataContext.Shops.Add(shop);
//
//         IOrder order = new Order(product, shop, dataRepository);
//         dataContext.Orders.Add(order);
//
//         ApproveOrder approveOrder = ApproveOrder.ApproveOrderExtra(order, shop, dataRepository);
//         dataContext.Events.Add(approveOrder);
//
//         IOrderStatus orderStatus = new OrderStatus(order);
//         dataContext.OrderStatuses.Add(orderStatus);
//     }
// }