using api.DTO.Books;
using api.Models;

namespace api.Mappers
{
    public static class BooksMapper
    {
        public static BooksDTO ToBooksDTO(this Books books)
        {
            return new BooksDTO
            {
                Id = books.Id,
                Title = books.Title,
                Genre = books.Genre,
                Count = books.Count,
                Author = books.Author?.ToAuthorSimpleDTO()
            };
        }

        public static BookSimpleDTO ToBookSimpleDTO(this Books books)
        {
            return new BookSimpleDTO
            {
                Id = books.Id,
                Title = books.Title,
                Genre = books.Genre,
                Count = books.Count
            };
        }

        public static Books ToBooksFromCreateDTO(this CreateBooksDTO createBooksDTO)
        {
            return new Books
            {
                Title = createBooksDTO.Title,
                Genre = createBooksDTO.Genre,
                Count = createBooksDTO.Count,
                AuthorName = createBooksDTO.AuthorName // Добавлена инициализация AuthorName
            };
        }
    }
}