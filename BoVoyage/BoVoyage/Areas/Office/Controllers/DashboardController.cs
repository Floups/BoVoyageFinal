using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Office.Models;
using BoVoyage.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]
    [Authorize(Roles = "Admin, Manager")]
    public class DashboardController : Controller
    {
        private readonly BoVoyageContext _context;

        public DashboardController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/ReservationVoyagesViewModels
        public async Task<IActionResult> Index()
        {
            var listeResa = await _context.Dossierresa
                .Include(d=>d.IdVoyageNavigation).ThenInclude(v=>v.IdDestinationNavigation)
                .Include(d => d.IdEtatDossierNavigation)
                .Where(e => e.IdEtatDossier == 2)
                .AsNoTracking()
                .ToListAsync();

            var listeVoyages = await _context.Voyage
                .Include(v => v.IdDestinationNavigation)
                .Where(v => v.PlacesDispo != 0 && v.DateDepart <= DateTime.Now.AddDays(15) && v.DateDepart > DateTime.Now)
                .AsNoTracking()
                .ToListAsync();

            var ResaVoy = new Dashboard() { Reservations = listeResa, Voyages= listeVoyages };

                return View(ResaVoy);
        }

        // GET: Office/Voyages/Details/5
        public async Task<IActionResult> Details(int? id, int? id2)
        {
            var elmtDashboard = new Dashboard() { Reservations = new List<Dossierresa>(), Voyages = new List<Voyage>() };
            if (id == null && id2 == null)
            {
                return NotFound();
            }

            if (id != null && id2 == null)
            {
                var resa = await _context.Dossierresa
                 .Include(d => d.IdVoyageNavigation).ThenInclude(v => v.IdDestinationNavigation)
                 .Include(r => r.IdEtatDossierNavigation)
                 .Include(r => r.IdClientNavigation).ThenInclude(c=>c.IdNavigation)
                 .FirstOrDefaultAsync(r => r.Id == id);
                elmtDashboard.Reservations.Add(resa);
            }

            if (id == null && id2 != null)
            {
                var voyage = await _context.Voyage
                   .Include(v => v.IdDestinationNavigation)
                   .FirstOrDefaultAsync(m => m.Id == id2);
                elmtDashboard.Voyages.Add(voyage);
            }

            if (elmtDashboard.Reservations == null || elmtDashboard.Voyages == null)
            {
                return NotFound();
            }

            return View(elmtDashboard);
        }

      

       
    }
}
