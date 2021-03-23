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
    [Route("api/frequency")]
    public class FrequencyController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IFrequencyRepository _frequencyRepository;

        public FrequencyController()
        {
            _frequencyRepository = new FrequencyRepository();
        }

        // GET: api/Frequencys
        [HttpGet]
        [ResponseType(typeof(IQueryable<Frequency>))]
        public IQueryable<Frequency> GetAll()
        {
            return _frequencyRepository.GetFrequencys();
        }

        // GET: api/Frequencys/5
        [HttpGet]
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult GetById(int id)
        {
            Frequency frequency = _frequencyRepository.GetFrequencyById(id);
            if (frequency == null)
            {
                return NotFound();
            }

            return Ok(frequency);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Frequency>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _frequencyRepository.SearchFrequencyByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Frequencys/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Frequency frequency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != frequency.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedFrequency = _frequencyRepository.UpdateFrequency(frequency);

            if (updatedFrequency == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/frequency?id={frequency.Id}");
        }

        // POST: api/Frequencys
        [HttpPost]
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult Insert(Frequency frequency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFrequency = _frequencyRepository.InsertFrequency(frequency);

            if (createdFrequency == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/frequency?id={createdFrequency.Id}", createdFrequency);
        }

        // DELETE: api/Frequencys/5
        [HttpDelete]
        [ResponseType(typeof(Frequency))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedFrequency = _frequencyRepository.DeleteFrequency(id);

            if (deletedFrequency == null)
            {
                return BadRequest("There was a problem deleting your frequency. Please try again.");
            }

            return Ok(deletedFrequency);
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