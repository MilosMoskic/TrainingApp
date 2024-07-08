using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class RunningSession
    {
        [Key]
        public int Id { get; set; }
        public decimal Time { get; set; }
        public decimal Distance { get; set; }
        public string Location { get; set; }
        public decimal? Cal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
