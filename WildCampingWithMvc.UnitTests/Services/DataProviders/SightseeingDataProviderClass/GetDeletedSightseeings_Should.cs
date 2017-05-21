using EFositories;
using NUnit.Framework;
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
    public class GetDeletedSightseeings_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string name_01 = "Name_01";
        private string name_02 = "Name_02";
        private string name_03 = "Name_03";

        [Test]
        public void CallExactlyOnceSightseeingRepositoryMethodGetAllWithIsDeletedTrue()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.GetDeletedSightseeings();

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository()
                .GetAll(c => c.IsDeleted == true), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenThereArentAnyDeletedSightseeingsInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetSightseeingRepository()
                .GetAll(c => c.IsDeleted == true)).Returns((IEnumerable<DbSightseeing>)null);

            // Act
            var sightseeings = provider.GetDeletedSightseeings();

            // Assert
            Assert.IsNull(sightseeings);
        }

        [Test]
        public void ReturnsAllDeletedSightseeings_WhenSuchSightseeingsExistInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            IEnumerable<DbSightseeing> dbSightseeings = this.GetDbSightseeings().Where(c => c.IsDeleted).ToList();

            Mock.Arrange(() => repository.GetSightseeingRepository()
                .GetAll(c => c.IsDeleted == true)).Returns(dbSightseeings);

            IEnumerable<ISightseeing> expectedSightseeings = this.GetSightseeings().Where(c => c.IsDeleted).ToList();

            // Act
            var sightseeings = provider.GetDeletedSightseeings();

            // Assert
            Assert.AreEqual(expectedSightseeings.Count(), sightseeings.Count());
            foreach (var doubleSightseeing in expectedSightseeings.Zip(sightseeings, Tuple.Create))
            {
                Assert.AreEqual(doubleSightseeing.Item1.Id, doubleSightseeing.Item2.Id);
            }
        }

        private IEnumerable<ISightseeing> GetSightseeings()
        {
            IEnumerable<ISightseeing> sightseeings =
                new List<ISightseeing>()
            {
                new Sightseeing()
                {
                    Id = this.id_01,
                    Name = this.name_01,
                    IsDeleted = true
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
                    Name = this.name_01,
                    IsDeleted = true
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
