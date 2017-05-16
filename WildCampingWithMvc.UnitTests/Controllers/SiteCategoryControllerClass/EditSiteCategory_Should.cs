using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.SiteCategory;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class EditSiteCategory_Should
    {
        private SiteCategoryControllerMock siteCategoryController;
        private Guid id = new Guid();

        [SetUp]
        public void ArrangeBeforeAnyTest()
        {
            // Arrange
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            this.siteCategoryController = new SiteCategoryControllerMock(
                siteCategoryProvider, campingPlaceProvider);
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenOnlyIdIsProvided()
        {
            // Arrange
            ISiteCategory category = Util.GetSiteCategory();
            string categoryImage = "data:image/jpeg;base64," + Convert.ToBase64String(category.Image);
            Mock.Arrange(() => this.siteCategoryController.SiteCategoryDataProvider.GetSiteCategoryById(category.Id)).Returns(category);
            
            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.EditSiteCategory(category.Id))
                .ShouldRenderDefaultView()
                .WithModel<AddSiteCategoryViewModel>(viewModel =>
                {
                    Assert.AreSame(category.Name, viewModel.Name);
                    Assert.AreSame(category.Description, viewModel.Description);
                    Assert.AreEqual(categoryImage, viewModel.ImageFileData);
                });
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.AddModelError("SomeError", "Error");

            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.EditSiteCategory(model, id))
                .ShouldRenderDefaultView()
                .WithModel<AddSiteCategoryViewModel>(viewModel =>
                {
                    Assert.AreSame(model.Name, viewModel.Name);
                    Assert.AreSame(model.Description, viewModel.Description);
                    Assert.AreSame(model.ImageFileData, viewModel.ImageFileData);
                });
        }

        [Test]
        public void RedirectToActionSiteCategoryDetailsWithTheSameId_WhenModelStateIsValid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.Clear();

            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.EditSiteCategory(model, id))
                .ShouldRedirectTo(c => c.SiteCategoryDetails(id));
        }

        [Test]
        public void CallSiteCategoryDataProviderMethodUpdateSiteCategoryOnce_WhenModelStateIsValid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.Clear();

            // Act
            siteCategoryController.EditSiteCategory(model, id);

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.UpdateSiteCategory(
                id, Arg.AnyString, Arg.AnyString, Arg.IsAny<byte[]>()), Occurs.Once());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
