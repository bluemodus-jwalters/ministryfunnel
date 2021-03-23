using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IUpInOutRepository
    {
        IQueryable<UpInOut> GetUpInOuts();

        UpInOut GetUpInOutById(int id);

        IQueryable<UpInOut> SearchUpInOutByName(string searchText);

        UpInOut UpdateUpInOut(UpInOut updatedUpInOut);

        UpInOut InsertUpInOut(UpInOut upInOut);

        UpInOut DeleteUpInOut(int id);
    }
}
