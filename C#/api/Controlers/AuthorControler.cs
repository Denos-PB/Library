using api.DTO.Author;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controlers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }
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
            await ExecuteAsync(_authorService.GetAsync, "Error retrieving authors");
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id) =>
            await ExecuteAsync(
                () => _authorService.GetAsync(id),
                $"Author with ID '{id}' not found");
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            try
            {
                await _authorService.CreateAsync(author);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating author", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Author author)
        {
            try
            {
                await _authorService.UpdateAsync(id, author);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating author", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _authorService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting author", error = ex.Message });
            }
        }      
    }
}