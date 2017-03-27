using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using WildCampingWithMvc.App_GlobalResources;
using WildCampingWithMvc.Areas.Admin.Controllers;
using WildCampingWithMvc.Areas.Admin.Models;

namespace WildCampingWithMvc.UnitTests.Admin.Controllers.UserControllerClass
{
    [TestFixture]
    public class UpdateUser_Should
    {
        [Test]
        public void ReturnGlobalResourcesErrValidation_WhenModelStateIsInvalid()
        {
            // Arrange
            string err = GlobalResources.ErrValidation;
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            userController.ModelState.AddModelError("SomeError", "Error");
            UserViewModel userModel = this.GetUserViewModel();

            // Act
            string msg = userController.UpdateUser(userModel);

            // Assert
            Assert.AreEqual(msg, err);
        }

        [Test]
        public void ReturnGlobalResourcesErrDbUpdate_WhenModelStateIsValidButCouldNotUpdateUser()
        {
            // Arrange
            string err = GlobalResources.ErrDbUpdate;
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            userController.ModelState.Clear();
            Mock.Arrange(() => campingUserProvider.UpdateCampingUser(Arg.AnyGuid, Arg.AnyString, Arg.AnyString))
                .Returns(0);
            UserViewModel userModel = this.GetUserViewModel();

            // Act
            string msg = userController.UpdateUser(userModel);

            // Assert
            Assert.AreEqual(msg, err);
        }

        [Test]
        public void ReturnGlobalResourcesSuccessUpdate_WhenModelStateIsValidAndUserIsUpdated()
        {
            // Arrange
            string err = GlobalResources.SuccessUpdate;
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            userController.ModelState.Clear();
            Mock.Arrange(() => campingUserProvider.UpdateCampingUser(Arg.AnyGuid, Arg.AnyString, Arg.AnyString))
                .Returns(1);
            UserViewModel userModel = this.GetUserViewModel();

            // Act
            string msg = userController.UpdateUser(userModel);

            // Assert
            Assert.AreEqual(msg, err);
        }

        private UserViewModel GetUserViewModel()
        {
            UserViewModel userModel = new UserViewModel();

            return userModel;
        }
    }
}
