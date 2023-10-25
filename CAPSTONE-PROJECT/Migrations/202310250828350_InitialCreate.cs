namespace CAPSTONE_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunni",
                c => new
                    {
                        IdAlunno = c.Int(nullable: false, identity: true),
                        FKDomandaIscrizione = c.Int(nullable: false),
                        FKPagamento = c.Int(),
                        FKClasse = c.Int(),
                    })
                .PrimaryKey(t => t.IdAlunno)
                .ForeignKey("dbo.Classi", t => t.FKClasse)
                .ForeignKey("dbo.DomandeIscrizione", t => t.FKDomandaIscrizione)
                .ForeignKey("dbo.Pagamenti", t => t.FKPagamento)
                .Index(t => t.FKDomandaIscrizione)
                .Index(t => t.FKPagamento)
                .Index(t => t.FKClasse);
            
            CreateTable(
                "dbo.Classi",
                c => new
                    {
                        IdClasse = c.Int(nullable: false, identity: true),
                        Anno = c.String(nullable: false, maxLength: 1),
                        Sezione = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.IdClasse);
            
            CreateTable(
                "dbo.DomandeIscrizione",
                c => new
                    {
                        IdDomanda = c.Int(nullable: false, identity: true),
                        NomeAlunno = c.String(maxLength: 50),
                        CognomeAlunno = c.String(maxLength: 50),
                        CFAlunno = c.String(maxLength: 16),
                        Eta = c.String(maxLength: 2),
                        Allergie = c.String(maxLength: 150),
                        Bilinguismo = c.Boolean(),
                        Assicurazione = c.Boolean(),
                        CFPapa = c.String(maxLength: 16),
                        CFMamma = c.String(maxLength: 16),
                        Isee = c.Decimal(storeType: "money"),
                        DomandaAccolta = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdDomanda);
            
            CreateTable(
                "dbo.AlunniListaAttesa",
                c => new
                    {
                        IdAlunnoLista = c.Int(nullable: false, identity: true),
                        FKDomandaIscrizione = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAlunnoLista)
                .ForeignKey("dbo.DomandeIscrizione", t => t.FKDomandaIscrizione)
                .Index(t => t.FKDomandaIscrizione);
            
            CreateTable(
                "dbo.Pagamenti",
                c => new
                    {
                        IdPagamento = c.Int(nullable: false, identity: true),
                        Mensa = c.Decimal(storeType: "money"),
                        TrasportoScolastico = c.Decimal(storeType: "money"),
                        Assicurazione = c.Decimal(storeType: "money"),
                        Bilinguismo = c.Decimal(storeType: "money"),
                        Totale = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.IdPagamento);
            
            CreateTable(
                "dbo.PagamentiEffettuati",
                c => new
                    {
                        IdPagamentoEffettuato = c.Int(nullable: false, identity: true),
                        TotalePagato = c.Decimal(storeType: "money"),
                        TotaleDaPagare = c.Decimal(storeType: "money"),
                        FKPagamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPagamentoEffettuato)
                .ForeignKey("dbo.Pagamenti", t => t.FKPagamento)
                .Index(t => t.FKPagamento);
            
            CreateTable(
                "dbo.Ruoli",
                c => new
                    {
                        IdRuolo = c.Int(nullable: false, identity: true),
                        Admin = c.Boolean(),
                        Genitore = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdRuolo);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FKRuolo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.Ruoli", t => t.FKRuolo)
                .Index(t => t.FKRuolo);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "FKRuolo", "dbo.Ruoli");
            DropForeignKey("dbo.PagamentiEffettuati", "FKPagamento", "dbo.Pagamenti");
            DropForeignKey("dbo.Alunni", "FKPagamento", "dbo.Pagamenti");
            DropForeignKey("dbo.AlunniListaAttesa", "FKDomandaIscrizione", "dbo.DomandeIscrizione");
            DropForeignKey("dbo.Alunni", "FKDomandaIscrizione", "dbo.DomandeIscrizione");
            DropForeignKey("dbo.Alunni", "FKClasse", "dbo.Classi");
            DropIndex("dbo.Users", new[] { "FKRuolo" });
            DropIndex("dbo.PagamentiEffettuati", new[] { "FKPagamento" });
            DropIndex("dbo.AlunniListaAttesa", new[] { "FKDomandaIscrizione" });
            DropIndex("dbo.Alunni", new[] { "FKClasse" });
            DropIndex("dbo.Alunni", new[] { "FKPagamento" });
            DropIndex("dbo.Alunni", new[] { "FKDomandaIscrizione" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Users");
            DropTable("dbo.Ruoli");
            DropTable("dbo.PagamentiEffettuati");
            DropTable("dbo.Pagamenti");
            DropTable("dbo.AlunniListaAttesa");
            DropTable("dbo.DomandeIscrizione");
            DropTable("dbo.Classi");
            DropTable("dbo.Alunni");
        }
    }
}
