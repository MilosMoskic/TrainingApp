using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Context;

namespace TrainingApp.Infastructure.Repositories
{
    public class NutritionRepository : INutritionRepository
    {
        private readonly TrainingContext _trainingContext;
        public NutritionRepository(TrainingContext trainingContext)
        {
            _trainingContext = trainingContext;
        }

        public Nutrition AddNutritionStats(Nutrition nutrition)
        {
            _trainingContext.Add(nutrition);
            _trainingContext.SaveChanges();

            return nutrition;
        }

        public Nutrition GetNutrition(int id)
        {
            return _trainingContext.Nutritions.Find(id);
        }

        public IQueryable<Nutrition> GetAllNutrition()
        {
            return _trainingContext.Nutritions.AsQueryable();
        }
    }
}
