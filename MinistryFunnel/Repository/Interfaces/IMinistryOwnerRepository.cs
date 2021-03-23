using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IMinistryOwnerRepository
    {
        IQueryable<MinistryOwner> GetMinistryOwners();

        MinistryOwner GetMinistryOwnerById(int id);

        IQueryable<MinistryOwner> SearchMinistryOwnerByName(string searchText);

        MinistryOwner UpdateMinistryOwner(MinistryOwner updatedMinistryOwner);

        MinistryOwner InsertMinistryOwner(MinistryOwner ministryOwner);

        MinistryOwner DeleteMinistryOwner(int id);
    }
}
