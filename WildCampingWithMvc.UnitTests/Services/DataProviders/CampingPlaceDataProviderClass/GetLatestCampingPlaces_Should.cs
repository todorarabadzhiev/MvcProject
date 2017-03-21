using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.CampingPlaceDataProviderClass
{
    [TestFixture]
    public class GetLatestCampingPlaces_Should
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

        private DateTime date_01 = new DateTime(2000, 1, 1);
        private DateTime date_02 = new DateTime(2005, 1, 1);
        private DateTime date_03 = new DateTime(2010, 1, 1);

        [TestCase(0)]
        [TestCase(-3)]
        public void ReturnsNull_WhenTheProvidedCountIsNotPositive(int count)
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            var places = provider.GetLatestCampingPlaces(count);

            // Assert
            Assert.IsNull(places);
        }

        [Test]
        public void CallsExactlyOnceCampingPlaceRepositoryMethodGetAllWithTwoExpressionsAsArguments_IfProvidedCountIsPositive()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            provider.GetLatestCampingPlaces(1);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository()
                .GetAll(p => !p.IsDeleted, p => p.AddedOn), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenThereArentAnyCampingPlacesInTheDB()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            IEnumerable<DbCampingPlace> dbPlaces = null;
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll
                (
                Arg.IsAny<Expression<Func<DbCampingPlace, bool>>>(),
                Arg.IsAny<Expression<Func<DbCampingPlace, DateTime>>>()
                )).Returns(dbPlaces);
            // Act
            var places = provider.GetLatestCampingPlaces(1);

            // Assert
            Assert.IsNull(places);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(10)]
        public void ReturnsRequiredNumberOfTheLatestCampingPlaces_WhenCampingPlacesExistInTheDB(int count)
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            List<DbCampingPlace> dbPlaces = this.GetDbCampingPlaces()
                .OrderByDescending(p => p.AddedOn).ToList();
            for (int i = 0; i < dbPlaces.Count / 2; i++)
            {
                var dbTemp = dbPlaces[i];
                dbPlaces[i] = dbPlaces[dbPlaces.Count - i - 1];
                dbPlaces[dbPlaces.Count - i - 1] = dbTemp;
            }

            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll
                (
                Arg.IsAny<Expression<Func<DbCampingPlace, bool>>>(),
                Arg.IsAny<Expression<Func<DbCampingPlace, DateTime>>>()
                )).Returns(dbPlaces);

            List<ICampingPlace> expectedPlaces = this.GetCampingPlaces()
                .OrderByDescending(p => p.AddedOn).ToList();
            // Get the required number
            while (expectedPlaces.Count > count)
            {
                expectedPlaces.RemoveAt(expectedPlaces.Count - 1);
            }
            // Get in reverse order
            for (int i = 0; i < expectedPlaces.Count / 2; i++)
            {
                var temp = expectedPlaces[i];
                expectedPlaces[i] = expectedPlaces[expectedPlaces.Count - i - 1];
                expectedPlaces[expectedPlaces.Count - i - 1] = temp;
            }

            // Act
            var places = provider.GetLatestCampingPlaces(count);

            // Assert
            Assert.AreEqual(expectedPlaces.Count, places.Count());
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
                    AddedBy = this.userName_01,
                    AddedOn = this.date_01
                },
                new CampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = this.userName_02,
                    AddedOn = this.date_02
                },
                new CampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    AddedBy = this.userName_03,
                    AddedOn = this.date_03
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
                    AddedOn = this.date_01
                },
                new DbCampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_02
                    },
                    AddedOn = this.date_02
                },
                new DbCampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    AddedBy = new DbCampingUser()
                    {
                        UserName = this.userName_03
                    },
                    AddedOn = this.date_03
                }
            };

            return dbPlaces;
        }
    }
}
