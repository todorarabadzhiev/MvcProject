using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class RecoverSightseeing_Should
    {
        private SightseeingControllerMock sightseeingController;
        private Guid id = new Guid();

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
        public void CallSightseeingDataProviderMethodGetDeletedSightseeingsOnce()
        {
            // Act
            this.sightseeingController.RecoverSightseeing(this.id);

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.RecoverDeletedSightseeingById(this.id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionSightseeingDetailsWithTheSameId()
        {
            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.RecoverSightseeing(this.id))
                .ShouldRedirectTo(c => c.SightseeingDetails(this.id));
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
