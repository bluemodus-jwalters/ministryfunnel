using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface ILevelOfImportanceRepository
    {
        IQueryable<LevelOfImportance> GetLevelOfImportances();

        LevelOfImportance GetLevelOfImportanceById(int id);

        IQueryable<LevelOfImportance> SearchLevelOfImportanceByName(string searchText);

        LevelOfImportance UpdateLevelOfImportance(LevelOfImportance updatedLevelOfImportance);

        LevelOfImportance InsertLevelOfImportance(LevelOfImportance levelOfImportance);

        LevelOfImportance DeleteLevelOfImportance(int id);
    }
}
