using Microsoft.EntityFrameworkCore;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Context;

namespace TrainingApp.Infastructure.Repositories
{
    public class WodRepository : IWodRepository
    {
        private readonly TrainingContext _trainingContext;
        public WodRepository(TrainingContext trainingContext)
        {
            _trainingContext = trainingContext;
        }

        public Wod CreateWod(Wod wod)
        {
            _trainingContext.Add(wod);
            _trainingContext.SaveChanges();

            return wod;
        }

        public IQueryable<Wod> GetAllWods()
        {
            return _trainingContext.Wods.AsQueryable();
        }

        public Wod GetWod(int id)
        {
            return _trainingContext.Wods.FirstOrDefault(w => w.Id == id);
        }

        public Wod UpdateWod(Wod wod)
        {
            _trainingContext.Update(wod);
            _trainingContext.SaveChanges();

            return wod;
        }

        public bool DeleteWod(Wod wod)
        {
            _trainingContext.Wods.Remove(wod);
            _trainingContext.SaveChanges();
            return true;
        }
    }
}
