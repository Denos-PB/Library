using api.DTO.Author;
using api.Models;

namespace api.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDTO ToAuthorDTO(this Author authormodel)
        {
            return new AuthorDTO
            {
                Id = authormodel.Id,
                Name = authormodel.Name,
                Books = authormodel.Books.Select(b => b.ToBookSimpleDTO()).ToList()
            };
        }

        public static AuthorSimpleDTO ToAuthorSimpleDTO(this Author authormodel)
        {
            return new AuthorSimpleDTO
            {
                Id = authormodel.Id,
                Name = authormodel.Name
            };
        }

        public static Author ToAuthorFromCreateDTO(this CreateAuthorDTO authorDTO) 
        {
            return new Author
            {
                Name = authorDTO.Name
            };
        }
    }
}