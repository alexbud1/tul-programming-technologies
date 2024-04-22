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
            
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Shop, "shopId");
            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_loginService.AdminLogged() && _loginService.ShopLogged() && !_loginService.SupplierLogged());
        }

        [TestMethod]
        public void Login_SupplierLogin_Success()
        {
            // Arrange
            //_dataRepositoryMock.Setup(repo => repo.GetSupplierById(It.IsAny<string>())).Returns(new Supplier()); // Mocking the GetSupplierById method
            // Act
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Supplier, "supplierId");
            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(!_loginService.AdminLogged() && !_loginService.ShopLogged() && _loginService.SupplierLogged());
        }

        [TestMethod]
        public void Login_InvalidChoice_ThrowsException()
        {
            Assert.ThrowsException<Exception>(() => _loginService.Login(100, "invalidId"));
        }

        // More test cases for other methods can be added similarly...
    }
}
