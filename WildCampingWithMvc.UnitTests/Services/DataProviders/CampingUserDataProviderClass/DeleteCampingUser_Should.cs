using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.UnitTests.Services.DataProviders.CampingUserDataProviderClass
{
    [TestFixture]
    public class DeleteCampingUser_Should
    {
        private Guid id_01 = Guid.NewGuid();

        [Test]
        public void CallsExactlyOnceCampingUserRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.DeleteCampingUser(id);

            // Assert
            Mock.Assert(() => repository.GetCampingUserRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenCampingUserIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingUser dbUser = Mock.Create<DbCampingUser>();
            Mock.Arrange(() => repository.GetCampingUserRepository().GetById(id)).Returns(dbUser);

            // Act
            provider.DeleteCampingUser(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }

        [Test]
        public void DoesNotCallUnitOfWorkMethodCommit_WhenCampingPlaceIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingUser dbUser = null;
            Mock.Arrange(() => repository.GetCampingUserRepository().GetById(id)).Returns(dbUser);

            // Act
            provider.DeleteCampingUser(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Never());
        }
    }
}
