using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CAPSTONE_PROJECT.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Alunni> Alunni { get; set; }
        public virtual DbSet<AlunniListaAttesa> AlunniListaAttesa { get; set; }
        public virtual DbSet<Classi> Classi { get; set; }
        public virtual DbSet<DomandeIscrizione> DomandeIscrizione { get; set; }
        public virtual DbSet<Pagamenti> Pagamenti { get; set; }
        public virtual DbSet<PagamentiEffettuati> PagamentiEffettuati { get; set; }
        public virtual DbSet<Ruoli> Ruoli { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classi>()
                .HasMany(e => e.Alunni)
                .WithOptional(e => e.Classi)
                .HasForeignKey(e => e.FKClasse);

            modelBuilder.Entity<DomandeIscrizione>()
                .Property(e => e.Isee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DomandeIscrizione>()
                .HasMany(e => e.Alunni)
                .WithRequired(e => e.DomandeIscrizione)
                .HasForeignKey(e => e.FKDomandaIscrizione)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomandeIscrizione>()
                .HasMany(e => e.AlunniListaAttesa)
                .WithRequired(e => e.DomandeIscrizione)
                .HasForeignKey(e => e.FKDomandaIscrizione)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pagamenti>()
                .Property(e => e.Mensa)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagamenti>()
                .Property(e => e.TrasportoScolastico)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagamenti>()
                .Property(e => e.Assicurazione)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagamenti>()
                .Property(e => e.Bilinguismo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagamenti>()
                .Property(e => e.Totale)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pagamenti>()
                .HasMany(e => e.Alunni)
                .WithOptional(e => e.Pagamenti)
                .HasForeignKey(e => e.FKPagamento);

            modelBuilder.Entity<Pagamenti>()
                .HasMany(e => e.PagamentiEffettuati)
                .WithRequired(e => e.Pagamenti)
                .HasForeignKey(e => e.FKPagamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PagamentiEffettuati>()
                .Property(e => e.TotalePagato)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PagamentiEffettuati>()
                .Property(e => e.TotaleDaPagare)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ruoli>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Ruoli)
                .HasForeignKey(e => e.FKRuolo)
                .WillCascadeOnDelete(false);
        }
    }
}
