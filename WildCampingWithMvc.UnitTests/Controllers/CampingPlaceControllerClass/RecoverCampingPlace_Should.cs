using NUnit.Framework;
using Services.DataProviders;
using System;
using System.Web;
using System.Web.Mvc;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class RecoverCampingPlace_Should
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

        [Test]
        public void RedirectToActionCampingPlaceDetailsWithTheSameIdAsArgument()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.RecoverCampingPlace(id))
                .ShouldRedirectTo(c => c.CampingPlaceDetails(id));
        }

        [Test]
        public void CallCampingPlaceProviderMethodRecoverDeletedCampingPlaceByIdOnceWithTheSameIdAsArgument()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            campingPlaceController.RecoverCampingPlace(id);

            // Assert
            Mock.Assert(() => campingPlaceController.CampingPlaceProvider.RecoverDeletedCampingPlaceById(id), Occurs.Once());
        }
    }
}
