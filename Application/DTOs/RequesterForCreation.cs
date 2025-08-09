using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RequesterForCreation : DonatorForCreation
    {
        [Required]
        [MaxLength(30)]
        public required string OrganizationName { get; set; }
    }
}
