using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Services
{
    public class WeightService : IWeightService
    {
        private readonly IWeightRepository _weightRepositoty;
        public WeightService(IWeightRepository weightRepository)
        {
            _weightRepositoty = weightRepository;
        }

        public Weight CreateWeight(Weight weight)
        {
            weight.Date = DateTime.Now;

            _weightRepositoty.CreateWeight(weight);

            return weight;
        }

        public Weight GetWeight(int id)
        {
            return _weightRepositoty.GetWeight(id);
        }

        public List<Weight> GetAllWeights()
        {
            return _weightRepositoty.GetAllWeights()
                .ToList();
        }

        public bool DeleteWeight(Weight weight)
        {
            _weightRepositoty.DeleteWeight(weight);
            return true;
        }
    }
}
