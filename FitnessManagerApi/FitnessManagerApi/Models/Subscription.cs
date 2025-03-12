using System.ComponentModel.DataAnnotations;

namespace FitnessManagerApi.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Price { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public Member? Member { get; set; } 
    }
}