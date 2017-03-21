using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.CampingPlaceDataProviderClass
{
    [TestFixture]
    public class DeleteCampingPlace_Should
    {
        private Guid id_01 = Guid.NewGuid();

        [Test]
        public void CallsExactlyOnceCampingPlaceRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.DeleteCampingPlace(id);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenCampingPlaceIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingPlace dbPlace = Mock.Create<DbCampingPlace>();
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            provider.DeleteCampingPlace(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }

        [Test]
        public void DoesNotCallUnitOfWorkMethodCommit_WhenCampingPlaceIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingPlace dbPlace = null;
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            provider.DeleteCampingPlace(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Never());
        }

        [Test]
        public void ChangesPropertyIsDeletedOfTheFoundCampingPlaceToTrue_WhenCampingPlaceIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingPlace dbPlace = Mock.Create<DbCampingPlace>();
            dbPlace.IsDeleted = false;
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            provider.DeleteCampingPlace(id);

            // Assert
            Assert.AreEqual(true, dbPlace.IsDeleted);
        }
    }
}
