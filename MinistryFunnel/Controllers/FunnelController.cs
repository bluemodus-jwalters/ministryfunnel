using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;

namespace MinistryFunnel.Controllers
{
    [Route("api/funnel")]
    public class FunnelController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IFunnelRepository _funnelRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public FunnelController()
        {
            _funnelRepository = new FunnelRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        // GET: api/Funnels
        [HttpGet]
        [ResponseType(typeof(IQueryable<Funnel>))]
        public IQueryable<Funnel> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "GetAll", null, null);

            return _funnelRepository.GetFunnels();
        }

        // GET: api/Funnels/5
        [HttpGet]
        [ResponseType(typeof(Funnel))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "Get By Id", id.ToString(), null);

            Funnel funnel = _funnelRepository.GetFunnelById(id);
            if (funnel == null)
            {
                return NotFound();
            }

            return Ok(funnel);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Funnel>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "Get by Name", searchText, null);

            var results = _funnelRepository.SearchFunnelByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Funnels/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Funnel funnel)
        {
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "Update", funnel.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funnel.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedFunnel = _funnelRepository.UpdateFunnel(funnel);

            if (updatedFunnel == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/funnel?id={funnel.Id}");
        }

        // POST: api/Funnels
        [HttpPost]
        [ResponseType(typeof(Funnel))]
        public IHttpActionResult Insert(Funnel funnel)
        {
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "Insert", funnel.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFunnel = _funnelRepository.InsertFunnel(funnel);

            if (createdFunnel == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/funnel?id={createdFunnel.Id}", createdFunnel);
        }

        // DELETE: api/Funnels/5
        [HttpDelete]
        [ResponseType(typeof(Funnel))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            _loggerService.CreateLog(_user, "API", "FunnelController", "Funnel", "Delete", id.ToString(), null);

            var deletedFunnel = _funnelRepository.DeleteFunnel(id);

            if (deletedFunnel == null)
            {
                return BadRequest("There was a problem deleting your funnel. Please try again.");
            }

            return Ok(deletedFunnel);
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