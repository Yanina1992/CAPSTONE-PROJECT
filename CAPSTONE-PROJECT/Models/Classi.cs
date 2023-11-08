namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Classi")]
    public partial class Classi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Classi()
        {
            Alunni = new HashSet<Alunni>();
        }

        [Key]
        public int IdClasse { get; set; }

        [Required]
        [StringLength(1)]
        public string Anno { get; set; }

        [StringLength(1)]
        public string Sezione { get; set; }

        [StringLength(10)]
        public string AnnoScolastico { get; set; }

        [NotMapped]
        public bool ConfermaClasse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alunni> Alunni { get; set; }
    }
}
