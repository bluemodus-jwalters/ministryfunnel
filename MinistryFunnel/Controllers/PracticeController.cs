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
    [Route("api/practice")]
    public class PracticeController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IPracticeRepository _practiceRepository;

        public PracticeController()
        {
            _practiceRepository = new PracticeRepository();
        }

        // GET: api/Practices
        [HttpGet]
        [ResponseType(typeof(IQueryable<Practice>))]
        public IQueryable<Practice> GetAll()
        {
            return _practiceRepository.GetPractices();
        }

        // GET: api/Practices/5
        [HttpGet]
        [ResponseType(typeof(Practice))]
        public IHttpActionResult GetById(int id)
        {
            Practice practice = _practiceRepository.GetPracticeById(id);
            if (practice == null)
            {
                return NotFound();
            }

            return Ok(practice);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Practice>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _practiceRepository.SearchPracticeByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Practices/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Practice practice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != practice.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedPractice = _practiceRepository.UpdatePractice(practice);

            if (updatedPractice == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/practice?id={practice.Id}");
        }

        // POST: api/Practices
        [HttpPost]
        [ResponseType(typeof(Practice))]
        public IHttpActionResult Insert(Practice practice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPractice = _practiceRepository.InsertPractice(practice);

            if (createdPractice == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/practice?id={createdPractice.Id}", createdPractice);
        }

        // DELETE: api/Practices/5
        [HttpDelete]
        [ResponseType(typeof(Practice))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedPractice = _practiceRepository.DeletePractice(id);

            if (deletedPractice == null)
            {
                return BadRequest("There was a problem deleting your practice. Please try again.");
            }

            return Ok(deletedPractice);
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