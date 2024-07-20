using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class Nutrition
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Activity Level is required.")]
        [Range(1, 5, ErrorMessage = "Activity Level must be between 1 and 5.")]
        public int ActivityLevel { get; set; }
        public decimal? BMR { get; set; }
        public decimal? TDEE { get; set; }
        public decimal? Calories { get; set; }
        public decimal? Carbs { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Fat { get; set; }
    }
}
