using Api.Books.Api.DTOs;
using Api.Books.Api.Extensions;
using Api.Books.Core.Interfaces.Aplications;
using Microsoft.AspNetCore.Mvc;

namespace Api.Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookApplication bookApplication, ILogger<BookController> logger) : ControllerBase
    {
        private readonly IBookApplication _bookApplication = bookApplication ?? throw new ArgumentNullException(nameof(bookApplication));
        private readonly ILogger<BookController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                var books = await _bookApplication.GetList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter livros.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookApplication.GetById(id);
                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter livro com ID {id}.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBook([FromBody] BookDetailsDto bookDto)
        {
            try
            {
                var newBook = bookDto.ToBookEntity();
                if (newBook is null) {
                    return BadRequest("Dados invalidos");
                }
                var bookId = await _bookApplication.Create(newBook);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar livro.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPatch()]
        public async Task<ActionResult> UpdateBook([FromBody] BookDetailsDto bookDto)
        {
            try
            {
                var bookToEdit = bookDto.ToBookEntity();
                if (bookToEdit is null)
                {
                    return BadRequest("Dados invalidos");
                }

                await _bookApplication.Update(bookToEdit);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar livro com ID {bookDto.Id}.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookApplication.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir livro com ID {id}.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
