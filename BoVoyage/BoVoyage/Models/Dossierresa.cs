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
        [Required(ErrorMessage = "Champ requis.")]
        [CreditCard (ErrorMessage = "Carte bancaire non reconnue.")]
        public string NumeroCb { get; set; }

        [Display(Name = "IdClient")]
        [Required(ErrorMessage = "Champ requis.")]
        public int IdClient { get; set; }

        [Display(Name = "Etat du dossier")]
        [Required(ErrorMessage = "Champ requis.")]
        public byte IdEtatDossier { get; set; }

        [Display(Name = "IdVoyage")]
        [Required(ErrorMessage = "Champ requis.")]
        public int IdVoyage { get; set; }

        [Display(Name = "Prix total")]
        [Required(ErrorMessage = "Champ requis.")]
        [DataType(DataType.Currency)]
        public decimal PrixTotal { get; set; }

        [Required(ErrorMessage = "Champ requis.")]
        public bool Assurance { get; set; }

        [Display(Name = "IdClient")]
        public virtual Client IdClientNavigation { get; set; }

        [Display(Name = "Etat du dossier")]
        public virtual Etatdossier IdEtatDossierNavigation { get; set; }

        [Display(Name = "IdVoyage")]
        public virtual Voyage IdVoyageNavigation { get; set; }
    }
}
