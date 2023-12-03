using Api.Books.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Api.Books.Api.DTOs
{
    public class BookDetailsDto: CommonDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MinLength(3, ErrorMessage = "O título deve ter no mínimo 3 caracteres.")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "A data de publicação é obrigatória.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1800-01-01", "9999-12-31", ErrorMessage = "A data de publicação deve ser maior que 1800.")]
        [DateNotInFuture(ErrorMessage = "A data de publicação não pode ser no futuro.")]
        public DateTime DatePublished { get; set; }
        public ICollection<AuthorDto>? Authors { get; set; }
    }
}
