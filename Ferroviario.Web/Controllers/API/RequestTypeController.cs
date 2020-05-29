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

namespace Ferroviario.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTypeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RequestTypeController(DataContext context,IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/RequestType
        [HttpGet]
        public async Task<IActionResult> GetRequestTypes()
        {
            List<RequestTypeEntity> requestTypes = await _context.RequestTypes.
                ToListAsync();
            return Ok(_converterHelper.ToRequestTypeResponse(requestTypes));
        }

        // GET: api/RequestType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestTypeEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestTypeEntity = await _context.RequestTypes.FindAsync(id);

            if (requestTypeEntity == null)
            {
                return NotFound();
            }

            return Ok(requestTypeEntity);
        }

        // PUT: api/RequestType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestTypeEntity([FromRoute] int id, [FromBody] RequestTypeEntity requestTypeEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestTypeEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestTypeEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestTypeEntityExists(id))
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

        // POST: api/RequestType
        [HttpPost]
        public async Task<IActionResult> PostRequestTypeEntity([FromBody] RequestTypeEntity requestTypeEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RequestTypes.Add(requestTypeEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestTypeEntity", new { id = requestTypeEntity.Id }, requestTypeEntity);
        }

        // DELETE: api/RequestType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestTypeEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestTypeEntity = await _context.RequestTypes.FindAsync(id);
            if (requestTypeEntity == null)
            {
                return NotFound();
            }

            _context.RequestTypes.Remove(requestTypeEntity);
            await _context.SaveChangesAsync();

            return Ok(requestTypeEntity);
        }

        private bool RequestTypeEntityExists(int id)
        {
            return _context.RequestTypes.Any(e => e.Id == id);
        }
    }
}