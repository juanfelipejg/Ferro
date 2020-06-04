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
    public class ReportsController : Controller
    {
        private readonly DataContext _context;

        public ReportsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportEntity = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportEntity == null)
            {
                return NotFound();
            }

            return View(reportEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Source,SourceLatitude,SourceLongitude,Name,LastName,Phone,Email,Description,PicturePath")] ReportEntity reportEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportEntity = await _context.Reports.FindAsync(id);
            if (reportEntity == null)
            {
                return NotFound();
            }
            return View(reportEntity);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Source,SourceLatitude,SourceLongitude,Name,LastName,Phone,Email,Description,PicturePath")] ReportEntity reportEntity)
        {
            if (id != reportEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportEntityExists(reportEntity.Id))
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
            return View(reportEntity);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportEntity = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportEntity == null)
            {
                return NotFound();
            }

            return View(reportEntity);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportEntity = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(reportEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportEntityExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
