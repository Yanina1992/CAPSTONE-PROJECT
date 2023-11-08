namespace CAPSTONE_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassiAnnoScRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classi", "AnnoScolastico", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classi", "AnnoScolastico", c => c.String(maxLength: 10));
        }
    }
}
