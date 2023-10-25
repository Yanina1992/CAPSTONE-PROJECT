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
        public int IdPagamentoEffettuato { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalePagato { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotaleDaPagare { get; set; }

        public int FKPagamento { get; set; }

        public virtual Pagamenti Pagamenti { get; set; }
    }
}
