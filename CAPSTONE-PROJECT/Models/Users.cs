namespace CAPSTONE_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int IdUser { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "Sei un amministratore?")]
        public int FKRuolo { get; set; }

        public virtual Ruoli Ruoli { get; set; }
    }
}
