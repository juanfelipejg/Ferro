using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Ferroviario.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers
{

    public class ChangesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public ChangesController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> IndexAll()
        {
            return View(await _context.Changes.Include(c=>c.FirstDriver).
                Include(c=>c.FirstDriverService).
                ThenInclude(s=>s.Service).
                Include(c=>c.SecondDriver).
                Include(c=>c.SecondDriverService).
                ThenInclude(s=>s.Service).
                ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            DateTime Tomorrow = DateTime.Today.AddDays(1).ToLocalTime();
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }            

            return View(await _context.Shifts.
                Include(s => s.User).
                Include(s => s.Service).
                Where(s => s.User.Id != user.Id && s.Date.Day == Tomorrow.Day).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChangeEntity changeEntity = await _context.Changes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (changeEntity == null)
            {
                return NotFound();
            }

            return View(changeEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChangeEntity changeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(changeEntity);
        }

        public async Task<IActionResult> CreateChange(int? idService, string idDriver)
        {

            DateTime Tomorrow = DateTime.Today.AddDays(1).ToLocalTime();

            UserEntity FirstDriver = await _userHelper.GetUserAsync(User.Identity.Name);

            ShiftEntity FirstShift = await _context.Shifts.
            Include(s => s.User).
            Include(s => s.Service).
            Where(s => s.Date.Day == Tomorrow.Day).FirstOrDefaultAsync(s => s.User == FirstDriver);

            UserEntity SecondDriver = await _context.Users.FindAsync(idDriver);

            ShiftEntity SecondShift = await _context.Shifts.Include(s => s.Service).
            Include(s => s.User).FirstOrDefaultAsync(s => s.Id == idService);


            ChangeViewModel model = new ChangeViewModel
            {
                FirstDriver = FirstDriver,
                FirstDriverId = FirstDriver.Id,

                FirstDriverService = FirstShift,
                FirstDriverServiceId= FirstShift.Id,

                SecondDriver = SecondDriver,
                SecondDriverId = SecondDriver.Id,

                SecondDriverService = SecondShift,
                SecondDriverServiceId=SecondShift.Id,
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChange(ChangeViewModel model)
        {

            if (ModelState.IsValid)
            {
                ChangeEntity changeEntity = await _converterHelper.ToChangeEntityAsync(model, true);
                _context.Add(changeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> SelectChanges()
        {
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            return View(await _context.Changes.
                Include(c => c.FirstDriver).
                Include(c => c.FirstDriverService).ThenInclude(s=>s.Service).
                Include(c=> c.SecondDriver).
                Include(c=>c.SecondDriverService).ThenInclude(s => s.Service).
                Where(c=>c.SecondDriver.Id == user.Id && c.State=="Pending").ToListAsync());
        }
        

        public async Task<IActionResult> ConfirmChange(int id)
        {
            ChangeEntity changeEntity = await _context.Changes.Include(c=>c.FirstDriver).
                Include(c => c.FirstDriverService).
                ThenInclude(c => c.Service).
                ThenInclude(s => s.ServiceDetail).
                Include(c=>c.SecondDriver).
                Include(c=>c.SecondDriverService).
                ThenInclude(s=>s.Service).
                ThenInclude(s=>s.ServiceDetail).
                FirstOrDefaultAsync(c => c.Id == id);

            ChangeViewModel change = _converterHelper.ToChangeViewModel(changeEntity);

            return View(change);
 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmChange(ChangeViewModel model,int idShift, int idShift2, int idService, int idService2)
        {
            ChangeEntity changeEntity = await _converterHelper.ToChangeEntityAsync(model, false);

            changeEntity.State = "Approved";

            _context.Update(changeEntity);
            await _context.SaveChangesAsync();

            ShiftEntity shiftEntity = await _context.Shifts.Include(s => s.User).Include(s => s.Service).
                FirstOrDefaultAsync(s => s.Id == idShift);

            shiftEntity.Service = await _context.Services.Include(s => s.ServiceDetail).
                FirstOrDefaultAsync(s => s.Id == idService);

            ShiftEntity shiftEntity2 = await _context.Shifts.Include(s => s.User).Include(s => s.Service).
                FirstOrDefaultAsync(s => s.Id == idShift2);

            shiftEntity2.Service = await _context.Services.Include(s => s.ServiceDetail).
            FirstOrDefaultAsync(s => s.Id == idService2);

            _context.Update(shiftEntity);
            await _context.SaveChangesAsync();

            _context.Update(shiftEntity2);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SelectChanges));
        }
        

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ChangeEntity changeEntity = await _context.Changes.FindAsync(id);
            if (changeEntity == null)
            {
                return NotFound();
            }
            return View(changeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChangeEntity changeEntity)
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

            ChangeEntity changeEntity = await _context.Changes
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
            ChangeEntity changeEntity = await _context.Changes.FindAsync(id);
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
