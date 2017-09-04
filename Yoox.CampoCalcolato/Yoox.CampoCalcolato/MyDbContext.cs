namespace Yoox.CampoCalcolato
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RigaOrdine>()
                .Property(RigaOrdine.Quantit‡Expression).HasColumnName("Quantit‡");

            modelBuilder.Entity<RigaOrdine>()
                .Property(RigaOrdine.PrezzoExpression).HasColumnName("Prezzo");

            modelBuilder.Entity<RigaOrdine>()
                .Property(RigaOrdine.ScontoExpression).HasColumnName("Sconto");
        }

        public override int SaveChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                                    .Where(x => x.Entity is INeedToCalculateSomething &&
                                    (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach(var entity in selectedEntityList)
            {
                var myEntity = (INeedToCalculateSomething)entity.Entity;
                myEntity.CalculateIt();
            }

            return base.SaveChanges();
        }

        public virtual DbSet<Ordine> Ordini { get; set; }
        public virtual DbSet<RigaOrdine> RigheOrdine { get; set; }
    }
}