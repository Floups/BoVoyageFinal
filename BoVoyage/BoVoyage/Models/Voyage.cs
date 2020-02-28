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
        public int IdDestination { get; set; }

        [DataType(DataType.Date), Display(Name = "Date de départ")]
        public DateTime DateDepart { get; set; }

        [DataType(DataType.Date), Display(Name = "Date de retour")]
        public DateTime DateRetour { get; set; }

        [Display(Name = "Nombre de places disponibles")]
        public int PlacesDispo { get; set; }

        [Display(Name = "Prix HT"),DataType(DataType.Currency)]
        public decimal PrixHt { get; set; }

        [Display(Name = "Taux de réduction")]
        public decimal Reduction { get; set; }

        [Display(Name = "Descriptif du voyage")]
        public string Descriptif { get; set; }

        [Display(Name ="Destination")]
        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
