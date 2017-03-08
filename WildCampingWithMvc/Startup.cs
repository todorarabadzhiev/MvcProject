using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WildCampingWithMvc.Startup))]
namespace WildCampingWithMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
