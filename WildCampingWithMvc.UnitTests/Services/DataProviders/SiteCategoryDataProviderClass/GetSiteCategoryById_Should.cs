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
    public class GetSiteCategoryById_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string name_01 = "Name_01";
        private string name_02 = "Name_02";
        private string name_03 = "Name_03";

        [Test]
        public void CallExactlyOnceSiteCategoryRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.GetSiteCategoryById(id);

            // Assert
            Mock.Assert(() => repository.GetSiteCategoryRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenSiteCategoryIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSiteCategory dbSiteCategory = null;
            Mock.Arrange(() => repository.GetSiteCategoryRepository().GetById(id)).Returns(dbSiteCategory);

            // Act
            ISiteCategory foundCategory = provider.GetSiteCategoryById(id);

            // Assert
            Assert.IsNull(foundCategory);
        }

        [Test]
        public void ReturnsCorrectSiteCategory_WhenSiteCategoryIsFoundById()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            ISiteCategory expectedCategory = this.GetSiteCategories()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            DbSiteCategory dbCategory = this.GetDbSiteCategories()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            Mock.Arrange(() => repository.GetSiteCategoryRepository().GetById(id)).Returns(dbCategory);

            // Act
            ISiteCategory foundCategory = provider.GetSiteCategoryById(id);

            // Assert
            Assert.AreEqual(foundCategory.Id, expectedCategory.Id);
            Assert.AreEqual(foundCategory.Name, expectedCategory.Name);
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
