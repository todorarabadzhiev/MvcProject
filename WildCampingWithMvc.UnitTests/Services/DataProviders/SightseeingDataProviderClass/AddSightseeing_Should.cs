using EFositories;
using NUnit.Framework;
using Services.DataProviders;
using System;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;

namespace CampingWebForms.Tests.Services.DataProviders.SightseeingProviderClass
{
    [TestFixture]
    public class AddSightseeing_Should
    {
        private string sightseeingName = "SomeName";

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedSightseeingNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);
            string expectedMessage = "Sightseeing Name";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddSightseeing(null, null, null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void CallsExactlyOnceSightseeingRepositoryMethodAddWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.AddSightseeing(this.sightseeingName, null, null);

            // Assert
            Mock.Assert(() => repository.GetSightseeingRepository().Add(Arg.IsAny<DbSightseeing>()), Occurs.Once());
        }

        [Test]
        public void CallsExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new SightseeingDataProvider(repository, unitOfWork);

            // Act
            provider.AddSightseeing(this.sightseeingName, null, null);

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }
    }
}
