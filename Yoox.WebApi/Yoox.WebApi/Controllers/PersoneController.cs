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
using Yoox.WebApi.Models;

namespace Yoox.WebApi.Controllers
{
    public class PersoneController : ApiController
    {
        private MyDbService db;

        public PersoneController(MyDbService db = null)
        {
            if(db == null)
            {
                db = new MyDbService();
            }

            this.db = db;
        }

        // GET: api/Persone
        public IQueryable<Persona> GetPersone()
        {
            return db.GetPersone();
        }

        // GET: api/Persone/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = db.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Persone/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(int id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            db.Put(persona);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Persone
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Post(persona);

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/Persone/5
        public IHttpActionResult DeletePersona(int id)
        {
            db.Delete(id);

            return Ok();
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