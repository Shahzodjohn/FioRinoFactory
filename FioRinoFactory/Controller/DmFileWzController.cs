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
    [Route("api/[controller]")]
    [ApiController]
    public class DmFileWzController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmFileWzController(FioAndRinoContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<DmFileWz>> PostDmFileWz(DmFileWz dmFileWz)
        {
            _context.DmFileWzs.Add(dmFileWz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmFileWz", new { id = dmFileWz.Id }, dmFileWz);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmFileWz>>> GetAllDmFileWz(string linkString = null)
        {
            IQueryable<DmFileWz> finalContext = _context.DmFileWzs;

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
        public async Task<ActionResult<DmFileWz>> GetDmFileWz(int id, string linkString = null)
        {
            IQueryable<DmFileWz> finalContext = _context.DmFileWzs;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmFileWz = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmFileWz == null)
            {
                return NotFound();
            }

            return dmFileWz;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmFileWz(int id, DmFileWz dmFileWz)
        {
            if (id != dmFileWz.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmFileWz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmFileWzExists(id))
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
        public async Task<IActionResult> DeleteDmFileWz(int id)
        {
            var dmFileWz = await _context.DmFileWzs.FindAsync(id);
            if (dmFileWz == null)
            {
                return NotFound();
            }

            _context.DmFileWzs.Remove(dmFileWz);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmFileWzExists(int id)
        {
            return _context.DmFileWzs.Any(e => e.Id == id);
        }
    }
}
