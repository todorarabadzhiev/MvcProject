using NUnit.Framework;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Models.CampingPlace;
using WildCampingWithMvc.UnitTests.Controllers.Mocked;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class Index_Should
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
        }

        [Test]
        public void CallCampingPlaceProviderMethodGetAllCampingPlacesOnce()
        {
            // Act
            this.campingPlaceController.Index();

            // Assert
            Mock.Assert(() => this.campingPlaceController.CampingPlaceProvider.GetAllCampingPlaces(), Occurs.Once());
        }

        [Test]
        public void ReturnDefaultViewWithTheCorrectModel()
        {
            // Act && Assert
            this.campingPlaceController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<MultipleCampingPlacesViewModel>();
        }
    }
}
