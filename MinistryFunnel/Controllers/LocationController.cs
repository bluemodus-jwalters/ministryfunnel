using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;

namespace MinistryFunnel.Controllers
{
    //TODO: add unit tests
    //TODO: versioning
    [Route("api/location")]
    public class LocationController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILocationRepository _locationRepository;

        public LocationController()
        {
            _locationRepository = new LocationRepository();
        }

        // GET: api/Locations
        [HttpGet]
        [ResponseType(typeof(IQueryable<Location>))]
        public IQueryable<Location> GetAll()
        {
            return _locationRepository.GetLocations();
        }

        // GET: api/Locations/5
        [HttpGet]
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetById(int id)
        {
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
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
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