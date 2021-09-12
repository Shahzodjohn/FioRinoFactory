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
    public class DmPositionsController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmPositionsController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmPosition>> PostDmPositions(DmPosition dmPositions)
        {
            _context.DmPositions.Add(dmPositions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmPositions", new { id = dmPositions.Id }, dmPositions);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmPosition>>> GetAllDmPositions(string linkString = null)
        {
            IQueryable<DmPosition> finalContext = _context.DmPositions;

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
        public async Task<ActionResult<DmPosition>> GetDmPositions(int id, string linkString = null)
        {
            IQueryable<DmPosition> finalContext = _context.DmPositions;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmPositions = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmPositions == null)
            {
                return NotFound();
            }

            return dmPositions;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmPositions(int id, DmPosition dmPositions)
        {
            if (id != dmPositions.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmPositions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmPositionsExists(id))
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
        public async Task<IActionResult> DeleteDmPositions(int id)
        {
            var dmPositions = await _context.DmPositions.FindAsync(id);
            if (dmPositions == null)
            {
                return NotFound();
            }

            _context.DmPositions.Remove(dmPositions);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmPositionsExists(int id)
        {
            return _context.DmPositions.Any(e => e.Id == id);
        }
    }
}
