using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Repository
{
    public class MinistryOwnerRepository : IMinistryOwnerRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public MinistryOwnerRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<MinistryOwner> GetMinistryOwners()
        {
            return db.MinistryOwner;
        }

        public MinistryOwner GetMinistryOwnerById(int id)
        {
            MinistryOwner ministryOwner = db.MinistryOwner.Find(id);

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Get By Id", id.ToString(), $"Results found: {ministryOwner != null}");

            return ministryOwner;
        }

        public IQueryable<MinistryOwner> SearchMinistryOwnerByName(string searchText)
        {
            var results = db.MinistryOwner.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public MinistryOwner InsertMinistryOwner(MinistryOwner ministryOwner)
        {
            try
            {
                ministryOwner.CreatedDateTime = DateTime.Now;
                db.MinistryOwner.Add(ministryOwner);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Create", ministryOwner.ToString());

                return ministryOwner;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Create", ministryOwner.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public MinistryOwner UpdateMinistryOwner(MinistryOwner updatedMinistryOwner)
        {
            var currentMinistryOwner = db.MinistryOwner.Find(updatedMinistryOwner.Id);
            if (currentMinistryOwner == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Update", string.Empty, $"MinistryOwner {updatedMinistryOwner.Id} not found to update.");
                return null;
            }

            if (updatedMinistryOwner.Name != null)
            {
                currentMinistryOwner.Name = updatedMinistryOwner.Name;
            }

            currentMinistryOwner.Archived = updatedMinistryOwner.Archived;
            currentMinistryOwner.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentMinistryOwner).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Update", currentMinistryOwner.ToString(), "Error updating ministryOwner: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Update", currentMinistryOwner.ToString(), "Error updating ministryOwner: " + e.Message);
                return null;
            }

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Update", currentMinistryOwner.ToString());
            return currentMinistryOwner;
        }

        public MinistryOwner DeleteMinistryOwner(int id)
        {
            MinistryOwner ministryOwner = db.MinistryOwner.Find(id);
            if (ministryOwner == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Delete", string.Empty, $"MinistryOwner {id} not found to delete.");
                return null;
            }

            try
            {
                db.MinistryOwner.Remove(ministryOwner);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Delete", ministryOwner.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryOwnerRepository", "MinistryOwner", "Delete", ministryOwner.ToString(), "Error deleting ministryOwner: " + e.Message);
                return null;
            }

            return ministryOwner;
        }
    }
}