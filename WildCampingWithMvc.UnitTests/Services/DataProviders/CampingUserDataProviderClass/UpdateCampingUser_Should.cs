using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace WildCampingWithMvc.UnitTests.Services.DataProviders.CampingUserDataProviderClass
{
    [TestFixture]
    public class UpdateCampingUser_Should
    {
        private string firstName = "some First Name";
        private string lastName = "some Last Name";
        private Guid id = Guid.NewGuid();

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedFirstNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            string expectedMessage = "FirstName";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.UpdateCampingUser(
               this.id, null, this.lastName));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedLastNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            string expectedMessage = "LastName";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.UpdateCampingUser(
               this.id, this.firstName, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentExceptionWithCorrectMessage_WhenProvidedCampingUserIdIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetCampingUserRepository()
                .GetById(this.id)).Returns((DbCampingUser)null);
            string expectedMessage = "Invalid CampingUserId";

            // Act&Assert
            var ex = Assert.Throws<ArgumentException>(() => provider.UpdateCampingUser(
               this.id, this.firstName, this.lastName));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void CallsExactlyOnceCampingUserRepositoryMethodUpdateWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateCampingUser(this.id, this.firstName, this.lastName);

            // Assert
            Mock.Assert(() => repository.GetCampingUserRepository().Update(Arg.IsAny<DbCampingUser>()), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingUserDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateCampingUser(this.id, this.firstName, this.lastName);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }
    }
}
