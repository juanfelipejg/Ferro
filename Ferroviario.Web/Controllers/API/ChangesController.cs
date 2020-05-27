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
                .Where(c => c.FirstDriver.Id == request.UserId.ToString() || c.SecondDriver.Id == request.UserId.ToString())
                .ToListAsync();

            List<ChangeResponse> changeResponses = new List<ChangeResponse>();

            foreach (ChangeEntity changeEntity in changeEntitys)
            {
                changeResponses.Add(_converterHelper.ToChangeResponse(changeEntity));
            }

            return Ok(changeResponses.OrderBy(c => c.DateLocal));
        }
    }

}
