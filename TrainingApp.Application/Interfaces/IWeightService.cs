using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Interfaces
{
    public interface IWeightService
    {
        Weight CreateWeight(Weight weight);
        List<Weight> GetAllWeights();
        Weight GetWeight(int id);
        bool DeleteWeight(Weight weight);
    }
}
