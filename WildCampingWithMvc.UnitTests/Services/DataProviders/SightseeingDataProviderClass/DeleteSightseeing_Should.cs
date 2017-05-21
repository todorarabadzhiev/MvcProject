﻿using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SightseeingDataProviderClass
{
    [TestFixture]
    public class DeleteSightseeing_Should
    {
        private Guid id_01 = Guid.NewGuid();

        [Test]
        public void CallsExactlyOnceSightseeingRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.DeleteSightseeing(id);

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenSightseeingIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSightseeing dbSightseeing = Mock.Create<DbSightseeing>();
            Mock.Arrange(() => repository.GetSightseeingRepository().GetById(id)).Returns(dbSightseeing);

            // Act
            provider.DeleteSightseeing(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }

        [Test]
        public void DoesNotCallUnitOfWorkMethodCommit_WhenSightseeingIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            Mock.Arrange(() => repository.GetSightseeingRepository().GetById(id)).Returns((DbSightseeing)null);

            // Act
            provider.DeleteSightseeing(id);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Never());
        }

        [Test]
        public void ChangesPropertyIsDeletedOfTheFoundSightseeingToTrue_WhenSightseeingIsFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSightseeing dbSightseeing = Mock.Create<DbSightseeing>();
            dbSightseeing.IsDeleted = false;
            Mock.Arrange(() => repository.GetSightseeingRepository().GetById(id)).Returns(dbSightseeing);

            // Act
            provider.DeleteSightseeing(id);

            // Assert
            Assert.IsTrue(dbSightseeing.IsDeleted);
        }
    }
}
