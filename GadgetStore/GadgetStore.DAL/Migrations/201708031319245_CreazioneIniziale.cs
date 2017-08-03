namespace GadgetStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreazioneIniziale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articoli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdottoId = c.Int(nullable: false),
                        PrezzoListino = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prodotti", t => t.ProdottoId, cascadeDelete: true)
                .Index(t => t.ProdottoId);
            
            CreateTable(
                "dbo.Prodotti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categorie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        FamigliaId = c.Int(),
                        PadreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Famiglie", t => t.FamigliaId)
                .ForeignKey("dbo.Categorie", t => t.PadreId)
                .Index(t => t.FamigliaId)
                .Index(t => t.PadreId);
            
            CreateTable(
                "dbo.Famiglie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticoliNeiCarrelli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticoloId = c.Int(nullable: false),
                        CarrelloId = c.Int(nullable: false),
                        Quantità = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Prezzo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articoli", t => t.ArticoloId, cascadeDelete: true)
                .ForeignKey("dbo.Carrelli", t => t.CarrelloId, cascadeDelete: true)
                .Index(t => t.ArticoloId)
                .Index(t => t.CarrelloId);
            
            CreateTable(
                "dbo.Carrelli",
                c => new
                    {
                        ClienteId = c.Int(nullable: false),
                        DataCreazione = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Clienti", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cognome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clienti", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.RigheOrdini",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticoloId = c.Int(nullable: false),
                        NomeProdotto = c.String(),
                        Quantità = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Prezzo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrdineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ordini", t => t.OrdineId, cascadeDelete: true)
                .Index(t => t.OrdineId);
            
            CreateTable(
                "dbo.CategorieProdotti",
                c => new
                    {
                        Prodotto_Id = c.Int(nullable: false),
                        Categoria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Prodotto_Id, t.Categoria_Id })
                .ForeignKey("dbo.Prodotti", t => t.Prodotto_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categorie", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Prodotto_Id)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.Clienti",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utenti", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Amministratori",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utenti", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Amministratori", "Id", "dbo.Utenti");
            DropForeignKey("dbo.Clienti", "Id", "dbo.Utenti");
            DropForeignKey("dbo.RigheOrdini", "OrdineId", "dbo.Ordini");
            DropForeignKey("dbo.Ordini", "ClienteId", "dbo.Clienti");
            DropForeignKey("dbo.ArticoliNeiCarrelli", "CarrelloId", "dbo.Carrelli");
            DropForeignKey("dbo.Carrelli", "ClienteId", "dbo.Clienti");
            DropForeignKey("dbo.ArticoliNeiCarrelli", "ArticoloId", "dbo.Articoli");
            DropForeignKey("dbo.CategorieProdotti", "Categoria_Id", "dbo.Categorie");
            DropForeignKey("dbo.CategorieProdotti", "Prodotto_Id", "dbo.Prodotti");
            DropForeignKey("dbo.Categorie", "PadreId", "dbo.Categorie");
            DropForeignKey("dbo.Categorie", "FamigliaId", "dbo.Famiglie");
            DropForeignKey("dbo.Articoli", "ProdottoId", "dbo.Prodotti");
            DropIndex("dbo.Amministratori", new[] { "Id" });
            DropIndex("dbo.Clienti", new[] { "Id" });
            DropIndex("dbo.CategorieProdotti", new[] { "Categoria_Id" });
            DropIndex("dbo.CategorieProdotti", new[] { "Prodotto_Id" });
            DropIndex("dbo.RigheOrdini", new[] { "OrdineId" });
            DropIndex("dbo.Ordini", new[] { "ClienteId" });
            DropIndex("dbo.Carrelli", new[] { "ClienteId" });
            DropIndex("dbo.ArticoliNeiCarrelli", new[] { "CarrelloId" });
            DropIndex("dbo.ArticoliNeiCarrelli", new[] { "ArticoloId" });
            DropIndex("dbo.Categorie", new[] { "PadreId" });
            DropIndex("dbo.Categorie", new[] { "FamigliaId" });
            DropIndex("dbo.Articoli", new[] { "ProdottoId" });
            DropTable("dbo.Amministratori");
            DropTable("dbo.Clienti");
            DropTable("dbo.CategorieProdotti");
            DropTable("dbo.RigheOrdini");
            DropTable("dbo.Ordini");
            DropTable("dbo.Utenti");
            DropTable("dbo.Carrelli");
            DropTable("dbo.ArticoliNeiCarrelli");
            DropTable("dbo.Famiglie");
            DropTable("dbo.Categorie");
            DropTable("dbo.Prodotti");
            DropTable("dbo.Articoli");
        }
    }
}
