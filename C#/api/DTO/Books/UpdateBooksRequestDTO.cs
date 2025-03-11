namespace api.DTO.Books
{
    public class UpdateBooksRequestDTO
    {
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required int Count { get; set; }
        public required string AuthorName { get; set; }
    }
}