using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SiteCategoryDataProviderClass
{
    [TestFixture]
    public class GetAllSiteCategories_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string name_01 = "Name_01";
        private string name_02 = "Name_02";
        private string name_03 = "Name_03";

        [Test]
        public void CallExactlyOnceSiteCategoryRepositoryMethodGetAll()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);

            // Act
            provider.GetAllSiteCategories();

            // Assert
            Mock.Assert(() => repository.GetSiteCategoryRepository()
                .GetAll(), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenThereArentAnySiteCategoriesInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            IEnumerable<DbSiteCategory> dbSiteCategories = null;
            Mock.Arrange(() => repository.GetSiteCategoryRepository()
                .GetAll()).Returns(dbSiteCategories);

            // Act
            var siteCategories = provider.GetAllSiteCategories();

            // Assert
            Assert.IsNull(siteCategories);
        }

        [Test]
        public void ReturnsAllSiteCategories_WhenSiteCategoriesExistInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            IEnumerable<DbSiteCategory> dbSiteCategories = this.GetDbSiteCategories();

            Mock.Arrange(() => repository.GetSiteCategoryRepository()
                .GetAll()).Returns(dbSiteCategories);

            IEnumerable<ISiteCategory> expectedSiteCategories = this.GetSiteCategories();

            // Act
            var siteCategories = provider.GetAllSiteCategories();

            // Assert
            Assert.AreEqual(expectedSiteCategories.Count(), siteCategories.Count());
            foreach (var doublePlace in expectedSiteCategories.Zip(siteCategories, Tuple.Create))
            {
                Assert.AreEqual(doublePlace.Item1.Id, doublePlace.Item2.Id);
            }
        }

        private IEnumerable<ISiteCategory> GetSiteCategories()
        {
            IEnumerable<ISiteCategory> siteCategories =
                new List<ISiteCategory>()
            {
                new SiteCategory()
                {
                    Id = this.id_01,
                    Name = this.name_01
                },
                new SiteCategory()
                {
                    Id = this.id_02,
                    Name = this.name_02
                },
                new SiteCategory()
                {
                    Id = this.id_03,
                    Name = this.name_03
                }
            };

            return siteCategories;
        }

        private IEnumerable<DbSiteCategory> GetDbSiteCategories()
        {
            IEnumerable<DbSiteCategory> dbSiteCategories =
                new List<DbSiteCategory>()
            {
                new DbSiteCategory()
                {
                    Id = this.id_01,
                    Name = this.name_01
                },
                new DbSiteCategory()
                {
                    Id = this.id_02,
                    Name = this.name_02
                },
                new DbSiteCategory()
                {
                    Id = this.id_03,
                    Name = this.name_03
                }
            };

            return dbSiteCategories;
        }
    }
}
