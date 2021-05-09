using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;

namespace MinistryFunnel.Controllers
{
    //TODO: add unit tests
    //TODO: versioning
    [Route("api/practice")]
    public class PracticeController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IPracticeRepository _practiceRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public PracticeController()
        {
            _practiceRepository = new PracticeRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        // GET: api/Practices
        [HttpGet]
        [ResponseType(typeof(IQueryable<Practice>))]
        public IQueryable<Practice> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "GetAll", null, null);

            return _practiceRepository.GetPractices();
        }

        // GET: api/Practices/5
        [HttpGet]
        [ResponseType(typeof(Practice))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "Get By Id", id.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "Get by Name", searchText, null);

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
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "Update", practice.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "Insert", practice.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "PracticeController", "Practice", "Delete", id.ToString(), null);

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