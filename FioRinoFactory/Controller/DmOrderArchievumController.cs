﻿using FioRinoFactory.Data;
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
    public class DmOrderArchievumController : ControllerBase
    {
        private readonly FioAndRinoContext _context;

        public DmOrderArchievumController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmOrderArchievum>> PostDmOrderArchievum(DmOrderArchievum dmOrderArchievum)
        {
            _context.DmOrderArchievums.Add(dmOrderArchievum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmOrderArchievum", new { id = dmOrderArchievum.Id }, dmOrderArchievum);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmOrderArchievum>>> GetAllDmOrderArchievum(string linkString = null)
        {
            IQueryable<DmOrderArchievum> finalContext = _context.DmOrderArchievums;

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
        public async Task<ActionResult<DmOrderArchievum>> GetDmOrderArchievum(int id, string linkString = null)
        {
            IQueryable<DmOrderArchievum> finalContext = _context.DmOrderArchievums;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmOrderArchievum = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmOrderArchievum == null)
            {
                return NotFound();
            }

            return dmOrderArchievum;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmOrderArchievum(int id, DmOrderArchievum dmOrderArchievum)
        {
            if (id != dmOrderArchievum.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmOrderArchievum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmOrderArchievumExists(id))
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
        public async Task<IActionResult> DeleteDmOrderArchievum(int id)
        {
            var dmOrderArchievum = await _context.DmOrderArchievums.FindAsync(id);
            if (dmOrderArchievum == null)
            {
                return NotFound();
            }

            _context.DmOrderArchievums.Remove(dmOrderArchievum);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmOrderArchievumExists(int id)
        {
            return _context.DmOrderArchievums.Any(e => e.Id == id);
        }
    }
}
