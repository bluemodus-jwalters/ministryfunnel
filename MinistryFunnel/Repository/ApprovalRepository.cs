using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MinistryFunnel.Repository
{
    public class ApprovalRepository : IApprovalRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public ApprovalRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Approval> GetApprovals()
        {
            return db.Approval;
        }

        public Approval GetApprovalById(int id)
        {
            Approval approval = db.Approval.Find(id);

            _loggerService.CreateLog("Jordan", "Approval", "Get By Id", id.ToString(), $"Results found: {approval != null}");

            return approval;
        }

        public IQueryable<Approval> SearchApprovalByName(string searchText)
        {
            var results = db.Approval.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog("Jordan", "Approval", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Approval InsertApproval(Approval approval)
        {
            try
            {
                approval.CreatedDateTime = DateTime.Now;
                db.Approval.Add(approval);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "Approval", "Create", approval.ToString());

                return approval;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Create", approval.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Approval UpdateApproval(Approval updatedApproval)
        {
            var currentApproval = db.Approval.Find(updatedApproval.Id);
            if (currentApproval == null)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Update", string.Empty, $"Approval {updatedApproval.Id} not found to update.");
                return null;
            }

            if (updatedApproval.Name != null)
            {
                currentApproval.Name = updatedApproval.Name;
            }

            currentApproval.Archived = updatedApproval.Archived;
            currentApproval.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentApproval).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Update", currentApproval.ToString(), "Error updating approval: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Update", currentApproval.ToString(), "Error updating approval: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "Approval", "Update", currentApproval.ToString());
            return currentApproval;
        }

        public Approval DeleteApproval(int id)
        {
            Approval approval = db.Approval.Find(id);
            if (approval == null)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Delete", string.Empty, $"Approval {id} not found to delete.");
                return null;
            }

            try
            {
                db.Approval.Remove(approval);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "Approval", "Delete", approval.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Approval", "Delete", approval.ToString(), "Error deleting approval: " + e.Message);
                return null;
            }

            return approval;
        }
    }
}