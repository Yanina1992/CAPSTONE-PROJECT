namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pagamenti")]
    public partial class Pagamenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pagamenti()
        {
            Alunni = new HashSet<Alunni>();
            PagamentiEffettuati = new HashSet<PagamentiEffettuati>();
        }

        [Key]
        public int IdPagamento { get; set; }

        [Column(TypeName = "money")]
        public decimal? Mensa { get; set; }

        [Column(TypeName = "money")]
        public decimal? TrasportoScolastico { get; set; }

        [Column(TypeName = "money")]
        public decimal? Assicurazione { get; set; }

        [Column(TypeName = "money")]
        public decimal? Bilinguismo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Totale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alunni> Alunni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PagamentiEffettuati> PagamentiEffettuati { get; set; }
    }
}
