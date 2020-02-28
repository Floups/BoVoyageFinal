using BoVoyage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyage.Areas.Client.Models
{
    public class VoyagePersonnesViewModel
    {
        public int Id { get; set; }

        public Voyage Voyage { get; set; }

        public List<Personne> Voyageurs { get; set; }

        public VoyagePersonnesViewModel()
        {

        }
        public VoyagePersonnesViewModel(Voyage voyage, List<Personne> voyageurs)
        {
            Voyage = voyage;
            Voyageurs = voyageurs;
        }
    }
}
