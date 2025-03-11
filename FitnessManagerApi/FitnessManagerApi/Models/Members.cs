namespace FitnessManagerApi.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FitnessNumber { get; set; } // Уникален
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubscriptionExpiryDate { get; set; }
    }
}