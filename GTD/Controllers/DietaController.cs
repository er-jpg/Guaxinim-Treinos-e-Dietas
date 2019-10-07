﻿using System;
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
            var gTDContext = _context.Dieta.Include(d => d.Semana);
            return View(await gTDContext.ToListAsync());
        }

        // GET: Dieta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dieta = await _context.Dieta
                .Include(d => d.Semana)
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
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID");
            return View();
        }

        // POST: Dieta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DietaID,DietaNome,DescDieta,Duracao,DataDieta,Completo,SemanaID")] Dieta dieta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dieta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID", dieta.SemanaID);
            return View(dieta);
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
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID", dieta.SemanaID);
            return View(dieta);
        }

        // POST: Dieta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("DietaID,DietaNome,DescDieta,Duracao,DataDieta,Completo,SemanaID")] Dieta dieta)
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
            ViewData["SemanaID"] = new SelectList(_context.Set<Semana>(), "SemanaID", "SemanaID", dieta.SemanaID);
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
        //        .Include(d => d.Semana)
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
    }
}