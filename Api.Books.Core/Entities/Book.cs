namespace Api.Books.Core.Entities
{
    public class Book: Common
    {
        public required string Title { get; set; }
        public DateTime DatePublished { get; set; }
        public ICollection<Author>? Authors { get; set; }
    }
}