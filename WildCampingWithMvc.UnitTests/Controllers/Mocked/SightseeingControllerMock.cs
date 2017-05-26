using Services.DataProviders;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.Mocked
{
    public class SightseeingControllerMock : SightseeingController
    {
        public ICampingPlaceDataProvider CampingPlaceProvider
        {
            get
            {
                return this.campingPlaceProvider;
            }
        }

        public ISightseeingDataProvider SightseeingDataProvider
        {
            get
            {
                return this.sightseeingDataProvider;
            }
        }

        public SightseeingControllerMock(ISightseeingDataProvider sightseeingDataProvider, ICampingPlaceDataProvider campingPlaceProvider) 
            : base(sightseeingDataProvider, campingPlaceProvider)
        {
        }
    }
}
