using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Repositories;

namespace Api.Books.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task MergeAuthor(Author author);
    }
}
