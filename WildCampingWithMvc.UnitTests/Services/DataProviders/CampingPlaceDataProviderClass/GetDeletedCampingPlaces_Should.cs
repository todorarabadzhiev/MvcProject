using EFositories;
using NUnit.Framework;
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
    public class GetDeletedCampingPlaces_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();
        private Guid id_04 = Guid.NewGuid();

        private string placeName_01 = "Name_01";
        private string placeName_02 = "Name_02";
        private string placeName_03 = "Name_03";
        private string placeName_04 = "Name_04";

        private string userName_01 = "User_01";
        private string userName_02 = "User_02";
        private string userName_03 = "User_03";
        private string userName_04 = "User_04";

        [Test]
        public void CallExactlyOnceCampingPlaceRepositoryMethodGetAllWithRightExpressionsAsArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            provider.GetDeletedCampingPlaces();

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository()
                .GetAll(p => p.IsDeleted == true), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenThereArentAnyDeletedCampingPlacesInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => p.IsDeleted == true)).Returns((IEnumerable<DbCampingPlace>)null);

            // Act
            var places = provider.GetDeletedCampingPlaces();

            // Assert
            Assert.IsNull(places);
        }

        [Test]
        public void ReturnsAllDeletedCampingPlaces_WhenSuchCampingPlacesExistInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            IEnumerable<DbCampingPlace> dbPlaces = this.GetDbCampingPlaces().Where(p => p.IsDeleted).ToList();

            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => p.IsDeleted == true)).Returns(dbPlaces);

            // Act
            IEnumerable<ICampingPlace> places = provider.GetDeletedCampingPlaces();

            // Assert
            Assert.AreEqual(dbPlaces.Count(), places.Count());
            foreach (var doublePlace in dbPlaces.Zip(places, Tuple.Create))
            {
                Assert.AreEqual(doublePlace.Item1.AddedBy.UserName, doublePlace.Item2.AddedBy);
                Assert.AreEqual(doublePlace.Item1.AddedOn, doublePlace.Item2.AddedOn);
                Assert.AreEqual(doublePlace.Item1.Description, doublePlace.Item2.Description);
                Assert.AreEqual(doublePlace.Item1.GoogleMapsUrl, doublePlace.Item2.GoogleMapsUrl);
                Assert.AreEqual(doublePlace.Item1.WaterOnSite, doublePlace.Item2.HasWater);
                Assert.AreEqual(doublePlace.Item1.Name, doublePlace.Item2.Name);
                Assert.AreEqual(doublePlace.Item1.IsDeleted, doublePlace.Item2.IsDeleted);
                Assert.AreEqual(doublePlace.Item1.Id, doublePlace.Item2.Id);

                for (int i = 0; i < doublePlace.Item1.DbSightseeings.Count; i++)
                {
                    var sightseeing = ((IList<DbSightseeing>)doublePlace.Item1.DbSightseeings)[i];
                    Assert.AreEqual(sightseeing.Id, ((IList<string>)doublePlace.Item2.SightseeingIds)[i]);
                    Assert.AreEqual(sightseeing.Name, ((IList<string>)doublePlace.Item2.SightseeingNames)[i]);
                }

                for (int i = 0; i < doublePlace.Item1.DbSiteCategories.Count; i++)
                {
                    var siteCategories = ((IList<DbSiteCategory>)doublePlace.Item1.DbSiteCategories)[i];
                    Assert.AreEqual(siteCategories.Id, ((IList<string>)doublePlace.Item2.SiteCategoriesIds)[i]);
                    Assert.AreEqual(siteCategories.Name, ((IList<string>)doublePlace.Item2.SiteCategoriesNames)[i]);
                }
                
                foreach (var doubleImgs in doublePlace.Item1.DbImageFiles.Zip(doublePlace.Item2.ImageFiles, Tuple.Create))
                {
                    Assert.AreEqual(doubleImgs.Item1.FileName, doubleImgs.Item2.FileName);
                    Assert.AreEqual(doubleImgs.Item1.DbCampingPlaceId, doubleImgs.Item2.CampingPlaceId);
                    Assert.AreEqual(doubleImgs.Item1.Id, doubleImgs.Item2.Id);
                    Assert.AreEqual(doubleImgs.Item1.Data, doubleImgs.Item2.Data);
                }
            }
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
                    }
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
                },
                new DbCampingPlace()
                {
                    Id = this.id_04,
                    Name = this.placeName_04,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_04
                    },
                    IsDeleted = true
                }
            };

            return dbPlaces;
        }
    }
}
