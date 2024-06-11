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
            _navigationService = new NavigationService();
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
