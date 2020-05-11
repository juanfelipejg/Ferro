using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController :ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ServicesController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            List<ServiceEntity> services = await _context.Services.
                Include(s=>s.ServiceDetail).
                ToListAsync();
            return Ok(_converterHelper.ToServiceResponse(services));
        }

    }
}
