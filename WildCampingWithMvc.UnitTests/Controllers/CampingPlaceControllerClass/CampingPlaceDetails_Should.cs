using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            IEnumerable<ICampingPlace> campingPlaces = this.GetCampingPlaces(1);
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
            IEnumerable<ICampingPlace> campingPlaces = this.GetCampingPlaces(1);
            ICampingPlace cp = campingPlaces.First();
            Mock.Arrange(() => campingPlaceProvider.GetCampingPlaceById(cp.Id))
                .Returns(campingPlaces);

            // Act & Assert
            campingPlaceController
                .WithCallTo(c => c.CampingPlaceDetails(cp.Id))
                .ShouldRenderDefaultView()
                .WithModel<CampingPlaceDetailsViewModel>(viewModel =>
                {
                    Assert.AreEqual(cp.Id, viewModel.Id);
                    Assert.AreSame(cp.Name, viewModel.Name);
                    //Assert.AreSame(cp.HasWater, viewModel.HasWater);
                    //Assert.AreSame(cp.GoogleMapsUrl, viewModel.GoogleMapsUrl);
                    //Assert.AreSame(cp.Description, viewModel.Description);
                    //Assert.AreSame(cp.AddedBy, viewModel.AddedBy);
                    //Assert.AreSame(cp.AddedOn, viewModel.AddedOn);
                });
        }

        private IEnumerable<ICampingPlace> GetCampingPlaces(int count)
        {
            ICollection<ICampingPlace> campingPlaces = new List<ICampingPlace>();
            for (int i = 0; i < count; i++)
            {
                var campingPlace = Mock.Create<ICampingPlace>();
                Guid id = Guid.NewGuid();
                Mock.Arrange(() => campingPlace.Id).Returns(id);

                string name = string.Format("some name_{0}", i);
                Mock.Arrange(() => campingPlace.Name).Returns(name);

                var imageFile = Mock.Create<IImageFile>();
                byte[] byteArray = new byte[] { };
                Mock.Arrange(() => imageFile.Data).Returns(byteArray);
                Mock.Arrange(() => campingPlace.ImageFiles)
                    .Returns(new List<IImageFile>() { imageFile });

                campingPlaces.Add(campingPlace);
            }

            return campingPlaces;
        }
    }
}
