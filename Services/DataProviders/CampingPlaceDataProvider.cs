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

        public IEnumerable<ICampingPlace>GetCampingPlacesBySearchName(string searchedName)
        {
            IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                this.repository.GetCampingPlaceRepository();
            var places = new List<ICampingPlace>();
            var dbPlaces = capmingPlaceRepository.GetAll(p => (!p.IsDeleted) && (p.Name.Contains(searchedName)));
            foreach (var p in dbPlaces)
            {
                places.Add(this.ConvertToPlace(p));
            }

            return places;
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
                places.Add(this.ConvertToPlace(p));
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
                places.Add(this.ConvertToPlace(p));
            }

            return places;
        }

        public void UpdateCampingPlace(
            Guid id, string name, string description, string googleMapsUrl, bool hasWater, 
            IEnumerable<string> sightseeingNames, IEnumerable<string> siteCategoryNames,
            IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            this.GuardCampingPlace(name, imageFileNames, imageFilesData);

            IGenericEFository<DbCampingPlace> campingPlaceRepository =
                    this.repository.GetCampingPlaceRepository();
            DbCampingPlace dbCampingPlace = campingPlaceRepository.GetById(id);
            if (dbCampingPlace == null)
            {
                throw new ArgumentException("Invalid CampingPlaceId");
            }

            ICampingPlace updatedCampingPlace = new CampingPlace();
            updatedCampingPlace.Id = id;
            updatedCampingPlace.Name = name;
            updatedCampingPlace.Description = description;
            updatedCampingPlace.GoogleMapsUrl = googleMapsUrl;
            updatedCampingPlace.HasWater = hasWater;
            updatedCampingPlace.SightseeingNames = sightseeingNames;
            updatedCampingPlace.SiteCategoriesNames = siteCategoryNames;
            using (var uw = this.unitOfWork())
            {
                this.UpdateFromPlace(updatedCampingPlace, dbCampingPlace);
                this.UpdateImages(dbCampingPlace, imageFileNames, imageFilesData);
                this.UpdateCategories(dbCampingPlace, siteCategoryNames);
                this.UpdateSightseeings(dbCampingPlace, sightseeingNames);
                
                campingPlaceRepository.Update(dbCampingPlace);
                uw.Commit();
            }
        }

        public void AddCampingPlace(
            string name, string addedBy, string description, string googleMapsUrl, bool hasWater,
            IEnumerable<string> sightseeingNames, IEnumerable<string> siteCategoryNames,
            IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            this.GuardCampingPlace(name, imageFileNames, imageFilesData);
            if (addedBy == null)
            {
                throw new ArgumentNullException("UserName");
            }

            ICampingPlace newCampingPlace = new CampingPlace();
            newCampingPlace.Name = name;
            newCampingPlace.AddedBy = addedBy;
            newCampingPlace.Description = description;
            newCampingPlace.GoogleMapsUrl = googleMapsUrl;
            newCampingPlace.HasWater = hasWater;
            newCampingPlace.SightseeingNames = sightseeingNames;
            newCampingPlace.SiteCategoriesNames = siteCategoryNames;
            DbCampingPlace dbCampingPlace =  this.AddFromPlace(newCampingPlace);
            dbCampingPlace.DbImageFiles = this.GetImageFiles(imageFileNames, imageFilesData, dbCampingPlace.Id);

            using (var uw = this.unitOfWork())
            {
                IGenericEFository<DbCampingPlace> capmingPlaceRepository =
                    this.repository.GetCampingPlaceRepository();
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
            places.Add(this.ConvertToPlace(dbPlace));

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
                    places.Add(this.ConvertToPlace(p));
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
                places.Add(this.ConvertToPlace(p));
            }

            return places;
        }

        private void UpdateSightseeings(DbCampingPlace dbCampingPlace, IEnumerable<string> sightseeingNames)
        {
            IGenericEFository<DbSightseeing> sightseeingRepository =
                    this.repository.GetSightseeingRepository();
            ICollection<DbSightseeing> newDbSightseeings = sightseeingRepository.GetAll(
                s => sightseeingNames.Contains(s.Name)).ToList();
            IEnumerable<DbSightseeing> oldDbSightseeings = sightseeingRepository.GetAll(
                s => s.DbCampingPlaces.FirstOrDefault(cp => cp.Id == dbCampingPlace.Id) != null);
            foreach (DbSightseeing dbSightseeing in oldDbSightseeings)
            {
                if (newDbSightseeings.Contains(dbSightseeing))
                {
                    newDbSightseeings.Remove(dbSightseeing);
                }
                else
                {
                    dbCampingPlace.DbSightseeings.Remove(dbSightseeing);
                    dbSightseeing.DbCampingPlaces.Remove(dbCampingPlace);
                }
            }

            foreach (DbSightseeing dbSightseeing in newDbSightseeings)
            {
                dbCampingPlace.DbSightseeings.Add(dbSightseeing);
                dbSightseeing.DbCampingPlaces.Add(dbCampingPlace);
            }
        }

        private void UpdateCategories(DbCampingPlace dbCampingPlace, IEnumerable<string> siteCategoryNames)
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                    this.repository.GetSiteCategoryRepository();
            ICollection<DbSiteCategory> newDbCategories = siteCategoryRepository.GetAll(
                s => siteCategoryNames.Contains(s.Name)).ToList();
            IEnumerable<DbSiteCategory> oldDbCategories = siteCategoryRepository.GetAll(
                cat => cat.DbCampingPlaces.FirstOrDefault(cp => cp.Id == dbCampingPlace.Id) != null);
            foreach (DbSiteCategory dbSiteCategory in oldDbCategories)
            {
                if (newDbCategories.Contains(dbSiteCategory))
                {
                    newDbCategories.Remove(dbSiteCategory);
                }
                else
                {
                    dbCampingPlace.DbSiteCategories.Remove(dbSiteCategory);
                    dbSiteCategory.DbCampingPlaces.Remove(dbCampingPlace);
                }
            }

            foreach (DbSiteCategory dbSiteCategory in newDbCategories)
            {
                dbCampingPlace.DbSiteCategories.Add(dbSiteCategory);
                dbSiteCategory.DbCampingPlaces.Add(dbCampingPlace);
            }
        }

        private void UpdateImages(DbCampingPlace dbCampingPlace, IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            IGenericEFository<DbImageFile> imageFileRepository =
                    this.repository.GetImageFileRepository();
            IEnumerable<DbImageFile> oldDbImages = imageFileRepository.GetAll(img => img.DbCampingPlaceId == dbCampingPlace.Id);
            ICollection<DbImageFile> newDbImages = this.GetImageFiles(imageFileNames, imageFilesData, dbCampingPlace.Id);
            foreach (DbImageFile dbImage in oldDbImages)
            {
                if (!newDbImages.Contains(dbImage))
                {
                    imageFileRepository.Delete(dbImage);
                }
                else
                {
                    newDbImages.Remove(dbImage);
                }
            }

            // Add new images
            foreach (DbImageFile dbImage in newDbImages)
            {
                dbCampingPlace.DbImageFiles.Add(dbImage);
                imageFileRepository.Add(dbImage);
            }
        }

        private ICollection<DbImageFile> GetImageFiles(IList<string> imageFileNames, IList<byte[]> imageFilesData, Guid dBcampingPlaceId)
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
                dbFile.DbCampingPlaceId = dBcampingPlaceId;

                dbFiles.Add(dbFile);
            }

            return dbFiles;
        }

        private void UpdateFromPlace(ICampingPlace p, DbCampingPlace place)
        {
            place.Name = p.Name;
            place.Description = p.Description;
            place.GoogleMapsUrl = p.GoogleMapsUrl;
            place.WaterOnSite = p.HasWater;
            //place.AddedOn = DateTime.Now;
            //place.IsDeleted = p.IsDeleted;
        }

        private DbCampingPlace AddFromPlace(ICampingPlace p)
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
            place.AddedById = place.AddedBy.Id;

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
            //place.AddedBy = p.AddedBy.FirstName + " " + p.AddedBy.LastName;
            place.AddedBy = p.AddedBy.UserName;
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

            place.SightseeingIds = sightseeingIds;
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

        private void GuardCampingPlace(string name, 
            IList<string> imageFileNames, IList<byte[]> imageFilesData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("CampingPlaceName");
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
        }
    }
}