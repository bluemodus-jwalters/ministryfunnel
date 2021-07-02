using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Managers;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;

namespace MinistryFunnel.Controllers
{
    //TODO: add unit tests
    //TODO: versioning
    [Route("api/location")]
    public class LocationController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILocationRepository _locationRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public LocationController()
        {
            _locationRepository = new LocationRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        // GET: api/Locations
        [HttpGet]
        [ResponseType(typeof(IQueryable<Location>))]
        [ApiAuthorization(Role = "discovery_api_edit")]
        public IQueryable<Location> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "GetAll", null, null);

            return _locationRepository.GetLocations();
        }

        // GET: api/Locations/5
        [HttpGet]
        [ResponseType(typeof(Location))]
        [ApiAuthorization(Role = "discovery_api_edit")]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "Get By Id", id.ToString(), null);

            Location location = _locationRepository.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Location>))]
        [ApiAuthorization(Role = "discovery_api_edit")]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "Get by Name", searchText, null);

            var results = _locationRepository.SearchLocationByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Locations/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Location location)
        {
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "Update", location.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedLocation = _locationRepository.UpdateLocation(location);

            if (updatedLocation == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/location?id={location.Id}");
        }

        // POST: api/Locations
        [HttpPost]
        [ResponseType(typeof(Location))]
        public IHttpActionResult Insert(Location location)
        {
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "Insert", location.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLocation = _locationRepository.InsertLocation(location);

            if (createdLocation == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/location?id={createdLocation.Id}", createdLocation);
        }

        // DELETE: api/Locations/5
        [HttpDelete]
        [ResponseType(typeof(Location))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            _loggerService.CreateLog(_user, "API", "LocationController", "Location", "Delete", id.ToString(), null);

            var deletedLocation = _locationRepository.DeleteLocation(id);

            if (deletedLocation == null)
            {
                return BadRequest("There was a problem deleting your location. Please try again.");
            }

            return Ok(deletedLocation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}