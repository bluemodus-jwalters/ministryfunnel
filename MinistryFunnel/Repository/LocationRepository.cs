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
    public class LocationRepository : ILocationRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public LocationRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<Location> GetLocations()
        {
            return db.Location;
        }

        public Location GetLocationById(int id)
        {
            Location location = db.Location.Find(id);

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Get By Id", id.ToString(), $"Results found: {location != null}");

            return location;
        }

        public IQueryable<Location> SearchLocationByName(string searchText)
        {
            var results = db.Location.Where(x => x.Name.Contains(searchText));

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Get By Name", searchText, $"Results found: {results != null}");

            return results;
        }

        public Location InsertLocation(Location location)
        {
            try
            {
                location.CreatedDateTime = DateTime.Now;
                db.Location.Add(location);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Create", location.ToString());

                return location;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Create", location.ToString(), "Error creating this record: " + e.Message);
                return null;
            }
        }

        public Location UpdateLocation(Location updatedLocation)
        {
            var currentLocation = db.Location.Find(updatedLocation.Id);
            if (currentLocation == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Update", string.Empty, $"Location {updatedLocation.Id} not found to update.");
                return null;
            }

            if (updatedLocation.Name != null)
            {
                currentLocation.Name = updatedLocation.Name;
            }

            currentLocation.Archived = updatedLocation.Archived;
            currentLocation.ModifiedDateTime = DateTime.Now;           

            db.Entry(currentLocation).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Update", currentLocation.ToString(), "Error updating location: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Update", currentLocation.ToString(), "Error updating location: " + e.Message);
                return null;
            }

            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Update", currentLocation.ToString());
            return currentLocation;
        }

        public Location DeleteLocation(int id)
        {
            Location location = db.Location.Find(id);
            if (location == null)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Delete", string.Empty, $"Location {id} not found to delete.");
                return null;
            }

            try
            {
                db.Location.Remove(location);
                db.SaveChanges();

                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Delete", location.ToString());
            }
            catch (Exception e)
            {
                _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LocationRepository", "Location", "Delete", location.ToString(), "Error deleting location: " + e.Message);
                return null;
            }

            return location;
        }
    }
}