namespace GadgetStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GestitiCampiStringa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prodotti", "Nome", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Categorie", "Nome", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Famiglie", "Nome", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Utenti", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Utenti", "Cognome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RigheOrdini", "NomeProdotto", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RigheOrdini", "NomeProdotto", c => c.String());
            AlterColumn("dbo.Utenti", "Cognome", c => c.String());
            AlterColumn("dbo.Utenti", "Nome", c => c.String());
            AlterColumn("dbo.Famiglie", "Nome", c => c.String());
            AlterColumn("dbo.Categorie", "Nome", c => c.String());
            AlterColumn("dbo.Prodotti", "Nome", c => c.String());
        }
    }
}
