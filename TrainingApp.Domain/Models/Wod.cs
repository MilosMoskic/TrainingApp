using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class Wod
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public string WOD { get; set; }
        public int? Reps { get; set; }
        public string? Time { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
