using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IPracticeRepository
    {
        IQueryable<Practice> GetPractices();

        Practice GetPracticeById(int id);

        IQueryable<Practice> SearchPracticeByName(string searchText);

        Practice UpdatePractice(Practice updatedPractice);

        Practice InsertPractice(Practice practice);

        Practice DeletePractice(int id);
    }
}
