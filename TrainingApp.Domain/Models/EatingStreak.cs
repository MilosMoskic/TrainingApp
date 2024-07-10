using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class EatingStreak
    {
        [Key]
        public int Id { get; set; }
        public DateTime EatingDate { get; set; }
    }
}
