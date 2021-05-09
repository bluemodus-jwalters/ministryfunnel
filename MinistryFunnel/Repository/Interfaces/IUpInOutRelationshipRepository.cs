using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IUpInOutRelatinshipRepository
    {
        IQueryable<UpInOutRelationship> GetUpInOutRelationships();

        IQueryable<UpInOutRelationship> GetUpInOutRelationshipsByMinistryId(int ministryId);
    }
}
