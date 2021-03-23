using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IResourceInvolvementRepository
    {
        IQueryable<ResourceInvolvement> GetResourceInvolvements();

        ResourceInvolvement GetResourceInvolvementById(int id);

        IQueryable<ResourceInvolvement> SearchResourceInvolvementByName(string searchText);

        ResourceInvolvement UpdateResourceInvolvement(ResourceInvolvement updatedResourceInvolvement);

        ResourceInvolvement InsertResourceInvolvement(ResourceInvolvement resourceInvolvement);

        ResourceInvolvement DeleteResourceInvolvement(int id);
    }
}
