using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.Sightseeing;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class Index_Should
    {
        private SightseeingControllerMock sightseeingController;

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
        public void CallSightseeingDataProviderMethodGetAllSightseeingsOnce()
        {
            // Act
            this.sightseeingController.Index();

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.GetAllSightseeings(), Occurs.Once());
        }

        [Test]
        public void ReturnDefaultViewWithTheCorrectModel()
        {
            // Act && Assert
            this.sightseeingController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<SightseeingsViewModel>();
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
