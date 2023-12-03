using Api.Books.Api.DTOs;
using Api.Books.Core.Entities;

namespace Api.Books.Api.Extensions
{
    public static class AuthorExtensions
    {
        public static ICollection<Author> ToAuthorEntities(this ICollection<AuthorDto> authorDtos)
        {
            return authorDtos.Select(authorDto => new Author
            {
                Name = authorDto.Name,
                Nickname = authorDto.Nickname,
                DateOfBirth = authorDto.DateOfBirth,
                DocumentNumber = authorDto.DocumentNumber
            }).ToList();
        }
    }
}
