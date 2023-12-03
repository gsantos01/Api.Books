using Api.Books.Api.Controllers;
using Api.Books.Api.DTOs;
using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Aplications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Api.Books.Tests.Unit.Controllers
{
    [TestClass]
    public class BookControllerTests
    {

        [TestMethod]
        [Fact]
        public async Task GetBooksShouldReturnOkWithBooks()
        {
            var mockBookApplication = new Mock<IBookApplication>();
            mockBookApplication.Setup(app => app.GetList())
                .ReturnsAsync(new List<Book> { new Book { Id = 1, Title = "Book 1" }, new Book { Id = 2, Title = "Book 2" } });

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookApplication.Object, mockLogger.Object);

            var result = await controller.GetBooks();

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var books = Xunit.Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Xunit.Assert.Equal(2, books?.Count());
        }

        [TestMethod]
        [Fact]
        public async Task GetBookByIdShouldReturnOkWithBook()
        {
            var bookId = 1;
            var mockBookApplication = new Mock<IBookApplication>();
            mockBookApplication.Setup(app => app.GetById(bookId))
                .ReturnsAsync(new Book { Id = bookId, Title = "Sample Book" });

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookApplication.Object, mockLogger.Object);
            var result = await controller.GetBookById(bookId);

            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            var book = Xunit.Assert.IsType<Book>(okResult.Value);
            Xunit.Assert.Equal("Sample Book", book.Title);
        }

        [TestMethod]
        [Fact]
        public async Task GetBookByIdShouldReturnNotFoundWhenBookNotFound()
        {
            var bookId = 1;
            var mockBookApplication = new Mock<IBookApplication>();
            mockBookApplication.Setup(app => app.GetById(bookId)).ReturnsAsync((Book)null);

            var mockLogger = new Mock<ILogger<BookController>>();

            var controller = new BookController(mockBookApplication.Object, mockLogger.Object);
            var result = await controller.GetBookById(bookId);

            Xunit.Assert.IsType<NotFoundResult>(result);
        }
    }
}