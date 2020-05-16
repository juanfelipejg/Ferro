using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ShiftsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetShifts()
        {
            List<ShiftEntity> shifts = await _context.Shifts.
            Include(s => s.Service).
            Include(s=> s.User).
            ToListAsync();
            return Ok(_converterHelper.ToShiftResponse(shifts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShiftEntity shiftEntity = await _context.Shifts.FindAsync(id);

            if (shiftEntity == null)
            {
                return NotFound();
            }

            return Ok(shiftEntity);
        }


    }
}