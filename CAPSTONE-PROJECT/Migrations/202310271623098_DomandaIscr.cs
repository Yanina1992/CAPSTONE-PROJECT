namespace CAPSTONE_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DomandaIscr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DomandeIscrizione", "Mensa", c => c.Boolean());
            AddColumn("dbo.DomandeIscrizione", "TrasportoScolastico", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DomandeIscrizione", "TrasportoScolastico");
            DropColumn("dbo.DomandeIscrizione", "Mensa");
        }
    }
}
