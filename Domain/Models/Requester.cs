using Domain.Models.Enums;

namespace Domain.Models
{
    public class Requester : User
    {
        public AdmissionStatus AdmissionStatus { get; set; }
    }
}
