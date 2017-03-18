namespace WildCampingWithMvc.Db.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WildCampingWithMvc.Db.WildCampingWithMvcDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WildCampingWithMvc.Db.WildCampingWithMvcDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.DbSightseeings.AddOrUpdate(
              s => s.Name,
              new DbSightseeing { Name = "Пещери" },
              new DbSightseeing { Name = "Водопади" },
              new DbSightseeing { Name = "Исторически събития" },
              new DbSightseeing { Name = "Археологически разкопки" },
              new DbSightseeing { Name = "Развлечения" }
            );
            context.DbSiteCategories.AddOrUpdate(
              s => s.Name,
              new DbSiteCategory { Name = "Море" },
              new DbSiteCategory { Name = "Планина" },
              new DbSiteCategory { Name = "Река" },
              new DbSiteCategory { Name = "Язовир" },
              new DbSiteCategory { Name = "Остров" }
            );
        }
    }
}
