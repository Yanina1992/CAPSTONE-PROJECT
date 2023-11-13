namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alunni")]
    public partial class Alunni
    {
        [Key]
        [Display(Name = "Alunno n.")]
        public int IdAlunno { get; set; }

        [Display(Name = "Domanda n.")]
        public int FKDomandaIscrizione { get; set; }

        [Display(Name = "Pagamento n.")]
        public int? FKPagamento { get; set; }

        [Display(Name = "Classe n.")]
        public int? FKClasse { get; set; }

        public virtual Classi Classi { get; set; }

        public virtual DomandeIscrizione DomandeIscrizione { get; set; }

        public virtual Pagamenti Pagamenti { get; set; }
    }
}
