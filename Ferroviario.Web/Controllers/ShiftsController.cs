using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Ferroviario.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public ShiftsController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper, IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }


        public async Task<IActionResult> Index()
        {
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);

            List<ShiftEntity> ShiftEntities = new List<ShiftEntity>();

            if (user.UserType.ToString() == "Admin")
            {
                ShiftEntities = await _context.ShiftEntity.
                Include(s => s.User).
                Include(s => s.Service).ToListAsync();
            }
            else
            {
                ShiftEntities = await _context.ShiftEntity.
                    Include(s => s.User).
                    Include(s => s.Service).
                    Where(s => s.User == user).ToListAsync();
            }



            return View(ShiftEntities);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceEntity serviceEntity = await _context.Services
                .Include(s => s.ServiceDetail)
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
            ShiftViewModel model = new ShiftViewModel
            {
                Drivers = _combosHelper.GetComboDrivers(),
                Services = _combosHelper.GetComboServices()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShiftViewModel model)
        {
            if (ModelState.IsValid)
            {
                ShiftEntity shiftEntity = await _converterHelper.ToShiftEntityAsync(model, true);
                _context.Add(shiftEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Drivers = _combosHelper.GetComboDrivers();
            model.Services = _combosHelper.GetComboServices();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShiftEntity shiftEntity = await _context.Shifts.Include(s => s.User)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shiftEntity == null)
            {
                return NotFound();
            }

            ShiftViewModel model = _converterHelper.ToShiftViewModel(shiftEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ShiftViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ShiftEntity shiftEntity = await _converterHelper.ToShiftEntityAsync(model, false);
                _context.Update(shiftEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShiftEntity shiftEntity = await _context.ShiftEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftEntity == null)
            {
                return NotFound();
            }

            return View(shiftEntity);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ShiftEntity shiftEntity = await _context.ShiftEntity.FindAsync(id);
            _context.ShiftEntity.Remove(shiftEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftEntityExists(int id)
        {
            return _context.ShiftEntity.Any(e => e.Id == id);
        }
    }
}
