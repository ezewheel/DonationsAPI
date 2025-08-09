using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class DonatorForCreation
    {
        [Required]
        [MaxLength(30)]
        public required string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Password { get; set; }
        [Required]
        [MaxLength(320)]
        public required string Email { get; set; }
    }
}
