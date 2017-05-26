using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.SiteCategory;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class AddSiteCategory_Should
    {
        private SiteCategoryControllerMock siteCategoryController;

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
        public void ReturnDefaultView_WhenNoParametersAreProvided()
        {
            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.AddSiteCategory())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.AddModelError("SomeError", "Error");

            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.AddSiteCategory(model))
                .ShouldRenderDefaultView()
                .WithModel<AddSiteCategoryViewModel>(viewModel =>
                {
                    Assert.AreSame(model.Name, viewModel.Name);
                    Assert.AreSame(model.Description, viewModel.Description);
                    Assert.AreSame(model.ImageFileData, viewModel.ImageFileData);
                });
        }

        [Test]
        public void RedirectToActionIndex_WhenModelStateIsValid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.Clear();

            // Act & Assert
            this.siteCategoryController
                .WithCallTo(c => c.AddSiteCategory(model))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallSiteCategoryDataProviderMethodAddSiteCategoryOnce_WhenModelStateIsValid()
        {
            // Arrange
            AddSiteCategoryViewModel model = Util.GetSiteCategoryViewModel();
            this.siteCategoryController.ModelState.Clear();

            // Act
            this.siteCategoryController.AddSiteCategory(model);

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.AddSiteCategory(
                Arg.AnyString, Arg.AnyString, Arg.IsAny<byte[]>()), Occurs.Once());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
