using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyage.Models
{
    public partial class Client
    {
        public Client()
        {
            Dossierresa = new HashSet<Dossierresa>();
        }

        [Display (Name ="IdClient")]
        public int Id { get; set; }

        [Display(Name = "IdClient")]
        public virtual Personne IdNavigation { get; set; }

        [Display(Name = "IdDossier")]
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
    }
}
