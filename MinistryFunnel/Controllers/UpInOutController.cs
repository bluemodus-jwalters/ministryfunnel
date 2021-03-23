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
    [Route("api/upInOut")]
    public class UpInOutController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IUpInOutRepository _upInOutRepository;

        public UpInOutController()
        {
            _upInOutRepository = new UpInOutRepository();
        }

        // GET: api/UpInOuts
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IQueryable<UpInOut> GetAll()
        {
            return _upInOutRepository.GetUpInOuts();
        }

        // GET: api/UpInOuts/5
        [HttpGet]
        [ResponseType(typeof(UpInOut))]
        public IHttpActionResult GetById(int id)
        {
            UpInOut upInOut = _upInOutRepository.GetUpInOutById(id);
            if (upInOut == null)
            {
                return NotFound();
            }

            return Ok(upInOut);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _upInOutRepository.SearchUpInOutByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/UpInOuts/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, UpInOut upInOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != upInOut.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedUpInOut = _upInOutRepository.UpdateUpInOut(upInOut);

            if (updatedUpInOut == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/upInOut?id={upInOut.Id}");
        }

        // POST: api/UpInOuts
        [HttpPost]
        [ResponseType(typeof(UpInOut))]
        public IHttpActionResult Insert(UpInOut upInOut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUpInOut = _upInOutRepository.InsertUpInOut(upInOut);

            if (createdUpInOut == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/upInOut?id={createdUpInOut.Id}", createdUpInOut);
        }

        // DELETE: api/UpInOuts/5
        [HttpDelete]
        [ResponseType(typeof(UpInOut))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedUpInOut = _upInOutRepository.DeleteUpInOut(id);

            if (deletedUpInOut == null)
            {
                return BadRequest("There was a problem deleting your upInOut. Please try again.");
            }

            return Ok(deletedUpInOut);
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