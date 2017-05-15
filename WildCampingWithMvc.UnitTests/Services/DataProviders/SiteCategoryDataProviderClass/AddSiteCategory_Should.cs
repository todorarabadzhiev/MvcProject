using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SiteCategoryProviderClass
{
    [TestFixture]
    public class AddSiteCategory_Should
    {
        private string siteCategoryName = "SomeName";

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSiteCategoryNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);
            string expectedMessage = "Category Name";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddSiteCategory(null, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void CallsExactlyOnceSiteCategoryRepositoryMethodAddWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);

            // Act
            provider.AddSiteCategory(this.siteCategoryName, null, null);

            // Assert
            Mock.Assert(() => repository.GetSiteCategoryRepository().Add(Arg.IsAny<DbSiteCategory>()), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SiteCategoryDataProvider(repository, unitOfWork);

            // Act
            provider.AddSiteCategory(this.siteCategoryName, null, null);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }
    }
}
