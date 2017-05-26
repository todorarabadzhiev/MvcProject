using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.Sightseeing;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    [TestFixture]
    public class AddSightseeing_Should
    {
        private SightseeingControllerMock sightseeingController;

        [SetUp]
        public void ArrangeBeforeAnyTest()
        {
            // Arrange
            var sightseeingProvider = Mock.Create<ISightseeingDataProvider>();
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            this.sightseeingController = new SightseeingControllerMock(
                sightseeingProvider, campingPlaceProvider);
        }

        [Test]
        public void ReturnDefaultView_WhenNoParametersAreProvided()
        {
            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.AddSightseeing())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.AddModelError("SomeError", "Error");

            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.AddSightseeing(model))
                .ShouldRenderDefaultView()
                .WithModel<AddSightseeingViewModel>(viewModel =>
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
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.Clear();

            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.AddSightseeing(model))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallSightseeingDataProviderMethodAddSightseeingOnce_WhenModelStateIsValid()
        {
            // Arrange
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.Clear();

            // Act
            this.sightseeingController.AddSightseeing(model);

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.AddSightseeing(
                Arg.AnyString, Arg.AnyString, Arg.IsAny<byte[]>()), Occurs.Once());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
