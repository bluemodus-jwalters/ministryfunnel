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
    public class FunnelRepository : IFunnelRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public FunnelRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Funnel> GetFunnels()
        {
            return db.Funnel;
        }

        public Funnel GetFunnelById(int id)
        {
            Funnel funnel = db.Funnel.Find(id);

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Get By Id", id.ToString(), $"Results found: {funnel != null}");

            return funnel;
        }

        public IQueryable<Funnel> SearchFunnelByName(string searchText)
        {
            var results = db.Funnel.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Funnel InsertFunnel(Funnel funnel)
        {
            try
            {
                funnel.CreatedDateTime = DateTime.Now;
                db.Funnel.Add(funnel);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Create", funnel.ToString());

                return funnel;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Create", funnel.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Funnel UpdateFunnel(Funnel updatedFunnel)
        {
            var currentFunnel = db.Funnel.Find(updatedFunnel.Id);
            if (currentFunnel == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Update", string.Empty, $"Funnel {updatedFunnel.Id} not found to update.");
                return null;
            }

            if (updatedFunnel.Name != null)
            {
                currentFunnel.Name = updatedFunnel.Name;
            }

            currentFunnel.Archived = updatedFunnel.Archived;
            currentFunnel.ModifiedDateTime = DateTime.Now;

            db.Entry(currentFunnel).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Update", currentFunnel.ToString(), "Error updating funnel: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Update", currentFunnel.ToString(), "Error updating funnel: " + e.Message);
                return null;
            }

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Update", currentFunnel.ToString());
            return currentFunnel;
        }

        public Funnel DeleteFunnel(int id)
        {
            Funnel funnel = db.Funnel.Find(id);
            if (funnel == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Delete", string.Empty, $"Funnel {id} not found to delete.");
                return null;
            }

            try
            {
                db.Funnel.Remove(funnel);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Delete", funnel.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FunnelRepository", "Funnel", "Delete", funnel.ToString(), "Error deleting funnel: " + e.Message);
                return null;
            }

            return funnel;
        }
    }
}