using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Areas.Admin.Controllers;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Admin.Controllers.UserControllerClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void AssignProviderCorrectValue_WhenProvidedWithValidParameter()
        {
            // Arrange
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();

            // Act
            UserControllerMock campingUserController = new UserControllerMock(campingUserProvider);

            // Assert
            Assert.AreSame(campingUserProvider, campingUserController.CampingUserDataProvider);
        }

        [Test]
        public void CreateCampingUserControllerInstance_WhenProvidedArgumentIsValid()
        {
            // Arrange
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();

            // Act
            UserController campingUserController = new UserController(campingUserProvider);

            // Assert
            Assert.IsInstanceOf<UserController>(campingUserController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingUserDataProviderIsNull()
        {
            //Arrange
            string expectedMessage = "CampingUserDataProvider";

            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new UserController(null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }
    }
}
