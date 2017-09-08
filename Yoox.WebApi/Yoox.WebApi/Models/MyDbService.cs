using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yoox.WebApi.Models
{
    public class MyDbService
    {
        private IDbContext db { get; set; }

        public MyDbService(IDbContext db)
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
    }
}