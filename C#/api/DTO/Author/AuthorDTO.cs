using api.DTO.Books;

namespace api.DTO.Author
{
    public class AuthorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookSimpleDTO> Books { get; set; }
    }
}