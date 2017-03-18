using System.Data.Entity;
using WildCampingWithMvc.Db.Models;

namespace EFositories
{
    public class WildCampingEFository : IWildCampingEFository
    {
        private readonly DbContext dbContext;

        public WildCampingEFository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IGenericEFository<DbCampingPlace> GetCampingPlaceRepository()
        {
            return new GenericEFository<DbCampingPlace>(this.dbContext);
        }

        public IGenericEFository<DbSiteCategory> GetSiteCategoryRepository()
        {
            return new GenericEFository<DbSiteCategory>(this.dbContext);
        }

        public IGenericEFository<DbSightseeing> GetSightseeingRepository()
        {
            return new GenericEFository<DbSightseeing>(this.dbContext);
        }

        public IGenericEFository<DbImageFile> GetImageFileRepository()
        {
            return new GenericEFository<DbImageFile>(this.dbContext);
        }

        public IGenericEFository<DbCampingUser> GetCampingUserRepository()
        {
            return new GenericEFository<DbCampingUser>(this.dbContext);
        }
    }
}
