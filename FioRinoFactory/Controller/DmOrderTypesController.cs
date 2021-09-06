using FioRinoFactory.Data;
using FioRinoFactory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.Controller
{
    [ApiController]
    [Route("webapi/[controller]")]
    public class DmOrderTypesController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmOrderTypesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmOrderType>> PostDmOrderTypes(DmOrderType dmOrderTypes)
        {
            _context.DmOrderTypes.Add(dmOrderTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmOrderTypes", new { id = dmOrderTypes.Id }, dmOrderTypes);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmOrderType>>> GetAllDmOrderTypes(string linkString = null)
        {
            IQueryable<DmOrderType> finalContext = _context.DmOrderTypes;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            return await finalContext.ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}/{linkString?}")]
        public async Task<ActionResult<DmOrderType>> GetDmOrderTypes(int id, string linkString = null)
        {
            IQueryable<DmOrderType> finalContext = _context.DmOrderTypes;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmOrderTypes = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmOrderTypes == null)
            {
                return NotFound();
            }

            return dmOrderTypes;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmOrderTypes(int id, DmOrderType dmOrderTypes)
        {
            if (id != dmOrderTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmOrderTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmOrderTypesExists(id))
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDmOrderTypes(int id)
        {
            var dmOrderTypes = await _context.DmOrderTypes.FindAsync(id);
            if (dmOrderTypes == null)
            {
                return NotFound();
            }

            _context.DmOrderTypes.Remove(dmOrderTypes);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmOrderTypesExists(int id)
        {
            return _context.DmOrderTypes.Any(e => e.Id == id);
        }
    }
}
