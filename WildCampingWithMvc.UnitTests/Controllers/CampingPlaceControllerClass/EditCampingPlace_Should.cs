using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class EditCampingPlace_Should
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
        public void ReturnDefaultViewWithCorrectViewModel_WhenUserIsAllowedToEditAndOnlyIdIsProvided()
        {
            // Arrange
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(1);
            ICampingPlace cp = campingPlaces.First();
            Mock.Arrange(() => this.campingPlaceController.CampingPlaceProvider.GetCampingPlaceById(cp.Id))
                .Returns(campingPlaces);

            ISightseeing sightseeing = Mock.Create<ISightseeing>();
            Guid sightseeingId = cp.SightseeingIds.First();
            string sightseeingName = cp.SightseeingNames.First();
            Mock.Arrange(() => sightseeing.Id).Returns(sightseeingId);
            Mock.Arrange(() => sightseeing.Name).Returns(sightseeingName);
            Mock.Arrange(() => this.campingPlaceController.SightseeingProvider.GetSightseeingById(sightseeingId))
                .Returns(sightseeing);

            ISiteCategory siteCategory = Mock.Create<ISiteCategory>();
            Guid siteCategoryId = cp.SiteCategoriesIds.First();
            string siteCategoryName = cp.SiteCategoriesNames.First();
            Mock.Arrange(() => siteCategory.Id).Returns(siteCategoryId);
            Mock.Arrange(() => siteCategory.Name).Returns(siteCategoryName);
            Mock.Arrange(() => this.campingPlaceController.SiteCategoryProvider.GetSiteCategoryById(siteCategoryId))
                .Returns(siteCategory);

            this.campingPlaceController.TempData["isAuthorized"] = true;

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.EditCampingPlace(cp.Id))
                .ShouldRenderDefaultView()
                .WithModel<AddCampingPlaceViewModel>(viewModel =>
                {
                    Assert.AreSame(cp.Name, viewModel.Name);
                    Assert.AreEqual(cp.HasWater, viewModel.HasWater);
                    Assert.AreSame(cp.GoogleMapsUrl, viewModel.GoogleMapsUrl);
                    Assert.AreSame(cp.Description, viewModel.Description);
                    Assert.AreSame(cp.ImageFiles[0].FileName, viewModel.ImageFileNames[0]);
                    Assert.AreEqual("data:image/jpeg;base64," + Convert.ToBase64String(cp.ImageFiles[0].Data), viewModel.ImageFilesData[0]);
                    Assert.AreSame(cp.SightseeingNames.First(), viewModel.SightseeingNames.First());
                    Assert.AreSame(cp.SiteCategoriesNames.First(), viewModel.SiteCategoriesNames.First());
                });
        }

        [Test]
        public void RedirectToActionCampingPlaceDetailsWithTheSameId_WhenUserIsNotAllowedToEditAndOnlyIdIsProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            this.campingPlaceController.TempData["isAuthorized"] = false;

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.EditCampingPlace(id))
                .ShouldRedirectTo(c => c.CampingPlaceDetails(id));
        }

        [Test]
        public void RedirectToActionCampingPlaceDetailsWithTheSameId_WhenUserIsNotAllowedToEditAndModelAndIdAreProvided()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.TempData["isAuthorized"] = false;

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.EditCampingPlace(model, id))
                .ShouldRedirectTo(c => c.CampingPlaceDetails(id));
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenUserIsAllowedToEditAndModelAndIdAreProvidedButModelStateIsInvalid()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.ModelState.AddModelError("SomeError", "Error");
            this.campingPlaceController.TempData["isAuthorized"] = true;

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.EditCampingPlace(model, id))
                .ShouldRenderDefaultView()
                .WithModel<AddCampingPlaceViewModel>(viewModel =>
                {
                    Assert.AreSame(model, viewModel);
                });
        }

        [Test]
        public void RedirectToActionCampingPlaceDetailsWithTheSameId_WhenUserIsAllowedToEditAndModelAndIdAreProvidedAndModelStateIsValid()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AddCampingPlaceViewModel model = Util.GetCampingPlaceViewModel();
            this.campingPlaceController.TempData["isAuthorized"] = true;
            this.campingPlaceController.ModelState.Clear();

            // Act & Assert
            this.campingPlaceController
                .WithCallTo(c => c.EditCampingPlace(model, id))
                .ShouldRedirectTo(c => c.CampingPlaceDetails(id));
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.campingPlaceController = null;
        }
    }
}
