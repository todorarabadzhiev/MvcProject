using Services.Models;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Models.SiteCategory;

namespace WildCampingWithMvc.UnitTests.Controllers.SiteCategoryControllerClass
{
    public static class Util
    {
        public static ISiteCategory GetSiteCategory()
        {
                var siteCategory = Mock.Create<ISiteCategory>();
                Guid id = Guid.NewGuid();
                Mock.Arrange(() => siteCategory.Id).Returns(id);

                string name = string.Format("some name_0");
                Mock.Arrange(() => siteCategory.Name).Returns(name);

                string description = string.Format("some description_0");
                Mock.Arrange(() => siteCategory.Description).Returns(description);

                byte[] byteArray = new byte[] { 111, 222, 29, 4 };
                Mock.Arrange(() => siteCategory.Image).Returns(byteArray);

            return siteCategory;
        }

        public static AddSiteCategoryViewModel GetSiteCategoryViewModel()
        {
            AddSiteCategoryViewModel siteCategoryViewModel = new AddSiteCategoryViewModel()
            {
                Name = "some name",
                Description = "some description",
                ImageFileName = "some file name",
                ImageFileData = "base64," + Convert.ToBase64String(new byte[] { 111, 121, 9, 212 })
            };

            return siteCategoryViewModel;
        }
    }
}
