namespace Domain.Models
{
    public class Requester : User
    {
        public required string OrganizationName { get; set; }
        public required Status CurrentStatus { get; set; }
    }
}
