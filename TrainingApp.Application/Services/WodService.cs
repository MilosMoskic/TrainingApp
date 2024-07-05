using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;    

namespace TrainingApp.Aplication.Services
{
    public class WodService : IWodService
    {
        private readonly IWodRepository _wodRepository;
        public WodService(IWodRepository wodRepository)
        {
            _wodRepository = wodRepository;
        }

        public Wod CreateWod(Wod wod)
        {
            wod.CreatedAt = DateTime.Now;

            _wodRepository.CreateWod(wod);

            return wod;
        }

        public List<Wod> GetAllWods()   
        {
            return _wodRepository.GetAllWods()
                .ToList();
        }

        public Wod UpdateWod(Wod wod)
        {
            wod.ModifiedAt = DateTime.Now;
            return _wodRepository.UpdateWod(wod);
        }

        public bool DeleteWod(Wod wod)
        {
            _wodRepository.DeleteWod(wod);
            return true;
        }
    }
}
