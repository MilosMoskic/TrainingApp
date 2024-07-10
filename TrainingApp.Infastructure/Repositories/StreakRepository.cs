using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Context;

namespace TrainingApp.Infastructure.Repositories
{
    public class StreakRepository : IStreakRepository
    {
        private readonly TrainingContext _trainingContext;
        public StreakRepository(TrainingContext trainingContext)
        {
            _trainingContext = trainingContext;
        }

        public IQueryable<EatingStreak> GetAllEatingStreaks()
        {
            return _trainingContext.EatingStreaks.AsQueryable();
        }

        public void CreateEatingStreak(EatingStreak eatingStreak)
        {
            _trainingContext.EatingStreaks.Add(eatingStreak);
            _trainingContext.SaveChanges();
        }
    }
}
