using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace WildCampingWithMvc.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        private const string dllName = "Services.dll";
        public override void Load()
        {
            Common autoBindInRequestScope = new Common(dllName);
            this.Bind(autoBindInRequestScope.AutoBindClassesInRequestScope);
        }
    }
}