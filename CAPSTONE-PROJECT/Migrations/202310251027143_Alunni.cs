namespace CAPSTONE_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alunni : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DomandeIscrizione", "NomeAlunno", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DomandeIscrizione", "CognomeAlunno", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DomandeIscrizione", "CFAlunno", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.DomandeIscrizione", "Eta", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.DomandeIscrizione", "CFMamma", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DomandeIscrizione", "CFMamma", c => c.String(maxLength: 16));
            AlterColumn("dbo.DomandeIscrizione", "Eta", c => c.String(maxLength: 2));
            AlterColumn("dbo.DomandeIscrizione", "CFAlunno", c => c.String(maxLength: 16));
            AlterColumn("dbo.DomandeIscrizione", "CognomeAlunno", c => c.String(maxLength: 50));
            AlterColumn("dbo.DomandeIscrizione", "NomeAlunno", c => c.String(maxLength: 50));
        }
    }
}
