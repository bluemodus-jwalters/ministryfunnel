using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IMinistryRepository
    {
        IQueryable<Ministry> GetMinistries();

        Ministry GetMinistryById(int id);

        IQueryable<Ministry> SearchMinistryByName(string searchText);

        Ministry UpdateMinistry(Ministry updatedMinistry);

        Ministry InsertMinistry(Ministry ministry);

        Ministry DeleteMinistry(int id);

        IQueryable<Ministry> GetDashboardList();

        IQueryable<Ministry> GetCalendarList();
    }
}
