using FioRinoFactory.Data;
using FioRinoFactory.Model;
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
    public class DmWzMagazynController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmWzMagazynController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmWzMagazyn>> PostDmWzMagazyn(DmWzMagazyn dmWzMagazyn)
        {
            _context.DmWzMagazyns.Add(dmWzMagazyn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmWzMagazyn", new { id = dmWzMagazyn.Id }, dmWzMagazyn);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmWzMagazyn>>> GetAllDmWzMagazyn(string linkString = null)
        {
            IQueryable<DmWzMagazyn> finalContext = _context.DmWzMagazyns;

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
        public async Task<ActionResult<DmWzMagazyn>> GetDmWzMagazyn(int id, string linkString = null)
        {
            IQueryable<DmWzMagazyn> finalContext = _context.DmWzMagazyns;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmWzMagazyn = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmWzMagazyn == null)
            {
                return NotFound();
            }

            return dmWzMagazyn;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmWzMagazyn(int id, DmWzMagazyn dmWzMagazyn)
        {
            if (id != dmWzMagazyn.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmWzMagazyn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmWzMagazynExists(id))
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
        public async Task<IActionResult> DeleteDmWzMagazyn(int id)
        {
            var dmWzMagazyn = await _context.DmWzMagazyns.FindAsync(id);
            if (dmWzMagazyn == null)
            {
                return NotFound();
            }

            _context.DmWzMagazyns.Remove(dmWzMagazyn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class ListOfWzOrdersInWZMAGAZYNParams { public int WzMagazynId { get; set; } }
        // EXPOSE_dm_WzMagazyn_ListOfWzOrdersInWZMAGAZYN

        [HttpPost("ListOfWzOrdersInWZMAGAZYN")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_WzMagazyn_ListOfWzOrdersInWZMAGAZYNResult>>> PostDmWzMagazynListOfWzOrdersInWZMAGAZYN([FromBody] ListOfWzOrdersInWZMAGAZYNParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_WzMagazyn_ListOfWzOrdersInWZMAGAZYNAsync /**/ (parameters.WzMagazynId);

            }
        }

        public class UpdateProductAmountInStanMagazynuParams
        {
            public int WzMagazynId { get; set; }
            public int Amount { get; set; }
        }
        // EXPOSE_dm_WzMagazyn_UpdateProductAmountInStanMagazynu

        [HttpPost("UpdateProductAmountInStanMagazynu")]
        public async Task<ActionResult> PostDmWzMagazynUpdateProductAmountInStanMagazynu([FromBody] UpdateProductAmountInStanMagazynuParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_WzMagazyn_UpdateProductAmountInStanMagazynu /**/ (parameters.WzMagazynId, parameters.Amount);
                return Ok();
            }
        }

        public class SendingOrderToMagazynParams { public int OrderProductId { get; set; } }
        // EXPOSE_dm_WzMagazyn_SendingOrderToMagazyn

        [HttpPost("SendingOrderToMagazyn")]
        public async Task<ActionResult> PostDmWzMagazynSendingOrderToMagazyn([FromBody] SendingOrderToMagazynParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                 db.EXPOSE_dm_WzMagazyn_SendingOrderToMagazyn /**/ (parameters.OrderProductId);
                return Ok();
            }
        }


        private bool DmWzMagazynExists(int id)
        {
            return _context.DmWzMagazyns.Any(e => e.Id == id);
        }
    }
}
