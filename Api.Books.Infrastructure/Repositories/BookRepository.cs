using Api.Books.Core.Entities;
using Api.Books.Infrastructure.Context;
using Api.Books.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Books.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private BooksDbContext _dbContext;
        public BookRepository(BooksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> GetBookAndAuthorsByIdAsync(int id)
        {
            var book = await _dbContext.Books
                .Include(p => p.Authors)
                .FirstOrDefaultAsync(p => p.Id == id);

            return book;
        }

        public async Task<IEnumerable<Book>?> GetAllBooks()
        {
            return await _dbContext.Books
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<bool> TitleExists(string title)
        {
            return await _dbContext.Books
                .AnyAsync(p => p.Title == title);
        }
    }
}
