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
    public class DmLogEntryController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmLogEntryController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmLogEntry>> PostDmLogEntry(DmLogEntry dmLogEntry)
        {
            _context.DmLogEntries.Add(dmLogEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmLogEntry", new { id = dmLogEntry.Id }, dmLogEntry);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmLogEntry>>> GetAllDmLogEntry(string linkString = null)
        {
            IQueryable<DmLogEntry> finalContext = _context.DmLogEntries;

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
        public async Task<ActionResult<DmLogEntry>> GetDmLogEntry(int id, string linkString = null)
        {
            IQueryable<DmLogEntry> finalContext = _context.DmLogEntries;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmLogEntry = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmLogEntry == null)
            {
                return NotFound();
            }

            return dmLogEntry;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmLogEntry(int id, DmLogEntry dmLogEntry)
        {
            if (id != dmLogEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmLogEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmLogEntryExists(id))
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
        public async Task<IActionResult> DeleteDmLogEntry(int id)
        {
            var dmLogEntry = await _context.DmLogEntries.FindAsync(id);
            if (dmLogEntry == null)
            {
                return NotFound();
            }

            _context.DmLogEntries.Remove(dmLogEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmLogEntryExists(int id)
        {
            return _context.DmLogEntries.Any(e => e.Id == id);
        }
    }
}
