using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Yoox.WebApi.Models
{
    public class MyDbService : IDisposable
    {
        private IDbContext db { get; set; }

        public MyDbService(IDbContext db = null)
        {
            if(db == null)
            {
                db = new MyDbContext();
            }

            this.db = db;
        }

        public IQueryable<Persona> GetPersone()
        {
            return db.Persone;
        }

        public Persona Find(int id)
        {
            return db.Persone.Find(id);
        }

        public void Put(Persona persona)
        {
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Post(Persona persona)
        {
            db.Persone.Add(persona);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Persona persona = db.Persone.Find(id);
            db.Persone.Remove(persona);
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                db = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}