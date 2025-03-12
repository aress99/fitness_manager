using System.ComponentModel.DataAnnotations;

namespace FitnessManagerApi.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime WorkoutDate { get; set; }

        public int DurationMinutes { get; set; }

        public double CaloriesBurned { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        public Member? Member { get; set; }
    }
}