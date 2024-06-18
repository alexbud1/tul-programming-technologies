using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ViewModel;
using LogicLayer.API;
using DataLayer.API;

namespace Tests.ViewModelTests
{
    [TestClass]
    public class AdminViewModelTests
    {
        private AdminViewModel _adminViewModel;
        private Mock<NavigationService> _navigationService;
        private Mock<ILoginService> _loginServiceMock;
        private Mock<IDataRepository> _repositoryMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginServiceMock = new Mock<ILoginService>();
            _loginServiceMock.Setup(x => x.AdminLogged()).Returns(true);
            _loginServiceMock.Setup(x => x.FindShops()).Returns(new List<IShop> { new Mock<IShop>().Object });
            _loginServiceMock.Setup(x => x.FindProducts()).Returns(new List<IProduct> { new Mock<IProduct>().Object });
            _loginServiceMock.Setup(x => x.FindSuppliers()).Returns(new List<ISupplier> { new Mock<ISupplier>().Object });
            _loginServiceMock.Setup(x => x.Login(It.IsAny<ILoginService.LoginChoiceEnum>(), It.IsAny<string>())).Returns(true);

            _repositoryMock = new Mock<IDataRepository>();
            _navigationService = new Mock<NavigationService>(_loginServiceMock.Object);
            //_navigationService.SetupGet(x => x.LogicLayer).Returns(_loginServiceMock.Object);
            //_navigationService.SetupGet(x => x.DataLayer).Returns(_repositoryMock.Object);

            _adminViewModel = new AdminViewModel(_navigationService.Object);
            /*{
                Shops = new ObservableCollection<IShop>(),
                Products = new ObservableCollection<IProduct>(),
                Szuppliers = new ObservableCollection<ISupplier>()
            };*/
            _adminViewModel.GetType().GetProperty("Shops")?.SetValue(_adminViewModel, new ObservableCollection<IShop>());
            _adminViewModel.GetType().GetProperty("Products")?.SetValue(_adminViewModel, new ObservableCollection<IProduct>());
            _adminViewModel.GetType().GetProperty("Suppliers")?.SetValue(_adminViewModel, new ObservableCollection<ISupplier>());
        }
        [TestMethod]
        public void TestLoadShops()
        {
            _adminViewModel.LoadShops();

            _loginServiceMock.Verify(x => x.Login(ILoginService.LoginChoiceEnum.Admin, ""), Times.Once);
            _loginServiceMock.Verify(x => x.FindShops(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestLoadProducts()
        {
            _adminViewModel.LoadProducts();

            _loginServiceMock.Verify(x => x.Login(ILoginService.LoginChoiceEnum.Admin, ""), Times.Once);
            _loginServiceMock.Verify(x => x.FindProducts(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestLoadSuppliers()
        {
            _adminViewModel.LoadSuppliers();

            _loginServiceMock.Verify(x => x.Login(ILoginService.LoginChoiceEnum.Admin,""), Times.Once);
            _loginServiceMock.Verify(x => x.FindSuppliers(), Times.AtLeastOnce);

        }
    }
}
