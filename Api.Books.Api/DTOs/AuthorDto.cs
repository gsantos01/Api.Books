using Api.Books.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Api.Books.Api.DTOs
{
    public class AuthorDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres.")]
        public required string Name { get; set; }
        public string? Nickname { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DateOfBirthNotInFuture(ErrorMessage = "A data de nascimento não pode ser no futuro.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "O número do documento é obrigatório.")]
        public required string DocumentNumber { get; set; }
    }
}
