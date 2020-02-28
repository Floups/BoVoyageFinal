using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Office.Models;
using BoVoyage.Models;

namespace BoVoyage.Areas.Office.Controllers
{
    [Area("Office")]

    public class ReservationVoyagesViewModelsController : Controller
    {
        private readonly BoVoyageContext _context;

        public ReservationVoyagesViewModelsController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Office/ReservationVoyagesViewModels
        public async Task<IActionResult> Index()
        {
            var listeResa = await _context.Dossierresa
                .Include(d => d.IdEtatDossierNavigation)
                .Where(e => e.IdEtatDossier == 2)
                .AsNoTracking()
                .ToListAsync();
            var listeVoyages = await _context.Voyage
                .Include(v=>v.IdDestinationNavigation)
                .Where(v => v.PlacesDispo != 0 && v.DateDepart <= DateTime.Now.AddDays(15) && v.DateDepart>DateTime.Now)
                .AsNoTracking()
                .ToListAsync();
            var ResaVoy = new ReservationVoyagesViewModel() { Reservations = listeResa, Voyages = listeVoyages };

            return View(ResaVoy);
        }

        // GET: Office/Voyages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage
                .Include(v => v.IdDestinationNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }
    }
}
