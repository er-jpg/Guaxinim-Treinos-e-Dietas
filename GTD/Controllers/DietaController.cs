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
        public async Task<IActionResult> Details(int? id, int? semanaID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta.FirstOrDefaultAsync(m => m.DietaID == id);
            var dietaSemana = await _context.DietaSemana.FirstOrDefaultAsync(m => m.SemanaID == id);
            if (dieta == null || dietaSemana == null)
            {
                return NotFound();
            }

            DietaSemanaViewModel vm = new DietaSemanaViewModel
            {
                Completo = dieta.Completo,
                DataDieta = dieta.DataDieta,
                DietaID = dieta.DietaID,
                DietaNome = dieta.DietaNome,
                SemanaID = dietaSemana.SemanaID,
                Texto = dietaSemana.DescDieta
            };

            return View(vm);
        }

        // GET: Dieta/Create
        public IActionResult Create()
        {
            // Começa a usar ViewModel para juntar as três tabelas
            // Funciona fazendo gambiarra com todas essas informações
            DietaSemanaViewModel dsvm = new DietaSemanaViewModel
            {
                SemanaID = 1
            };
            return View(dsvm);
        }

        // POST: Dieta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemanaID,DietaID,DietaNome,Texto,DataDieta,Completo")] DietaSemanaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Dieta dieta = new Dieta
                {
                    Completo = vm.Completo,
                    DataDieta = vm.DataDieta,
                    DietaNome = vm.DietaNome
                };

                _context.Dieta.Add(dieta);

                DietaSemana dietaSemana = new DietaSemana
                {
                    Dieta = dieta,
                    SemanaID = 1,
                    DescDieta = vm.Texto
                };

                _context.DietaSemana.Add(dietaSemana);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Dieta/Edit/5
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

            var dieta = await _context.Dieta.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }

            DietaSemanaViewModel dsvm = new DietaSemanaViewModel
            {
                DietaID = dieta.DietaID,
                DataDieta = dieta.DataDieta,
                Completo = dieta.Completo,
                DietaNome = dieta.DietaNome
            };

            var dietaSemana = await _context.DietaSemana.Where(x => x.DietaID == id).FirstAsync();
            dsvm.Texto = dietaSemana.DescDieta;
            dsvm.SemanaID = semanaID;

            return View(dsvm);
        }

        // POST: Dieta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string salvar,[Bind("SemanaID,DietaID,DietaNome,Texto,DataDieta,Completo")] DietaSemanaViewModel vm)
        {
            if (id != vm.DietaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Dieta dieta = new Dieta
                    {
                        DietaID = vm.DietaID,
                        Completo = vm.Completo,
                        DataDieta = vm.DataDieta,
                        DietaNome = vm.DietaNome
                    };
                    _context.Update(dieta);

                    DietaSemana dietaSemana = new DietaSemana
                    {
                        SemanaID = vm.SemanaID,
                        DescDieta = vm.Texto,
                        DietaID = vm.DietaID
                    };
                    _context.Update(dietaSemana);
                    await _context.SaveChangesAsync();

                    if (salvar.Equals("Próxima Semana"))
                    {
                        return View(Edit(vm.DietaID, vm.SemanaID + 1));
                    }

                    else if (salvar.Equals("Salvar"))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietaExists(vm.DietaID))
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
