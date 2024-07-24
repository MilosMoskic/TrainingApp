using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Interfaces
{
    public interface INutritionService
    {
        void CalculateAndAddNutritionStats(Nutrition nutrition);
        Nutrition GetNutritionStats(int id);
        List<Nutrition> GetAllNutritions();
    }
}
