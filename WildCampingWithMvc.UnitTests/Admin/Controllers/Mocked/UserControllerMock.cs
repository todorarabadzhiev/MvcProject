using Services.DataProviders;
using WildCampingWithMvc.Areas.Admin.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.Mocked
{
    public class UserControllerMock : UserController
    {
        public ICampingUserDataProvider CampingUserDataProvider
        {
            get
            {
                return this.campingUserDataProvider;
            }
        }
        
        public UserControllerMock(ICampingUserDataProvider campingUserDataProvider) 
            : base(campingUserDataProvider)
        {
        }
    }
}
