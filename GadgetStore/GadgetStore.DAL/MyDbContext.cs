namespace GadgetStore.DAL
{
    using Entities.Catalogo;
    using Entities.Utenti;
    using Entities.Vendita;
    using System;
    using System.Collections.Generic;
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

            var prodottiManager = modelBuilder.Entity<Prodotto>();
            prodottiManager
                .HasMany(p => p.Categorie)
                .WithMany(c => c.Prodotti)
                .Map(m => m.ToTable("CategorieProdotti"));

            var clientiManager = modelBuilder.Entity<Cliente>();
            clientiManager
                .HasRequired(c => c.Carrello)
                .WithRequiredPrincipal(c => c.Cliente);
        }

        public virtual DbSet<Utente> Utenti { get; set; }
        public virtual DbSet<Famiglia> Famiglie { get; set; }
        public virtual DbSet<Categoria> Categorie { get; set; }
        public virtual DbSet<Prodotto> Prodotti { get; set; }
        public virtual DbSet<Articolo> Articoli { get; set; }
        public virtual DbSet<Carrello> Carrelli { get; set; }
        public virtual DbSet<ArticoloNelCarrello> ArticoliNeiCarrelli { get; set; }
        public virtual DbSet<Ordine> Ordini { get; set; }
        public virtual DbSet<RigaOrdine> RigheOrdini { get; set; }

    }
}