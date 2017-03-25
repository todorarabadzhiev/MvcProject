using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WildCampingWithMvc;
using WildCampingWithMvc.Controllers;

namespace WildCampingWithMvc.UnitTests.Controllers.HomeControllerClass
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectCampingPlaces()
        {
            // Arrange
            //var bookServiceMock = new Mock<IBookService>();
            //var categoryServiceMock = new Mock<ICategoryService>();
            //var bookModel = new BookModel()
            //{
            //    Id = Guid.NewGuid(),
            //    Author = "author",
            //    Description = "description",
            //    ISBN = "ISBN",
            //    Title = "title",
            //    WebSite = "website"
            //};

            //var bookViewModel = new BookViewModel(bookModel);

            //bookServiceMock.Setup(m => m.GetById(bookModel.Id)).Returns(bookModel);

            //BookController bookController = new BookController(bookServiceMock.Object, categoryServiceMock.Object);

            //// Act & Assert
            //bookController
            //    .WithCallTo(b => b.Details(bookModel.Id))
            //    .ShouldRenderDefaultView()
            //    .WithModel<BookViewModel>(viewModel =>
            //    {
            //        Assert.AreEqual(bookModel.Author, viewModel.Author);
            //        Assert.AreEqual(bookModel.ISBN, viewModel.ISBN);
            //        Assert.AreEqual(bookModel.Title, viewModel.Title);
            //        Assert.AreEqual(bookModel.WebSite, viewModel.WebSite);
            //        Assert.AreEqual(bookModel.Description, viewModel.Description);
            //    });
        }

        [Test]
        public void About()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.About() as ViewResult;

            //// Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Contact() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }

        [Test]
        public void FakeUnitTest()
        {
            //FAKE TEST
        }

        [Test]
        public void SecondFakeUnitTest()
        {
            //FAKE TEST
        }
    }
}
