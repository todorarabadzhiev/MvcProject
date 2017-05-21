using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SightseeingDataProviderClass
{
    [TestFixture]
    public class UpdateSightseeing_Should
    {
        private string sightseeingName = "SomeName";
        private Guid id = Guid.NewGuid();

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSightseeingNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            string expectedMessage = "Sightseeing Name";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.UpdateSightseeing(
                this.id, null, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentExceptionWithCorrectMessage_WhenProvidedSightseeingIdIsNotFound()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetSightseeingRepository()
                .GetById(this.id)).Returns((DbSightseeing)null);
            string expectedMessage = "Invalid Sightseeing Id";

            // Act&Assert
            var ex = Assert.Throws<ArgumentException>(() => provider.UpdateSightseeing(
               this.id, this.sightseeingName, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }
        
        [Test]
        public void CallsExactlyOnceSightseeingRepositoryMethodUpdateWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateSightseeing(this.id, this.sightseeingName, null, null);

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository().Update(Arg.IsAny<DbSightseeing>()), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.UpdateSightseeing(this.id, this.sightseeingName, null, null);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }
    }
}
