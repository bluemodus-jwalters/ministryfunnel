using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.Repository.Interfaces
{
    interface ILocationRepository
    {
        IQueryable<Location> GetLocations();

        Location GetLocationById(int id);

        IQueryable<Location> SearchLocationByName(string searchText);

        Location UpdateLocation(Location updatedLocation);

        Location InsertLocation(Location location);

        Location DeleteLocation(int id);
    }
}
