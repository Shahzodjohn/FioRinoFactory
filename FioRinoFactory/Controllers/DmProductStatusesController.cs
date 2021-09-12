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
    public class DmProductStatusesController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmProductStatusesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmProductStatus>> PostDmProductStatuses(DmProductStatus dmProductStatuses)
        {
            _context.DmProductStatuses.Add(dmProductStatuses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmProductStatuses", new { id = dmProductStatuses.Id }, dmProductStatuses);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmProductStatus>>> GetAllDmProductStatuses(string linkString = null)
        {
            IQueryable<DmProductStatus> finalContext = _context.DmProductStatuses;

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
        public async Task<ActionResult<DmProductStatus>> GetDmProductStatuses(int id, string linkString = null)
        {
            IQueryable<DmProductStatus> finalContext = _context.DmProductStatuses;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmProductStatuses = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmProductStatuses == null)
            {
                return NotFound();
            }

            return dmProductStatuses;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmProductStatuses(int id, DmProductStatus dmProductStatuses)
        {
            if (id != dmProductStatuses.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmProductStatuses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmProductStatusesExists(id))
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
        public async Task<IActionResult> DeleteDmProductStatuses(int id)
        {
            var dmProductStatuses = await _context.DmProductStatuses.FindAsync(id);
            if (dmProductStatuses == null)
            {
                return NotFound();
            }

            _context.DmProductStatuses.Remove(dmProductStatuses);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmProductStatusesExists(int id)
        {
            return _context.DmProductStatuses.Any(e => e.Id == id);
        }
    }
}
