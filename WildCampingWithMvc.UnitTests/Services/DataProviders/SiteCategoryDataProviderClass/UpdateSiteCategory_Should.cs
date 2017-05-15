using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SiteCategoryDataProviderClass
{
    [TestFixture]
    public class UpdateCampingPlace_Should
    {
        private string siteCategoryName = "SomeName";
        private Guid id = Guid.NewGuid();

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSiteCategoryNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            string expectedMessage = "Category Name";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.UpdateSiteCategory(
                this.id, null, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentExceptionWithCorrectMessage_WhenProvidedSiteCategoryIdIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetSiteCategoryRepository()
                .GetById(this.id)).Returns((DbSiteCategory)null);
            string expectedMessage = "Invalid SiteCategory Id";

            // Act&Assert
            var ex = Assert.Throws<ArgumentException>(() => provider.UpdateSiteCategory(
               this.id, this.siteCategoryName, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }
        
        [Test]
        public void CallsExactlyOnceSiteCategoryRepositoryMethodUpdateWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateSiteCategory(this.id, this.siteCategoryName, null, null);

            // Assert
            Mock.Assert(() => repository.GetSiteCategoryRepository().Update(Arg.IsAny<DbSiteCategory>()), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateSiteCategory(this.id, this.siteCategoryName, null, null);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }
    }
}
