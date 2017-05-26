﻿using NUnit.Framework;
using Services.DataProviders;
using System.Web;
using System.Web.Mvc;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class DeletedCampingPlaces_Should
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
        public void ReturnDefaultWithTheCorrectViewModel()
        {
            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.DeletedCampingPlaces())
                .ShouldRenderDefaultView()
                .WithModel<MultipleCampingPlacesViewModel>();
        }

        [Test]
        public void CallCampingPlaceProviderMethodGetDeletedCampingPlacesOnce()
        {
            // Act
            campingPlaceController.DeletedCampingPlaces();

            // Assert
            Mock.Assert(() => campingPlaceController.CampingPlaceProvider.GetDeletedCampingPlaces(), Occurs.Once());
        }
    }
}