using Ferroviario.Common.Models;
using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Ferroviario.Web.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
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
            ThenInclude(s=>s.ServiceDetail).
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

        [HttpPost]
        [Route("GetShiftsForUser")]
        public async Task<IActionResult> GetShiftsForUser([FromBody] ShiftsForUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            UserEntity userEntity = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId.ToString());

            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            List<ShiftEntity> shiftEntities = await _context.Shifts.Include(s => s.Service).
                ThenInclude(s=>s.ServiceDetail).
                Include(s => s.User).
                Where(r => r.User.Id == request.UserId.ToString()).ToListAsync();

            List<ShiftResponse> shiftResponses = new List<ShiftResponse>();

            foreach (ShiftEntity shiftEntity in shiftEntities)
            {
                shiftResponses.Add(_converterHelper.ToShiftResponse(shiftEntity));
            }

            return Ok(shiftResponses);
        }


    }
}