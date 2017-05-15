using Services.DataProviders;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.Mocked
{
    public class SiteCategoryControllerMock : SiteCategoryController
    {
        public ICampingPlaceDataProvider CampingPlaceProvider
        {
            get
            {
                return this.campingPlaceProvider;
            }
        }

        public ISiteCategoryDataProvider SiteCategoryDataProvider
        {
            get
            {
                return this.siteCategoryDataProvider;
            }
        }

        public SiteCategoryControllerMock(ISiteCategoryDataProvider siteCategoryProvider, ICampingPlaceDataProvider campingPlaceProvider) 
            : base(siteCategoryProvider, campingPlaceProvider)
        {
        }
    }
}
