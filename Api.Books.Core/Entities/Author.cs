namespace Api.Books.Core.Entities
{
    public class Author : Common
    {
        public required string Name { get; set; }
        public string? Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book>? Books { get; set; }
        public required string DocumentNumber { get; set; }
    }
}