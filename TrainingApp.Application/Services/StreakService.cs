using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Services
{
    public class StreakService : IStreakService
    {
        private readonly IStreakRepository _streakRepository;
        public StreakService(IStreakRepository streakRepository)
        {
            _streakRepository = streakRepository;
        }

        public List<EatingStreak> GetEatingStreak()
        {
            return _streakRepository.GetAllEatingStreaks().OrderBy(e => e.EatingDate).ToList();
        }

        public int CalculateCurrentStreak()
        {
            var eatings = GetEatingStreak();

            if (!eatings.Any())
            {
                return 0;
            }

            int streak = 0;
            DateTime? lastDate = null;

            foreach (var eating in eatings)
            {
                if (lastDate == null || eating.EatingDate == lastDate.Value.AddDays(1))
                {
                    streak++;
                    lastDate = eating.EatingDate;
                }
                else if (eating.EatingDate > lastDate.Value.AddDays(1))
                {
                    streak = 1;
                    lastDate = eating.EatingDate;
                }
            }

            // Reset streak if last entry was not yesterday
            if (lastDate.Value.Date < DateTime.Today.AddDays(-1))
            {
                streak = 0;
            }

            return streak;
        }

        public bool CanIncreaseStreak()
        {
            var lastEating = _streakRepository.GetAllEatingStreaks().OrderByDescending(e => e.EatingDate).FirstOrDefault();
            if (lastEating == null)
            {
                return true;
            }

            return lastEating.EatingDate.Date < DateTime.Today;
        }

        public void IncreaseStreak()
        {
            if (CanIncreaseStreak())
            {
                // Reset streak if last entry was not yesterday
                var lastEating = _streakRepository.GetAllEatingStreaks().OrderByDescending(e => e.EatingDate).FirstOrDefault();
                if (lastEating != null && lastEating.EatingDate.Date < DateTime.Today.AddDays(-1))
                {
                    // Add logic here to handle streak reset if necessary
                }

                var newEatingStreak = new EatingStreak { EatingDate = DateTime.Today };
                _streakRepository.CreateEatingStreak(newEatingStreak);
            }
            else
            {
                throw new InvalidOperationException("You can only increase the streak once per day.");
            }
        }
    }
}
