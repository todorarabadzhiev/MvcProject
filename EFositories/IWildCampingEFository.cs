using WildCampingWithMvc.Db.Models;

namespace EFositories
{
    public interface IWildCampingEFository
    {
        IGenericEFository<DbCampingPlace> GetCampingPlaceRepository();
        IGenericEFository<DbCampingUser> GetCampingUserRepository();
        IGenericEFository<DbSiteCategory> GetSiteCategoryRepository();
        IGenericEFository<DbSightseeing> GetSightseeingRepository();
        IGenericEFository<DbImageFile> GetImageFileRepository();
    }
}
