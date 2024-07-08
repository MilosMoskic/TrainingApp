using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Services
{
    public class RunningSessionService : IRunningSessionService
    {
        private readonly IRunningSessionRepository _runningSessionRepository;
        public RunningSessionService(IRunningSessionRepository runningSessionRepository)
        {
            _runningSessionRepository = runningSessionRepository;
        }

        public RunningSession CreateRunningSession(RunningSession runningSession)
        {
            decimal weightKg = 70.0m;
            runningSession.CreatedAt = DateTime.Now;

            runningSession.Cal = CalculateCalories(runningSession.Distance, runningSession.Time, weightKg);
            _runningSessionRepository.CreateRunningSession(runningSession);

            return runningSession;
        }

        public RunningSession GetRunningSession(int id) 
        {
            return _runningSessionRepository.GetRunningSession(id);
        }

        public List<RunningSession> GetAllRunningSessions()
        {
            return _runningSessionRepository.GetAllRunningSessions()
                .ToList();
        }

        public RunningSession UpdateRunningSession(RunningSession runningSession)
        {
            decimal weightKg = 70.0m;
            runningSession.ModifiedAt = DateTime.Now;

            runningSession.Cal = CalculateCalories(runningSession.Distance, runningSession.Time, weightKg);
            return _runningSessionRepository.UpdateRunningSession(runningSession);
        }

        public bool DeleteRunningSession(RunningSession runningSession)
        {
            _runningSessionRepository.DeleteRunningSession(runningSession);
            return true;
        }

        private static decimal GetMetValue(decimal speed)
        {
            if (speed < 8)
                return 6.0m; // walking
            else if (speed < 9.5m)
                return 8.3m; // jogging
            else if (speed < 11m)
                return 9.8m; // running
            else if (speed < 12.5m)
                return 11.0m; // running faster
            else if (speed < 14m)
                return 11.5m;
            else if (speed < 16m)
                return 12.8m;
            else
                return 16.0m; // sprinting
        }

        public static decimal CalculateCalories(decimal distanceKm, decimal timeMinutes, decimal weightKg)
        {
            if (timeMinutes == 0)
                throw new ArgumentException("Time cannot be zero.");

            decimal timeHours = timeMinutes / 60.0m;
            decimal speed = distanceKm / timeHours;
            decimal met = GetMetValue(speed);

            return met * weightKg * timeHours;
        }
    }
}
