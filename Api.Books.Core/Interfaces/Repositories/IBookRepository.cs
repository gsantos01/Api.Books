using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Repositories;

namespace Api.Books.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> GetBookAndAuthorsByIdAsync(int id);
        Task<IEnumerable<Book>?> GetAllBooks();
        Task<bool> TitleExists(string title);
    }
}
