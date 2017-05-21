using EFositories;
using Services.Models;
using System;
using System.Collections.Generic;
using WildCampingWithMvc.Db.Models;

namespace Services.DataProviders
{
    public class SiteCategoryDataProvider : ISiteCategoryDataProvider
    {
        protected readonly IWildCampingEFository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public SiteCategoryDataProvider(IWildCampingEFository repository, Func<IUnitOfWork> unitOfWork)
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

        public void UpdateSiteCategory(Guid id, string name, string description, byte[] imageFileData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Category Name");
            }

            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                    this.repository.GetSiteCategoryRepository();
            DbSiteCategory dbSiteCategory = siteCategoryRepository.GetById(id);
            if (dbSiteCategory == null)
            {
                throw new ArgumentException("Invalid SiteCategory Id");
            }

            ISiteCategory updatedSiteCategory = new SiteCategory();
            updatedSiteCategory.Id = id;
            updatedSiteCategory.Name = name;
            updatedSiteCategory.Description = description;
            updatedSiteCategory.Image = imageFileData;
            using (var uw = this.unitOfWork())
            {
                this.UpdateFromCategory(updatedSiteCategory, dbSiteCategory);

                siteCategoryRepository.Update(dbSiteCategory);
                uw.Commit();
            }
        }

        public void DeleteSiteCategory(Guid id)
        {
            this.ChangeIsDeletedPropertyTo(id, true);
        }

        public void RecoverDeletedCategoryById(Guid id)
        {
            this.ChangeIsDeletedPropertyTo(id, false);
        }

        public void AddSiteCategory(string name, string description, byte[] imageFileData)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Category Name");
            }

            ISiteCategory newSiteCategory = new SiteCategory();
            newSiteCategory.Name = name;
            newSiteCategory.Description = description;
            newSiteCategory.Image = imageFileData;
            DbSiteCategory dbSiteCategory = this.ConvertFromSiteCategory(newSiteCategory);

            using (var uw = this.unitOfWork())
            {
                IGenericEFository<DbSiteCategory> siteCategoryRepository =
                    this.repository.GetSiteCategoryRepository();
                siteCategoryRepository.Add(dbSiteCategory);
                uw.Commit();
            }
        }

        public IEnumerable<ISiteCategory> GetAllSiteCategories()
        {
            return this.GetCategoriesByIsDeletedProperty(false);
        }

        public IEnumerable<ISiteCategory> GetDeletedSiteCategories()
        {
            return this.GetCategoriesByIsDeletedProperty(true);
        }

        public ISiteCategory GetSiteCategoryById(Guid id)
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            DbSiteCategory dbCategory = siteCategoryRepository.GetById(id);
            if (dbCategory == null || dbCategory.IsDeleted)
            {
                return null;
            }

            ISiteCategory category = ConvertToSiteCategory(dbCategory);

            return category;
        }

        private ISiteCategory ConvertToSiteCategory(DbSiteCategory c)
        {
            ISiteCategory category = new SiteCategory();
            category.Name = c.Name;
            category.Id = c.Id;
            category.Description = c.Description;
            category.IsDeleted = c.IsDeleted;
            category.Image = c.Image;

            return category;
        }

        private DbSiteCategory ConvertFromSiteCategory(ISiteCategory category)
        {
            DbSiteCategory c = new DbSiteCategory();
            c.Name = category.Name;
            c.Description = category.Description;
            c.IsDeleted = category.IsDeleted;
            c.Image = category.Image;

            return c;
        }

        private void UpdateFromCategory(ISiteCategory siteCategory, DbSiteCategory dbCategory)
        {
            dbCategory.Name = siteCategory.Name;
            dbCategory.Description = siteCategory.Description;
            dbCategory.Image = siteCategory.Image;
        }

        private IEnumerable<ISiteCategory> GetCategoriesByIsDeletedProperty(bool isDeleted)
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            var dbCategories = siteCategoryRepository.GetAll(c => c.IsDeleted == isDeleted);
            if (dbCategories == null)
            {
                return null;
            }

            var categories = new List<ISiteCategory>();
            foreach (var c in dbCategories)
            {
                categories.Add(this.ConvertToSiteCategory(c));
            }

            return categories;
        }

        private void ChangeIsDeletedPropertyTo(Guid id, bool isDeleted)
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            var dbSiteCategory = siteCategoryRepository.GetById(id);
            if (dbSiteCategory != null)
            {
                using (var uw = this.unitOfWork())
                {
                    dbSiteCategory.IsDeleted = isDeleted;
                    uw.Commit();
                }
            }
        }
    }
}
