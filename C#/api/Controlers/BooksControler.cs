using api.DTO.Books;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controlers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService) => 
            _booksService = booksService;

        private async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> action, string errorMessage)
        {
            try
            {
                var result = await action();
                if (result == null || (result is IEnumerable<Books> books && !books.Any()))
                    return NotFound(errorMessage);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = errorMessage, error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await ExecuteAsync(_booksService.GetAsync, "Error retrieving books");

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id) =>
            await ExecuteAsync(
                () => _booksService.GetAsync(id), 
                $"Book with ID '{id}' not found");

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetByGenre([FromRoute] string genre) =>
            await ExecuteAsync(
                () => _booksService.GetByGenreAsync(genre), 
                $"No books found in genre '{genre}'");

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string title) =>
            await ExecuteAsync(
                () => _booksService.GetByTitleAsync(title), 
                $"No books found with title '{title}'");

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBooksDTO booksDTO)
        {
            try
            {
                var book = new Books
                {
                    Title = booksDTO.Title,
                    Genre = booksDTO.Genre,
                    Count = booksDTO.Count,
                    AuthorName = booksDTO.AuthorName
                };

                await _booksService.CreateAsync(book);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating book", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateBooksRequestDTO updateDTO)
        {
            try
            {
                var book = await _booksService.GetAsync(id) 
                    ?? throw new KeyNotFoundException($"Book with ID '{id}' not found");

                book.Title = updateDTO.Title;
                book.Genre = updateDTO.Genre;
                book.Count = updateDTO.Count;
                book.AuthorName = updateDTO.AuthorName;

                await _booksService.UpdateAsync(id, book);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating book", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var book = await _booksService.GetAsync(id) 
                    ?? throw new KeyNotFoundException($"Book with ID '{id}' not found");

                await _booksService.RemoveAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting book", error = ex.Message });
            }
        }
    }
}