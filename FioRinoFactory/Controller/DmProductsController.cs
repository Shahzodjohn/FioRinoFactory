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
    public class DmProductsController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmProductsController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmProduct>> PostDmProducts(DmProduct dmProducts)
        {
            _context.DmProducts.Add(dmProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmProducts", new { id = dmProducts.Id }, dmProducts);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmProduct>>> GetAllDmProducts(string linkString = null)
        {
            IQueryable<DmProduct> finalContext = _context.DmProducts;

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
        public async Task<ActionResult<DmProduct>> GetDmProducts(int id, string linkString = null)
        {
            IQueryable<DmProduct> finalContext = _context.DmProducts;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmProducts = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmProducts == null)
            {
                return NotFound();
            }

            return dmProducts;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmProducts(int id, DmProduct dmProducts)
        {
            if (id != dmProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmProductsExists(id))
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
        public async Task<IActionResult> DeleteDmProducts(int id)
        {
            var dmProducts = await _context.DmProducts.FindAsync(id);
            if (dmProducts == null)
            {
                return NotFound();
            }

            _context.DmProducts.Remove(dmProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmProductsExists(int id)
        {
            return _context.DmProducts.Any(e => e.Id == id);
        }
    }
}
