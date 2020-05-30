using Ferroviario.Web.Data;
using Ferroviario.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferroviario.Common.Models;
using System.Globalization;
using Ferroviario.Web.Resources;
using Ferroviario.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ferroviario.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]

    public class ChangesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ChangesController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetChangesForUser")]    
        public async Task<IActionResult> GetChangesForUser([FromBody] ChangesForUserRequest request)
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

            List<ChangeEntity> changeEntitys = await _context.Changes
                .Include(c => c.FirstDriver)
                .Include(c => c.FirstDriverService)
                .ThenInclude(c => c.Service)
                .ThenInclude(s => s.ServiceDetail)
                .Include(c => c.SecondDriver)
                .Include(c => c.SecondDriverService)
                .ThenInclude(s => s.Service)
                .ThenInclude(s => s.ServiceDetail)
                .Where(c => c.SecondDriver.Id == request.UserId.ToString() && c.State=="Pending")
                .ToListAsync();

            List<ChangeResponse> changeResponses = new List<ChangeResponse>();

            foreach (ChangeEntity changeEntity in changeEntitys)
            {
                changeResponses.Add(_converterHelper.ToChangeResponse(changeEntity));
            }

            return Ok(changeResponses.OrderBy(c => c.DateLocal));
        }

        [HttpPost]
        [Route("PostChange")]
        public async Task<IActionResult> PostChange([FromBody] ChangeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            ChangeEntity changeEntity = new ChangeEntity()
            {
                FirstDriver = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.FirstDriverId.ToString()),
                FirstDriverService = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == request.FirstShift),
                SecondDriver = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.SecondDriverId.ToString()),
                SecondDriverService = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == request.SecondShift),
                State = "Pending"
            };

            _context.Changes.Add(changeEntity);
            await _context.SaveChangesAsync();
            return NoContent();            
        }

        [HttpPost]
        [Route("PutChange")]
        public async Task<IActionResult> PutChange([FromBody] ConfirmChangeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            ChangeEntity changeEntity = await _context.Changes.Include(c=>c.FirstDriver).
                Include(c=>c.FirstDriverService).
                ThenInclude(c=>c.Service).
                Include(c=>c.SecondDriver).
                Include(c=>c.SecondDriverService).
                ThenInclude(c=>c.Service).
                FirstOrDefaultAsync(c=>c.Id == request.ChangeId);

            changeEntity.FirstDriverService.Service = await _context.Services.FindAsync(request.SecondServiceId);

            changeEntity.SecondDriverService.Service = await _context.Services.FindAsync(request.FirstServiceId);

            changeEntity.State = "Approved";

            _context.Changes.Update(changeEntity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
