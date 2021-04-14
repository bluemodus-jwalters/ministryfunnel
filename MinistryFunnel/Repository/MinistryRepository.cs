using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Repository
{
    public class MinistryRepository : IMinistryRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public MinistryRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Ministry> GetMinistries()
        {
            return db.Ministry;
        }

        public Ministry GetMinistryById(int id)
        {
            Ministry ministry = db.Ministry.Find(id);

            _loggerService.CreateLog("Jordan", "Ministry", "Get By Id", id.ToString(), $"Results found: {ministry != null}");

            return ministry;
        }

        public IQueryable<Ministry> SearchMinistryByName(string searchText)
        {
            var results = db.Ministry.Where(x => x.Event.Contains(searchText));

            _loggerService.CreateLog("Jordan", "Ministry", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Ministry InsertMinistry(Ministry ministry)
        {
            try
            {
                ministry.CreatedDateTime = DateTime.Now;
                db.Ministry.Add(ministry);
                db.SaveChanges();

                if (ministry.UpInOutIds.Length > 0)
                {
                    InsertUpInOutRelationships(ministry.Id, ministry.UpInOutIds);
                }

                if (ministry.ResourceInvolvementIds.Length > 0)
                {
                    InsertResourceInvolvementIds(ministry.Id, ministry.ResourceInvolvementIds);
                }
                

                _loggerService.CreateLog("Jordan", "Ministry", "Create", ministry.ToString());

                return ministry;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Create", ministry.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        private void InsertUpInOutRelationships(int ministryId, int[] upInOuts)
        {
            foreach (int upInOut in upInOuts)
            {
                db.UpInOutRelaionship.Add(new UpInOutRelationship { MinistryId = ministryId, UpInOutId = upInOut });
            }

            db.SaveChanges();

            _loggerService.CreateLog("Jordan", "Ministry", "Create Up In Out Relationships", upInOuts.ToString());
        }

        private void InsertResourceInvolvementIds(int ministryId, int[] resourceInvolvementIds)
        {
            foreach (int resourceInvovlementId in resourceInvolvementIds)
            {
                db.ResourceInvolvementRelaionship.Add(new ResourceInvolvementRelationship { MinistryId = ministryId,  ResourceInvolvementId = resourceInvovlementId });
            }

            db.SaveChanges();

            _loggerService.CreateLog("Jordan", "Ministry", "Create Resource Involvement Relationships", resourceInvolvementIds.ToString());
        }

        public Ministry UpdateMinistry(Ministry updatedMinistry)
        {
            //Assuming all fields are filled in that are supposed to be for this except created time
            var currentMinistry = db.Ministry.Find(updatedMinistry.Id);
            if (currentMinistry == null)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Update", string.Empty, $"Ministry {updatedMinistry.Id} not found to update.");
                return null;
            }

            currentMinistry.MinistryOwnerId = updatedMinistry.MinistryOwnerId;
            currentMinistry.Event = updatedMinistry.Event;
            currentMinistry.Purpose = updatedMinistry.Purpose;
            currentMinistry.DesiredOutcome = updatedMinistry.DesiredOutcome;
            currentMinistry.PracticeId = updatedMinistry.PracticeId;
            currentMinistry.FunnelId = updatedMinistry.FunnelId;
            currentMinistry.LocationId = updatedMinistry.LocationId;
            currentMinistry.StartDate = updatedMinistry.StartDate;
            currentMinistry.EndDate = updatedMinistry.EndDate;
            currentMinistry.FrequencyId = updatedMinistry.FrequencyId;
            currentMinistry.KidCare = updatedMinistry.KidCare;
            currentMinistry.LevelOfImportanceId = updatedMinistry.LevelOfImportanceId;
            currentMinistry.ApprovalId = updatedMinistry.ApprovalId;
            currentMinistry.Comments = updatedMinistry.Comments;

            currentMinistry.Archived = updatedMinistry.Archived;
            currentMinistry.ModifiedDateTime = DateTime.Now;

            db.Entry(currentMinistry).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Update", currentMinistry.ToString(), "Error updating ministry: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Update", currentMinistry.ToString(), "Error updating ministry: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "Ministry", "Update", currentMinistry.ToString());
            return currentMinistry;
        }

        public Ministry DeleteMinistry(int id)
        {
            Ministry ministry = db.Ministry.Find(id);
            if (ministry == null)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Delete", string.Empty, $"Ministry {id} not found to delete.");
                return null;
            }

            try
            {
                //TODO: have to remove the foreign key restraints before deleting the record
                db.Ministry.Remove(ministry);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "Ministry", "Delete", ministry.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "Ministry", "Delete", ministry.ToString(), "Error deleting ministry: " + e.Message);
                return null;
            }

            return ministry;
        }
    }
}