using api.DTO.Author;

namespace api.DTO.Books
{
    public class BooksDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        public AuthorSimpleDTO Author { get; set; }
    }
}