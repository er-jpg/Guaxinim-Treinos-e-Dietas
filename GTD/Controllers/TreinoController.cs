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
    public class TreinoController : Controller
    {
        private readonly GTDContext _context;

        public TreinoController(GTDContext context)
        {
            _context = context;
        }

        // GET: Treino
        public async Task<IActionResult> Index()
        {
            return View(await _context.Treino.ToListAsync());
        }

        // GET: Treino/Details/5
        public async Task<IActionResult> Details(int? id, int? semana)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino.FirstOrDefaultAsync(m => m.TreinoID == id);

            if (semana == null) semana = 1;
            var treinoSemana = await _context.TreinoSemana.FirstOrDefaultAsync(m => m.SemanaID == semana);
            if (treino == null || treinoSemana == null)
            {
                return NotFound();
            }

            TreinoSemanaViewModel vm = new TreinoSemanaViewModel
            {
                Completo = treino.Completo,
                DataTreino = treino.DataTreino??DateTime.Now,
                TreinoID = treino.TreinoID,
                TreinoNome = treino.TreinoNome,
                SemanaID = treinoSemana.SemanaID,
                Texto = treinoSemana.DescTreino
            };

            return View(vm);
        }

        // GET: Treino/Create
        public IActionResult Create()
        {
            TreinoSemanaViewModel vm = new TreinoSemanaViewModel();
            ViewBag.Semana = 1;
            return View(vm);
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemanaID,TreinoID,TreinoNome,Texto,DataTreino,Completo")] TreinoSemanaViewModel vm, string salvar)
        {
            if (ModelState.IsValid)
            {
                Treino treino = new Treino
                {
                    Completo = vm.Completo,
                    DataTreino = vm.DataTreino,
                    TreinoNome = vm.TreinoNome
                };

                _context.Treino.Add(treino);

                if (vm.SemanaID == null) vm.SemanaID = 1;

                TreinoSemana treinoSemana = new TreinoSemana
                {
                    Treino = treino,
                    SemanaID = vm.SemanaID,
                    DescTreino = vm.Texto
                };

                _context.TreinoSemana.Add(treinoSemana);

                await _context.SaveChangesAsync();

                if (salvar.Equals("Próxima Semana"))
                {
                    return RedirectToAction("Edit", new { id = _context.Treino.Max(o => o.TreinoID), semana = 2 });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vm);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(int? id, int? semana)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (semana == null)
            {
                semana = 1;
            }

            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }

            TreinoSemanaViewModel vm = new TreinoSemanaViewModel
            {
                TreinoID = treino.TreinoID,
                DataTreino = treino.DataTreino??DateTime.Now,
                Completo = treino.Completo,
                TreinoNome = treino.TreinoNome
            };

            var treinoSemana = await _context.TreinoSemana.Where(x => x.TreinoID == id).FirstOrDefaultAsync();
            vm.Texto = treinoSemana.DescTreino;
            vm.SemanaID = semana;

            if (TempData["SemanaID"] == null)
            {
                ViewBag.Semana = semana;
            }
            else
            {
                ViewBag.Semana = TempData["SemanaID"];
            }

            return View(vm);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string salvar, [Bind("SemanaID,TreinoID,TreinoNome,Texto,DataTreino,Completo")] TreinoSemanaViewModel vm)
        {
            if (id != vm.TreinoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Treino treino = new Treino
                    {
                        TreinoID = vm.TreinoID,
                        Completo = vm.Completo,
                        DataTreino = vm.DataTreino,
                        TreinoNome = vm.TreinoNome
                    };
                    _context.Update(treino);

                    TreinoSemana treinoSemana = new TreinoSemana
                    {
                        SemanaID = vm.SemanaID,
                        DescTreino = vm.Texto,
                        TreinoID = vm.TreinoID
                    };

                    if (_context.TreinoSemana.Any(o => o.SemanaID == vm.SemanaID))
                    {
                        _context.Update(treinoSemana);
                    }

                    else
                    {
                        if (_context.Semana.Any(o => o.SemanaID == vm.SemanaID))
                            AddOneWeek();

                        _context.TreinoSemana.Add(treinoSemana);
                    }


                    await _context.SaveChangesAsync();

                    if (salvar.Equals("Próxima Semana"))
                    {
                        if (vm.SemanaID == null) vm.SemanaID = 1;
                        TempData["SemanaID"] = vm.SemanaID + 1;
                        vm.SemanaID++;
                        return RedirectToAction("Edit", new { id = vm.TreinoID, semana = vm.SemanaID });
                    }

                    else if (salvar.Equals("Salvar"))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinoExists(vm.TreinoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var treino = await _context.Treino.FindAsync(id);
            _context.Treino.Remove(treino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(int? id)
        {
            return _context.Treino.Any(e => e.TreinoID == id);
        }

        // função pra adicionar semana
        private void AddOneWeek()
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
