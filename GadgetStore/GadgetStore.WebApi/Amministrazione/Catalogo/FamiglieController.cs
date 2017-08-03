using GadgetStore.DAL;
using GadgetStore.DAL.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GadgetStore.WebApi.Amministrazione.Catalogo
{
    public class FamiglieController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        [ResponseType(typeof(IEnumerable<Famiglia>))]
        public IHttpActionResult Get()
        {
            var famiglie = db.Famiglie.OrderBy(f => f.Nome).ToList();
            return Ok(famiglie);
        }

        [ResponseType(typeof(Famiglia))]
        public IHttpActionResult Get(int id)
        {
            var famiglia = db.Famiglie.Find(id);
            
            if(famiglia == null)
            {
                return NotFound();
            }

            return Ok(famiglia);
        }

        [ResponseType(typeof(Famiglia))]
        public IHttpActionResult Post(FamigliePost model)
        {
            if(db.Famiglie.Any(f => f.Nome == model.Nome))
            {
                ModelState.AddModelError("Nome", "Il nome indicato è stato già utilizzato");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var famiglia = new Famiglia();
            famiglia.Nome = model.Nome;
            db.Famiglie.Add(famiglia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = famiglia.Id }, famiglia);
        }

        [ResponseType(typeof(Famiglia))]
        public IHttpActionResult Put(int id, FamigliePost model)
        {
            if (db.Famiglie.Any(f => f.Nome == model.Nome && f.Id != id))
            {
                ModelState.AddModelError("Nome", "Il nome indicato è stato già utilizzato");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var famiglia = db.Famiglie.Find(id);

            if(famiglia == null)
            {
                return NotFound();
            }

            famiglia.Nome = model.Nome;
            db.SaveChanges();

            return Ok(famiglia);
        }

        [ResponseType(typeof(Famiglia))]
        public IHttpActionResult Delete(int id)
        {
            var famiglia = db.Famiglie.Find(id);

            if (famiglia == null)
            {
                return NotFound();
            }

            db.Famiglie.Remove(famiglia);
            db.SaveChanges();

            return Ok(famiglia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
