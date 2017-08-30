namespace MyTrattoria.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrutturaIniziale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comande",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrdineId = c.Int(nullable: false),
                        PietanzaId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Prezzo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stato = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ordini", t => t.OrdineId, cascadeDelete: true)
                .Index(t => t.OrdineId);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TavoloId = c.Int(nullable: false),
                        DataCreazione = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tavoli", t => t.TavoloId, cascadeDelete: true)
                .Index(t => t.TavoloId);
            
            CreateTable(
                "dbo.Tavoli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false, maxLength: 10),
                        DataCreazione = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Incassi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataIncasso = c.DateTime(nullable: false),
                        Totale = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pietanze",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        Prezzo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordini", "TavoloId", "dbo.Tavoli");
            DropForeignKey("dbo.Comande", "OrdineId", "dbo.Ordini");
            DropIndex("dbo.Ordini", new[] { "TavoloId" });
            DropIndex("dbo.Comande", new[] { "OrdineId" });
            DropTable("dbo.Pietanze");
            DropTable("dbo.Incassi");
            DropTable("dbo.Tavoli");
            DropTable("dbo.Ordini");
            DropTable("dbo.Comande");
        }
    }
}
