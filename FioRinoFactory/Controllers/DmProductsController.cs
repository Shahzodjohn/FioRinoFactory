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
    public class DmProductsController : Controller
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

        public class CreateProductParams
        {
            public string SKUnumber { get; set; }
            public string ProductName { get; set; }
            public int SizeId { get; set; }
            public int Amount { get; set; }
            public string Gtin { get; set; }
            public int CategoryId { get; set; }
        }
        // EXPOSE_dm_Products_CreateProduct

        [HttpPost("CreateProduct")]
        public async Task<ActionResult> PostDmProductsCreateProduct([FromBody] CreateProductParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_Products_CreateProduct /**/ (parameters.SKUnumber, parameters.ProductName, parameters.SizeId, parameters.Amount, parameters.Gtin, parameters.CategoryId);
                return Ok();
            }
        }

        public class ScanningForAddingProductParams
        {
            public string SKU { get; set; }
            public string ProductName { get; set; }
            public string Gtin { get; set; }
            public int SizeId { get; set; }
            public int Amount { get; set; }
            public int CategoryId { get; set; }
        }
        // EXPOSE_dm_Products_ScanningForAddingProduct

        [HttpPost("ScanningForAddingProduct")]
        public async Task<ActionResult> PostDmProductsScanningForAddingProduct([FromBody] ScanningForAddingProductParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_Products_ScanningForAddingProduct /**/ (parameters.SKU, parameters.ProductName, parameters.Gtin, parameters.SizeId, parameters.Amount, parameters.CategoryId);
                return Ok();
            }
        }

        public class SearchingByNameCategoryAndSizeParams
        {
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public string Size { get; set; }
        }
        // EXPOSE_dm_Products_SearchingByNameCategoryAndSize

        [HttpPost("SearchingByNameCategoryAndSize")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_Products_SearchingByNameCategoryAndSizeResult>>> PostDmProductsSearchingByNameCategoryAndSize([FromBody] SearchingByNameCategoryAndSizeParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_Products_SearchingByNameCategoryAndSizeAsync /**/ (parameters.ProductName, parameters.CategoryName, parameters.Size);

            }
        }

        public class SelectingCurrentOrderByStatusParams { public int OrderId { get; set; } }
        // EXPOSE_dm_Products_SelectingCurrentOrderByStatus

        [HttpPost("SelectingCurrentOrderByStatus")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_Products_SelectingCurrentOrderByStatusResult>>> PostDmProductsSelectingCurrentOrderByStatus([FromBody] SelectingCurrentOrderByStatusParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_Products_SelectingCurrentOrderByStatusAsync /**/ (parameters.OrderId);

            }
        }

        public class SelectingProductGtinParams { public int ProductId { get; set; } }
        // EXPOSE_dm_Products_SelectingProductGtin

        [HttpPost("SelectingProductGtin")]
        public async Task<ActionResult<List<SPToCoreContext.EXPOSE_dm_Products_SelectingProductGtinResult>>> PostDmProductsSelectingProductGtin([FromBody] SelectingProductGtinParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                return await db.EXPOSE_dm_Products_SelectingProductGtinAsync /**/ (parameters.ProductId);

            }
        }

        public class UpdatingWhileScanningBySKUCodeParams { public string SKU { get; set; } }
        // EXPOSE_dm_Products_UpdatingWhileScanningBySKUCode

        [HttpPost("UpdatingWhileScanningBySKUCode")]
        public async Task<ActionResult> PostDmProductsUpdatingWhileScanningBySKUCode([FromBody] UpdatingWhileScanningBySKUCodeParams parameters)
        {
            using (SPToCoreContext db = new SPToCoreContext())
            {
                db.EXPOSE_dm_Products_UpdatingWhileScanningBySKUCode /**/ (parameters.SKU);
                return Ok();
            }
        }


        private bool DmProductsExists(int id)
        {
            return _context.DmProducts.Any(e => e.Id == id);
        }
    }
}
