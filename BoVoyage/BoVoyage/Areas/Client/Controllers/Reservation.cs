using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoVoyage.Areas.Client.Models;
using BoVoyage.Models;

namespace BoVoyage.Areas.Client.Controllers
{
    [Area("Client")]
    public class Reservation : Controller
    {
        private readonly BoVoyageContext _context;

        public Reservation(BoVoyageContext context)
        {
            _context = context;
        }

        // GET: Client/Reservation
        public async Task<IActionResult> Index(int idVoyage, int nbPersonnes)
        {
            var voyage = await _context.Voyage.Where(v => v.Id == idVoyage).Include(v => v.IdDestinationNavigation).FirstOrDefaultAsync();
            List<Personne> voyageurs = new List<Personne>();
            for (int i = 0; i < nbPersonnes; i++)
            {
                voyageurs.Add(new Personne());
            }

            var voyagePersonnes = new VoyagePersonnesViewModel(voyage, voyageurs);

            return View(voyagePersonnes);
        }

        //public IActionResult AjoutVoyageur([Bind("Civilite", "Nom", "Prenom", "Datenaissance", "Email", "Telephone")] Personne personne, [FromQuery] int idVoyage, [FromQuery] int nbPersonnes, List<Personne> personnes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        personnes.Add(personne);

        //        return RedirectToAction(nameof(Index));
        //    }
        //}       
      
    }
}
