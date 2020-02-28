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
            
            return View();
        }

    }
}
