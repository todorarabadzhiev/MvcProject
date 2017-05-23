using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.CampingPlaceDataProviderClass
{
    [TestFixture]
    public class GetCampingPlaceById_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string placeName_01 = "Name_01";
        private string placeName_02 = "Name_02";
        private string placeName_03 = "Name_03";

        private string userName_01 = "User_01";
        private string userName_02 = "User_02";
        private string userName_03 = "User_03";

        [Test]
        public void CallExactlyOnceCampingPlaceRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.GetCampingPlaceById(id);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void ReturnNull_WhenCampingPlaceIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingPlace dbPlace = null;
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            IEnumerable<ICampingPlace> foundPlace = provider.GetCampingPlaceById(id);

            // Assert
            Assert.IsNull(foundPlace);
        }

        [Test]
        public void ReturnNull_WhenCampingPlaceIsFoundButTheIsDeletedPropertyIsTrue()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingPlace dbPlace = new DbCampingPlace() { Id = id, IsDeleted = true };
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            IEnumerable<ICampingPlace> foundPlace = provider.GetCampingPlaceById(id);

            // Assert
            Assert.IsNull(foundPlace);
        }

        [Test]
        public void ReturnCorrectCampingPlace_WhenCampingPlaceIsFoundByIdAndItsIsDeletedPropertyIsFalse()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            ICampingPlace expectedPlace = this.GetCampingPlaces()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            DbCampingPlace dbPlace = this.GetDbCampingPlaces()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            Mock.Arrange(() => repository.GetCampingPlaceRepository().GetById(id)).Returns(dbPlace);

            // Act
            IEnumerable<ICampingPlace> foundPlaces = provider.GetCampingPlaceById(id);

            // Assert
            Assert.AreEqual(1, foundPlaces.Count());
            foreach (var foundPlace in foundPlaces)
            {
                Assert.AreEqual(foundPlace.Id, expectedPlace.Id);
                Assert.AreEqual(foundPlace.Name, expectedPlace.Name);
            }
        }

        private IEnumerable<ICampingPlace> GetCampingPlaces()
        {
            IEnumerable<ICampingPlace> places = new List<ICampingPlace>()
            {
                new CampingPlace()
                {
                    Id = this.id_01,
                    Name = this.placeName_01,
                    AddedBy = this.userName_01
                },
                new CampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = this.userName_02
                },
                new CampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    AddedBy = this.userName_01
                }
            };

            return places;
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
                    Name = this.placeName_03,
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
