using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Context;

namespace TrainingApp.Infastructure.Repositories
{
    public class WeightRepository : IWeightRepository
    {
        private readonly TrainingContext _trainingContext;
        public WeightRepository(TrainingContext trainingContext)
        {
            _trainingContext = trainingContext;
        }

        public Weight CreateWeight(Weight weight)
        {
            _trainingContext.Add(weight);
            _trainingContext.SaveChanges();

            return weight;
        }

        public IQueryable<Weight> GetAllWeights()
        {
            return _trainingContext.Weights.AsQueryable();
        }

        public Weight GetWeight(int id)
        {
            return _trainingContext.Weights.FirstOrDefault(w => w.Id == id);
        }

        public bool DeleteWeight(Weight weight)
        {
            _trainingContext.Weights.Remove(weight);
            _trainingContext.SaveChanges();
            return true;
        }
    }
}
