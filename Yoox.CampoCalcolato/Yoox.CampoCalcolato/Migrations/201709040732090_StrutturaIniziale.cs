namespace Yoox.CampoCalcolato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrutturaIniziale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RigheOrdine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descrizione = c.String(),
                        Prezzo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantità = c.Int(nullable: false),
                        Sconto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotaleRiga = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RigheOrdine");
        }
    }
}
