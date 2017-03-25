using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System.Collections.Generic;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.UnitTests.Controllers.HomeControllerClass
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallCampPlaceDataProviderMethodGetLatestCampingPlacesExactlyOnceWithCorrectParameter()
        {
            // Arrange
            int placesToReturn = 6;
            var campingPlaceDataProviderMock = Mock.Create<ICampingPlaceDataProvider>();

            HomeController homeController = new HomeController(campingPlaceDataProviderMock);

            // Act
            homeController.Index();

            // Assert
            Mock.Assert(() => campingPlaceDataProviderMock.GetLatestCampingPlaces(placesToReturn), Occurs.Once());
        }

        [Test]
        public void ReturnViewWithModelWithCorrectCampingPlaces()
        {
            // Arrange
            int placesToReturn = 6;
            var latestPlaces = Mock.Create<IEnumerable<ICampingPlace>>();
            var campingPlaceDataProviderMock = Mock.Create<ICampingPlaceDataProvider>();
            Mock.Arrange(() => campingPlaceDataProviderMock.GetLatestCampingPlaces(placesToReturn))
                .Returns(latestPlaces);
            HomeController homeController = new HomeController(campingPlaceDataProviderMock);

            // Act & Assert
            homeController
                .WithCallTo(h => h.Index())
                .ShouldRenderDefaultView()
                .WithModel<MultipleCampingPlacesViewModel>(viewModel =>
                {
                    Assert.AreSame(latestPlaces, viewModel.CampingPlaces);
                });
        }
    }
}
