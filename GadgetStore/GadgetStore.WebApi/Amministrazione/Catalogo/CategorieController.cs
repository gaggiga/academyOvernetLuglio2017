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
    public class CategorieController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        [ResponseType(typeof(IEnumerable<Categoria>))]
        [ActionName("dellafamiglia")]
        public IHttpActionResult GetByFamiglia(int famigliaId)
        {
            var famiglia = db.Famiglie.Include("Categorie").FirstOrDefault(f => f.Id == famigliaId);

            if(famiglia == null)
            {
                return NotFound();
            }

            var categorie = famiglia.Categorie.Where(c => c.PadreId == null).OrderBy(c => c.Nome);
            return Ok(categorie);
        }

        [ResponseType(typeof(Categoria))]
        public IHttpActionResult Get(int? id)
        {
            if(id == null)
            {
                return InternalServerError(new ArgumentNullException("id"));
            }

            var categoria = db.Categorie.Include("Figli").FirstOrDefault(c => c.Id == id.Value);
            
            if(categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [ResponseType(typeof(Categoria))]
        public IHttpActionResult Post(CategoriePost model)
        {
            if (db.Categorie.Any(f => f.Nome == model.Nome))
            {
                ModelState.AddModelError("Nome", "Il nome indicato è stato già utilizzato");
            }

            // TODO: Manca la verifica dell'esistenza di padreId o Famigliaid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = new Categoria();
            categoria.Nome = model.Nome;
            categoria.FamigliaId = model.FamigliaId;
            categoria.PadreId = model.PadreId;

            db.Categorie.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.Id }, categoria);
        }

        [ResponseType(typeof(Categoria))]
        public IHttpActionResult Put(int id, CategoriePost model)
        {
            if (db.Categorie.Any(f => f.Nome == model.Nome && f.Id != id))
            {
                ModelState.AddModelError("Nome", "Il nome indicato è stato già utilizzato");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = db.Categorie.Find(id);

            if(categoria == null)
            {
                return NotFound();
            }

            categoria.Nome = model.Nome;
            categoria.FamigliaId = model.FamigliaId;
            categoria.PadreId = model.PadreId;
            db.SaveChanges();

            return Ok(categoria);
        }

        [ResponseType(typeof(Categoria))]
        public IHttpActionResult Delete(int id)
        {
            var categoria = db.Categorie.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorie.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
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
