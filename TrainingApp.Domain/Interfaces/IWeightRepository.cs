using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface IWeightRepository
    {
        Weight CreateWeight(Weight weight);
        IQueryable<Weight> GetAllWeights();
        Weight GetWeight(int id);
        bool DeleteWeight(Weight weight);
    }
}
