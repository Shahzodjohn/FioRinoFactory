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
    public class DmOrderProductsController : Controller
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

        public class DetailProductListStanMagazynuParams { public int ProductId { get; set; } }
        // EXPOSE_dm_OrderProducts_DetailProductListStanMagazynu

        [HttpPost("DetailProductListStanMagazynu")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_OrderProducts_DetailProductListStanMagazynuResult>>> PostDmOrderProductsDetailProductListStanMagazynu([FromBody] DetailProductListStanMagazynuParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_OrderProducts_DetailProductListStanMagazynuAsync /**/ (parameters.ProductId);

            }
        }

        public class CreateOrderToOrderProductsParams { public int OrderId { get; set; } }
        // EXPOSE_dm_OrderProducts_CreateOrderToOrderProducts

        [HttpPost("CreateOrderToOrderProducts")]
        public async Task<ActionResult> PostDmOrderProductsCreateOrderToOrderProducts([FromBody] CreateOrderToOrderProductsParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_OrderProducts_CreateOrderToOrderProducts /**/ (parameters.OrderId);
                return Ok();
            }
        }

        public class InsertingOrderIdProductsIdParams
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Amount { get; set; }
            public int SizeId { get; set; }
            public int ProductStatusId { get; set; }
            public int SenderId { get; set; }
            public int RecieverId { get; set; }
            public bool is_removed { get; set; }
        }
        // EXPOSE_dm_OrderProducts_InsertingOrderIdProductsId

        [HttpPost("InsertingOrderIdProductsId")]
        public async Task<ActionResult> PostDmOrderProductsInsertingOrderIdProductsId([FromBody] InsertingOrderIdProductsIdParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_OrderProducts_InsertingOrderIdProductsId /**/ (parameters.OrderId, parameters.ProductId, parameters.Amount, parameters.SizeId, parameters.ProductStatusId, parameters.SenderId, parameters.RecieverId, parameters.is_removed);
                return Ok();
            }
        }

        public class OpenningTheSpecificOrderParams { public int OrderId { get; set; } }
        // EXPOSE_dm_OrderProducts_OpenningTheSpecificOrder

        [HttpPost("OpenningTheSpecificOrder")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_OrderProducts_OpenningTheSpecificOrderResult>>> PostDmOrderProductsOpenningTheSpecificOrder([FromBody] OpenningTheSpecificOrderParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_OrderProducts_OpenningTheSpecificOrderAsync /**/ (parameters.OrderId);

            }
        }

        public class GetOrderDetailParams { public int OrderId { get; set; } }
        // EXPOSE_dm_OrderProducts_GetOrderDetail

        [HttpPost("GetOrderDetail")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_OrderProducts_GetOrderDetailResult>>> PostDmOrderProductsGetOrderDetail([FromBody] GetOrderDetailParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_OrderProducts_GetOrderDetailAsync /**/ (parameters.OrderId);

            }
        }

        public class PersonalWzDetailsParams { public int OrderId { get; set; } }
        // EXPOSE_dm_OrderProducts_PersonalWzDetails

        [HttpPost("PersonalWzDetails")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_OrderProducts_PersonalWzDetailsResult>>> PostDmOrderProductsPersonalWzDetails([FromBody] PersonalWzDetailsParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_OrderProducts_PersonalWzDetailsAsync /**/ (parameters.OrderId);

            }
        }

        public class SelectingProductsAfterScanningParams { public int OrderId { get; set; } }
        // EXPOSE_dm_OrderProducts_SelectingProductsAfterScanning

        [HttpPost("SelectingProductsAfterScanning")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_OrderProducts_SelectingProductsAfterScanningResult>>> PostDmOrderProductsSelectingProductsAfterScanning([FromBody] SelectingProductsAfterScanningParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_OrderProducts_SelectingProductsAfterScanningAsync /**/ (parameters.OrderId);

            }
        }


        private bool DmOrderProductsExists(int id)
        {
            return _context.DmOrderProducts.Any(e => e.Id == id);
        }
    }
}
