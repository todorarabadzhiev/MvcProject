using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateSightseeingControllerInstance_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingDataProvider = Mock.Create<ISightseeingDataProvider>();

            // Act
            SightseeingController sightseeingController = new SightseeingController(
                sightseeingDataProvider, campingPlaceProvider);

            // Assert
            Assert.IsInstanceOf<SightseeingController>(sightseeingController);
        }
        
        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSightseeingDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "SightseeingDataProvider";
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SightseeingController(null, campingPlaceProvider));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingPlaceDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "CampingPlaceProvider";
            var sightseeingDataProvider = Mock.Create<ISightseeingDataProvider>();

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SightseeingController(sightseeingDataProvider, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void AssignAllProvidersCorrectValues_WhenProvidedWithValidParameters()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingDataProvider = Mock.Create<ISightseeingDataProvider>();

            // Act
            SightseeingControllerMock sightseeingController = new SightseeingControllerMock(
                sightseeingDataProvider, campingPlaceProvider);

            // Assert
            Assert.AreSame(campingPlaceProvider, sightseeingController.CampingPlaceProvider);
            Assert.AreSame(sightseeingDataProvider, sightseeingController.SightseeingDataProvider);
        }
    }
}
