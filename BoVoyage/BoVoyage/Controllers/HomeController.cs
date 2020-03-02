using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoVoyage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace BoVoyage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BoVoyageContext _context;

        public HomeController(ILogger<HomeController> logger, BoVoyageContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var voyagesMoinsCher = await _context.Voyage.OrderBy(v => v.PrixHt).Take(5).Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).ToListAsync();
            ViewBag.VoyagesMoinsCher = voyagesMoinsCher;

            ViewBag.VoyagesDateProche = await _context.Voyage.OrderBy(v => v.DateDepart).Take(5).Include(v => v.IdDestinationNavigation).ThenInclude(d => d.Photo).ToListAsync();

            var idRegionNbvoyage = new List<int>();
            var regionNbVoyage = new List<Destination>();

            using (var cnx = (SqlConnection)_context.Database.GetDbConnection())
            {

                //On récupère les id des régions avec le plus de voyage
                var cmd = new SqlCommand(@"select idDestination from Voyage group by IdDestination order by Count(*) desc ", cnx);

                cnx.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        idRegionNbvoyage.Add((int)sdr["IdDestination"]);
                    }

                }
                foreach (var item in idRegionNbvoyage)
                {
                    regionNbVoyage.Add(_context.Destination.Include(d => d.Photo).Where(d => d.Id == item).FirstOrDefault());
                }
            }

            ViewBag.RegionNbVoyage = regionNbVoyage;
            return View();
        }

        public IActionResult AProposDe()
        {
            return View();
        }

        public IActionResult Contact(ContactViewModel newContact)
        {

            return View(newContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Civilite", "Nom", "Prenom", "Telephone", "Email", "SujetMessage", "Message")] ContactViewModel newContact)
        {
            if (ModelState.IsValid)
            {
                var pers = _context.Personne.Where(p => p.Email == newContact.Email).FirstOrDefault();
                if (pers == null)
                {
                    Personne personne = new Personne()
                    {
                        Civilite = newContact.Civilite,
                        Nom = newContact.Nom,
                        Prenom = newContact.Prenom,
                        Telephone = newContact.Telephone,
                        Email = newContact.Email,
                        TypePers = 3
                    };

                    _context.Personne.Add(personne);
                    await _context.SaveChangesAsync();

                    //return RedirectToAction(nameof(Index));
                }
                if (newContact.SujetMessage != null && newContact.Message != null)
                    ViewBag.Valid = "Le message a bien été envoyé.";

            }


            return View("Contact", newContact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
