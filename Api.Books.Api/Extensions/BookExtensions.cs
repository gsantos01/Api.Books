using Api.Books.Api.DTOs;
using Api.Books.Core.Entities;

namespace Api.Books.Api.Extensions
{
    public static class BookExtensions
    {
        public static Book? ToBookEntity(this BookDetailsDto bookDto)
        {
            if (bookDto == null)
                return null;

            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                DatePublished = bookDto.DatePublished,
                Authors = bookDto?.Authors?.ToAuthorEntities()
            };
        }

        public static ICollection<Book?>? ToBookEntities(this ICollection<BookDetailsDto> bookDtos)
        {
            return bookDtos?.Select(bookDto => bookDto.ToBookEntity()).ToList();
        }
    }
}
