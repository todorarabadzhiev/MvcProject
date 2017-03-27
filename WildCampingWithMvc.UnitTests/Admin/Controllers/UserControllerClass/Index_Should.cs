using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Areas.Admin.Controllers;

namespace WildCampingWithMvc.UnitTests.Admin.Controllers.UserControllerClass
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);

            // Act && Assert
            userController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
