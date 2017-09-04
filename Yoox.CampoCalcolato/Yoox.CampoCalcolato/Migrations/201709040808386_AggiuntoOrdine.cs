namespace Yoox.CampoCalcolato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntoOrdine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        TotaleOrdine = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RigheOrdine", "Ordine_Id", c => c.Int());
            CreateIndex("dbo.RigheOrdine", "Ordine_Id");
            AddForeignKey("dbo.RigheOrdine", "Ordine_Id", "dbo.Ordini", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RigheOrdine", "Ordine_Id", "dbo.Ordini");
            DropIndex("dbo.RigheOrdine", new[] { "Ordine_Id" });
            DropColumn("dbo.RigheOrdine", "Ordine_Id");
            DropTable("dbo.Ordini");
        }
    }
}
