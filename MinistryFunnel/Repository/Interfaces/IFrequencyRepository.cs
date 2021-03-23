using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IFrequencyRepository
    {
        IQueryable<Frequency> GetFrequencys();

        Frequency GetFrequencyById(int id);

        IQueryable<Frequency> SearchFrequencyByName(string searchText);

        Frequency UpdateFrequency(Frequency updatedFrequency);

        Frequency InsertFrequency(Frequency frequency);

        Frequency DeleteFrequency(int id);
    }
}
