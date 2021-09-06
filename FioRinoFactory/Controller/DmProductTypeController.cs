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
    public class DmProductTypeController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmProductTypeController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmProductType>> PostDmProductType(DmProductType dmProductType)
        {
            _context.DmProductTypes.Add(dmProductType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmProductType", new { id = dmProductType.Id }, dmProductType);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmProductType>>> GetAllDmProductType(string linkString = null)
        {
            IQueryable<DmProductType> finalContext = _context.DmProductTypes;

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
        public async Task<ActionResult<DmProductType>> GetDmProductType(int id, string linkString = null)
        {
            IQueryable<DmProductType> finalContext = _context.DmProductTypes;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmProductType = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmProductType == null)
            {
                return NotFound();
            }

            return dmProductType;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmProductType(int id, DmProductType dmProductType)
        {
            if (id != dmProductType.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmProductType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmProductTypeExists(id))
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
        public async Task<IActionResult> DeleteDmProductType(int id)
        {
            var dmProductType = await _context.DmProductTypes.FindAsync(id);
            if (dmProductType == null)
            {
                return NotFound();
            }

            _context.DmProductTypes.Remove(dmProductType);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmProductTypeExists(int id)
        {
            return _context.DmProductTypes.Any(e => e.Id == id);
        }
    }
}
