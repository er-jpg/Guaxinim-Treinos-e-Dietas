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

namespace GTD.Controllers
{
    [Authorize]
    public class DiarioController : Controller
    {
        private readonly GTDContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DiarioController(GTDContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Diario
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var planoCom = await _context.Plano
                .Include(d => d.Diario)
                .Where(s => s.Selecionado == true)
                .Where(u => u.UserID == user.Id)
                .ToListAsync();

            var diario = await _context.Diario
                .Include(p => p.Plano)
                .Include(t => t.Plano.Treino)
                .Include(ts => ts.Plano.Treino.TreinoSemana)
                .Include(d => d.Plano.Dieta)
                .Include(ds => ds.Plano.Dieta.DietaSemana)
                .ToListAsync();

            if (planoCom.Count != 1)
            {
                ViewBag.Erro = "Favor selecionar um plano como atual.";
                return View();
            }
            else
            {
                if (diario.Count == 0)
                {
                    CreateDiario(user.Id, DateTime.Now);
                    var diarioAfter = await _context.Diario
                        .Include(p => p.Plano)
                        .ToListAsync();
                }
                return View(diario.ElementAtOrDefault(0));
            }
            //return View(await _context.Diario.ToListAsync());
        }

        // Cria o diário
        public void CreateDiario(string id, DateTime dtDay)
        {
            var planoCom = _context.Plano
                .Include(d => d.Diario)
                .Where(s => s.Selecionado == true)
                .Where(u => u.UserID == id)
                .ToList();

            var dd = new Diario
            {
                DataDiario = dtDay,
                PlanoID = planoCom.ElementAt(0).PlanoID
            };

            _context.Diario.Add(dd);
            _context.SaveChanges();

        }

        [HttpPost]
        public async void CompleteTreino(int id)
        {
            var diario = _context.Diario.Where(x => x.DiarioID == id).FirstOrDefault();
            diario.CompletoTreino = true;
            _context.Update(diario);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async void CompleteDieta(int id)
        {
            var diario = _context.Diario.Where(x => x.DiarioID == id).FirstOrDefault();
            diario.CompletoDieta = true;
            _context.Update(diario);
            await _context.SaveChangesAsync();
        }
    }
}
