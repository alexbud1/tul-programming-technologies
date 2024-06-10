using LogicLayer.Implementations;
using LogicLayer.API;
using DataLayer.API;
using Moq;
using DataLayer.Database;

namespace LogicLayer.UnitTests
{
    [TestClass]
    public class LoginServiceTests
    {
        private Mock<IDataRepository> _dataRepositoryMock;
        private ILoginService _loginService;

        [TestInitialize]
        public void Initialize()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _loginService = new LoginService(_dataRepositoryMock.Object);

            Assert.IsFalse(_loginService.AdminLogged());
            Assert.IsFalse(_loginService.ShopLogged());
            Assert.IsFalse(_loginService.SupplierLogged());
        }

        // Test cases for Login method
        [TestMethod]
        public void Login_AdminLogin_Success()
        {
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Admin, "");
            Assert.IsTrue(result);
            Assert.IsTrue(_loginService.AdminLogged() && !_loginService.ShopLogged() && !_loginService.SupplierLogged());
        }

        [TestMethod]
        public void Login_ShopLogin_Success()
        {
            var shop = new Mock<IShop>();
            shop.Setup(x => x.ShopId).Returns("1");

            _dataRepositoryMock.Setup(x => x.GetShopAsync(It.IsAny<string>())).Returns(Task.FromResult<IShop>(shop.Object));

            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Shop, "1");

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_loginService.AdminLogged() && _loginService.ShopLogged() && !_loginService.SupplierLogged());
        }

        [TestMethod]
        public void Login_SupplierLogin_Success()
        {
            var supplier = new Mock<ISupplier>();
            supplier.Setup(x => x.SupplierId).Returns("1");

            _dataRepositoryMock.Setup(x => x.GetSupplierAsync(It.IsAny<string>())).Returns(Task.FromResult<ISupplier>(supplier.Object));

            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Supplier, "1");

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_loginService.AdminLogged() && !_loginService.ShopLogged() && _loginService.SupplierLogged());
        }

        [TestMethod]
        public void Login_InvalidChoice_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() => _loginService.Login(100, "invalidId"));
        }

        [TestMethod]
        public void Logout_Admin_Success()
        {
            Login_AdminLogin_Success();
            _loginService.Logout();
            Assert.IsFalse(_loginService.AdminLogged() || _loginService.ShopLogged() || _loginService.SupplierLogged());
        }

        [TestMethod]
        public void Logout_Shop_Success()
        {
            Login_ShopLogin_Success();
            _loginService.Logout();
            Assert.IsFalse(_loginService.AdminLogged() || _loginService.ShopLogged() || _loginService.SupplierLogged());
        }

        [TestMethod]
        public void Logout_Supplier_Success()
        {
            Login_SupplierLogin_Success();
            _loginService.Logout();
            Assert.IsFalse(_loginService.AdminLogged() || _loginService.ShopLogged() || _loginService.SupplierLogged());
        }

        [TestMethod]
        public void SetStatus_ValidOrderStatusId_Success()
        {
            // Arrange
            var orderStatusId = "testOrderStatusId";
            var orderId = "testOrderId";

            var orderStatusMock = new Mock<IOrderStatus>();
            orderStatusMock.Setup(x => x.OrderId).Returns(orderId);

            _dataRepositoryMock.Setup(x => x.GetOrderStatusAsync(orderStatusId)).Returns(Task.FromResult<IOrderStatus>(orderStatusMock.Object));

            // Act
            _loginService.SetStatus(orderStatusId, OrderStatusEnum.Completed);

            // Assert
            _dataRepositoryMock.Verify(x => x.UpdateOrderStatusAsync(orderStatusId, OrderStatusEnum.Completed, orderId), Times.Once);
        }


        [TestMethod]
        public void FindOrders_SupplierLogged_Success()
        {
            // Arrange
            var supplierId = "testSupplierId";
            var orderId = "testOrderId";
            var productId = "testProductId";

            var productMock = new Mock<IProduct>();
            productMock.Setup(p => p.ProductId).Returns(productId);
            productMock.Setup(p => p.SupplierId).Returns(supplierId);

            var orderMock = new Mock<IOrder>();
            orderMock.Setup(o => o.OrderId).Returns(orderId);
            orderMock.Setup(o => o.ProductId).Returns(productId);

            var supplierMock = new Mock<ISupplier>();
            supplierMock.Setup(x => x.SupplierId).Returns(supplierId);

            var orderStatusMock = new Mock<IOrderStatus>();
            orderStatusMock.Setup(x => x.OrderId).Returns(orderId);

            var orderStatuses = new Dictionary<string, IOrderStatus>
    {
        { "test", orderStatusMock.Object }
    };

            _dataRepositoryMock.Setup(x => x.GetProductAsync(productId)).Returns(Task.FromResult<IProduct>(productMock.Object));
            _dataRepositoryMock.Setup(x => x.GetSupplierAsync(It.IsAny<string>())).Returns(Task.FromResult<ISupplier>(supplierMock.Object));
            _dataRepositoryMock.Setup(x => x.GetOrderAsync(orderId)).Returns(Task.FromResult<IOrder>(orderMock.Object));
            _dataRepositoryMock.Setup(x => x.GetOrderStatusesAsync()).ReturnsAsync(orderStatuses);

            _loginService.Login(ILoginService.LoginChoiceEnum.Supplier, supplierId);

            // Act
            var result = _loginService.FindOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(orderId, result[0].OrderId);
        }




        [TestMethod]
        public void FindOrders_ShopLogged_ReturnsOrders()
        {
            // Arrange
            var shopId = "testShopId";
            var orderId = "testOrderId";
            var orderStatusMock = new Mock<IOrderStatus>();
            orderStatusMock.Setup(x => x.OrderId).Returns(orderId);
            var orderStatuses = new Dictionary<string, IOrderStatus>
            {
                { "test", orderStatusMock.Object }
            };

            var shopMock = new Mock<IShop>();
            shopMock.Setup(x => x.ShopId).Returns(shopId);

            _dataRepositoryMock.Setup(x => x.GetShopAsync(It.IsAny<string>())).Returns(Task.FromResult<IShop>(shopMock.Object));

            var orderMock = new Mock<IOrder>();
            orderMock.Setup(o => o.OrderId).Returns(orderId);
            orderMock.Setup(o => o.ShopId).Returns(shopId);


            _dataRepositoryMock.Setup(x => x.GetOrderStatusesAsync()).ReturnsAsync(orderStatuses);
            _dataRepositoryMock.Setup(x => x.GetOrderAsync(orderId)).Returns(Task.FromResult<IOrder>(orderMock.Object));

            _loginService.Login(ILoginService.LoginChoiceEnum.Shop, shopId);

            // Act
            var result = _loginService.FindOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(orderId, result[0].OrderId);
        }




        [TestMethod]
        public void FindOrders_NotLogged_ThrowsException()
        {
            Assert.ThrowsException<Exception>(() => _loginService.FindOrders());
        }
    }
}
