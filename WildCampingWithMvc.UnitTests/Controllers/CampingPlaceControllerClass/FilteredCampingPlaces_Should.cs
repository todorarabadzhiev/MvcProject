using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class FilteredCampingPlaces_Should
    {
        private CampingPlaceControllerMock campingPlaceController;

        [SetUp]
        public void ArrangeBeforeAnyTest()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            this.campingPlaceController = new CampingPlaceControllerMock(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CallCampPlaceDataProviderMethodGetAllCampingPlacesExactlyOnceWithNoParameters_WhenSearchTermIsNullOrEmpty(string searchTerm)
        {
            // Arrange
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(3);
            Mock.Arrange(() => this.campingPlaceController.CampingPlaceProvider.GetAllCampingPlaces())
                .Returns(campingPlaces);

            // Act
            this.campingPlaceController.FilteredCampingPlaces(searchTerm);

            // Assert
            Mock.Assert(() => this.campingPlaceController.CampingPlaceProvider.GetAllCampingPlaces(), Occurs.Once());
        }

        [TestCase(null)]
        [TestCase("")]
        public void ReturnPartialViewMultipleCampingPlacesPartialWithCorrectViewModel_WhenSearchTermIsNullOrEmpty(string searchTerm)
        {
            // Arrange
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(4);
            Mock.Arrange(() => this.campingPlaceController.CampingPlaceProvider.GetAllCampingPlaces())
                .Returns(campingPlaces);

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.FilteredCampingPlaces(searchTerm))
                .ShouldRenderPartialView("_MultipleCampingPlacesPartial")
                .WithModel<MultipleCampingPlacesViewModel>(viewModel =>
                {
                    Assert.AreEqual(viewModel.CampingPlaces.Count(), campingPlaces.Count());
                });
        }

        [Test]
        public void CallCampPlaceDataProviderMethodGetCampingPlacesBySearchNameExactlyOnceWithCorrectParameter_WhenSearchTermIsNotNullOrEmpty()
        {
            // Arrange
            string searchTerm = "some search term";
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(1);
            Mock.Arrange(() => this.campingPlaceController.CampingPlaceProvider.GetCampingPlacesBySearchName(searchTerm))
                .Returns(campingPlaces);

            // Act
            this.campingPlaceController.FilteredCampingPlaces(searchTerm);

            // Assert
            Mock.Assert(() => this.campingPlaceController.CampingPlaceProvider.GetCampingPlacesBySearchName(searchTerm), Occurs.Once());
        }

        [Test]
        public void ReturnPartialViewMultipleCampingPlacesPartialWithCorrectViewModel_WhenSearchTermIsNotNullOrEmpty()
        {
            // Arrange
            string searchTerm = "some search term";
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(2);
            Mock.Arrange(() => this.campingPlaceController.CampingPlaceProvider.GetCampingPlacesBySearchName(searchTerm))
                .Returns(campingPlaces);

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.FilteredCampingPlaces(searchTerm))
                .ShouldRenderPartialView("_MultipleCampingPlacesPartial")
                .WithModel<MultipleCampingPlacesViewModel>(viewModel =>
                {
                    Assert.AreEqual(viewModel.CampingPlaces.Count(), campingPlaces.Count());
                });
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.campingPlaceController = null;
        }
    }
}
