using CommonUtilities.Utilities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.Db
{
    public class WildCampingWithMvcDbContext : DbContext
    {
        public WildCampingWithMvcDbContext()
            : base(Utilities.DbConnectionName)
        {
        }

        public DbSet<DbSiteCategory> DbSiteCategories { get; set; }
        public DbSet<DbCampingPlace> DbCampingPlaces { get; set; }
        public DbSet<DbCampingUser> DbCampingUsers { get; set; }
        public DbSet<DbSightseeing> DbSightseeings { get; set; }
        public DbSet<DbImageFile> DbImageFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
