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
    public class ResourceInvolvementRepository : IResourceInvolvementRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public ResourceInvolvementRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<ResourceInvolvement> GetResourceInvolvements()
        {
            return db.ResourceInvolvement;
        }

        public ResourceInvolvement GetResourceInvolvementById(int id)
        {
            ResourceInvolvement resourceInvolvement = db.ResourceInvolvement.Find(id);

            _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Get By Id", id.ToString(), $"Results found: {resourceInvolvement != null}");

            return resourceInvolvement;
        }

        public IQueryable<ResourceInvolvement> SearchResourceInvolvementByName(string searchText)
        {
            var results = db.ResourceInvolvement.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public ResourceInvolvement InsertResourceInvolvement(ResourceInvolvement resourceInvolvement)
        {
            try
            {
                resourceInvolvement.CreatedDateTime = DateTime.Now;
                db.ResourceInvolvement.Add(resourceInvolvement);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Create", resourceInvolvement.ToString());

                return resourceInvolvement;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Create", resourceInvolvement.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public ResourceInvolvement UpdateResourceInvolvement(ResourceInvolvement updatedResourceInvolvement)
        {
            var currentResourceInvolvement = db.ResourceInvolvement.Find(updatedResourceInvolvement.Id);
            if (currentResourceInvolvement == null)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Update", string.Empty, $"ResourceInvolvement {updatedResourceInvolvement.Id} not found to update.");
                return null;
            }

            if (updatedResourceInvolvement.Name != null)
            {
                currentResourceInvolvement.Name = updatedResourceInvolvement.Name;
            }

            currentResourceInvolvement.Archived = updatedResourceInvolvement.Archived;
            currentResourceInvolvement.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentResourceInvolvement).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Update", currentResourceInvolvement.ToString(), "Error updating resourceInvolvement: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Update", currentResourceInvolvement.ToString(), "Error updating resourceInvolvement: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Update", currentResourceInvolvement.ToString());
            return currentResourceInvolvement;
        }

        public ResourceInvolvement DeleteResourceInvolvement(int id)
        {
            ResourceInvolvement resourceInvolvement = db.ResourceInvolvement.Find(id);
            if (resourceInvolvement == null)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Delete", string.Empty, $"ResourceInvolvement {id} not found to delete.");
                return null;
            }

            try
            {
                db.ResourceInvolvement.Remove(resourceInvolvement);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Delete", resourceInvolvement.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "ResourceInvolvement", "Delete", resourceInvolvement.ToString(), "Error deleting resourceInvolvement: " + e.Message);
                return null;
            }

            return resourceInvolvement;
        }
    }
}