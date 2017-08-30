namespace MyTrattoria.Sql
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SqlDbContext : DbContext
    {
        public SqlDbContext()
            : base("name=SqlDbConnectionString")
        {
        }

        public virtual DbSet<Pietanza> Pietanze { get; set; }
        public virtual DbSet<Incasso> Incassi { get; set; }
        public virtual DbSet<Tavolo> Tavoli { get; set; }
        public virtual DbSet<Ordine> Ordini { get; set; }
        public virtual DbSet<Comanda> Comande { get; set; }
    }
}