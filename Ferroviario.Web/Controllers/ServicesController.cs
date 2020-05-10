using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Models;
using Ferroviario.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Ferroviario.Web.Controllers
{
    
    public class ServicesController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ServicesController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceEntity serviceEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(AddDetail)}/{serviceEntity.Id}"); ;
            }
            return View(serviceEntity);
        }

        [Authorize(Roles = "Admin")]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceEntity serviceEntity)
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceEntity serviceEntity = await _context.Services.FindAsync(id);    
                
            if (serviceEntity == null)
            {
                return NotFound();
            }

            ServiceDetailViewModel model = new ServiceDetailViewModel
            {
                Service = serviceEntity,
                ServiceId = serviceEntity.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetail(ServiceDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceDetailEntity serviceDetailEntity = await _converterHelper.ToServiceDetailEntityAsync(model, true);
               _context.Add(serviceDetailEntity);
               await _context.SaveChangesAsync();
               return RedirectToAction($"{nameof(Details)}/{serviceDetailEntity.Id}");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceDetailEntity serviceDetail = await _context.ServiceDetails
                .FindAsync(id);
            if (serviceDetail == null)
            {
                return NotFound();
            }

            ServiceDetailViewModel model = _converterHelper.ToServiceDetailViewModel(serviceDetail);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetail(ServiceDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceDetailEntity serviceDetailEntity = await _converterHelper.ToServiceDetailEntityAsync(model, false);
                _context.Update(serviceDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{serviceDetailEntity.Id}");
            }

            return View(model);
        }
    }
}
