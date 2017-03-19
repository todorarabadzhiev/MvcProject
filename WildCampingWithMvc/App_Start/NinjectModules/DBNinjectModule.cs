using WildCampingWithMvc.Db;
using Ninject;
using Ninject.Modules;
using EFositories;
using System;
using System.Data.Entity;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

namespace WildCampingWithMvc.App_Start.NinjectModules
{
    public class DBNinjectModule : NinjectModule
    {
        private const string dllName = "EFositories.dll";
        public override void Load()
        {
            Common autoBindInRequestScope = new Common(dllName);
            this.Bind(autoBindInRequestScope.AutoBindClassesInRequestScope);

            //this.Bind<IWildCampingEFository>().To<WildCampingEFository>().InRequestScope();
            //this.Bind(typeof(IGenericEFository<>)).To(typeof(GenericEFository<>)).InRequestScope();
            this.Bind<DbContext>().To<WildCampingWithMvcDbContext>().InRequestScope();
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>()).InRequestScope();
        }
    }
}