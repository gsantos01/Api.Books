namespace Api.Books.Core.Entities
{
    public class Book: Common
    {
        public string? Title { get; set; }
        public int YearPublished { get; set; }
        public ICollection<Author>? Authors { get; set; }
    }
}