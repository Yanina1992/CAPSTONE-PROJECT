namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PagamentiEffettuati")]
    public partial class PagamentiEffettuati
    {
        [Key]
        [Display(Name = "Pagamento effettuato n.")]
        public int IdPagamentoEffettuato { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Totale pagato")]
        public decimal? TotalePagato { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Totale da pagare")]
        public decimal? TotaleDaPagare { get; set; }

        [Display(Name = "Pagamento n.")]
        public int FKPagamento { get; set; }

        public virtual Pagamenti Pagamenti { get; set; }
    }
}
