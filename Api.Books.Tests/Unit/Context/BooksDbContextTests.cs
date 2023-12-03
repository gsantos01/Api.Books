using Api.Books.Core.Entities;
using Api.Books.Infrastructure.Context;
using Api.Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Books.Tests.Unit.Context
{
    [TestClass]
    public class BooksDbContextTests
    {
        [TestMethod]
        [Fact]
        public void CanAddBookToDb()
        {
            
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Add_Book_To_Db")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                
                context.Books.Add(new Book { Title = "Sample Book" });
                context.SaveChanges();
            }

            
            using (var context = new BooksDbContext(options))
            {
                Xunit.Assert.Equal(1, context.Books.Count());
                Xunit.Assert.Equal("Sample Book", context.Books.Single().Title);
            }
        }

        [TestMethod]
        [Fact]
        public void CanAddBookWithTwoAuthorsToDb()
        {
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Add_Book_With_Two_Authors_To_Db")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                var book = new Book
                {
                    Title = "Book with Two Authors",
                    Authors = new List<Author>()
                    {
                        new() { Name = "Author 1", DocumentNumber = "123" },
                        new() { Name = "Author 2", DocumentNumber = "456" }
                    }
                };

                context.Books.Add(book);
                context.SaveChanges();
            }

            using (var context = new BooksDbContext(options))
            {
                var savedBook = context.Books
                    .Include(b => b.Authors)
                    .Single(b => b.Title == "Book with Two Authors");

                Xunit.Assert.Equal(2, savedBook?.Authors?.Count);
                Xunit.Assert.Equal("Author 1", savedBook?.Authors?.FirstOrDefault(a => a.Name == "Author 1")?.Name);
                Xunit.Assert.Equal("Author 2", savedBook?.Authors?.FirstOrDefault(a => a.Name == "Author 2")?.Name);
            }
        }

        [TestMethod]
        [Fact]
        public void CanSelectBooksFromDb()
        {
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Select_Books_From_Db")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                context.Books.Add(new Book { Title = "Book 1" });
                context.Books.Add(new Book { Title = "Book 2" });
                context.SaveChanges();
            }

            using (var context = new BooksDbContext(options))
            {
                var books = context.Books.ToList();

                Xunit.Assert.Equal(2, books.Count);
                Xunit.Assert.Contains(books, b => b.Title == "Book 1");
                Xunit.Assert.Contains(books, b => b.Title == "Book 2");
            }
        }

        [TestMethod]
        [Fact]
        public async Task CanCheckIfTitleExistsInRepository()
        {
            var options = new DbContextOptionsBuilder<BooksDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Check_If_Title_Exists_In_Repository")
                .Options;

            using (var context = new BooksDbContext(options))
            {
                var bookRepository = new BookRepository(context);
                await bookRepository.AddAsync(new Book { Title = "Existing Book" });
            }

            using (var context = new BooksDbContext(options))
            {
                var bookRepository = new BookRepository(context);
                var titleExists = await bookRepository.TitleExists("Existing Book");

                Xunit.Assert.True(titleExists);
            }
        }
    }
}
