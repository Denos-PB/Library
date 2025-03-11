using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;


namespace api.Controlers
{
    [Route("api/readers")]
    [ApiController]
    public class ReaderControler : ControllerBase
    {
        private readonly ReaderService _readerService;

        public ReaderControler(ReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var readers = await _readerService.GetAsync();
                return Ok(readers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving readers", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReader([FromBody] Reader reader)
        {
            try
            {
                await _readerService.CreateReaderAsync(reader);
                return Ok(reader);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating reader", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReader([FromRoute] string id, [FromBody] Reader reader)
        {
            try
            {
                await _readerService.UpdateReaderAsync(id, reader);
                return Ok(reader);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating reader", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader([FromRoute] string id)
        {
            try
            {
                await _readerService.RemoveReaderAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting reader", error = ex.Message });
            }
        }

        [HttpPost("{readerId}/books/{bookId}/comments")]
        public async Task<IActionResult> AddComment(
            [FromRoute] string readerId,
            [FromRoute] string bookId,
            [FromBody] Coment comment)
        {
            try
            {
                await _readerService.CreateComentAsync(readerId, bookId, comment);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating comment", error = ex.Message });
            }
        }
    }
}