using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureGaming.Data;
using AzureGaming.Models;

namespace AzureGaming.Controllers
{
    public class AbonnementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbonnementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Abonnements
        public async Task<IActionResult> Index()
        {
            if (!_context.Abonnements.Any())
            {
               return RedirectToAction("Create");
            }
            else{
                return _context.Abonnements != null ?
                        View(await _context.Abonnements.Where(a => a.Mail == HttpContext.User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Abonnements'  is null.");
            }
        }

        // GET: Abonnements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Abonnements == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // GET: Abonnements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abonnements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateDebut,Duree,TarifMensuel,Mail")] Abonnement abonnement)
        {
            abonnement.Mail = HttpContext.User.Identity.Name;

            _context.Add(abonnement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Abonnements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Abonnements == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }
            return View(abonnement);
        }

        // POST: Abonnements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateDebut,Duree,TarifMensuel,Mail")] Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementExists(abonnement.Id))
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
            return View(abonnement);
        }

        // GET: Abonnements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Abonnements == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // POST: Abonnements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Abonnements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Abonnements'  is null.");
            }
            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement != null)
            {
                _context.Abonnements.Remove(abonnement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementExists(int id)
        {
            return (_context.Abonnements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
