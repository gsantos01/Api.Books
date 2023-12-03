using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Aplications;
using Api.Books.Interfaces.Repositories;

namespace Api.Books.Core.Aplications
{
    public class BookApplication(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookApplication
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public async Task<IEnumerable<Book>?> GetList()
        {
            var response = await _bookRepository.GetAllBooks();
            return response;
        }

        public async Task<Book?> GetById(int id)
        {
            var response = await _bookRepository.GetBookAndAuthorsByIdAsync(id);
            return response;
        }

        public async Task<bool> Create(Book book)
        {
            var books = await _bookRepository.TitleExists(book.Title ?? "Sem titulo");

            if (!books) {
                book.CreatedAt = DateTime.Now;
                await _bookRepository.AddAsync(book);

                if(book.Authors != null)
                foreach (var author in book.Authors)
                    await _authorRepository.MergeAuthor(author);

                return true;
            }
            else
                return false;
        }

        public async Task Update(Book book)
        {
            var bookToChange = await _bookRepository.GetByIdAsync(book.Id);
            if (bookToChange != null) {
                await _bookRepository.UpdateAsync(bookToChange);

                if (book.Authors != null)
                    foreach (var author in book.Authors)
                        await _authorRepository.MergeAuthor(author);
            }  
        }

        public async Task DeleteById(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
