using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Dossierresa
    {
        [Display(Name = "IdDossier")]
        public int Id { get; set; }

        [Display(Name = "Numéro carte bancaire")]
        public string NumeroCb { get; set; }

        [Display(Name = "IdClient")]
        public int IdClient { get; set; }

        [Display(Name = "Etat du dossier")]
        public byte IdEtatDossier { get; set; }

        [Display(Name = "IdVoyage")]
        public int IdVoyage { get; set; }

        [Display(Name = "Prix total")]
        public decimal PrixTotal { get; set; }

        [Display(Name = "IdClient")]
        public virtual Client IdClientNavigation { get; set; }

        [Display(Name = "Etat du dossier")]
        public virtual Etatdossier IdEtatDossierNavigation { get; set; }

        [Display(Name = "IdVoyage")]
        public virtual Voyage IdVoyageNavigation { get; set; }
    }
}
