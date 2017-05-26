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
    public class EditSightseeing_Should
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
        public void ReturnDefaultViewWithCorrectViewModel_WhenOnlyIdIsProvided()
        {
            // Arrange
            ISightseeing sightseeing = Util.GetSightseeing();
            string sightseeingImage = "data:image/jpeg;base64," + Convert.ToBase64String(sightseeing.Image);
            Mock.Arrange(() => this.sightseeingController.SightseeingDataProvider.GetSightseeingById(sightseeing.Id)).Returns(sightseeing);
            
            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.EditSightseeing(sightseeing.Id))
                .ShouldRenderDefaultView()
                .WithModel<AddSightseeingViewModel>(viewModel =>
                {
                    Assert.AreSame(sightseeing.Name, viewModel.Name);
                    Assert.AreSame(sightseeing.Description, viewModel.Description);
                    Assert.AreEqual(sightseeingImage, viewModel.ImageFileData);
                });
        }

        [Test]
        public void ReturnDefaultViewWithCorrectViewModel_WhenModelStateIsInvalid()
        {
            // Arrange
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.AddModelError("SomeError", "Error");

            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.EditSightseeing(model, id))
                .ShouldRenderDefaultView()
                .WithModel<AddSightseeingViewModel>(viewModel =>
                {
                    Assert.AreSame(model.Name, viewModel.Name);
                    Assert.AreSame(model.Description, viewModel.Description);
                    Assert.AreSame(model.ImageFileData, viewModel.ImageFileData);
                });
        }

        [Test]
        public void RedirectToActionSightseeingDetailsWithTheSameId_WhenModelStateIsValid()
        {
            // Arrange
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.Clear();

            // Act & Assert
            this.sightseeingController
                .WithCallTo(c => c.EditSightseeing(model, id))
                .ShouldRedirectTo(c => c.SightseeingDetails(id));
        }

        [Test]
        public void CallSightseeingDataProviderMethodUpdateSightseeingOnce_WhenModelStateIsValid()
        {
            // Arrange
            AddSightseeingViewModel model = Util.GetSightseeingViewModel();
            this.sightseeingController.ModelState.Clear();

            // Act
            this.sightseeingController.EditSightseeing(model, id);

            // Assert
            Mock.Assert(() => this.sightseeingController.SightseeingDataProvider.UpdateSightseeing(
                id, Arg.AnyString, Arg.AnyString, Arg.IsAny<byte[]>()), Occurs.Once());
        }

        [TearDown]
        public void RunAfterAnyTest()
        {
            this.sightseeingController = null;
        }
    }
}
