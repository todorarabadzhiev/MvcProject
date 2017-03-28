using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WildCampingWithMvc.Db.Models;
using Ninject;
using WildCampingWithMvc.App_Start;
using WildCampingWithMvc.Db;

namespace WildCampingWithMvc.IntegrationTests.Services.DataProviders
{
    [TestFixture]
    public class SiteCategoryDataProviderTests
    {
        private IEnumerable<DbSiteCategory> dbCategories = Utils.GetDbCategory(3);
        private ISiteCategoryDataProvider provider;
        private IWildCampingEFository repository;
        private static IKernel kernel;

        [OneTimeSetUp]
        public void TestInit()
        {
            kernel = NinjectWebCommon.CreateKernel();
            WildCampingWithMvcDbContext dbContext = kernel.Get<WildCampingWithMvcDbContext>();

            foreach (var dbCategory in this.dbCategories)
            {
                dbContext.DbSiteCategories.Add(dbCategory);
            }

            dbContext.SaveChanges();

            this.repository = kernel.Get<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = kernel.Get<Func<IUnitOfWork>>();
            this.provider = new SiteCategoryDataProvider(repository, unitOfWork);
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            WildCampingWithMvcDbContext dbContext = kernel.Get<WildCampingWithMvcDbContext>();

            foreach (var dbCategory in this.dbCategories)
            {
                dbContext.DbSiteCategories.Attach(dbCategory);
                dbContext.DbSiteCategories.Remove(dbCategory);
            }

            dbContext.SaveChanges();
        }

        [Test]
        public void GetAllSiteCategories_ShouldReturnsAllSiteCategories_WhenSiteCategoriesExistInTheDB()
        {
            // Arrange
            IEnumerable<DbSiteCategory> dbSiteCategories =
                repository.GetSiteCategoryRepository().GetAll();

            // Act
            var siteCategories = provider.GetAllSiteCategories();

            // Assert
            Assert.AreEqual(dbSiteCategories.Count(), siteCategories.Count());
            foreach (var doubleCategory in dbSiteCategories.Zip(siteCategories, Tuple.Create))
            {
                Assert.AreEqual(doubleCategory.Item1.Id, doubleCategory.Item2.Id);
                Assert.AreEqual(doubleCategory.Item1.Name, doubleCategory.Item2.Name);
            }
        }

        [Test]
        public void GetSiteCategoryById_ShouldReturnTheCorrectSiteCategory_WhenSiteCategoryWithProvidedIdExistsInTheDB()
        {
            // Arrange
            Guid id = this.dbCategories.First().Id;
            DbSiteCategory expectedSiteCategory =
                repository.GetSiteCategoryRepository().GetById(id);

            // Act
            var siteCategory = provider.GetSiteCategoryById(id);

            // Assert
            Assert.AreEqual(expectedSiteCategory.Id, siteCategory.Id);
            Assert.AreEqual(expectedSiteCategory.Name, siteCategory.Name);

        }

        [Test]
        public void GetSiteCategoryById_ShouldReturnNull_WhenSiteCategoryWithProvidedIdDoesNotExistInTheDB()
        {
            // Arrange
            Guid id = Guid.Empty;

            // Act
            var siteCategory = provider.GetSiteCategoryById(id);

            // Assert
            Assert.IsNull(siteCategory);

        }
    }
}
