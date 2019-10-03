using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTD.Data;
using GTD.Models;

namespace GTD.Controllers
{
    public class DiarioController : Controller
    {
        private readonly GTDContext _context;

        public DiarioController(GTDContext context)
        {
            _context = context;
        }

        // GET: Diario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diario.ToListAsync());
        }

    }
}
