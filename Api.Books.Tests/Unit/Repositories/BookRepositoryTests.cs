using Api.Books.Core.Entities;
using Api.Books.Infrastructure.Context;
using Api.Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Api.Books.Tests.Unit.Repositories
{
    [TestClass]
    public class BookRepositoryTests
    {
        [TestMethod]
        [Fact]
        public async Task GetBookAndAuthorsByIdAsyncShouldReturnBookWithAuthors()
        {
            var bookId = 1;
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBookAndAuthorsByIdAsync_Should_Return_Book_With_Authors")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                context.Books.Add(new Book
                {
                    Id = bookId,
                    Title = "Sample Book",
                    Authors = new List<Author> { new() { Id = 1, Name = "Author 1", DocumentNumber = "123" } }
                });
                context.SaveChanges();
            }


            using (var context = new BooksDbContext(options))
            {
                var mockRepository = new Mock<BookRepository>(context) { CallBase = true };
                var bookWithAuthors = await mockRepository.Object.GetBookAndAuthorsByIdAsync(bookId);

                Xunit.Assert.NotNull(bookWithAuthors);
                Xunit.Assert.Equal("Sample Book", bookWithAuthors.Title);
                Xunit.Assert.Single(bookWithAuthors.Authors);
                Xunit.Assert.Equal("Author 1", bookWithAuthors.Authors.First().Name);
            }
        }

        [TestMethod]
        [Fact]
        public async Task GetAllBooksShouldReturnAllBooks()
        {
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBooks_Should_Return_All_Books")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                context.Books.Add(new Book { Id = 1, Title = "Book 1" });
                context.Books.Add(new Book { Id = 2, Title = "Book 2" });
                context.SaveChanges();
            }

            using (var context = new BooksDbContext(options))
            {
                var mockRepository = new Mock<BookRepository>(context) { CallBase = true };
                var allBooks = await mockRepository.Object.GetAllBooks();

                Xunit.Assert.Equal(2, allBooks?.Count() ?? 0);
                Xunit.Assert.Contains(allBooks, b => b.Title == "Book 1");
                Xunit.Assert.Contains(allBooks, b => b.Title == "Book 2");
            }
        }

        [TestMethod]
        [Fact]
        public async Task TitleExistsShouldReturnTrueIfTitleExists()
        {

            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "TitleExists_Should_Return_True_If_Title_Exists")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                context.Books.Add(new Book { Id = 1, Title = "Existing Book" });
                context.SaveChanges();
            }


            using (var context = new BooksDbContext(options))
            {
                var mockRepository = new Mock<BookRepository>(context) { CallBase = true };
                var titleExists = await mockRepository.Object.TitleExists("Existing Book");


                Xunit.Assert.True(titleExists);
            }
        }
    }
}