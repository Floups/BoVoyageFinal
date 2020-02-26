using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BoVoyage.Models;
using Microsoft.EntityFrameworkCore;

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
            var voyagesMoinsCher = await _context.Voyage.OrderByDescending(v => v.PrixHt).Take(5).ToListAsync();
            ViewBag.VoyagesMoinsCher = voyagesMoinsCher;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
