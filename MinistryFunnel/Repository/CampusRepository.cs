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
    public class CampusRepository : ICampusRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public CampusRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Campus> GetCampuss()
        {
            return db.Campus;
        }

        public Campus GetCampusById(int id)
        {
            Campus campus = db.Campus.Find(id);

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Get By Id", id.ToString(), $"Results found: {campus != null}");

            return campus;
        }

        public IQueryable<Campus> SearchCampusByName(string searchText)
        {
            var results = db.Campus.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Campus InsertCampus(Campus campus)
        {
            try
            {
                campus.CreatedDateTime = DateTime.Now;
                db.Campus.Add(campus);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Create", campus.ToString());

                return campus;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Create", campus.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Campus UpdateCampus(Campus updatedCampus)
        {
            var currentCampus = db.Campus.Find(updatedCampus.Id);
            if (currentCampus == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Update", string.Empty, $"Campus {updatedCampus.Id} not found to update.");
                return null;
            }

            if (updatedCampus.Name != null)
            {
                currentCampus.Name = updatedCampus.Name;
            }

            currentCampus.Archived = updatedCampus.Archived;
            currentCampus.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentCampus).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Update", currentCampus.ToString(), "Error updating campus: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Update", currentCampus.ToString(), "Error updating campus: " + e.Message);
                return null;
            }

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Update", currentCampus.ToString());
            return currentCampus;
        }

        public Campus DeleteCampus(int id)
        {
            Campus campus = db.Campus.Find(id);
            if (campus == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Delete", string.Empty, $"Campus {id} not found to delete.");
                return null;
            }

            try
            {
                db.Campus.Remove(campus);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Delete", campus.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "CampusRepository", "Campus", "Delete", campus.ToString(), "Error deleting campus: " + e.Message);
                return null;
            }

            return campus;
        }
    }
}