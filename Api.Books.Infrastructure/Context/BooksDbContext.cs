using Api.Books.Core.Entities;
using Api.Books.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Books.Infrastructure.Context
{
    public class BooksDbContext(DbContextOptions<BooksDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }

}
