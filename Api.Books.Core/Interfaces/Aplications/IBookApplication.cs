using Api.Books.Core.Entities;

namespace Api.Books.Core.Interfaces.Aplications
{
    public interface IBookApplication
    {
        Task<IEnumerable<Book>?> GetList();
        Task<Book?> GetById(int id);
        Task<bool> Create(Book book);
        Task Update(Book book);
        Task DeleteById(int id);
    }
}
