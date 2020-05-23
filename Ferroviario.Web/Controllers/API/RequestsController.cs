using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ferroviario.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RequestsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            List<RequestEntity> requests = await _context.Requests.
                Include(r=>r.Type).
                ToListAsync();
            return Ok(_converterHelper.ToRequestResponse(requests));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestEntity = await _context.Requests.FindAsync(id);

            if (requestEntity == null)
            {
                return NotFound();
            }

            return Ok(requestEntity);
        }

        
    }
}