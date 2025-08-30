using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BloodRequestId { get; set; }
        public required BloodRequest BloodRequest { get; set; }
        public int DonatorId { get; set; }
        public required Donator Donator { get; set; }
    }
}
