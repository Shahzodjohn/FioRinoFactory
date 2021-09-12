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
    public class DmOrdersController : Controller
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

        public class CreateOrderParams
        {
            public DateTime CraeteAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public int CategoryId { get; set; }
            public int OrderStatusId { get; set; }
            public int FileWzId { get; set; }
            public int SizeId { get; set; }
            public bool Is_removed { get; set; }
            public DateTime ImplementationDate { get; set; }
            public int Amount { get; set; }
            public string SourceOfOrder { get; set; }
            public int SenderId { get; set; }
            public int ProductId { get; set; }
        }
        // EXPOSE_dm_Orders_CreateOrder

        [HttpPost("CreateOrder")]
        public async Task<ActionResult> PostDmOrdersCreateOrder([FromBody] CreateOrderParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_Orders_CreateOrder /**/ (parameters.CraeteAt, parameters.UpdatedAt, parameters.CategoryId, parameters.OrderStatusId, parameters.FileWzId, parameters.SizeId, parameters.Is_removed, parameters.ImplementationDate, parameters.Amount, parameters.SourceOfOrder, parameters.SenderId, parameters.ProductId);
                return Ok();
            }
        }

        public class UpdateParams { public int OrderId { get; set; } }
        // EXPOSE_dm_Orders_Update

        [HttpPost("Update")]
        public async Task<ActionResult> PostDmOrdersUpdate([FromBody] UpdateParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_Orders_Update /**/ (parameters.OrderId);
                return Ok();
            }
        }

        // EXPOSE_dm_Orders_SelectingAllNewOrders

        [HttpPost("SelectingAllNewOrders")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_Orders_SelectingAllNewOrdersResult>>> PostDmOrdersSelectingAllNewOrders()
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_Orders_SelectingAllNewOrdersAsync /**/ ();

            }
        }


        private bool DmOrdersExists(int id)
        {
            return _context.DmOrders.Any(e => e.Id == id);
        }
    }
}
