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

        public IEnumerable<ISiteCategory> GetAllSiteCategories()
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            var dbCategories = siteCategoryRepository.GetAll();
            if (dbCategories == null)
            {
                return null;
            }

            var categories = new List<ISiteCategory>();
            foreach (var c in dbCategories)
            {
                categories.Add(ConvertToSiteCategory(c));
            }

            return categories;
        }

        public ISiteCategory GetSiteCategoryById(Guid id)
        {
            IGenericEFository<DbSiteCategory> siteCategoryRepository =
                this.repository.GetSiteCategoryRepository();
            DbSiteCategory dbCategory = siteCategoryRepository.GetById(id);
            if (dbCategory == null)
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

            return category;
        }
    }
}
