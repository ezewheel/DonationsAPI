using Domain.Enums;

namespace Application.DTOs
{
    public class UserForView
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required Role Role { get; set; }
        public required bool AccountStatus { get; set; }
        public required string UserType { get; set; }
    }
}
