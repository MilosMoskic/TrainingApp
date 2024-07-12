using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Interfaces
{
    public interface IWodService
    {
        Wod CreateWod(Wod wod);
        List<Wod> GetAllWods();
        Wod GetRandomWod();
        Wod UpdateWod(Wod wod);
        bool DeleteWod(Wod wod);
    }
}
