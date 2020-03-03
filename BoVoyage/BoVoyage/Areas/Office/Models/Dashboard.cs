using BoVoyage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyage.Areas.Office.Models
{
    
    public class Dashboard
    {
        public int Id { get; set; }
        public List<Dossierresa> Reservations { get; set; }
        public List<Voyage> Voyages { get; set; }

    }
}
