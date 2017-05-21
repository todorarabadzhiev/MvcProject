using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SiteCategoryDataProviderClass
{
    [TestFixture]
    public class DeleteSiteCategory_Should
    {
        private Guid id_01 = Guid.NewGuid();

        [Test]
        public void CallsExactlyOnceSiteCategoryRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.DeleteSiteCategory(id);

            // Assert
            Mock.Assert(() => repository.GetSiteCategoryRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenSiteCategoryIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSiteCategory dbCategory = Mock.Create<DbSiteCategory>();
            Mock.Arrange(() => repository.GetSiteCategoryRepository().GetById(id)).Returns(dbCategory);

            // Act
            provider.DeleteSiteCategory(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }

        [Test]
        public void DoesNotCallUnitOfWorkMethodCommit_WhenSiteCategoryIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            Mock.Arrange(() => repository.GetSiteCategoryRepository().GetById(id)).Returns((DbSiteCategory)null);

            // Act
            provider.DeleteSiteCategory(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Never());
        }

        [Test]
        public void ChangesPropertyIsDeletedOfTheFoundSiteCategoryToTrue_WhenSiteCategoryIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSiteCategory dbCategory = Mock.Create<DbSiteCategory>();
            dbCategory.IsDeleted = false;
            Mock.Arrange(() => repository.GetSiteCategoryRepository().GetById(id)).Returns(dbCategory);

            // Act
            provider.DeleteSiteCategory(id);

            // Assert
            Assert.IsTrue(dbCategory.IsDeleted);
        }
    }
}
