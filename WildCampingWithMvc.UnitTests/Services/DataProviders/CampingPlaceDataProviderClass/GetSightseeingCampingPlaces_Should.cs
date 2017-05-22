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
    public class GetSightseeingCampingPlaces_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string placeName_01 = "Name_01";
        private string placeName_02 = "Name_02";
        private string placeName_03 = "Name_03";

        private string sightseeingName_01 = "Sightseeing_01";
        private string sightseeingName_02 = "Sightseeing_02";

        [Test]
        public void ReturnNull_WhenProvidedSightseeingNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            var places = provider.GetSightseeingCampingPlaces(null);

            // Assert
            Assert.IsNull(places);
        }

        [Test]
        public void CallsExactlyOnceCampingPlaceRepositoryMethodGetAllWithTypeOfExpressionAsArgument_WhenProvidedSightseeingNameIsValid()
        {
            // Arrange
            string sightseeingName = this.sightseeingName_01;
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            var places = provider.GetSightseeingCampingPlaces(sightseeingName);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().GetAll(p => (!p.IsDeleted) &&
            (p.DbSightseeings.FirstOrDefault(s => s.Name == sightseeingName) != null)), Occurs.Once());
            //Mock.Assert(() => repository.GetCampingPlaceRepository().GetAll(Arg.IsAny<Expression<Func<DbCampingPlace, bool>>>()), Occurs.Once());
        }

        [Test]
        public void ReturnSightseeingCampingPlaces_WhenProvidedSightseeingNameIsValid()
        {
            // Arrange
            string sightseeingName = this.sightseeingName_01;
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            var expectedPlaces = this.GetCampingPlaces()
                .Where(p => p.SightseeingNames.FirstOrDefault(s => s == sightseeingName) != null)
                .ToList();
            IEnumerable<DbCampingPlace> dbPlaces = this.GetDbCampingPlaces()
                .Where(p => p.DbSightseeings.FirstOrDefault(s => s.Name == sightseeingName) != null)
                .ToList();
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => (!p.IsDeleted) &&
                    (p.DbSightseeings.FirstOrDefault(s => s.Name == sightseeingName) != null)))
                .Returns(dbPlaces);

            // Act
            var places = provider.GetSightseeingCampingPlaces(sightseeingName);

            // Assert
            Assert.AreEqual(expectedPlaces.Count(), places.Count());
            foreach (var doublePlace in expectedPlaces.Zip(places, Tuple.Create))
            {
                Assert.AreEqual(doublePlace.Item1.Id, doublePlace.Item2.Id);
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
                    SightseeingNames = new List<string>()
                    {
                        this.sightseeingName_01
                    }
                },
                new CampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    SightseeingNames = new List<string>()
                    {
                        this.sightseeingName_02
                    }
                },
                new CampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    SightseeingNames = new List<string>()
                    {
                        this.sightseeingName_01
                    }
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
                    AddedBy = Mock.Create<DbCampingUser>(),
                    DbSightseeings = new List<DbSightseeing>()
                    {
                        new DbSightseeing()
                            {
                                Name = this.sightseeingName_01
                            }
                    }
                },
                new DbCampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = Mock.Create<DbCampingUser>(),
                    DbSightseeings = new List<DbSightseeing>()
                    {
                        new DbSightseeing()
                            {
                                Name = this.sightseeingName_02
                            }
                    }
                },
                new DbCampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    AddedBy = Mock.Create<DbCampingUser>(),
                    DbSightseeings = new List<DbSightseeing>()
                    {
                        new DbSightseeing()
                            {
                                Name = this.sightseeingName_01
                            }
                    }
                }
            };

            return dbPlaces;
        }
    }
}
