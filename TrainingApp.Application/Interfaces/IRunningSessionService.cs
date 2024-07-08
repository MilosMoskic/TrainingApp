using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Interfaces
{
    public interface IRunningSessionService
    {
        RunningSession CreateRunningSession(RunningSession runningSession);
        RunningSession GetRunningSession(int id);
        List<RunningSession> GetAllRunningSessions();
        RunningSession UpdateRunningSession(RunningSession runningSession);
        bool DeleteRunningSession(RunningSession runningSession);
    }
}
