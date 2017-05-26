using Services.Models;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Models.Sightseeing;

namespace WildCampingWithMvc.UnitTests.Controllers.SightseeingControllerClass
{
    public static class Util
    {
        public static ISightseeing GetSightseeing()
        {
                var sightseeing = Mock.Create<ISightseeing>();
                Guid id = Guid.NewGuid();
                Mock.Arrange(() => sightseeing.Id).Returns(id);

                string name = string.Format("some name_0");
                Mock.Arrange(() => sightseeing.Name).Returns(name);

                string description = string.Format("some description_0");
                Mock.Arrange(() => sightseeing.Description).Returns(description);

                byte[] byteArray = new byte[] { 111, 222, 29, 4 };
                Mock.Arrange(() => sightseeing.Image).Returns(byteArray);

            return sightseeing;
        }

        public static AddSightseeingViewModel GetSightseeingViewModel()
        {
            AddSightseeingViewModel sightseeingViewModel = new AddSightseeingViewModel()
            {
                Name = "some name",
                Description = "some description",
                ImageFileName = "some file name",
                ImageFileData = "base64," + Convert.ToBase64String(new byte[] { 111, 121, 9, 212 })
            };

            return sightseeingViewModel;
        }
    }
}
