using CommonUtilities.Utilities;
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
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class AddCampingPlace_Should
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
        public void ReturnDefaultView_WhenNoParametersAreProvided()
        {
            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.AddCampingPlace())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.ModelState.AddModelError("SomeError", "Error");

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.AddCampingPlace(model))
                .ShouldRenderDefaultView()
                .WithModel<AddCampingPlaceViewModel>();
        }

        [Test]
        public void RedirectToActionIndexOfHomeController_WhenModelStateIsValid()
        {
            // Arrange
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.ModelState.Clear();

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.AddCampingPlace(model))
                .ShouldRedirectTo<HomeController>(c => c.Index());
        }

        [Test]
        public void CallCampingPlaceProvideAddCampingPlaceMethodOnce()
        {
            // Arrange
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.ModelState.Clear();

            // Act
            campingPlaceController.AddCampingPlace(model);

            // Assert
            Mock.Assert(() => this.campingPlaceController.CampingPlaceProvider.AddCampingPlace(
                Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyBool, 
                Arg.IsAny<IEnumerable<string>>(), Arg.IsAny<IEnumerable<string>>(), 
                Arg.IsAny<IList<string>>(), Arg.IsAny<IList<byte[]>>()), Occurs.Once());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.campingPlaceController = null;
        }
    }
}
