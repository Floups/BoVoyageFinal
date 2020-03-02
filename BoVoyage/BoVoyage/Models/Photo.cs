using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Photo
    {
        [Display(Name = "IdPhoto")]
        public int Id { get; set; }

        [Display(Name = "Nom du fichier")]
        public string NomFichier { get; set; }

        [Display(Name = "IdDestination")]
        public int IdDestination { get; set; }

        [Display(Name = "IdDestination")]
        public virtual Destination IdDestinationNavigation { get; set; }
    }
}
