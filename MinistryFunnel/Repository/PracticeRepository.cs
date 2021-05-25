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
    public class PracticeRepository : IPracticeRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public PracticeRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Practice> GetPractices()
        {
            return db.Practice;
        }

        public Practice GetPracticeById(int id)
        {
            Practice practice = db.Practice.Find(id);

            _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Get By Id", id.ToString(), $"Results found: {practice != null}");

            return practice;
        }

        public IQueryable<Practice> SearchPracticeByName(string searchText)
        {
            var results = db.Practice.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Practice InsertPractice(Practice practice)
        {
            try
            {
                practice.CreatedDateTime = DateTime.Now;
                db.Practice.Add(practice);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Create", practice.ToString());

                return practice;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Create", practice.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Practice UpdatePractice(Practice updatedPractice)
        {
            var currentPractice = db.Practice.Find(updatedPractice.Id);
            if (currentPractice == null)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Update", string.Empty, $"Practice {updatedPractice.Id} not found to update.");
                return null;
            }

            if (updatedPractice.Name != null)
            {
                currentPractice.Name = updatedPractice.Name;
            }

            currentPractice.Archived = updatedPractice.Archived;
            currentPractice.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentPractice).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Update", currentPractice.ToString(), "Error updating practice: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Update", currentPractice.ToString(), "Error updating practice: " + e.Message);
                return null;
            }

            _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Update", currentPractice.ToString());
            return currentPractice;
        }

        public Practice DeletePractice(int id)
        {
            Practice practice = db.Practice.Find(id);
            if (practice == null)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Delete", string.Empty, $"Practice {id} not found to delete.");
                return null;
            }

            try
            {
                db.Practice.Remove(practice);
                db.SaveChanges();

                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Delete", practice.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog("Jordan", "API", "PracticeRepository", "Practice", "Delete", practice.ToString(), "Error deleting practice: " + e.Message);
                return null;
            }

            return practice;
        }
    }
}