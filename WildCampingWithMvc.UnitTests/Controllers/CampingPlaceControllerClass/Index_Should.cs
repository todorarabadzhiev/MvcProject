using NUnit.Framework;
using Services.DataProviders;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnRedirectToActionIndex()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            var campingPlaceController = new CampingPlaceController(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);

            // Act && Assert
            campingPlaceController
                .WithCallTo(c => c.Index())
                .ShouldRedirectTo(c => c.AllCampingPlaces());
        }
    }
}
