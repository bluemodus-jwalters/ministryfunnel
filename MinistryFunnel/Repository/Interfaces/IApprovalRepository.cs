using MinistryFunnel.Models;
using System.Linq;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IApprovalRepository
    {
        IQueryable<Approval> GetApprovals();

        Approval GetApprovalById(int id);

        IQueryable<Approval> SearchApprovalByName(string searchText);

        Approval UpdateApproval(Approval updatedApproval);

        Approval InsertApproval(Approval approval);

        Approval DeleteApproval(int id);
    }
}
