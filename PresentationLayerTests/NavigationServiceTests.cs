using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ViewModel;
using Moq;
using LogicLayer.API;

namespace Tests.PresentationLayerTests
{
    [TestClass]
    public class NavigationServiceTests
    {
        private NavigationService _navigationService;
        private Mock<AdminViewModel> _adminPanelViewModelMock;
        private Mock<ILoginService> _loginServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            var dataLayer = new DataLayer.Implementations.DataRepository(new DataLayer.Implementations.DataContext());
            var logicLayer = new LogicLayer.Implementations.LoginService(dataLayer);
            _navigationService = new NavigationService(logicLayer);
            _adminPanelViewModelMock = new Mock<AdminViewModel>();
            _loginServiceMock = new Mock<ILoginService>();
        }

        [TestMethod]
        public void NavigationService_Initialize_CurrentViewModelSetCorrectly()
        {
            // Act
            var currentViewModel = _navigationService.CurrentViewModel;

            // Assert
            Assert.IsInstanceOfType(currentViewModel, typeof(LoginViewModel));
        }
    }

}
