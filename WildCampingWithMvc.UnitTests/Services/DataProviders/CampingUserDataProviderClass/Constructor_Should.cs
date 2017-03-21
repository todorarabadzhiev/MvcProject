using CampingWebForms.Tests.Mocked;
using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using System;
using Telerik.JustMock;

namespace CampingWebForms.Tests.Services.DataProviders.CampingUserDataProviderClass
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateInstanceOfCampingUserDataProvider_WhenProvidedWithValidParameters()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();

            // Act
            CampingUserDataProvider provider = new CampingUserDataProvider(repository, unitOfWork);

            // Assert
            Assert.IsInstanceOf<CampingUserDataProvider>(provider);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContainingCampingDBRepository_WhenProvidedRepositoryIsNull()
        {
            // Arrange
            IWildCampingEFository repository = null;
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            string expectedMessage = "WildCampingEFository";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CampingUserDataProvider(repository, unitOfWork));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContainingUnitOfWork_WhenProvidedUnitOfWorkIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = null;
            string expectedMessage = "UnitOfWork";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CampingUserDataProvider(repository, unitOfWork));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void AssignRepositoryAndUnitOfWorkCorrectValues_WhenProvidedWithValidParameters()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();

            // Act
            var provider = new CampingUserDataProviderMock(repository, unitOfWork);

            // Assert
            Assert.AreSame(repository, provider.Repository);
            Assert.AreSame(unitOfWork, provider.UnitOfWork);
        }
    }
}
