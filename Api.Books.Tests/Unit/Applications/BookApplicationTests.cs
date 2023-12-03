using Api.Books.Core.Aplications;
using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Aplications;
using Api.Books.Interfaces.Repositories;
using Moq;

namespace Api.Books.Tests.Unit.Applications
{
    [TestClass]
    public class BookApplicationTests
    {
        [TestMethod]
        [Fact]
        public async Task GetListShouldReturnAllBooks()
        {
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetAllBooks())
                .ReturnsAsync(new List<Book> { new() { Id = 1, Title = "Book 1" }, new() { Id = 2, Title = "Book 2" } });

            var bookApplication = new BookApplication(mockBookRepository.Object, Mock.Of<IAuthorRepository>());

            var result = await bookApplication.GetList();

            Xunit.Assert.Equal(2, result?.Count());
            Xunit.Assert.Contains(result, b => b.Title == "Book 1");
            Xunit.Assert.Contains(result, b => b.Title == "Book 2");
        }

        [TestMethod]
        [Fact]
        public async Task GetByIdShouldReturnBookWithAuthors()
        {
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookAndAuthorsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { Id = 1, Title = "Sample Book", Authors = new List<Author> { new Author { Id = 1, Name = "Author 1", DocumentNumber = "123" } } });

            var bookApplication = new BookApplication(mockBookRepository.Object, Mock.Of<IAuthorRepository>());

            var result = await bookApplication.GetById(1);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Sample Book", result.Title);
            Xunit.Assert.Single(result?.Authors);
            Xunit.Assert.Equal("Author 1", result.Authors.First().Name);
        }

        [TestMethod]
        [Fact]
        public async Task CreateShouldAddBookAndAuthors()
        {
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.TitleExists(It.IsAny<string>())).ReturnsAsync(false);

            var mockAuthorRepository = new Mock<IAuthorRepository>();

            var bookApplication = new BookApplication(mockBookRepository.Object, mockAuthorRepository.Object);

            var newBook = new Book
            {
                Title = "New Book",
                Authors = new List<Author> { new Author { Name = "New Author", DocumentNumber = "123" } }
            };
            
            var result = await bookApplication.Create(newBook);

            Xunit.Assert.True(result);
            mockBookRepository.Verify(repo => repo.AddAsync(newBook), Times.Once);
            mockAuthorRepository.Verify(repo => repo.MergeAuthor(It.IsAny<Author>()), Times.Once);
        }

        [TestMethod]
        [Fact]
        public async Task UpdateShouldUpdateBookAndAuthors()
        {
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { Id = 1, Title = "Existing Book" });

            var mockAuthorRepository = new Mock<IAuthorRepository>();

            var bookApplication = new BookApplication(mockBookRepository.Object, mockAuthorRepository.Object);

            var updatedBook = new Book
            {
                Id = 1,
                Title = "Updated Book",
                Authors = new List<Author> { new Author { Name = "Updated Author", DocumentNumber = "123" } }
            };

            await bookApplication.Update(updatedBook);

            mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
            mockAuthorRepository.Verify(repo => repo.MergeAuthor(It.IsAny<Author>()), Times.Once);
        }

        [TestMethod]
        [Fact]
        public async Task DeleteByIdShouldDeleteBook()
        {
            var mockBookRepository = new Mock<IBookRepository>();

            var bookApplication = new BookApplication(mockBookRepository.Object, Mock.Of<IAuthorRepository>());

            await bookApplication.DeleteById(1);

            mockBookRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
