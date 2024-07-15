using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface INutritionRepository
    {
        Nutrition AddNutritionStats(Nutrition nutrition);
        Nutrition GetNutrition(int id);
        IQueryable<Nutrition> GetAllNutrition();
    }
}
