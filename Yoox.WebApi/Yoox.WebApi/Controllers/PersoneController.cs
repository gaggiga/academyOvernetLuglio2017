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
        private IDbContext db = new MyDbContext();

        public PersoneController(IDbContext db = null)
        {
            if(db == null)
            {
                db = new MyDbContext();
            }

            this.db = db;
        }

        // GET: api/Persone
        public IQueryable<Persona> GetPersone()
        {
            return db.Persone;
        }

        // GET: api/Persone/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = db.Persone.Find(id);
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

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            db.Persone.Add(persona);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/Persone/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona = db.Persone.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Persone.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int id)
        {
            return db.Persone.Count(e => e.Id == id) > 0;
        }
    }
}