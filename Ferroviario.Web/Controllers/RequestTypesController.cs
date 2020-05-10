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
    public class RequestTypesController : Controller
    {
        private readonly DataContext _context;

        public RequestTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: RequestTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RequestTypes.ToListAsync());
        }

        // GET: RequestTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestTypeEntity = await _context.RequestTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestTypeEntity == null)
            {
                return NotFound();
            }

            return View(requestTypeEntity);
        }

        // GET: RequestTypes/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestTypeEntity requestTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestTypeEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Already there is a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                }

            }
            return View(requestTypeEntity);
        }

        // GET: RequestTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestTypeEntity = await _context.RequestTypes.FindAsync(id);
            if (requestTypeEntity == null)
            {
                return NotFound();
            }
            return View(requestTypeEntity);
        }

        // POST: RequestTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] RequestTypeEntity requestTypeEntity)
        {
            if (id != requestTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestTypeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestTypeEntityExists(requestTypeEntity.Id))
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
            return View(requestTypeEntity);
        }

        // GET: RequestTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestTypeEntity = await _context.RequestTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestTypeEntity == null)
            {
                return NotFound();
            }

            return View(requestTypeEntity);
        }

        // POST: RequestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestTypeEntity = await _context.RequestTypes.FindAsync(id);
            _context.RequestTypes.Remove(requestTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestTypeEntityExists(int id)
        {
            return _context.RequestTypes.Any(e => e.Id == id);
        }
    }
}
