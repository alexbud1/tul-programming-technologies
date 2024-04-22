using System;
using System.Collections.Generic;
using DataLayer.API;

namespace LogicLayer.API;
public interface ILoginService
{
    #region Login

    bool Login(LoginChoiceEnum loginChoice, string id);
    bool Login(int loginChoice, string id);
    void Logout();
    bool AdminLogged();
    bool ShopLogged();
    bool SupplierLogged();

    #endregion Login

    #region Order

    void PlaceOrder(string orderId, string productId);
    void CancelOrder(string orderId);
    List<IOrderStatus> FindOrders();
    IOrder FindOrderById(string id);
    List<IOrderStatus> FindOrdersByStatus(OrderStatusEnum status);

    #endregion Order

    #region Status

    void SetStatus(string orderStatusId, OrderStatusEnum status);
    void SetStatus(string orderStatusId, string status);
    void SetPending(string orderStatusId);
    void SetProcessing(string orderStatusId);
    void SetCompleted(string orderStatusId);
    void SetCancelled(string orderStatusId);

    #endregion Status
        
    public enum LoginChoiceEnum
    {
        Admin,
        Shop,
        Supplier
    }
}