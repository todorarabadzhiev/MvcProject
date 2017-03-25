using Services.DataProviders;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.Mocked
{
    public class HomeControllerMock : HomeController
    {
        public ICampingPlaceDataProvider CampPlaceDataProvider
        {
            get
            {
                return this.campPlaceDataProvider;
            }
        }

        public HomeControllerMock(ICampingPlaceDataProvider campPlaceDataProvider) 
            : base(campPlaceDataProvider)
        {
        }

    }
}
