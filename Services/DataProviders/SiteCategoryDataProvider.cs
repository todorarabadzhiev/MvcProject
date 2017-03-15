using Repositories;
using Services.Models;
using System;
using System.Collections.Generic;

namespace Services.DataProviders
{
    public class SiteCategoryDataProvider : ISiteCategoryDataProvider
    {
        protected readonly ICampingDBRepository repository;
        protected readonly Func<IUnitOfWork> unitOfWork;

        public SiteCategoryDataProvider(ICampingDBRepository repository, Func<IUnitOfWork> unitOfWork)
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
        public IEnumerable<ISiteCategory> GetAllSiteCategories()
        {
            IGenericRepository<CampingDB.Models.SiteCategory> siteCategoryRepository =
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

        private ISiteCategory ConvertToSiteCategory(CampingDB.Models.SiteCategory c)
        {
            ISiteCategory category = new SiteCategory();
            category.Name = c.Name;
            category.Id = c.Id;

            return category;
        }
    }
}
