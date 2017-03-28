using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.IntegrationTests.Services.DataProviders
{
    internal static class Utils
    {
        internal static ICollection<DbSiteCategory> GetDbCategory(int count)
        {
            ICollection<DbSiteCategory> categories = new List<DbSiteCategory>();
            for (int i = 0; i < count; i++)
            {
                DbSiteCategory dbCategory = new DbSiteCategory()
                {
                    Name = string.Format("Some Category_{0}", i)
                };

                categories.Add(dbCategory);
            }

            return categories;
        }

        internal static ICollection<DbSightseeing> GetDbSightseeings(int count)
        {
            ICollection<DbSightseeing> sightseeings = new List<DbSightseeing>();
            for (int i = 0; i < count; i++)
            {
                DbSightseeing dbSightseeing = new DbSightseeing()
                {
                    Name = string.Format("Some Sightseeing_{0}", i)
                };

                sightseeings.Add(dbSightseeing);
            }

            return sightseeings;
        }

        internal static DbCampingUser dbCampingUser = new DbCampingUser()
        {
            UserName = "Some UserName",
            FirstName = "Some FirstName",
            LastName = "Some LastName"
        };

        internal static DbCampingPlace dbCampingPlace = new DbCampingPlace()
        {
            Name = "Some CampingPlace",
            AddedOn = DateTime.Now,
            Description = "Some description",
            WaterOnSite = true
        };
    }
}
