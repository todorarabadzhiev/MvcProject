using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCampingPlaceControllerInstance_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act
            CampingPlaceController campingPlaceController = new CampingPlaceController(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);

            // Assert
            Assert.IsInstanceOf<CampingPlaceController>(campingPlaceController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingPlaceDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "CampingPlaceProvider";
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new CampingPlaceController(null, sightseeingsProvider, siteCategoryProvider));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSightseeingDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "SightseeingProvider";
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new CampingPlaceController(campingPlaceProvider, null, siteCategoryProvider));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSiteCategoryDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "SiteCategoryProvider";
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new CampingPlaceController(campingPlaceProvider, sightseeingsProvider, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void AssignAllProvidersCorrectValues_WhenProvidedWithValidParameters()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act
            CampingPlaceControllerMock campingPlaceController = new CampingPlaceControllerMock(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);

            // Assert
            Assert.AreSame(campingPlaceProvider, campingPlaceController.CampingPlaceProvider);
            Assert.AreSame(sightseeingsProvider, campingPlaceController.SightseeingProvider);
            Assert.AreSame(siteCategoryProvider, campingPlaceController.SiteCategoryProvider);
        }
    }
}
