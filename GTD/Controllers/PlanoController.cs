using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTD.Data;
using GTD.Models;
using Microsoft.AspNetCore.Identity;
using GTD.Models.Infra;
using Microsoft.AspNetCore.Authorization;
using GTD.ViewModels;

namespace GTD.Controllers
{
    [Authorize]
    public class PlanoController : Controller
    {
        private readonly GTDContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PlanoController(GTDContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Plano
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var planos = await _context.Plano.Where(m => m.UserID == user.Id).ToListAsync();
            return View(planos);
        }

        // GET: Plano/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Plano
                .FirstOrDefaultAsync(m => m.PlanoID == id);
            if (plano == null)
            {
                return NotFound();
            }

            ViewBag.Dieta = _context.Plano.Include(x => x.Dieta).FirstOrDefaultAsync(x => x.PlanoID == id);
            ViewBag.Treino = _context.Plano.Include(x => x.Treino).FirstOrDefaultAsync(x => x.PlanoID == id);
            return View(plano);
        }

        // GET: Plano/Create
        public IActionResult Create()
        {
            //ViewBag.Treinos = new SelectList(_context.Treino.ToList(), "TreinoID", "TreinoNome");
            //ViewBag.Dietas = new SelectList(_context.Dieta.ToList(), "DietaID", "DietaNome");
            var treinos = _context.Treino.OrderBy(i => i.TreinoID).ToList();
            treinos.Insert(0, new Treino() { TreinoID = 0, TreinoNome = "Selecione o treino" });

            var dietas = _context.Dieta.OrderBy(i => i.DietaID).ToList();
            dietas.Insert(0, new Dieta() { DietaID = 0, DietaNome = "Selecione a dieta" });

            ViewBag.Treinos = treinos;
            ViewBag.Dietas = dietas;

            return View();
        }

        // POST: Plano/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanoID,TreinoID,DietaID,SemanaInicio,Selecionado,PlanoNome,Duracao,Completo")] Plano param)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                param.UserID = user.Id;

                _context.Add(param);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(param);
        }

        // GET: Plano/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Plano.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }

            var treinos = _context.Treino.OrderBy(i => i.TreinoID).ToList();
            treinos.Insert(0, new Treino() { TreinoID = 0, TreinoNome = "Selecione o treino" });

            var dietas = _context.Dieta.OrderBy(i => i.DietaID).ToList();
            dietas.Insert(0, new Dieta() { DietaID = 0, DietaNome = "Selecione a dieta" });

            ViewBag.Treinos = treinos;
            ViewBag.Dietas = dietas;
            return View(plano);
        }

        // POST: Plano/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PlanoID,TreinoID,DietaID,SemanaInicio,Selecionado,PlanoNome,Duracao,Completo")] Plano param)
        {
            if (id != param.PlanoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    param.UserID = user.Id;
                    _context.Update(param);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoExists(param.PlanoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(param);
        }

        // POST: Plano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var plano = await _context.Plano.FindAsync(id);
            _context.Plano.Remove(plano);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoExists(int? id)
        {
            return _context.Plano.Any(e => e.PlanoID == id);
        }
    }
}
