using LogicLayer.Implementations;
using LogicLayer.API;
using DataLayer.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogicLayer.UnitTests
{
    [TestClass]
    public class LoginServiceTests
    {
        private Mock<IDataRepository> _dataRepositoryMock;
        private LoginService _loginService;

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
            // Arrange
            // Act
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Admin, "adminId");
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_ShopLogin_Success()
        {
            // Arrange
            _dataRepositoryMock.Setup(repo => repo.GetShopById(It.IsAny<string>())).Returns(new Shop()); // Mocking the GetShopById method
            // Act
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Shop, "shopId");
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_SupplierLogin_Success()
        {
            // Arrange
            _dataRepositoryMock.Setup(repo => repo.GetSupplierById(It.IsAny<string>())).Returns(new Supplier()); // Mocking the GetSupplierById method
            // Act
            var result = _loginService.Login(ILoginService.LoginChoiceEnum.Supplier, "supplierId");
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_InvalidChoice_ThrowsException()
        {
            // Arrange
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _loginService.Login(100, "invalidId"));
        }

        // More test cases for other methods can be added similarly...
    }
}
