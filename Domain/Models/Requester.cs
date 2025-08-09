using Domain.Enums;

namespace Domain.Models
{
    public class Requester : User
    {
        public required string OrganizationName { get; set; }
        public Status CurrentStatus { get; set; } = Status.Pending;
    }
}
