using Repositories;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.DataProviders
{
    public class CampingPlaceDataProvider : ICampingPlaceDataProvider
    {
        protected readonly ICampingDBRepository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public CampingPlaceDataProvider(ICampingDBRepository repository, Func<IUnitOfWork> unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("CampingDBRepository");
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

            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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

            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var places = new List<ICampingPlace>();
            var dbPlaces = capmingPlaceRepository.GetAll(p => (!p.IsDeleted) &&
            (p.SiteCategories.FirstOrDefault(s => s.Name == categoryName) != null));
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

            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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
                CampingDB.Models.CampingPlace dbCampingPlace = ConvertFromPlace(newCampingPlace);
                dbCampingPlace.ImageFiles = GetImageFiles(imageFileNames, imageFilesData);
                capmingPlaceRepository.Add(dbCampingPlace);
                uw.Commit();
            }
        }

        public void DeleteCampingPlace(Guid id)
        {
            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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
            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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

            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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
            IGenericRepository<CampingDB.Models.CampingPlace> capmingPlaceRepository =
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

        private ICollection<CampingDB.Models.ImageFile> GetImageFiles(IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            if (imageFileNames == null || imageFilesData == null)
            {
                return null;
            }

            ICollection<CampingDB.Models.ImageFile> dbFiles = new List<CampingDB.Models.ImageFile>();
            for (int i = 0; i < imageFileNames.Count; i++)
            {
                CampingDB.Models.ImageFile dbFile = new CampingDB.Models.ImageFile();
                dbFile.FileName = imageFileNames[i];
                dbFile.Data = imageFilesData[i];

                dbFiles.Add(dbFile);
            }

            return dbFiles;
        }

        private CampingDB.Models.CampingPlace ConvertFromPlace(ICampingPlace p)
        {
            CampingDB.Models.CampingPlace place = new CampingDB.Models.CampingPlace();
            place.Name = p.Name;
            place.Description = p.Description;
            place.GoogleMapsUrl = p.GoogleMapsUrl;
            place.WaterOnSite = p.HasWater;
            place.AddedOn = DateTime.Now;
            place.IsDeleted = p.IsDeleted;

            IGenericRepository<CampingDB.Models.CampingUser> campingUserRepository =
                this.repository.GetCampingUserRepository();
            place.AddedBy = campingUserRepository.GetAll(u => u.UserName == p.AddedBy).FirstOrDefault();

            IGenericRepository<CampingDB.Models.Sightseeing> sightseeingRepository =
                this.repository.GetSightseeingRepository();
            IGenericRepository<CampingDB.Models.SiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            place.Sightseeings = sightseeingRepository.GetAll(s => p.SightseeingNames.Contains(s.Name)).ToList();
            place.SiteCategories = siteCategoryRepository.GetAll(s => p.SiteCategoriesNames.Contains(s.Name)).ToList();

            return place;
        }

        private ICampingPlace ConvertToPlace(CampingDB.Models.CampingPlace p)
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
            foreach (var s in p.Sightseeings)
            {
                sightseeingIds.Add(s.Id);
                sightseeingNames.Add(s.Name);
            }

            place.SiteCategoriesIds = sightseeingIds;
            place.SightseeingNames = sightseeingNames;

            List<Guid> siteCategoriesIds = new List<Guid>();
            List<string> siteCategoriesNames = new List<string>();
            foreach (var c in p.SiteCategories)
            {
                siteCategoriesIds.Add(c.Id);
                siteCategoriesNames.Add(c.Name);
            }

            place.SiteCategoriesIds = siteCategoriesIds;
            place.SiteCategoriesNames = siteCategoriesNames;

            List<string> imgFileNames = new List<string>();
            List<byte[]> imgFilesData = new List<byte[]>();
            var imgRepository = repository.GetImageFileRepository();
            var dbImages = imgRepository.GetAll(img => img.CampingPlaceId == p.Id);
            List<IImageFile> imageFiles = new List<IImageFile>();
            foreach (var dbImg in dbImages)
            {
                IImageFile img = new ImageFile();
                img.CampingPlaceId = dbImg.CampingPlaceId;
                img.Data = dbImg.Data;
                img.FileName = dbImg.FileName;
                imageFiles.Add(img);
            }

            place.ImageFiles = imageFiles;

            return place;
        }
    }
}