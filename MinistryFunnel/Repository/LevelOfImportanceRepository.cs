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
    public class LevelOfImportanceRepository : ILevelOfImportanceRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public LevelOfImportanceRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<LevelOfImportance> GetLevelOfImportances()
        {
            return db.LevelOfImportance;
        }

        public LevelOfImportance GetLevelOfImportanceById(int id)
        {
            LevelOfImportance levelOfImportance = db.LevelOfImportance.Find(id);

            _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Get By Id", id.ToString(), $"Results found: {levelOfImportance != null}");

            return levelOfImportance;
        }

        public IQueryable<LevelOfImportance> SearchLevelOfImportanceByName(string searchText)
        {
            var results = db.LevelOfImportance.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public LevelOfImportance InsertLevelOfImportance(LevelOfImportance levelOfImportance)
        {
            try
            {
                levelOfImportance.CreatedDateTime = DateTime.Now;
                db.LevelOfImportance.Add(levelOfImportance);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Create", levelOfImportance.ToString());

                return levelOfImportance;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Create", levelOfImportance.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public LevelOfImportance UpdateLevelOfImportance(LevelOfImportance updatedLevelOfImportance)
        {
            var currentLevelOfImportance = db.LevelOfImportance.Find(updatedLevelOfImportance.Id);
            if (currentLevelOfImportance == null)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Update", string.Empty, $"LevelOfImportance {updatedLevelOfImportance.Id} not found to update.");
                return null;
            }

            if (updatedLevelOfImportance.Name != null)
            {
                currentLevelOfImportance.Name = updatedLevelOfImportance.Name;
            }

            currentLevelOfImportance.Archived = updatedLevelOfImportance.Archived;
            currentLevelOfImportance.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentLevelOfImportance).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Update", currentLevelOfImportance.ToString(), "Error updating levelOfImportance: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Update", currentLevelOfImportance.ToString(), "Error updating levelOfImportance: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Update", currentLevelOfImportance.ToString());
            return currentLevelOfImportance;
        }

        public LevelOfImportance DeleteLevelOfImportance(int id)
        {
            LevelOfImportance levelOfImportance = db.LevelOfImportance.Find(id);
            if (levelOfImportance == null)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Delete", string.Empty, $"LevelOfImportance {id} not found to delete.");
                return null;
            }

            try
            {
                db.LevelOfImportance.Remove(levelOfImportance);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Delete", levelOfImportance.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "LevelOfImportanceRepository", "LevelOfImportance", "Delete", levelOfImportance.ToString(), "Error deleting levelOfImportance: " + e.Message);
                return null;
            }

            return levelOfImportance;
        }
    }
}