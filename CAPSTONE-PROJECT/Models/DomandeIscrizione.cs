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
        [Display(Name = "Domanda n.")]
        public int IdDomanda { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Nome alunno")]
        public string NomeAlunno { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Cognome alunno")]
        public string CognomeAlunno { get; set; }

        [StringLength(16)]
        [Required]
        [Display(Name = "Codice fiscale alunno")]
        public string CFAlunno { get; set; }

        [StringLength(2)]
        [Required]
        [Display(Name = "Età alunno")]
        public string Eta { get; set; }

        [StringLength(150)]
        [Display(Name = "Indicare eventuali allergie")]
        public string Allergie { get; set; }

        public bool? Bilinguismo { get; set; }

        public bool? Assicurazione { get; set; }

        [StringLength(16)]
        [Display(Name = "Codice fiscale genitore (o tutore)")]
        public string CFPapa { get; set; }

        [StringLength(16)]
        [Required]
        [Display(Name = "Codice fiscale genitore 2 (o tutore)")]
        public string CFMamma { get; set; }

        [Column(TypeName = "money")]
        public decimal? Isee { get; set; }

        [Display(Name = "Domanda accolta")]
        public bool? DomandaAccolta { get; set; }

        public virtual bool? Mensa { get; set; }

        [Display(Name = "Trasporto scolastico")]
        public virtual bool? TrasportoScolastico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alunni> Alunni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlunniListaAttesa> AlunniListaAttesa { get; set; }
    }
}
