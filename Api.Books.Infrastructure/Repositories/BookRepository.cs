using Api.Books.Core.Entities;
using Api.Books.Infrastructure.Context;
using Api.Books.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Books.Infrastructure.Repositories
{
    public class BookRepository(BooksDbContext dbContext) : Repository<Book>(dbContext), IBookRepository
    {
        private readonly BooksDbContext _dbContext = dbContext;

        public async Task<Book?> GetBookAndAuthorsByIdAsync(int id)
        {
            var book = await _dbContext.Books
                .Include(p => p.Authors)
                .FirstOrDefaultAsync(p => p.Id == id);

            //TODO: Correção no mapeamento para remover esse paliativo
            if (book != null && book.Authors != null) {
                foreach (var author in book.Authors) {
                    author.Books = null;
                }
            }

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
