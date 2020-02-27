using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoVoyage.Models;

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

        public IActionResult Index()
        {
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
            var pers = _context.Personne.Where(p => p.Email == newContact.Email).FirstOrDefault();
            
            if (pers == null)
            {
                Personne personne = new Personne()
                {
                    Civilite = newContact.Civilite,
                    Nom = newContact.Nom,
                    Prenom = newContact.Prenom,
                    Telephone = newContact.Telephone,
                    Email = newContact.Email
                };

                if (ModelState.IsValid)
                {
                    _context.Personne.Add(personne);
                    await _context.SaveChangesAsync();
                    
                    //return RedirectToAction(nameof(Contact));
                }
            }
            if(ModelState.IsValid && newContact.SujetMessage!=null && newContact.Message!=null)
            ViewBag.Valid = "Le message a bien été envoyé.";
            return View("Contact", newContact);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
