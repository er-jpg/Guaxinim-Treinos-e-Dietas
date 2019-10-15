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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino
                .FirstOrDefaultAsync(m => m.TreinoID == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treino/Create
        public IActionResult Create()
        {
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID");
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreinoID,TreinoNome,DescTreino,Duracao,DataTreino,Completo,SemanaID")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID");
            return View(treino);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID");
            return View(treino);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("TreinoID,TreinoNome,DescTreino,Duracao,DataTreino,Completo,SemanaID")] Treino treino)
        {
            if (id != treino.TreinoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinoExists(treino.TreinoID))
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
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID");
            return View(treino);
        }

        // GET: Treino/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var treino = await _context.Treino
        //        .FirstOrDefaultAsync(m => m.TreinoID == id);
        //    if (treino == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(treino);
        //}

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
    }
}
