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
    public class DmOrderProductsController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmOrderProductsController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmOrderProduct>> PostDmOrderProducts(DmOrderProduct dmOrderProducts)
        {
            _context.DmOrderProducts.Add(dmOrderProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmOrderProducts", new { id = dmOrderProducts.Id }, dmOrderProducts);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmOrderProduct>>> GetAllDmOrderProducts(string linkString = null)
        {
            IQueryable<DmOrderProduct> finalContext = _context.DmOrderProducts;

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
        public async Task<ActionResult<DmOrderProduct>> GetDmOrderProducts(int id, string linkString = null)
        {
            IQueryable<DmOrderProduct> finalContext = _context.DmOrderProducts;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmOrderProducts = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmOrderProducts == null)
            {
                return NotFound();
            }

            return dmOrderProducts;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmOrderProducts(int id, DmOrderProduct dmOrderProducts)
        {
            if (id != dmOrderProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmOrderProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmOrderProductsExists(id))
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
        public async Task<IActionResult> DeleteDmOrderProducts(int id)
        {
            var dmOrderProducts = await _context.DmOrderProducts.FindAsync(id);
            if (dmOrderProducts == null)
            {
                return NotFound();
            }

            _context.DmOrderProducts.Remove(dmOrderProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmOrderProductsExists(int id)
        {
            return _context.DmOrderProducts.Any(e => e.Id == id);
        }
    }
}
