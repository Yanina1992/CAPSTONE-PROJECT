namespace CAPSTONE_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Classi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classi", "AnnoScolastico", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classi", "AnnoScolastico");
        }
    }
}
