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

        [Display(Name = "IdDestination")]
        public int Id { get; set; }

        [Display(Name = "IdDestinationParente")]
        public int? IdParente { get; set; }

        [Display(Name = "Nom de la destination")]
        [Required(ErrorMessage = "Champ requis.")]
        public string Nom { get; set; }

        [Display(Name = "Type de destination")]
        [Required(ErrorMessage = "Champ requis.")]
        public byte Niveau { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Champ requis.")]
        public string Description { get; set; }

        [Display(Name = "IdDestinationParente")]
        public virtual Destination IdParenteNavigation { get; set; }
        public virtual ICollection<Destination> InverseIdParenteNavigation { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<Voyage> Voyage { get; set; }
    }
}
