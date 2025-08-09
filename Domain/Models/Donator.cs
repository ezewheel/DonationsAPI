namespace Domain.Models
{
    public class Donator : User
    {
        public int DonationCount { get; set; } = 0;
        public string? BloodType { get; set; }
    }
}
