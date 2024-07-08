using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Context;

namespace TrainingApp.Infastructure.Repositories
{
    public class RunningSessionRepository : IRunningSessionRepository
    {
        private readonly TrainingContext _trainingContext;
        public RunningSessionRepository(TrainingContext trainingContext)
        {
            _trainingContext = trainingContext;
        }

        public RunningSession CreateRunningSession(RunningSession runningSession)
        {
            _trainingContext.Add(runningSession);
            _trainingContext.SaveChanges();

            return runningSession;
        }

        public IQueryable<RunningSession> GetAllRunningSessions()
        {
            return _trainingContext.RunningSessions.AsQueryable();
        }

        public RunningSession GetRunningSession(int id)
        {
            return _trainingContext.RunningSessions.FirstOrDefault(w => w.Id == id);
        }

        public RunningSession UpdateRunningSession(RunningSession runningSession)
        {
            _trainingContext.Update(runningSession);
            _trainingContext.SaveChanges();

            return runningSession;
        }

        public bool DeleteRunningSession(RunningSession runningSession)
        {
            _trainingContext.RunningSessions.Remove(runningSession);
            _trainingContext.SaveChanges();
            return true;
        }
    }
}
