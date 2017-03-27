using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.UnitTests.Services.DataProviders.CampingPlaceDataProviderClass
{
    [TestFixture]
    public class GetCampingPlacesBySearchName_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string placeName_01 = "Name_01";
        private string placeName_02 = "Name_02";

        private string userName_01 = "User_01";
        private string userName_02 = "User_02";
        private string userName_03 = "User_03";

        [Test]
        public void CallExactlyOnceCampingPlaceRepositoryMethodGetAllWithCorrectExpressionAsArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string searchName = this.placeName_01;

            // Act
            provider.GetCampingPlacesBySearchName(searchName);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().GetAll(p => (!p.IsDeleted) && (p.Name.Contains(searchName))), Occurs.Once());
        }

        [Test]
        public void ReturnCorrectCampingPlaces_WhenMatchesOfTheSearchTermExist()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string searchName = this.placeName_01;
            IEnumerable<DbCampingPlace> dbPlaces = this.GetDbCampingPlaces()
                .Where(p => p.Name == searchName)
                .ToList();
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetAll(p => (!p.IsDeleted) && (p.Name.Contains(searchName)))).Returns(dbPlaces);

            // Act
            IEnumerable<ICampingPlace> foundPlaces = provider.GetCampingPlacesBySearchName(searchName);

            // Assert
            Assert.AreEqual(dbPlaces.Count(), foundPlaces.Count());
            foreach (var doublePlace in dbPlaces.Zip(foundPlaces, Tuple.Create))
            {
                Assert.AreEqual(doublePlace.Item1.Id, doublePlace.Item2.Id);
                Assert.AreEqual(doublePlace.Item1.Name, doublePlace.Item2.Name);
            }

        }

        [Test]
        public void ReturnEmptyCollection_WhenThereArentAnyMatchingCampingPlaces()
        {
            // Arrange
            string searchName = "fake name";
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            IEnumerable<DbCampingPlace> dbPlaces = new List<DbCampingPlace>();
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => (!p.IsDeleted) && (p.Name.Contains(searchName)))).Returns(dbPlaces);

            // Act
            var places = provider.GetCampingPlacesBySearchName(searchName);

            // Assert
            Assert.IsEmpty(places);
        }

        [Test]
        public void ReturnNull_WhenTheSearchTermIsNull()
        {
            // Arrange
            string searchName = null;
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => (!p.IsDeleted) && (p.Name.Contains(searchName)))).Returns((IEnumerable<DbCampingPlace>)null);

            // Act
            var places = provider.GetCampingPlacesBySearchName(searchName);

            // Assert
            Assert.IsNull(places);
        }

        private IEnumerable<DbCampingPlace> GetDbCampingPlaces()
        {
            IEnumerable<DbCampingPlace> dbPlaces =
                new List<DbCampingPlace>()
            {
                new DbCampingPlace()
                {
                    Id = this.id_01,
                    Name = this.placeName_01,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_01
                    },
                    Description = "",
                    GoogleMapsUrl = "",
                    WaterOnSite = false,
                    IsDeleted = false,
                    AddedOn = DateTime.Now,
                    DbImageFiles = new List<DbImageFile>(),
                    DbSightseeings = new List<DbSightseeing>(),
                    DbSiteCategories = new List<DbSiteCategory>()
                },
                new DbCampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_02
                    }
                },
                new DbCampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_01,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_03
                    }
                }
            };

            return dbPlaces;
        }
    }
}
