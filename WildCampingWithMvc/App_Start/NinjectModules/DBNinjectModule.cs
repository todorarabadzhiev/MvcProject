using WildCampingWithMvc.Db;
using Ninject;
using Ninject.Modules;
using EFositories;
using System;
using System.Data.Entity;

namespace WildCampingWithMvc.App_Start.NinjectModules
{
    public class DBNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IWildCampingEFository>().To<WildCampingEFository>();
            this.Bind(typeof(IGenericEFository<>)).To(typeof(GenericEFository<>));
            this.Bind<DbContext>().To<WildCampingWithMvcDbContext>().InSingletonScope();
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>());
            //this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}