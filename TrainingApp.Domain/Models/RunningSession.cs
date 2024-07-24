using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class RunningSession
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Time is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Time must be greater than 0.")]
        public decimal Time { get; set; }

        [Required(ErrorMessage = "Distance is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Distance must be greater than 0.")]
        public decimal Distance { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { get; set; }

        public decimal? Cal { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}