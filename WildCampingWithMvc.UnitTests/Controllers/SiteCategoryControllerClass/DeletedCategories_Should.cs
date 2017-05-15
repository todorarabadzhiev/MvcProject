using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.SiteCategory;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class DeletedCategories_Should
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
        public void CallSiteCategoryDataProviderMethodGetDeletedSiteCategoriesOnce()
        {
            // Act
            this.siteCategoryController.DeletedCategories();

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.GetDeletedSiteCategories(), Occurs.Once());
        }

        [Test]
        public void ReturnDefaultViewWithTheCorrectModel()
        {
            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.DeletedCategories())
                .ShouldRenderDefaultView()
                .WithModel<SiteCategoriesViewModel>();
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
