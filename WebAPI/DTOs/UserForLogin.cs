using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOs
{
    public class UserForLogin
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
