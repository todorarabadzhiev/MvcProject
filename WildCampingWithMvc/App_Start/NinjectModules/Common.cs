using Ninject.Extensions.Conventions.Syntax;
using Ninject.Web.Common;

namespace WildCampingWithMvc.App_Start.NinjectModules
{
    public class Common
    {
        private readonly string dllName;

        public Common(string dllName)
        {
            this.dllName = dllName;
        }

        public void AutoBindClassesInRequestScope(IFromSyntax autoBindings)
        {
            autoBindings
                .FromAssembliesMatching(dllName)
                .SelectAllClasses()
                .BindDefaultInterfaces()
                .Configure(y => y.InRequestScope());
        }
    }
}