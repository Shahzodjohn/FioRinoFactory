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
    public class DmSizesController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmSizesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmSize>> PostDmSizes(DmSize dmSizes)
        {
            _context.DmSizes.Add(dmSizes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmSizes", new { id = dmSizes.Id }, dmSizes);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmSize>>> GetAllDmSizes(string linkString = null)
        {
            IQueryable<DmSize> finalContext = _context.DmSizes;

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
        public async Task<ActionResult<DmSize>> GetDmSizes(int id, string linkString = null)
        {
            IQueryable<DmSize> finalContext = _context.DmSizes;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmSizes = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmSizes == null)
            {
                return NotFound();
            }

            return dmSizes;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmSizes(int id, DmSize dmSizes)
        {
            if (id != dmSizes.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmSizes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmSizesExists(id))
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
        public async Task<IActionResult> DeleteDmSizes(int id)
        {
            var dmSizes = await _context.DmSizes.FindAsync(id);
            if (dmSizes == null)
            {
                return NotFound();
            }

            _context.DmSizes.Remove(dmSizes);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmSizesExists(int id)
        {
            return _context.DmSizes.Any(e => e.Id == id);
        }
    }
}
