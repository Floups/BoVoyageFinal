﻿using System;
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

        public int Id { get; set; }
        public int IdDestination { get; set; }
        [DataType(DataType.Date), Display(Name = "Date de départ")]
        public DateTime DateDepart { get; set; }
        [DataType(DataType.Date), Display(Name = "Date de retour")]
        public DateTime DateRetour { get; set; }
        [Display(Name = "Nombre de places disponibles")]
        public int PlacesDispo { get; set; }
        [Display(Name = "Prix HT"),DataType(DataType.Currency)]
        public decimal PrixHt { get; set; }
        public decimal Reduction { get; set; }
        public string Descriptif { get; set; }

        [Display(Name ="Destination")]
        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
