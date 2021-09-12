using FioRinoFactory.Data;
using FioRinoFactory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.Controllers
{
    [ApiController]
    [Route("webapi/[controller]")]
    public class DmOrderStatusesController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmOrderStatusesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmOrderStatus>> PostDmOrderStatuses(DmOrderStatus dmOrderStatuses)
        {
            _context.DmOrderStatuses.Add(dmOrderStatuses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmOrderStatuses", new { id = dmOrderStatuses.Id }, dmOrderStatuses);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmOrderStatus>>> GetAllDmOrderStatuses(string linkString = null)
        {
            IQueryable<DmOrderStatus> finalContext = _context.DmOrderStatuses;

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
        public async Task<ActionResult<DmOrderStatus>> GetDmOrderStatuses(int id, string linkString = null)
        {
            IQueryable<DmOrderStatus> finalContext = _context.DmOrderStatuses;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmOrderStatuses = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmOrderStatuses == null)
            {
                return NotFound();
            }

            return dmOrderStatuses;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmOrderStatuses(int id, DmOrderStatus dmOrderStatuses)
        {
            if (id != dmOrderStatuses.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmOrderStatuses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmOrderStatusesExists(id))
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
        public async Task<IActionResult> DeleteDmOrderStatuses(int id)
        {
            var dmOrderStatuses = await _context.DmOrderStatuses.FindAsync(id);
            if (dmOrderStatuses == null)
            {
                return NotFound();
            }

            _context.DmOrderStatuses.Remove(dmOrderStatuses);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmOrderStatusesExists(int id)
        {
            return _context.DmOrderStatuses.Any(e => e.Id == id);
        }
    }
}
