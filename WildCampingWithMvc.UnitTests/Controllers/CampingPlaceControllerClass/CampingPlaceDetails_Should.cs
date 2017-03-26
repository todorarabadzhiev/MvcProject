using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Controllers;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    [TestFixture]
    public class CampingPlaceDetails_Should
    {
        [Test]
        public void CallCampPlaceDataProviderMethodGetCampingPlaceByIdExactlyOnceWithCorrectParameter()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            CampingPlaceController campingPlaceController = new CampingPlaceController(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(1);
            Guid id = campingPlaces.First().Id;
            Mock.Arrange(() => campingPlaceProvider.GetCampingPlaceById(id))
                .Returns(campingPlaces);

            // Act
            campingPlaceController.CampingPlaceDetails(id);

            // Assert
            Mock.Assert(() => campingPlaceProvider.GetCampingPlaceById(id), Occurs.Once());
        }

        [Test]
        public void ReturnViewWithModelWithCorrectCampingPlace()
        {
            // Arrange
            var campingPlaceProvider = Mock.Create<ICampingPlaceDataProvider>();
            var sightseeingsProvider = Mock.Create<ISightseeingDataProvider>();
            var siteCategoryProvider = Mock.Create<ISiteCategoryDataProvider>();
            CampingPlaceController campingPlaceController = new CampingPlaceController(
                campingPlaceProvider,
                sightseeingsProvider,
                siteCategoryProvider);
            IEnumerable<ICampingPlace> campingPlaces = Util.GetCampingPlaces(1);
            ICampingPlace cp = campingPlaces.First();
            Mock.Arrange(() => campingPlaceProvider.GetCampingPlaceById(cp.Id))
                .Returns(campingPlaces);

            ISightseeing sightseeing = Mock.Create<ISightseeing>();
            Guid sightseeingId = cp.SightseeingIds.First();
            string sightseeingName = cp.SightseeingNames.First();
            Mock.Arrange(() => sightseeing.Id).Returns(sightseeingId);
            Mock.Arrange(() => sightseeing.Name).Returns(sightseeingName);
            Mock.Arrange(() => sightseeingsProvider.GetSightseeingById(sightseeingId))
                .Returns(sightseeing);

            ISiteCategory siteCategory = Mock.Create<ISiteCategory>();
            Guid siteCategoryId = cp.SiteCategoriesIds.First();
            string siteCategoryName = cp.SiteCategoriesNames.First();
            Mock.Arrange(() => siteCategory.Id).Returns(siteCategoryId);
            Mock.Arrange(() => siteCategory.Name).Returns(siteCategoryName);
            Mock.Arrange(() => siteCategoryProvider.GetSiteCategoryById(siteCategoryId))
                .Returns(siteCategory);

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.CampingPlaceDetails(cp.Id))
                .ShouldRenderDefaultView()
                .WithModel<CampingPlaceDetailsViewModel>(viewModel =>
                {
                    Assert.AreEqual(cp.Id, viewModel.Id);
                    Assert.AreSame(cp.Name, viewModel.Name);
                    Assert.AreEqual(cp.HasWater, viewModel.HasWater);
                    Assert.AreSame(cp.GoogleMapsUrl, viewModel.GoogleMapsUrl);
                    Assert.AreSame(cp.Description, viewModel.Description);
                    Assert.AreSame(cp.AddedBy, viewModel.AddedBy);
                    Assert.AreEqual(cp.AddedOn, viewModel.AddedOn);
                    Assert.AreSame(cp.ImageFiles[0].FileName, viewModel.ImageFileNames[0]);
                    Assert.AreSame(cp.ImageFiles[0].Data, viewModel.ImageFilesData[0]);
                    Assert.AreEqual(cp.SightseeingIds.First(), viewModel.Sightseeings.First().Id);
                    Assert.AreSame(cp.SightseeingNames.First(), viewModel.Sightseeings.First().Name);
                    Assert.AreEqual(cp.SiteCategoriesIds.First(), viewModel.SiteCategories.First().Id);
                    Assert.AreSame(cp.SiteCategoriesNames.First(), viewModel.SiteCategories.First().Name);
                });
        }
    }
}
