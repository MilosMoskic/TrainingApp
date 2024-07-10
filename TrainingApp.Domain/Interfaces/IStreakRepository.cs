using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface IStreakRepository
    {
        IQueryable<EatingStreak> GetAllEatingStreaks();
        void CreateEatingStreak(EatingStreak eatingStreak);
    }
}
