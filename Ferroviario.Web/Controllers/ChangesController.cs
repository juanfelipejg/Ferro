using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;

namespace Ferroviario.Web.Controllers
{
    public class ChangesController : Controller
    {
        private readonly DataContext _context;

        public ChangesController(DataContext context)
        {
            _context = context;
        }

        // GET: Changes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Changes.ToListAsync());
        }

        // GET: Changes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeEntity = await _context.Changes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (changeEntity == null)
            {
                return NotFound();
            }

            return View(changeEntity);
        }

        // GET: Changes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Changes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,State")] ChangeEntity changeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(changeEntity);
        }

        // GET: Changes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeEntity = await _context.Changes.FindAsync(id);
            if (changeEntity == null)
            {
                return NotFound();
            }
            return View(changeEntity);
        }

        // POST: Changes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,State")] ChangeEntity changeEntity)
        {
            if (id != changeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(changeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChangeEntityExists(changeEntity.Id))
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
            return View(changeEntity);
        }

        // GET: Changes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeEntity = await _context.Changes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (changeEntity == null)
            {
                return NotFound();
            }

            return View(changeEntity);
        }

        // POST: Changes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var changeEntity = await _context.Changes.FindAsync(id);
            _context.Changes.Remove(changeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChangeEntityExists(int id)
        {
            return _context.Changes.Any(e => e.Id == id);
        }
    }
}
