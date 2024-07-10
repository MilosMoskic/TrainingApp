using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Interfaces
{
    public interface IStreakService
    {
        List<EatingStreak> GetEatingStreak();
        int CalculateCurrentStreak();
        bool CanIncreaseStreak();
        void IncreaseStreak();
    }
}
