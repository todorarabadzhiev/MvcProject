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
    public class SiteCategoryDetails_Should
    {
        private SiteCategoryControllerMock siteCategoryController;
        private ISiteCategory category = new SiteCategory()
        {
            Id = new Guid(),
            Name = "Some Name"
        };

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
        public void RedirectToActionIndex_WhenProvidedIdIsNull()
        {
            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.SiteCategoryDetails(null))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallSiteCategoryDataProviderMethodGetSiteCategoryByIdWithTheSameIdOnce()
        {
            // Act
            this.siteCategoryController.SiteCategoryDetails(this.category.Id);

            // Assert
            Mock.Assert(() => this.siteCategoryController.SiteCategoryDataProvider.GetSiteCategoryById(this.category.Id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionIndex_WhenCategoryWithTheProvidedIdIsNotFound()
        {
            // Arrange
            Mock.Arrange(() => this.siteCategoryController.SiteCategoryDataProvider.GetSiteCategoryById(this.category.Id)).Returns((ISiteCategory)null);

            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.SiteCategoryDetails(this.category.Id))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallCampingPlaceProviderMethodGetSiteCategoryCampingPlacesWithCorrectArgumentOnce()
        {
            // Arrange
            Mock.Arrange(() => this.siteCategoryController.SiteCategoryDataProvider
            .GetSiteCategoryById(this.category.Id)).Returns(this.category);

            // Act
            this.siteCategoryController.SiteCategoryDetails(this.category.Id);

            // Assert
            Mock.Assert(() => this.siteCategoryController.CampingPlaceProvider
            .GetSiteCategoryCampingPlaces(this.category.Name), Occurs.Once());
        }

        [Test]
        public void ReturnDefaultViewWithTheCorrectModel_WhenTheProvidedIdIsValid()
        {
            // Act && Assert
            this.siteCategoryController
                .WithCallTo(c => c.SiteCategoryDetails(this.category.Id))
                .ShouldRenderDefaultView()
                .WithModel<SiteCategoryDetailsViewModel>();
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.siteCategoryController = null;
        }
    }
}
