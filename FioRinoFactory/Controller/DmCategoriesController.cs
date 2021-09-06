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
    public class DmCategoriesController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmCategoriesController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmCategory>> PostDmCategories(DmCategory dmCategories)
        {
            _context.DmCategories.Add(dmCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmCategories", new { id = dmCategories.Id }, dmCategories);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmCategory>>> GetAllDmCategories(string linkString = null)
        {
            IQueryable<DmCategory> finalContext = _context.DmCategories;

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
        public async Task<ActionResult<DmCategory>> GetDmCategories(int id, string linkString = null)
        {
            IQueryable<DmCategory> finalContext = _context.DmCategories;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmCategories = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmCategories == null)
            {
                return NotFound();
            }

            return dmCategories;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmCategories(int id, DmCategory dmCategories)
        {
            if (id != dmCategories.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmCategoriesExists(id))
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
        public async Task<IActionResult> DeleteDmCategories(int id)
        {
            var dmCategories = await _context.DmCategories.FindAsync(id);
            if (dmCategories == null)
            {
                return NotFound();
            }

            _context.DmCategories.Remove(dmCategories);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmCategoriesExists(int id)
        {
            return _context.DmCategories.Any(e => e.Id == id);
        }
    }
}
