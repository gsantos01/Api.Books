using Api.Books.Core.Entities;

namespace Api.Books.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        IQueryable<Author> Authors { get; }
    }
}
