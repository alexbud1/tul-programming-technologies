using Castle.Components.DictionaryAdapter.Xml;
using LogicLayer.Implementations;
using LogicLayer.API;
using DataLayer.API;
using Moq;

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

            _dataRepositoryMock.Setup(x => x.GetShopById(It.IsAny<string>())).Returns(shop.Object);
            
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

            
            _dataRepositoryMock.Setup(x => x.GetSupplierById(It.IsAny<string>())).Returns(supplier.Object);
            // Act
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
        public void SetStatus_Correct()
        {
            Login_SupplierLogin_Success();

            var orderStatusMock = new Mock<IOrderStatus>();
            orderStatusMock.Setup(x => x.OrderStatusId).Returns("1");
            orderStatusMock.Setup(x => x.Status).Equals(OrderStatusEnum.Processing);

            _dataRepositoryMock.Setup(x => x.GetOrdersStatuses()).Returns(new List<IOrderStatus> {orderStatusMock.Object});
            _loginService.SetStatus("1", OrderStatusEnum.Cancelled);
            
            Assert.ThrowsException<NullReferenceException>(() => _loginService.SetStatus("2", OrderStatusEnum.Cancelled));
            Assert.ThrowsException<NullReferenceException>(() => _loginService.SetStatus("2", "Pending"));
        }

        [TestMethod]
        public void FindOrders()
        {
            var orderStatusMock = new Mock<IOrderStatus>();
            _dataRepositoryMock.Setup(x => x.GetOrdersStatuses()).Returns(new List<IOrderStatus> {orderStatusMock.Object});

            Assert.ThrowsException<Exception>(() => _loginService.FindOrders());
        }
        
        
    }
}
