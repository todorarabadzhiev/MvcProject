using NUnit.Framework;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WildCampingWithMvc.Db;

namespace WildCampingWithMvc.IntegrationTests
{
    [SetUpFixture]
    public class TestInitializer
    {
        [OneTimeSetUp]
        public static void AssemblyInit()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WildCampingWithMvcDbContext, TestDbConfiguration>());
        }
    }

    public sealed class TestDbConfiguration : DbMigrationsConfiguration<WildCampingWithMvcDbContext>
    {
        public TestDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}