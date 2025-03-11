namespace api.DTO.Books
{
    public class CreateBooksDTO
    {

        public required string Title { get; set; }
        public required string Genre { get; set; }
        public int Count { get; set; }
        public required string AuthorName { get; set; } 
    }
}