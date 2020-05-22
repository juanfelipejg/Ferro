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
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace Ferroviario.Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public RequestsController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper,
            IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
            return View(await _context.Requests.
                Include(r=>r.Type).
                Include(r=>r.User).
                Where(r=>r.User == user).
                ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestEntity = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestEntity == null)
            {
                return NotFound();
            }

            return View(requestEntity);
        }


        public async Task<IActionResult> Create()
        {
            UserEntity user = await _userHelper.GetUserAsync(User.Identity.Name);
            RequestViewModel model = new RequestViewModel
            {
                Types = _combosHelper.GetComboTypes(),
                UserId = user.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                RequestEntity requestEntity = await _converterHelper.ToRequestEntityAsync(model, true);
                _context.Add(requestEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Types = _combosHelper.GetComboTypes();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RequestEntity requestEntity = await _context.Requests.Include(r=>r.Type).FirstOrDefaultAsync(r=>r.Id == id);
            if (requestEntity == null)
            {
                return NotFound();
            }

            RequestViewModel model = _converterHelper.ToRequestViewModel(requestEntity);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequestViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                RequestEntity requestEntity = await _converterHelper.ToRequestEntityAsync(model, false);
                    _context.Update(requestEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));                
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestEntity = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestEntity == null)
            {
                return NotFound();
            }

            return View(requestEntity);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestEntity = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(requestEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestEntityExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
