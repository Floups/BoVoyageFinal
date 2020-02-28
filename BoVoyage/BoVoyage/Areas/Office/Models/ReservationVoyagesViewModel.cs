using BoVoyage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BoVoyage.Areas.Office.Models
{
    
    public class ReservationVoyagesViewModel
    {
        public int Id { get; set; }
        public List<Dossierresa> Reservations { get; set; }
        public List<Voyage> Voyages { get; set; }

    }
}
