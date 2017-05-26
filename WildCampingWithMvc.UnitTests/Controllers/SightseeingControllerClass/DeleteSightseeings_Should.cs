using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class DeleteSightseeings_Should
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
        public void CallSightseeingDataProviderMethodDeleteSightseeingOnce()
        {
            // Act
            this.sightseeingController.DeleteSightseeing(this.id);

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.DeleteSightseeing(this.id), Occurs.Once());
        }

        [Test]
        public void RedirectToActionIndex()
        {
            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.DeleteSightseeing(this.id))
                .ShouldRedirectTo(c => c.Index());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
