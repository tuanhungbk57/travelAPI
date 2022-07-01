using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.TravelAPI.Auth;
using NTH.TravelAPI.Models;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomepagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomepagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Homepages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vihomepage>>> GetVihomepage()
        {
          if (_context.Vihomepage == null)
          {
              return NotFound();
          }
            return await _context.Vihomepage.ToListAsync();
        }

        // GET: api/Homepages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vihomepage>> GetVihomepage(uint id)
        {
          if (_context.Vihomepage == null)
          {
              return NotFound();
          }
            var vihomepage = await _context.Vihomepage.FindAsync(id);

            if (vihomepage == null)
            {
                return NotFound();
            }

            return vihomepage;
        }

        // PUT: api/Homepages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVihomepage(uint id, Vihomepage vihomepage)
        {
            if (id != vihomepage.Id)
            {
                return BadRequest();
            }

            _context.Entry(vihomepage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VihomepageExists(id))
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

        // POST: api/Homepages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vihomepage>> PostVihomepage(Vihomepage vihomepage)
        {
          if (_context.Vihomepage == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Vihomepage'  is null.");
          }
            _context.Vihomepage.Add(vihomepage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVihomepage", new { id = vihomepage.Id }, vihomepage);
        }

        // DELETE: api/Homepages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVihomepage(uint id)
        {
            if (_context.Vihomepage == null)
            {
                return NotFound();
            }
            var vihomepage = await _context.Vihomepage.FindAsync(id);
            if (vihomepage == null)
            {
                return NotFound();
            }

            _context.Vihomepage.Remove(vihomepage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VihomepageExists(uint id)
        {
            return (_context.Vihomepage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
