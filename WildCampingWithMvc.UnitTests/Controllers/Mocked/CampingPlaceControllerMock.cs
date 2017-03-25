using Services.DataProviders;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.Mocked
{
    public class CampingPlaceControllerMock : CampingPlaceController
    {
        public ICampingPlaceDataProvider CampingPlaceProvider
        {
            get
            {
                return this.campingPlaceProvider;
            }
        }

        public ISightseeingDataProvider SightseeingProvider
        {
            get
            {
                return this.sightseeingProvider;
            }
        }

        public ISiteCategoryDataProvider SiteCategoryProvider
        {
            get
            {
                return this.siteCategoryProvider;
            }
        }

        public CampingPlaceControllerMock(ICampingPlaceDataProvider campingPlaceProvider, ISightseeingDataProvider sightseeingProvider, ISiteCategoryDataProvider siteCategoryProvider) 
            : base(campingPlaceProvider, sightseeingProvider, siteCategoryProvider)
        {
        }
    }
}
