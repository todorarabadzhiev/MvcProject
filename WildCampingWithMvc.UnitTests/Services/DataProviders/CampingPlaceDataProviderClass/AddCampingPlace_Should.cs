using NUnit.Framework;
using EFositories;
using Services.DataProviders;
using System;
using System.Collections.Generic;
using Telerik.JustMock;
using WildCampingWithMvc.Db.Models;
using System.Linq.Expressions;

namespace CampingWebForms.Tests.Services.DataProviders.CampingPlaceDataProviderClass
{
    [TestFixture]
    public class AddCampingPlace_Should
    {
        private string campingPlaceName = "SomeName";
        private string addedBy = "SomeUserName";
        private DbCampingUser dbUser = new DbCampingUser()
        {
            Id = Guid.NewGuid()
        };

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedCampingPlaceNameIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlaceName";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
                null, this.addedBy, null, null, false, null, null, 
                this.GetImageFileNames(), this.GetImageFilesData()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedAddeByIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "UserName";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
               this.campingPlaceName, null, null, null, false, null, null,
               this.GetImageFileNames(), this.GetImageFilesData()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedImageNameListIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlace ImageFiles";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
               this.campingPlaceName, this.addedBy, null, null, false, null, null,
               null, this.GetImageFilesData()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedImageDataListIsNull()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlace ImageFiles";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
               this.campingPlaceName, this.addedBy, null, null, false, null, null,
               this.GetImageFileNames(), null));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedImageDataListIsEmpty()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlace ImageFiles";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
               this.campingPlaceName, this.addedBy, null, null, false, null, null,
               this.GetImageFileNames(), new List<byte[]>()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithCorrectMessage_WhenProvidedImageNamesListIsEmpty()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlace ImageFiles";

            // Act&Assert
            var ex = Assert.Throws<ArgumentNullException>(() => provider.AddCampingPlace(
               this.campingPlaceName, this.addedBy, null, null, false, null, null,
               new List<string>(), this.GetImageFilesData()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void ThrowArgumentExceptionWithCorrectMessage_WhenProvidedImageNamesListAndImageDataListDifferInCount()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            string expectedMessage = "CampingPlace ImageFiles Names vs Data";

            // Act&Assert
            var ex = Assert.Throws<ArgumentException>(() => provider.AddCampingPlace(
               this.campingPlaceName, this.addedBy, null, null, false, null, null,
               this.GetImageFileNamesTwo(), this.GetImageFilesData()));
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void CallExactlyOnceCampingPlaceRepositoryMethodAddWithValidArgument_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetCampingUserRepository()
                .GetAll(Arg.IsAny<Expression<Func<DbCampingUser, bool>>>()))
                .Returns(new List<DbCampingUser>() { this.dbUser});

            // Act
            provider.AddCampingPlace(this.campingPlaceName, this.addedBy,
                null, null, false, null, null,
               this.GetImageFileNames(), this.GetImageFilesData());

            // Assert
            Mock.Assert(() => repository.GetCampingPlaceRepository().Add(Arg.IsAny<DbCampingPlace>()), Occurs.Once());
        }

        [Test]
        public void CallExactlyOnceUnitOfWorkMethodCommit_WhenProvidedArgumentsAreValid()
        {
            // Arrange
            IWildCampingEFository repository = Mock.Create<IWildCampingEFository>();
            Func<IUnitOfWork> unitOfWork = Mock.Create<Func<IUnitOfWork>>();
            var provider = new CampingPlaceDataProvider(repository, unitOfWork);
            Mock.Arrange(() => repository.GetCampingUserRepository()
                .GetAll(Arg.IsAny<Expression<Func<DbCampingUser, bool>>>()))
                .Returns(new List<DbCampingUser>() { this.dbUser });

            // Act
            provider.AddCampingPlace(this.campingPlaceName, this.addedBy,
                null, null, false, null, null,
               this.GetImageFileNames(), this.GetImageFilesData());

            // Assert
            Mock.Assert(() => unitOfWork().Commit(), Occurs.Once());
        }

        private IList<string> GetImageFileNames()
        {
            IList<string> imageFileNames = new List<string>()
            {
                "Image_01",
                "Image_02",
                "Image_03"
            };

            return imageFileNames;
        }

        private IList<string> GetImageFileNamesTwo()
        {
            IList<string> imageFileNames = new List<string>()
            {
                "Image_01",
                "Image_02"
            };

            return imageFileNames;
        }

        private IList<byte[]> GetImageFilesData()
        {
            IList<byte[]> imageFilesData = new List<byte[]>()
            {
                new byte[2],
                new byte[2],
                new byte[2],
            };

            return imageFilesData;
        }
    }
}
