using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class Wod
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Day is required.")]
        [StringLength(50, ErrorMessage = "Day cannot be longer than 50 characters.")]
        public string Day { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type cannot be longer than 50 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "WOD description is required.")]
        [StringLength(500, ErrorMessage = "WOD description cannot be longer than 500 characters.")]
        public string WOD { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reps must be a positive number.")]
        public int? Reps { get; set; }
        [StringLength(50, ErrorMessage = "Time cannot be longer than 50 characters.")]
        public string? Time { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
