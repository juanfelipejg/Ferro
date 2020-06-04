using Ferroviario.Common.Models;
using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Helpers;
using Ferroviario.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Ferroviario.Web.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;

        public ReportsController(DataContext context, IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostReport([FromBody] ReportRequest reportRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(reportRequest.CultureInfo);
            Resource.Culture = cultureInfo;

            ReportEntity report = new ReportEntity
            {
                Date = DateTime.UtcNow,
                Source = reportRequest.Source,
                SourceLatitude = reportRequest.SourceLatitude,
                SourceLongitude = reportRequest.SourceLongitude,
                Name = reportRequest.Name,
                LastName = reportRequest.LastName,
                Phone = reportRequest.Phone,
                Email = reportRequest.Email,
                Description = reportRequest.Description                
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
