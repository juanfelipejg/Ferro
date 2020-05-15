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

        // PUT: api/Shifts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftEntity([FromRoute] int id, [FromBody] ShiftEntity shiftEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shiftEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shifts
        [HttpPost]
        public async Task<IActionResult> PostShiftEntity([FromBody] ShiftEntity shiftEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shifts.Add(shiftEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftEntity", new { id = shiftEntity.Id }, shiftEntity);
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftEntity([FromRoute] int id)
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

            _context.Shifts.Remove(shiftEntity);
            await _context.SaveChangesAsync();

            return Ok(shiftEntity);
        }

        private bool ShiftEntityExists(int id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }
    }
}