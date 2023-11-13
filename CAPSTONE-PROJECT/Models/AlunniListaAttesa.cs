namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlunniListaAttesa")]
    public partial class AlunniListaAttesa
    {
        [Key]
        [Display(Name = "Alunno in lista d'attesa n.")]
        public int IdAlunnoLista { get; set; }

        [Display(Name = "Domanda n.")]
        public int FKDomandaIscrizione { get; set; }
        public virtual DomandeIscrizione DomandeIscrizione { get; set; }
    }
}
