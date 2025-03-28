using System.ComponentModel.DataAnnotations;

namespace api.DTO.Account
{
    public class RegisterDTO
    {
        [Required]
        public required string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}