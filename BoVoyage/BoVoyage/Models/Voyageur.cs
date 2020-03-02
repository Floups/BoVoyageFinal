using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Voyageur
    {
        [Display(Name = "IdVoyageur")]
        public int Id { get; set; }

        [Display(Name = "IdVoyage")]
        public int Idvoyage { get; set; }

        public virtual Personne IdNavigation { get; set; }
        public virtual Voyage IdvoyageNavigation { get; set; }
    }
}
