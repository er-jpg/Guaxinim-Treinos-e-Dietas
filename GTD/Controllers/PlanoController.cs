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

            return View(plano);
        }

        // GET: Plano/Create
        public IActionResult Create()
        {
            //ViewBag.Treinos = new SelectList(_context.Treino.ToList(), "TreinoID", "TreinoNome");
            //ViewBag.Dietas = new SelectList(_context.Dieta.ToList(), "DietaID", "DietaNome");
            var plano = new PlanoViewModel
            {
                Treinos = from t in _context.Treino
                          select new SelectListItem
                          {
                              Selected = t.TreinoID.ToString() == "Ativo",
                              Text = t.TreinoNome,
                              Value = t.TreinoNome
                          },

                Dietas = from d in _context.Dieta
                         select new SelectListItem
                         {
                             Selected = d.DietaID.ToString() == "Ativo",
                             Text = d.DietaNome,
                             Value = d.DietaNome
                         },
            };

            return View(plano);
        }

        // POST: Plano/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanoID,TreinoID,DietaID,Selecionado,PlanoNome,Duracao,Completo")] PlanoViewModel param)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                param.UserID = user.Id;

                Plano plano = new Plano
                {
                    ApplicationUser = param.ApplicationUser,
                    UserID = param.UserID,
                    Completo = param.Completo,
                    DietaID = param.DietaID,
                    TreinoID = param.TreinoID,
                    Duracao = param.Duracao,
                    PlanoNome = param.PlanoNome,
                    Selecionado = param.Selecionado,
                    SemanaInicio = param.SemanaInicio
                };

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
            return View(plano);
        }

        // POST: Plano/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PlanoID,PlanoNome,Duracao,Completo")] Plano plano)
        {
            if (id != plano.PlanoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    plano.UserID = user.Id;
                    _context.Update(plano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoExists(plano.PlanoID))
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
            return View(plano);
        }

        // GET: Plano/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(plano);
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
