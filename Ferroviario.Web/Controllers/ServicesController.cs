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
    public class ServicesController : Controller
    {
        private readonly DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

         public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceEntity = await _context.Services
                .Include(s=>s.ServiceDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceEntity == null)
            {
                return NotFound();
            }

            return View(serviceEntity);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InitialHour,InitialStation,FinalHour,FinalStation")] ServiceEntity serviceEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceEntity);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceEntity = await _context.Services.FindAsync(id);
            if (serviceEntity == null)
            {
                return NotFound();
            }
            return View(serviceEntity);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InitialHour,InitialStation,FinalHour,FinalStation")] ServiceEntity serviceEntity)
        {
            if (id != serviceEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceEntityExists(serviceEntity.Id))
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
            return View(serviceEntity);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceEntity = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceEntity == null)
            {
                return NotFound();
            }

            return View(serviceEntity);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceEntity = await _context.Services.FindAsync(id);
            _context.Services.Remove(serviceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceEntityExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceEntity serviceEntity = await _context.Services
                .Include(r => r.ServiceDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceEntity == null)
            {
                return NotFound();
            }

            return View(serviceEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetail(ServiceEntity serviceEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{serviceEntity.Id}");
            }

            return View(serviceEntity);
        }
    }
}
