using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Destination
    {
        public Destination()
        {
            InverseIdParenteNavigation = new HashSet<Destination>();
            Photo = new List<Photo>();
            Voyage = new HashSet<Voyage>();
        }
        public int Id { get; set; }
        [Display(Name ="Pays")]
        public int? IdParente { get; set; }
        [StringLength(100)]
        public string Nom { get; set; }
        [Range(1,3)]
        public byte Niveau { get; set; }
        [StringLength(1500)]
        public string Description { get; set; }

        [Display(Name = "Pays")]
        public virtual Destination IdParenteNavigation { get; set; }
        public virtual ICollection<Destination> InverseIdParenteNavigation { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<Voyage> Voyage { get; set; }
    }
}
