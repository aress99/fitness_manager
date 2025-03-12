using System.ComponentModel.DataAnnotations;

namespace FitnessManagerApi.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string FitnessNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public double Height { get; set; } 
    }
}