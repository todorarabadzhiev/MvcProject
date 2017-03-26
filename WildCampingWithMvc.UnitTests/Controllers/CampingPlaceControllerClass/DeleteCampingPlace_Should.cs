using NUnit.Framework;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class DeleteCampingPlace_Should
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

            HttpContextBase httpContext = Mock.Create<HttpContextBase>();
            Mock.Arrange(() => httpContext.Cache).Returns(HttpRuntime.Cache);
            campingPlaceController.ControllerContext = new ControllerContext();
            campingPlaceController.ControllerContext.HttpContext = httpContext;
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RedirectToActionIndex(bool isAllowed)
        {
            // Arrange
            this.campingPlaceController.TempData["isAuthorized"] = isAllowed;
            Guid id = Guid.NewGuid();

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.DeleteCampingPlace(id))
                .ShouldRedirectTo(c => c.Index);
        }

        [Test]
        public void CallCampingPlaceProviderMethodDeleteCampingPlaceWithIdOnce_WhenUserIsAllowedToDelete()
        {
            // Arrange
            this.campingPlaceController.TempData["isAuthorized"] = true;
            Guid id = Guid.NewGuid();

            // Act
            campingPlaceController.DeleteCampingPlace(id);

            // Assert
            Mock.Assert(() => campingPlaceController.CampingPlaceProvider.DeleteCampingPlace(id), Occurs.Once());
        }
    }
}
