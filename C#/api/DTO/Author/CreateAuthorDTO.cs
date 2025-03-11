using api.DTO.Books;

namespace api.DTO.Author
{
    public class CreateAuthorDTO
    {
        public required string Name { get; set; }
        public ICollection<BooksDTO> Books { get; set; }
    }
}