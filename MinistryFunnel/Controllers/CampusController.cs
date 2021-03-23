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
    //TODO: add unit tests
    //TODO: versioning
    [Route("api/campus")]
    public class CampusController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ICampusRepository _campusRepository;

        public CampusController()
        {
            _campusRepository = new CampusRepository();
        }

        // GET: api/Campuss
        [HttpGet]
        [ResponseType(typeof(IQueryable<Campus>))]
        public IQueryable<Campus> GetAll()
        {
            return _campusRepository.GetCampuss();
        }

        // GET: api/Campuss/5
        [HttpGet]
        [ResponseType(typeof(Campus))]
        public IHttpActionResult GetById(int id)
        {
            Campus campus = _campusRepository.GetCampusById(id);
            if (campus == null)
            {
                return NotFound();
            }

            return Ok(campus);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Campus>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _campusRepository.SearchCampusByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Campuss/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != campus.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedCampus = _campusRepository.UpdateCampus(campus);

            if (updatedCampus == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/campus?id={campus.Id}");
        }

        // POST: api/Campuss
        [HttpPost]
        [ResponseType(typeof(Campus))]
        public IHttpActionResult Insert(Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCampus = _campusRepository.InsertCampus(campus);

            if (createdCampus == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/campus?id={createdCampus.Id}", createdCampus);
        }

        // DELETE: api/Campuss/5
        [HttpDelete]
        [ResponseType(typeof(Campus))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedCampus = _campusRepository.DeleteCampus(id);

            if (deletedCampus == null)
            {
                return BadRequest("There was a problem deleting your campus. Please try again.");
            }

            return Ok(deletedCampus);
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