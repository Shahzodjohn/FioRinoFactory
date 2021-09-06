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
    public class DmRolesController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmRolesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmRole>> PostDmRoles(DmRole dmRoles)
        {
            _context.DmRoles.Add(dmRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmRoles", new { id = dmRoles.Id }, dmRoles);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmRole>>> GetAllDmRoles(string linkString = null)
        {
            IQueryable<DmRole> finalContext = _context.DmRoles;

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
        public async Task<ActionResult<DmRole>> GetDmRoles(int id, string linkString = null)
        {
            IQueryable<DmRole> finalContext = _context.DmRoles;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmRoles = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmRoles == null)
            {
                return NotFound();
            }

            return dmRoles;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmRoles(int id, DmRole dmRoles)
        {
            if (id != dmRoles.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmRolesExists(id))
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
        public async Task<IActionResult> DeleteDmRoles(int id)
        {
            var dmRoles = await _context.DmRoles.FindAsync(id);
            if (dmRoles == null)
            {
                return NotFound();
            }

            _context.DmRoles.Remove(dmRoles);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmRolesExists(int id)
        {
            return _context.DmRoles.Any(e => e.Id == id);
        }
    }
}
