using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using WildCampingWithMvc.App_GlobalResources;
using WildCampingWithMvc.Areas.Admin.Controllers;
using WildCampingWithMvc.Areas.Admin.Models;

namespace WildCampingWithMvc.UnitTests.Admin.Controllers.UserControllerClass
{
    [TestFixture]
    public class DeleteUser_Should
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
            string msg = userController.DeleteUser(userModel);

            // Assert
            Assert.AreEqual(msg, err);
        }

        [Test]
        public void ReturnGlobalResourcesErrDbUpdate_WhenModelStateIsValidButCouldNoDeleteUser()
        {
            // Arrange
            string err = GlobalResources.ErrDbUpdate;
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            userController.ModelState.Clear();
            Mock.Arrange(() => campingUserProvider.DeleteCampingUser(Arg.AnyGuid))
                .Returns(0);
            UserViewModel userModel = this.GetUserViewModel();

            // Act
            string msg = userController.DeleteUser(userModel);

            // Assert
            Assert.AreEqual(msg, err);
        }

        [Test]
        public void ReturnGlobalResourcesSuccessDelete_WhenModelStateIsValidAndUserIsDeleted()
        {
            // Arrange
            string err = GlobalResources.SuccessDelete;
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            userController.ModelState.Clear();
            Mock.Arrange(() => campingUserProvider.DeleteCampingUser(Arg.AnyGuid))
                .Returns(1);
            UserViewModel userModel = this.GetUserViewModel();

            // Act
            string msg = userController.DeleteUser(userModel);

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
