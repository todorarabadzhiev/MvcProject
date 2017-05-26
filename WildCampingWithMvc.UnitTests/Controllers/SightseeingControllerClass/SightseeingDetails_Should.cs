using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.Sightseeing;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class SightseeingDetails_Should
    {
        private SightseeingControllerMock sightseeingController;
        private ISightseeing sightseeing = new Sightseeing()
        {
            Id = new Guid(),
            Name = "Some Name"
        };

        [SetUp]
        public void ArrangeBeforeAnyTest()
        {
            // Arrange
            var sightseeingDataProvider = Mock.Create<ISightseeingDataProvider>();
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            this.sightseeingController = new SightseeingControllerMock(
                sightseeingDataProvider, campingPlaceProvider);
        }

        [Test]
        public void RedirectToActionIndex_WhenProvidedIdIsNull()
        {
            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.SightseeingDetails(null))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallSightseeingDataProviderMethodGetSightseeingByIdWithTheSameIdOnce()
        {
            // Act
            this.sightseeingController.SightseeingDetails(this.sightseeing.Id);

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.GetSightseeingById(this.sightseeing.Id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionIndex_WhenSightseeingWithTheProvidedIdIsNotFound()
        {
            // Arrange
            Mock.Arrange(() => this.sightseeingController.SightseeingDataProvider.GetSightseeingById(this.sightseeing.Id)).Returns((ISightseeing)null);

            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.SightseeingDetails(this.sightseeing.Id))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallCampingPlaceProviderMethodGetSightseeingCampingPlacesWithCorrectArgumentOnce()
        {
            // Arrange
            Mock.Arrange(() => this.sightseeingController.SightseeingDataProvider
            .GetSightseeingById(this.sightseeing.Id)).Returns(this.sightseeing);

            // Act
            this.sightseeingController.SightseeingDetails(this.sightseeing.Id);

            // Assert
            Mock.Assert(() => this.sightseeingController.CampingPlaceProvider
            .GetSightseeingCampingPlaces(this.sightseeing.Name), Occurs.Once());
        }

        [Test]
        public void ReturnDefaultViewWithTheCorrectModel_WhenTheProvidedIdIsValid()
        {
            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.SightseeingDetails(this.sightseeing.Id))
                .ShouldRenderDefaultView()
                .WithModel<SightseeingDetailsViewModel>();
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
