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
    public class FrequencyRepository : IFrequencyRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public FrequencyRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Frequency> GetFrequencys()
        {
            return db.Frequency;
        }

        public Frequency GetFrequencyById(int id)
        {
            Frequency frequency = db.Frequency.Find(id);

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Get By Id", id.ToString(), $"Results found: {frequency != null}");

            return frequency;
        }

        public IQueryable<Frequency> SearchFrequencyByName(string searchText)
        {
            var results = db.Frequency.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Frequency InsertFrequency(Frequency frequency)
        {
            try
            {
                frequency.CreatedDateTime = DateTime.Now;
                db.Frequency.Add(frequency);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Create", frequency.ToString());

                return frequency;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Create", frequency.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Frequency UpdateFrequency(Frequency updatedFrequency)
        {
            var currentFrequency = db.Frequency.Find(updatedFrequency.Id);
            if (currentFrequency == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Update", string.Empty, $"Frequency {updatedFrequency.Id} not found to update.");
                return null;
            }

            if (updatedFrequency.Name != null)
            {
                currentFrequency.Name = updatedFrequency.Name;
            }

            currentFrequency.Archived = updatedFrequency.Archived;
            currentFrequency.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentFrequency).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Update", currentFrequency.ToString(), "Error updating frequency: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Update", currentFrequency.ToString(), "Error updating frequency: " + e.Message);
                return null;
            }

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Update", currentFrequency.ToString());
            return currentFrequency;
        }

        public Frequency DeleteFrequency(int id)
        {
            Frequency frequency = db.Frequency.Find(id);
            if (frequency == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Delete", string.Empty, $"Frequency {id} not found to delete.");
                return null;
            }

            try
            {
                db.Frequency.Remove(frequency);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Delete", frequency.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "FrequencyRepository", "Frequency", "Delete", frequency.ToString(), "Error deleting frequency: " + e.Message);
                return null;
            }

            return frequency;
        }
    }
}