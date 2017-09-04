namespace Yoox.PausaGianluca.DAL
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<Casa> Case { get; set; }
        public virtual DbSet<Persona> Persone { get; set; }
    }
}