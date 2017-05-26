using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class RecoverSiteCategory_Should
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
        public void CallSiteCategoryDataProviderMethodGetDeletedSiteCategoriesOnce()
        {
            // Act
            this.siteCategoryController.RecoverSiteCategory(this.id);

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.RecoverDeletedCategoryById(this.id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionSiteCategoryDetailsWithTheSameId()
        {
            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.RecoverSiteCategory(this.id))
                .ShouldRedirectTo(c => c.SiteCategoryDetails(this.id));
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
