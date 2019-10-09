using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTD.Data;
using GTD.Models;
using GTD.ViewModels;

namespace GTD.Controllers
{
    public class DietaController : Controller
    {
        private readonly GTDContext _context;

        public DietaController(GTDContext context)
        {
            _context = context;
        }

        // GET: Dieta
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dieta.ToListAsync());
        }

        // GET: Dieta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta
                .FirstOrDefaultAsync(m => m.DietaID == id);
            if (dieta == null)
            {
                return NotFound();
            }

            return View(dieta);
        }

        // GET: Dieta/Create
        public IActionResult Create()
        {
            // Começa a usar ViewModel para juntar as três tabelas
            // Funciona fazendo gambiarra com todas essas informações
            DietaSemanaViewModel dsvm = new DietaSemanaViewModel();
            dsvm.Semanas = _context.Semana.Select( v => new SelectListItem { Text = v.SemanaNum.ToString(), Value = v.SemanaID.ToString() }).ToList();
            return View(dsvm);
        }

        // POST: Dieta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Semana,DietaID,DietaNome,DescDieta,DataDieta,Completo")] DietaSemanaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Dieta dieta = new Dieta();
                Semana semana = new Semana();
                DietaSemana dietaSemana = new DietaSemana();

                dieta.Completo = vm.Completo;
                dieta.DataDieta = vm.DataDieta;
                dieta.DietaNome = vm.DietaNome;

                semana.SemanaID = vm.SemanaID;

                dietaSemana.DescDieta = vm.Texto;

                semana.DietaSemana = dieta.DietaSemana;

                _context.Dieta.Add(dieta);
                await _context.SaveChangesAsync();

                //_context.Add(dieta);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Dieta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }
            return View(dieta);
        }

        // POST: Dieta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("DietaID,DietaNome,DescDieta,DataDieta,Completo")] Dieta dieta)
        {
            if (id != dieta.DietaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dieta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietaExists(dieta.DietaID))
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
            return View(dieta);
        }

        // GET: Dieta/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var dieta = await _context.Dieta
        //        .FirstOrDefaultAsync(m => m.DietaID == id);
        //    if (dieta == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(dieta);
        //}

        // POST: Dieta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var dieta = await _context.Dieta.FindAsync(id);
            _context.Dieta.Remove(dieta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DietaExists(int? id)
        {
            return _context.Dieta.Any(e => e.DietaID == id);
        }

        // função pra adicionar semana
        public void AddOneWeek()
        {
            var umaSemana = new Semana()
            {
                SemanaNum = _context.Semana.Max(s => s.SemanaNum) + 1,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(7)
            };
            _context.Semana.Add(umaSemana);
            _context.SaveChanges();
        }
    }
}
