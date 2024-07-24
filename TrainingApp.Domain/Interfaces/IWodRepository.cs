using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface IWodRepository
    {
        Wod CreateWod(Wod wod);
        IQueryable<Wod> GetAllWods();
        Wod UpdateWod(Wod wod);
        bool DeleteWod(Wod wod);
    }
}
