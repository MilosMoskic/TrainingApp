using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class Nutrition
    {
        [Key]
        public int Id { get; set; }
        public string Gender { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int Age { get; set; }
        public int ActivityLevel { get; set; }
        public decimal? BMR { get; set; }
        public decimal? TDEE { get; set; }
        public decimal? Calories { get; set; }
        public decimal? Carbs { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Fat { get; set; }
    }
}
