using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Domain.Models
{
    public class Weight
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal WeightAmount { get; set; }
    }
}