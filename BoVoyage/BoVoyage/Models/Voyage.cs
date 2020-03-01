using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Voyage
    {
        public Voyage()
        {
            Dossierresa = new HashSet<Dossierresa>();
            Voyageur = new HashSet<Voyageur>();
        }
        [Display(Name = "IdVoyage")]
        public int Id { get; set; }

        [Display(Name = "IdDestination")]
        [Required(ErrorMessage = "Champ requis.")]
        public int IdDestination { get; set; }

        [DataType(DataType.Date), Display(Name = "Date de départ")]
        [Required(ErrorMessage = "Champ requis.")]
        public DateTime DateDepart { get; set; }

        [DataType(DataType.Date), Display(Name = "Date de retour")]
        [Required(ErrorMessage = "Champ requis.")]
        public DateTime DateRetour { get; set; }

        [Display(Name = "Nombre de places disponibles")]
        [Required(ErrorMessage = "Champ requis.")]
        public int PlacesDispo { get; set; }

        [Display(Name = "Prix HT"),DataType(DataType.Currency)]
        [Required(ErrorMessage = "Champ requis.")]
        public decimal PrixHt { get; set; }

        [Display(Name = "Taux de réduction")]
        [Required(ErrorMessage = "Champ requis.")]
        public decimal Reduction { get; set; }

        [Display(Name = "Descriptif du voyage")]
        public string Descriptif { get; set; }

        [Display(Name ="Destination")]
        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
