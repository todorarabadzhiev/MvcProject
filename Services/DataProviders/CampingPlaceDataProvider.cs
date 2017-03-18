using EFositories;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WildCampingWithMvc.Db.Models;

namespace Services.DataProviders
{
    public class CampingPlaceDataProvider : ICampingPlaceDataProvider
    {
        protected readonly IWildCampingEFository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public CampingPlaceDataProvider(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("WildCampingEFository");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork");
            }

            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ICampingPlace> GetUserCampingPlaces(string userName)
        {
            if (userName == null)
            {
                return null;
            }

            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var places = new List<ICampingPlace>();
            var dbPlaces = capmingPlaceRepository.GetAll(p => (!p.IsDeleted) && (p.AddedBy.UserName == userName));
            foreach (var p in dbPlaces)
            {
                places.Add(ConvertToPlace(p));
            }

            return places;
        }

        public IEnumerable<ICampingPlace> GetSiteCategoryCampingPlaces(string categoryName)
        {
            if (categoryName == null)
            {
                return null;
            }

            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var places = new List<ICampingPlace>();
            var dbPlaces = capmingPlaceRepository.GetAll(p => (!p.IsDeleted) &&
            (p.DbSiteCategories.FirstOrDefault(s => s.Name == categoryName) != null));
            foreach (var p in dbPlaces)
            {
                places.Add(ConvertToPlace(p));
            }

            return places;
        }

        public void AddCampingPlace(
            string name, string addedBy, string description, string googleMapsUrl, bool hasWater,
            IEnumerable<string> sightseeingNames, IEnumerable<string> siteCategoryNames,
            IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("CampingPlaceName");
            }

            if (addedBy == null)
            {
                throw new ArgumentNullException("UserName");
            }

            if (imageFileNames == null ||
                !imageFileNames.GetEnumerator().MoveNext() ||
                imageFilesData == null ||
                !imageFilesData.GetEnumerator().MoveNext())
            {
                throw new ArgumentNullException("CampingPlace ImageFiles");
            }

            if (imageFileNames.Count != imageFilesData.Count)
            {
                throw new ArgumentException("CampingPlace ImageFiles Names vs Data");
            }

            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            ICampingPlace newCampingPlace = new CampingPlace();
            newCampingPlace.Name = name;
            newCampingPlace.AddedBy = addedBy;
            newCampingPlace.Description = description;
            newCampingPlace.GoogleMapsUrl = googleMapsUrl;
            newCampingPlace.HasWater = hasWater;
            newCampingPlace.SightseeingNames = sightseeingNames;
            newCampingPlace.SiteCategoriesNames = siteCategoryNames;

            using (var uw = this.unitOfWork())
            {
                DbCampingPlace dbCampingPlace = ConvertFromPlace(newCampingPlace);
                dbCampingPlace.DbImageFiles = GetImageFiles(imageFileNames, imageFilesData);
                capmingPlaceRepository.Add(dbCampingPlace);
                uw.Commit();
            }
        }

        public void DeleteCampingPlace(Guid id)
        {
            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var dbPlace = capmingPlaceRepository.GetById(id);
            if (dbPlace != null)
            {
                using (var uw = this.unitOfWork())
                {
                    dbPlace.IsDeleted = true;
                    uw.Commit();
                }
            }
        }

        public IEnumerable<ICampingPlace> GetCampingPlaceById(Guid id)
        {
            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var dbPlace = capmingPlaceRepository.GetById(id);
            if (dbPlace == null || dbPlace.IsDeleted)
            {
                return null;
            }

            var places = new List<ICampingPlace>();
            places.Add(ConvertToPlace(dbPlace));

            return places;
        }

        public IEnumerable<ICampingPlace> GetLatestCampingPlaces(int count)
        {
            if (count <= 0)
            {
                return (null);
            }

            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var places = new List<ICampingPlace>();
            var dbPlaces = capmingPlaceRepository.GetAll(p => !p.IsDeleted, p => p.AddedOn);

            if (dbPlaces == null)
            {
                return null;
            }

            int counter = 0;
            int total = dbPlaces.Count();
            foreach (var p in dbPlaces)
            {
                counter++;
                if (counter > total - count)
                {
                    places.Add(ConvertToPlace(p));
                }
            }

            return places;
        }

        public IEnumerable<ICampingPlace> GetAllCampingPlaces()
        {
            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var dbPlaces = capmingPlaceRepository.GetAll(p => !p.IsDeleted);
            if (dbPlaces == null)
            {
                return null;
            }

            var places = new List<ICampingPlace>();
            foreach (var p in dbPlaces)
            {
                places.Add(ConvertToPlace(p));
            }

            return places;
        }

        private ICollection<DbImageFile> GetImageFiles(IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            if (imageFileNames == null || imageFilesData == null)
            {
                return null;
            }

            ICollection<DbImageFile> dbFiles = new List<DbImageFile>();
            for (int i = 0; i < imageFileNames.Count; i++)
            {
                DbImageFile dbFile = new DbImageFile();
                dbFile.FileName = imageFileNames[i];
                dbFile.Data = imageFilesData[i];

                dbFiles.Add(dbFile);
            }

            return dbFiles;
        }

        private DbCampingPlace ConvertFromPlace(ICampingPlace p)
        {
            DbCampingPlace place = new DbCampingPlace();
            place.Name = p.Name;
            place.Description = p.Description;
            place.GoogleMapsUrl = p.GoogleMapsUrl;
            place.WaterOnSite = p.HasWater;
            place.AddedOn = DateTime.Now;
            place.IsDeleted = p.IsDeleted;

            IGenericEFository<DbCampingUser> campingUserRepository =
                this.repository.GetCampingUserRepository();
            place.AddedBy = campingUserRepository.GetAll(u => u.UserName == p.AddedBy).FirstOrDefault();

            IGenericEFository<DbSightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            place.DbSightseeings = sightseeingRepository.GetAll(s => p.SightseeingNames.Contains(s.Name)).ToList();
            place.DbSiteCategories = siteCategoryRepository.GetAll(s => p.SiteCategoriesNames.Contains(s.Name)).ToList();

            return place;
        }

        private ICampingPlace ConvertToPlace(DbCampingPlace p)
        {
            ICampingPlace place = new CampingPlace();
            place.Name = p.Name;
            place.Id = p.Id;
            place.AddedBy = p.AddedBy.FirstName + " " + p.AddedBy.LastName;
            place.AddedOn = p.AddedOn;
            place.Description = p.Description;
            place.GoogleMapsUrl = p.GoogleMapsUrl;
            place.HasWater = p.WaterOnSite;
            place.IsDeleted = p.IsDeleted;

            List<Guid> sightseeingIds = new List<Guid>();
            List<string> sightseeingNames = new List<string>();
            foreach (var s in p.DbSightseeings)
            {
                sightseeingIds.Add(s.Id);
                sightseeingNames.Add(s.Name);
            }

            place.SiteCategoriesIds = sightseeingIds;
            place.SightseeingNames = sightseeingNames;

            List<Guid> siteCategoriesIds = new List<Guid>();
            List<string> siteCategoriesNames = new List<string>();
            foreach (var c in p.DbSiteCategories)
            {
                siteCategoriesIds.Add(c.Id);
                siteCategoriesNames.Add(c.Name);
            }

            place.SiteCategoriesIds = siteCategoriesIds;
            place.SiteCategoriesNames = siteCategoriesNames;

            List<string> imgFileNames = new List<string>();
            List<byte[]> imgFilesData = new List<byte[]>();
            var imgRepository = repository.GetImageFileRepository();
            var dbImages = imgRepository.GetAll(img => img.DbCampingPlaceId == p.Id);
            List<IImageFile> imageFiles = new List<IImageFile>();
            foreach (var dbImg in dbImages)
            {
                IImageFile img = new ImageFile();
                img.CampingPlaceId = dbImg.DbCampingPlaceId;
                img.Data = dbImg.Data;
                img.FileName = dbImg.FileName;
                imageFiles.Add(img);
            }

            place.ImageFiles = imageFiles;

            return place;
        }
    }
}