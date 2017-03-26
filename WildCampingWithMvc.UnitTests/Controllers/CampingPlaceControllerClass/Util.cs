using Services.Models;
using System;
using System.Collections.Generic;
using Telerik.JustMock;
using WildCampingWithMvc.Models.CampingPlace;

namespace WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass
{
    internal static class Util
    {
        public static AddCampingPlaceViewModel GetCampingPlaceViewModel()
        {
            AddCampingPlaceViewModel campingPlaceViewModel = new AddCampingPlaceViewModel()
            {
                Name = "some name",
                GoogleMapsUrl = "some url",
                Description = "some description",
                HasWater = true,
                ImageFileNames = new List<string>()
                {
                    "some file name"
                },
                ImageFilesData = new List<string>()
                {
                    "base64," + Convert.ToBase64String(new byte[] { 111, 121, 9, 212 })
                },
                SightseeingNames = new List<string>()
                {
                    "some sightseeing name"
                },
                SiteCategoriesNames = new List<string>()
                {
                    "some SITECATEGORIES name"
                }
            };

            return campingPlaceViewModel;
        }

        public static IEnumerable<ICampingPlace> GetCampingPlaces(int count)
        {
            ICollection<ICampingPlace> campingPlaces = new List<ICampingPlace>();
            for (int i = 0; i < count; i++)
            {
                var campingPlace = Mock.Create<ICampingPlace>();
                Guid id = Guid.NewGuid();
                Mock.Arrange(() => campingPlace.Id).Returns(id);

                string name = string.Format("some name_{0}", i);
                Mock.Arrange(() => campingPlace.Name).Returns(name);

                string url = string.Format("some url_{0}", i);
                Mock.Arrange(() => campingPlace.GoogleMapsUrl).Returns(url);

                string description = string.Format("some description_{0}", i);
                Mock.Arrange(() => campingPlace.Description).Returns(description);

                Mock.Arrange(() => campingPlace.HasWater).Returns(true);

                string addedBy = string.Format("some user name_{0}", i);
                Mock.Arrange(() => campingPlace.AddedBy).Returns(addedBy);

                Mock.Arrange(() => campingPlace.AddedOn).Returns(DateTime.Now);

                var imageFile = Mock.Create<IImageFile>();
                byte[] byteArray = new byte[] { 111, 222, 29, 4 };
                string fileName = string.Format("some file name_{0}", i);
                Mock.Arrange(() => imageFile.FileName).Returns(fileName);
                Mock.Arrange(() => imageFile.Data).Returns(byteArray);
                Mock.Arrange(() => campingPlace.ImageFiles)
                    .Returns(new List<IImageFile>() { imageFile });

                Guid sightseeingId = Guid.NewGuid();
                Mock.Arrange(() => campingPlace.SightseeingIds).Returns(new List<Guid>() { sightseeingId });

                string sightseeingName = string.Format("some sightseeing name_{0}", i);
                Mock.Arrange(() => campingPlace.SightseeingNames).Returns(new List<string>() { sightseeingName });

                Guid siteCategoryId = Guid.NewGuid();
                Mock.Arrange(() => campingPlace.SiteCategoriesIds).Returns(new List<Guid>() { siteCategoryId });

                string siteCategoryName = string.Format("some sitecategory name_{0}", i);
                Mock.Arrange(() => campingPlace.SiteCategoriesNames).Returns(new List<string>() { siteCategoryName });

                campingPlaces.Add(campingPlace);
            }

            return campingPlaces;
        }
    }
}
