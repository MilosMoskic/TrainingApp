using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface IRunningSessionRepository
    {
        RunningSession CreateRunningSession(RunningSession runningSession);
        IQueryable<RunningSession> GetAllRunningSessions();
        RunningSession GetRunningSession(int id);
        RunningSession UpdateRunningSession(RunningSession runningSession);
        bool DeleteRunningSession(RunningSession runningSession);
    }
}
