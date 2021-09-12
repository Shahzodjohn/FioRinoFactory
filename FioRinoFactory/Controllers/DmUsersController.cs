using FioRinoFactory.Data;
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
    public class DmUsersController : Controller
    {
        private readonly FioAndRinoContext _context;

        public DmUsersController(FioAndRinoContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<DmUser>> PostDmUsers(DmUser dmUsers)
        {
            _context.DmUsers.Add(dmUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDmUsers", new { id = dmUsers.Id }, dmUsers);
        }


        [HttpGet("all/{linkString?}")]
        public async Task<ActionResult<IEnumerable<DmUser>>> GetAllDmUsers(string linkString = null)
        {
            IQueryable<DmUser> finalContext = _context.DmUsers;

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
        public async Task<ActionResult<DmUser>> GetDmUsers(int id, string linkString = null)
        {
            IQueryable<DmUser> finalContext = _context.DmUsers;

            if (!string.IsNullOrWhiteSpace(linkString))
            {
                foreach (var es in (linkString ?? "").Split(";"))
                {
                    finalContext = finalContext.Include(es);
                }
            }

            var dmUsers = await finalContext.FirstOrDefaultAsync(e => e.Id == id);

            if (dmUsers == null)
            {
                return NotFound();
            }

            return dmUsers;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDmUsers(int id, DmUser dmUsers)
        {
            if (id != dmUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(dmUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DmUsersExists(id))
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
        public async Task<IActionResult> DeleteDmUsers(int id)
        {
            var dmUsers = await _context.DmUsers.FindAsync(id);
            if (dmUsers == null)
            {
                return NotFound();
            }

            _context.DmUsers.Remove(dmUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DmUsersExists(int id)
        {
            return _context.DmUsers.Any(e => e.Id == id);
        }
    }
}
