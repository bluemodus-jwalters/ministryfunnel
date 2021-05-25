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
    public class UpInOutRepository : IUpInOutRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public UpInOutRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<UpInOut> GetUpInOuts()
        {
            return db.UpInOut;
        }

        public UpInOut GetUpInOutById(int id)
        {
            UpInOut upInOut = db.UpInOut.Find(id);

            _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Get By Id", id.ToString(), $"Results found: {upInOut != null}");

            return upInOut;
        }

        public IQueryable<UpInOut> SearchUpInOutByName(string searchText)
        {
            var results = db.UpInOut.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public UpInOut InsertUpInOut(UpInOut upInOut)
        {
            try
            {
                upInOut.CreatedDateTime = DateTime.Now;
                db.UpInOut.Add(upInOut);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Create", upInOut.ToString());

                return upInOut;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Create", upInOut.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public UpInOut UpdateUpInOut(UpInOut updatedUpInOut)
        {
            var currentUpInOut = db.UpInOut.Find(updatedUpInOut.Id);
            if (currentUpInOut == null)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Update", string.Empty, $"UpInOut {updatedUpInOut.Id} not found to update.");
                return null;
            }

            if (updatedUpInOut.Name != null)
            {
                currentUpInOut.Name = updatedUpInOut.Name;
            }

            currentUpInOut.Archived = updatedUpInOut.Archived;
            currentUpInOut.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentUpInOut).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Update", currentUpInOut.ToString(), "Error updating upInOut: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Update", currentUpInOut.ToString(), "Error updating upInOut: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Update", currentUpInOut.ToString());
            return currentUpInOut;
        }

        public UpInOut DeleteUpInOut(int id)
        {
            UpInOut upInOut = db.UpInOut.Find(id);
            if (upInOut == null)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Delete", string.Empty, $"UpInOut {id} not found to delete.");
                return null;
            }

            try
            {
                db.UpInOut.Remove(upInOut);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Delete", upInOut.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "UpInOutRepository", "UpInOut", "Delete", upInOut.ToString(), "Error deleting upInOut: " + e.Message);
                return null;
            }

            return upInOut;
        }
    }
}