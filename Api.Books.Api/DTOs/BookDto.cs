namespace Api.Books.Api.DTOs
{
    public class BookDto : CommonDto
    {
        public required string Title { get; set; }
        public int YearPublished { get; set; }
    }
}