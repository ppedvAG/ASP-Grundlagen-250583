using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using M000_DataAccess.Models;

namespace M007_Validierung.Controllers
{
    public class KurseControllerGenerated(KursDBContext db) : Controller
    {

		// GET: KurseControllerGenerated
		public async Task<IActionResult> Index()
        {
            return View(await db.Kurse.ToListAsync());
        }

        // GET: KurseControllerGenerated/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await db.Kurse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurse == null)
            {
                return NotFound();
            }

            return View(kurse);
        }

        // GET: KurseControllerGenerated/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KurseControllerGenerated/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kursname,DauerInTagen,Aktiv")] Kurse kurse)
        {
            if (ModelState.IsValid)
            {
                db.Add(kurse);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kurse);
        }

        // GET: KurseControllerGenerated/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await db.Kurse.FindAsync(id);
            if (kurse == null)
            {
                return NotFound();
            }
            return View(kurse);
        }

        // POST: KurseControllerGenerated/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kursname,DauerInTagen,Aktiv")] Kurse kurse)
        {
            if (id != kurse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(kurse);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KurseExists(kurse.Id))
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
            return View(kurse);
        }

        // GET: KurseControllerGenerated/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurse = await db.Kurse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurse == null)
            {
                return NotFound();
            }

            return View(kurse);
        }

        // POST: KurseControllerGenerated/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kurse = await db.Kurse.FindAsync(id);
            if (kurse != null)
            {
                db.Kurse.Remove(kurse);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KurseExists(int id)
        {
            return db.Kurse.Any(e => e.Id == id);
        }
    }
}
