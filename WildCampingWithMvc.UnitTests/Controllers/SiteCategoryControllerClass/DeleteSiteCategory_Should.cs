using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    [TestFixture]
    public class DeleteSiteCategory_Should
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
        public void CallSiteCategoryDataProviderMethodDeleteSiteCategoryOnce()
        {
            // Act
            this.siteCategoryController.DeleteSiteCategory(this.id);

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.DeleteSiteCategory(this.id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionIndex()
        {
            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.DeleteSiteCategory(this.id))
                .ShouldRedirectTo(c => c.Index());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
