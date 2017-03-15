using WildCampingWithMvc.Db.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WildCampingWithMvc.Db
{
    public class WildCampingWithMvcDbContext : DbContext
    {
        public WildCampingWithMvcDbContext()
            : base("WildCampingWithMvcDB")
        {
        }

        public IDbSet<DbSiteCategory> DbSiteCategories { get; set; }
        public IDbSet<DbCampingPlace> DbCampingPlaces { get; set; }
        public DbSet<DbCampingUser> DbCampingUsers { get; set; }
        public DbSet<DbSightseeing> DbSightseeings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
