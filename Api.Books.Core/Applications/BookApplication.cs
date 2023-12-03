using Api.Books.Core.Entities;
using Api.Books.Core.Interfaces.Aplications;
using Api.Books.Interfaces.Repositories;

namespace Api.Books.Core.Aplications
{
    public class BookApplication(IBookRepository bookRepository): IBookApplication
    {
        private readonly IBookRepository _bookRepository = bookRepository;

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
                await _bookRepository.AddAsync(book);
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
            }  
        }

        public async Task DeleteById(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
