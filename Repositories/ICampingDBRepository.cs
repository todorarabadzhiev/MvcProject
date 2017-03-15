using CampingDB.Models;

namespace Repositories
{
    public interface ICampingDBRepository
    {
        IGenericRepository<CampingPlace> GetCampingPlaceRepository();
        IGenericRepository<CampingUser> GetCampingUserRepository();
        IGenericRepository<SiteCategory> GetSiteCategoryRepository();
        IGenericRepository<Sightseeing> GetSightseeingRepository();
        IGenericRepository<ImageFile> GetImageFileRepository();
    }
}
