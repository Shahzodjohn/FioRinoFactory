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
    public class DmOrdersController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmOrdersController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmOrder>> PostDmOrders(DmOrder dmOrders)
        {
            _context.DmOrders.Add(dmOrders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmOrders", new { id = dmOrders.Id }, dmOrders);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmOrder>>> GetAllDmOrders(string linkString = null)
        {
            IQueryable<DmOrder> finalContext = _context.DmOrders;

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
        public async Task<ActionResult<DmOrder>> GetDmOrders(int id, string linkString = null)
        {
            IQueryable<DmOrder> finalContext = _context.DmOrders;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmOrders = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmOrders == null)
            {
                return NotFound();
            }

            return dmOrders;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmOrders(int id, DmOrder dmOrders)
        {
            if (id != dmOrders.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmOrders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmOrdersExists(id))
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
        public async Task<IActionResult> DeleteDmOrders(int id)
        {
            var dmOrders = await _context.DmOrders.FindAsync(id);
            if (dmOrders == null)
            {
                return NotFound();
            }

            _context.DmOrders.Remove(dmOrders);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmOrdersExists(int id)
        {
            return _context.DmOrders.Any(e => e.Id == id);
        }
    }
}
