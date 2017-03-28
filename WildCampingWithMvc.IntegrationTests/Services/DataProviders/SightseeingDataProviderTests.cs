using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WildCampingWithMvc.Db.Models;
using Ninject;
using WildCampingWithMvc.App_Start;
using WildCampingWithMvc.Db;

namespace WildCampingWithMvc.IntegrationTests.Services.DataProviders
{
    [TestFixture]
    public class SightseeingDataProviderTests
    {
        private IEnumerable<DbSightseeing> dbSightseeings = Utils.GetDbSightseeings(3);
        private ISightseeingDataProvider provider;
        private IWildCampingEFository repository;
        private static IKernel kernel;

        [OneTimeSetUp]
        public void TestInit()
        {
            kernel = NinjectWebCommon.CreateKernel();
            WildCampingWithMvcDbContext dbContext = kernel.Get<WildCampingWithMvcDbContext>();

            foreach (var dbSightseeing in this.dbSightseeings)
            {
                dbContext.DbSightseeings.Add(dbSightseeing);
            }

            dbContext.SaveChanges();

            this.repository = kernel.Get<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = kernel.Get<Func<IUnitOfWork>>();
            this.provider = new SightseeingDataProvider(repository, unitOfWork);
        }

        [OneTimeTearDown]
        public void TestCleanup()
        {
            WildCampingWithMvcDbContext dbContext = kernel.Get<WildCampingWithMvcDbContext>();

            foreach (var dbSightseeing in this.dbSightseeings)
            {
                dbContext.DbSightseeings.Attach(dbSightseeing);
                dbContext.DbSightseeings.Remove(dbSightseeing);
            }

            dbContext.SaveChanges();
        }

        [Test]
        public void GetAllSightseeings_ShouldReturnsAllSightseeings_WhenSightseeingsExistInTheDB()
        {
            // Arrange
            IEnumerable<DbSightseeing> expectedSightseeings =
                repository.GetSightseeingRepository().GetAll();

            // Act
            var actualSightseeings = provider.GetAllSightseeings();

            // Assert
            Assert.AreEqual(expectedSightseeings.Count(), actualSightseeings.Count());
            foreach (var doubleSightseeing in expectedSightseeings.Zip(actualSightseeings, Tuple.Create))
            {
                Assert.AreEqual(doubleSightseeing.Item1.Id, doubleSightseeing.Item2.Id);
                Assert.AreEqual(doubleSightseeing.Item1.Name, doubleSightseeing.Item2.Name);
            }
        }

        [Test]
        public void GetSightseeingById_ShouldReturnTheCorrectSightseeing_WhenSightseeingWithProvidedIdExistsInTheDB()
        {
            // Arrange
            Guid id = this.dbSightseeings.First().Id;
            DbSightseeing expectedSightseeing =
                repository.GetSightseeingRepository().GetById(id);

            // Act
            var sightseeing = provider.GetSightseeingById(id);

            // Assert
            Assert.AreEqual(expectedSightseeing.Id, sightseeing.Id);
            Assert.AreEqual(expectedSightseeing.Name, sightseeing.Name);

        }

        [Test]
        public void GetSightseeingById_ShouldReturnNull_WhenSightseeingWithProvidedIdDoesNotExistInTheDB()
        {
            // Arrange
            Guid id = Guid.Empty;

            // Act
            var sightseeing = provider.GetSightseeingById(id);

            // Assert
            Assert.IsNull(sightseeing);

        }
    }
}
