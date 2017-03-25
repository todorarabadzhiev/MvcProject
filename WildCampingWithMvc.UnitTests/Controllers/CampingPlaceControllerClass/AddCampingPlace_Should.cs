using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class AddCampingPlace_Should
    {
        [Test]
        public void ReturnDefaultView_WhenNoParametersAreProvided()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            CampingPlaceController campingPlaceController = new CampingPlaceController(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);

            HttpContextBase httpContext = Mock.Create<HttpContextBase>();
            Mock.Arrange(() => httpContext.Cache).Returns(HttpRuntime.Cache);
            campingPlaceController.ControllerContext = new ControllerContext();
            campingPlaceController.ControllerContext.HttpContext = httpContext;

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.AddCampingPlace())
                .ShouldRenderDefaultView();
        }
    }
}
