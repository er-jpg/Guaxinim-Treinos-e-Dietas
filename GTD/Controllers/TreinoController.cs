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
        public async Task<IActionResult> Details(int? id, int? semanaID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino.FirstOrDefaultAsync(m => m.TreinoID == id);
            var treinoSemana = await _context.TreinoSemana.FirstOrDefaultAsync(m => m.SemanaID == semanaID);
            if (treino == null || treinoSemana == null)
            {
                return NotFound();
            }

            TreinoSemanaViewModel vm = new TreinoSemanaViewModel
            {
                Completo = treino.Completo,
                DataTreino = treino.DataTreino,
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
            TreinoSemanaViewModel vm = new TreinoSemanaViewModel
            {
                SemanaID = 1
            };
            return View(vm);
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemanaID,TreinoID,TreinoNome,Texto,DataTreino,Completo")] TreinoSemanaViewModel vm)
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

                TreinoSemana treinoSemana = new TreinoSemana
                {
                    Treino = treino,
                    SemanaID = 1,
                    DescTreino = vm.Texto
                };

                _context.TreinoSemana.Add(treinoSemana);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(int? id, int? semanaID)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (semanaID == null)
            {
                semanaID = 1;
            }

            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }

            TreinoSemanaViewModel vm = new TreinoSemanaViewModel
            {
                TreinoID = treino.TreinoID,
                DataTreino = treino.DataTreino,
                Completo = treino.Completo,
                TreinoNome = treino.TreinoNome
            };

            var treinoSemana = await _context.TreinoSemana.Where(x => x.TreinoID == id).FirstOrDefaultAsync();
            vm.Texto = treinoSemana.DescTreino;
            vm.SemanaID = semanaID;

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
                    _context.Update(treinoSemana);
                    await _context.SaveChangesAsync();

                    if (salvar.Equals("Próxima Semana"))
                    {
                        return View(Edit(vm.TreinoID, vm.SemanaID + 1));
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
