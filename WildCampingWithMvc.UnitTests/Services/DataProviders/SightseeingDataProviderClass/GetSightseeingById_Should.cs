using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SightseeingDataProviderClass
{
    [TestFixture]
    public class GetSightseeingById_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string name_01 = "Name_01";
        private string name_02 = "Name_02";
        private string name_03 = "Name_03";

        [Test]
        public void CallExactlyOnceSightseeingRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.GetSightseeingById(id);

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenSightseeingIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbSightseeing dbSightseeing = null;
            Mock.Arrange(() => repository.GetSightseeingRepository().GetById(id)).Returns(dbSightseeing);

            // Act
            ISightseeing foundPlace = provider.GetSightseeingById(id);

            // Assert
            Assert.IsNull(foundPlace);
        }

        [Test]
        public void ReturnsCorrectSightseeing_WhenSightseeingIsFoundById()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            ISightseeing expectedSightseeing = this.GetSightseeings()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            DbSightseeing dbSightseeing = this.GetDbSightseeings()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            Mock.Arrange(() => repository.GetSightseeingRepository().GetById(id)).Returns(dbSightseeing);

            // Act
            ISightseeing foundSightseeing = provider.GetSightseeingById(id);

            // Assert
            Assert.AreEqual(foundSightseeing.Id, expectedSightseeing.Id);
            Assert.AreEqual(foundSightseeing.Name, expectedSightseeing.Name);
        }

        private IEnumerable<ISightseeing> GetSightseeings()
        {
            IEnumerable<ISightseeing> sightseeings =
                new List<ISightseeing>()
            {
                new Sightseeing()
                {
                    Id = this.id_01,
                    Name = this.name_01
                },
                new Sightseeing()
                {
                    Id = this.id_02,
                    Name = this.name_02
                },
                new Sightseeing()
                {
                    Id = this.id_03,
                    Name = this.name_03
                }
            };

            return sightseeings;
        }

        private IEnumerable<DbSightseeing> GetDbSightseeings()
        {
            IEnumerable<DbSightseeing> dbSightseeings =
                new List<DbSightseeing>()
            {
                new DbSightseeing()
                {
                    Id = this.id_01,
                    Name = this.name_01
                },
                new DbSightseeing()
                {
                    Id = this.id_02,
                    Name = this.name_02
                },
                new DbSightseeing()
                {
                    Id = this.id_03,
                    Name = this.name_03
                }
            };

            return dbSightseeings;
        }
    }
}
