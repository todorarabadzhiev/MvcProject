using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateSiteCategoryControllerInstance_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act
            SiteCategoryController siteCategoryController = new SiteCategoryController(
                siteCategoryProvider, campingPlaceProvider);

            // Assert
            Assert.IsInstanceOf<SiteCategoryController>(siteCategoryController);
        }
        
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSiteCategoryDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "SiteCategoryDataProvider";
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SiteCategoryController(null, campingPlaceProvider));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingPlaceDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "CampingPlaceProvider";
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SiteCategoryController(siteCategoryProvider, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void AssignAllProvidersCorrectValues_WhenProvidedWithValidParameters()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();

            // Act
            SiteCategoryControllerMock campingPlaceController = new SiteCategoryControllerMock(
                siteCategoryProvider, campingPlaceProvider);

            // Assert
            Assert.AreSame(campingPlaceProvider, campingPlaceController.CampingPlaceProvider);
            Assert.AreSame(siteCategoryProvider, campingPlaceController.SiteCategoryDataProvider);
        }
    }
}
