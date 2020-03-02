using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Authorization;

namespace BoVoyage.Areas.Client.Controllers
{
    [Area("Client")]
    public class VoyagesController : Controller
    {
        private readonly BoVoyageContext _context;

        public VoyagesController(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Client/Voyages
        public async Task<IActionResult> Index(int? dest, decimal prixMin, decimal prixMax,DateTime dateMin, DateTime dateMax)
        {
            var listeDestinations = await _context.Destination.ToListAsync();
            ViewBag.Destinations = new SelectList(listeDestinations, "Id", "Nom",dest);
            ViewBag.PrixMin = prixMin;
            ViewBag.PrixMax = prixMax;
            
            IQueryable<Voyage> voyages = _context.Voyage.Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo);
            
            if (dest != null && dest!=0)
                voyages = voyages.Where(v => v.IdDestinationNavigation.Id == dest);
            if (prixMin != 0 && prixMax == 0)
                voyages = voyages.Where(v => v.PrixHt >= prixMin);
            if (prixMin == 0 && prixMax != 0)
                voyages = voyages.Where(v => v.PrixHt <= prixMax);
            if (prixMin != 0 && prixMax != 0)
                voyages =  voyages.Where(v => v.PrixHt >= prixMin && v.PrixHt <= prixMax);
            if (dateMin != DateTime.MinValue && dateMax == DateTime.MinValue)
            {
                ViewBag.DateMin = dateMin.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart >= dateMin);
            }
            if (dateMin == DateTime.MinValue && dateMax != DateTime.MinValue)
            {
                ViewBag.DateMax = dateMax.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart <= dateMax);
            }
            if (dateMin != DateTime.MinValue && dateMax != DateTime.MinValue)
            {
                ViewBag.DateMin = dateMin.ToString("yyyy-MM-dd");
                ViewBag.DateMax = dateMax.ToString("yyyy-MM-dd");
                voyages = voyages.Where(v => v.DateDepart >= dateMin && v.DateDepart <= dateMax);
            }


            var listeVoyages = await voyages.ToListAsync();            

            return View(listeVoyages);
        }

        // GET: Client/Voyages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Le voyage demandé ne se trouve pas dans notre base de données.");
            }

            var voyage = await _context.Voyage
                .Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound("Le voyage demandé ne se trouve pas dans notre base de données.");
            }

            return View(voyage);
        }      
    }
}
