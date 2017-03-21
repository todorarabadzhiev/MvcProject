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
    public class GetSiteCategoryCampingPlaces_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string placeName_01 = "Name_01";
        private string placeName_02 = "Name_02";
        private string placeName_03 = "Name_03";

        private string categoryName_01 = "Category_01";
        private string categoryName_02 = "Category_02";

        [Test]
        public void ReturnNull_WhenProvidedCategoryNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            var places = provider.GetSiteCategoryCampingPlaces(null);

            // Assert
            Assert.IsNull(places);
        }

        [Test]
        public void CallsExactlyOnceCampingPlaceRepositoryMethodGetAllWithTypeOfExpressionAsArgument_WhenProvidedCategoryNameIsValid()
        {
            // Arrange
            string categoryName = this.categoryName_01;
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);

            // Act
            var places = provider.GetSiteCategoryCampingPlaces(categoryName);

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().GetAll(p => (!p.IsDeleted) &&
            (p.DbSiteCategories.FirstOrDefault(s => s.Name == categoryName) != null)), Occurs.Once());
            //Mock.Assert(() => repository.GetCampingPlaceRepository().GetAll(Arg.IsAny<Expression<Func<DbCampingPlace, bool>>>()), Occurs.Once());
        }

        [Test]
        public void ReturnSiteCategoryCampingPlaces_WhenProvidedCategoryNameIsValid()
        {
            // Arrange
            string categoryName = this.categoryName_01;
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            var expectedPlaces = this.GetCampingPlaces()
                .Where(p => p.SiteCategoriesNames.FirstOrDefault(s => s == categoryName) != null)
                .ToList();
            IEnumerable<DbCampingPlace> dbPlaces = this.GetDbCampingPlaces()
                .Where(p => p.DbSiteCategories.FirstOrDefault(s => s.Name == categoryName) != null)
                .ToList();
            Mock.Arrange(() => repository.GetCampingPlaceRepository()
                .GetAll(p => (!p.IsDeleted) &&
                    (p.DbSiteCategories.FirstOrDefault(s => s.Name == categoryName) != null)))
                .Returns(dbPlaces);

            // Act
            var places = provider.GetSiteCategoryCampingPlaces(categoryName);

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
                    SiteCategoriesNames = new List<string>()
                    {
                        this.categoryName_01
                    }
                },
                new CampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    SiteCategoriesNames = new List<string>()
                    {
                        this.categoryName_02
                    }
                },
                new CampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    SiteCategoriesNames = new List<string>()
                    {
                        this.categoryName_01
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
                    DbSiteCategories = new List<DbSiteCategory>()
                    {
                        new DbSiteCategory()
                            {
                                Name = this.categoryName_01
                            }
                    }
                },
                new DbCampingPlace()
                {
                    Id = this.id_02,
                    Name = this.placeName_02,
                    AddedBy = Mock.Create<DbCampingUser>(),
                    DbSiteCategories = new List<DbSiteCategory>()
                    {
                        new DbSiteCategory()
                            {
                                Name = this.categoryName_02
                            }
                    }
                },
                new DbCampingPlace()
                {
                    Id = this.id_03,
                    Name = this.placeName_03,
                    AddedBy = Mock.Create<DbCampingUser>(),
                    DbSiteCategories = new List<DbSiteCategory>()
                    {
                        new DbSiteCategory()
                            {
                                Name = this.categoryName_01
                            }
                    }
                }
            };

            return dbPlaces;
        }
    }
}
