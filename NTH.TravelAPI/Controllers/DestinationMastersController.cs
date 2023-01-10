using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DestinationMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DestinationMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DestinationMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinationMaster>>> GetDestinationMasters()
        {
          if (_context.DestinationMasters == null)
          {
              return NotFound();
          }
            return await _context.DestinationMasters.ToListAsync();
        }

        // GET: api/DestinationMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinationMaster>> GetDestinationMaster(int id)
        {
          if (_context.DestinationMasters == null)
          {
              return NotFound();
          }
            var destinationMaster = await _context.DestinationMasters.FindAsync(id);

            if (destinationMaster == null)
            {
                return NotFound();
            }

            return destinationMaster;
        }

        // PUT: api/DestinationMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestinationMaster(int id, DestinationMaster destinationMaster)
        {
            if (id != destinationMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(destinationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationMasterExists(id))
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

        // POST: api/DestinationMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DestinationMaster>> PostDestinationMaster(DestinationMaster destinationMaster)
        {
          if (_context.DestinationMasters == null)
          {
              return Problem("Entity set 'ApplicationDbContext.DestinationMasters'  is null.");
          }
            _context.DestinationMasters.Add(destinationMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestinationMaster", new { id = destinationMaster.Id }, destinationMaster);
        }

        // DELETE: api/DestinationMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinationMaster(int id)
        {
            if (_context.DestinationMasters == null)
            {
                return NotFound();
            }
            var destinationMaster = await _context.DestinationMasters.FindAsync(id);
            //var destination = await _context.Destinations.Where<Destination>(item =>(item.DestinationURL == destinationMaster.DestinationURL)).ToListAsync();
            if (destinationMaster == null)
            {
                return NotFound();
            }

            _context.DestinationMasters.Remove(destinationMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinationMasterExists(int id)
        {
            return (_context.DestinationMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
