using NUnit.Framework;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.HomeControllerClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateHomeControllerInstance_WhenProvidedArgumentIsValid()
        {
            // Arrange
            var provider = Mock.Create<ICampingPlaceDataProvider>();

            // Act
            HomeController homeController = new HomeController(provider);

            // Assert
            Assert.IsInstanceOf<HomeController>(homeController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingPlaceDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "CampingPlaceDataProvider";

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new HomeController(null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void AssignProviderCorrectValue_WhenProvidedWithValidParameter()
        {
            // Arrange
            var provider = Mock.Create<ICampingPlaceDataProvider>();

            // Act
            HomeControllerMock homeController = new HomeControllerMock(provider);

            // Assert
            Assert.AreSame(provider, homeController.CampPlaceDataProvider);
        }
    }
}
