using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.UnitTests.Services.DataProviders.CampingUserDataProviderClass
{
    [TestFixture]
    public class GetCampingUserById_Should
    {
        private Guid id_01 = Guid.NewGuid();
        private Guid id_02 = Guid.NewGuid();
        private Guid id_03 = Guid.NewGuid();

        private string userName_01 = "Name_01";
        private string userName_02 = "Name_02";
        private string userName_03 = "Name_03";

        [Test]
        public void CallExactlyOnceCampingUserRepositoryMethodGetByIdWithCorrectArgument()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;

            // Act
            provider.GetCampingUserById(id);

            // Assert
            Mock.Assert(() => repository.GetCampingUserRepository().GetById(id), Occurs.Once());
        }

        [Test]
        public void ReturnsNull_WhenCampingUserIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            DbCampingUser dbUser = null;
            Mock.Arrange(() => repository.GetCampingUserRepository().GetById(id)).Returns(dbUser);

            // Act
            ICampingUser foundUser = provider.GetCampingUserById(id);

            // Assert
            Assert.IsNull(foundUser);
        }

        [Test]
        public void ReturnsCorrectCampingUser_WhenCampingUserIsFoundById()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Guid id = this.id_01;
            ICampingUser expectedUser = this.GetCampingUsers()
                .Where(p => p.Id == id)
                .FirstOrDefault();
            DbCampingUser dbUser = this.GetDbCampingUsers()
                .Where(u => u.Id == id)
                .FirstOrDefault();
            Mock.Arrange(() => repository.GetCampingUserRepository().GetById(id)).Returns(dbUser);

            // Act
            ICampingUser foundUser = provider.GetCampingUserById(id);

            // Assert
            Assert.AreEqual(foundUser.Id, expectedUser.Id);
            Assert.AreEqual(foundUser.UserName, expectedUser.UserName);
        }

        private IEnumerable<ICampingUser> GetCampingUsers()
        {
            IEnumerable<ICampingUser> users = new List<ICampingUser>()
            {
                new CampingUser()
                {
                    Id = this.id_01,
                    UserName = this.userName_01
                },
                new CampingUser()
                {
                    Id = this.id_02,
                    UserName = this.userName_02
                },
                new CampingUser()
                {
                    Id = this.id_03,
                    UserName = this.userName_03
                }
            };

            return users;
        }

        private IEnumerable<DbCampingUser> GetDbCampingUsers()
        {
            IEnumerable<DbCampingUser> dbUsers =
                new List<DbCampingUser>()
            {
                new DbCampingUser()
                {
                    Id = this.id_01,
                    UserName = this.userName_01
                },
                new DbCampingUser()
                {
                    Id = this.id_02,
                    UserName = this.userName_02
                },
                new DbCampingUser()
                {
                    Id = this.id_03,
                    UserName = this.userName_03
                }
            };

            return dbUsers;
        }
    }
}
