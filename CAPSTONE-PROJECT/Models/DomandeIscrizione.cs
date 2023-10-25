namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DomandeIscrizione")]
    public partial class DomandeIscrizione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DomandeIscrizione()
        {
            Alunni = new HashSet<Alunni>();
            AlunniListaAttesa = new HashSet<AlunniListaAttesa>();
        }

        [Key]
        public int IdDomanda { get; set; }

        [StringLength(50)]
        [Required]
        public string NomeAlunno { get; set; }

        [StringLength(50)]
        [Required]
        public string CognomeAlunno { get; set; }

        [StringLength(16)]
        [Required]
        public string CFAlunno { get; set; }

        [StringLength(2)]
        [Required]
        public string Eta { get; set; }

        [StringLength(150)]
        public string Allergie { get; set; }

        public bool? Bilinguismo { get; set; }

        public bool? Assicurazione { get; set; }

        [StringLength(16)]
        public string CFPapa { get; set; }

        [StringLength(16)]
        [Required]
        public string CFMamma { get; set; }

        [Column(TypeName = "money")]
        public decimal? Isee { get; set; }

        public bool? DomandaAccolta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alunni> Alunni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlunniListaAttesa> AlunniListaAttesa { get; set; }
    }
}
