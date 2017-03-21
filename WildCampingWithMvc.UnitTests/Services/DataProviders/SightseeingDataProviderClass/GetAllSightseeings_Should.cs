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
    public class GetAllSightseeings_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string name_01 = "Name_01";
        private string name_02 = "Name_02";
        private string name_03 = "Name_03";

        [Test]
        public void CallExactlyOnceSightseeingRepositoryMethodGetAll()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.GetAllSightseeings();

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository()
                .GetAll(), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenThereArentAnySightseeingsInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            IEnumerable<DbSightseeing> dbSightseeings = null;
            Mock.Arrange(() => repository.GetSightseeingRepository()
                .GetAll()).Returns(dbSightseeings);

            // Act
            var sightseeings = provider.GetAllSightseeings();

            // Assert
            Assert.IsNull(sightseeings);
        }

        [Test]
        public void ReturnsAllSightseeings_WhenSightseeingsExistInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            IEnumerable<DbSightseeing> dbSightseeings = this.GetDbSightseeings();

            Mock.Arrange(() => repository.GetSightseeingRepository()
                .GetAll()).Returns(dbSightseeings);

            IEnumerable<ISightseeing> expectedSightseeings = this.GetSightseeings();

            // Act
            var sightseeings = provider.GetAllSightseeings();

            // Assert
            Assert.AreEqual(expectedSightseeings.Count(), sightseeings.Count());
            foreach (var doublePlace in expectedSightseeings.Zip(sightseeings, Tuple.Create))
            {
                Assert.AreEqual(doublePlace.Item1.Id, doublePlace.Item2.Id);
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
