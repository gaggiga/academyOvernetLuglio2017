namespace Yoox.WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbContext : DbContext, IDbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<Persona> Persone { get; set; }
    }
}