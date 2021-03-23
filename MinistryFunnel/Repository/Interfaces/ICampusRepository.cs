using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface ICampusRepository
    {
        IQueryable<Campus> GetCampuss();

        Campus GetCampusById(int id);

        IQueryable<Campus> SearchCampusByName(string searchText);

        Campus UpdateCampus(Campus updatedCampus);

        Campus InsertCampus(Campus campus);

        Campus DeleteCampus(int id);
    }
}
