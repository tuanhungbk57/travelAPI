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
    public class TripdetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TripdetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tripdetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tripdetail>>> GetTripdetails()
        {
          if (_context.Tripdetails == null)
          {
              return NotFound();
          }
            return await _context.Tripdetails.ToListAsync();
        }

        // GET: api/Tripdetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tripdetail>> GetTripdetail(int id)
        {
          if (_context.Tripdetails == null)
          {
              return NotFound();
          }
            var tripdetail = await _context.Tripdetails.FindAsync(id);

            if (tripdetail == null)
            {
                return NotFound();
            }

            return tripdetail;
        }

        [HttpGet("{tripMasterId}/{lang}")]
        public async Task<ActionResult<Tripdetail>> GetTripdetailByTripAndLang(int tripMasterId, string lang)
        {
            if (_context.Tripdetails == null)
            {
                return NotFound();
            }
            var tripdetail = await _context.Tripdetails.Where<Tripdetail>(item => (item.TripMasterId == tripMasterId && item.Lang == lang)).FirstOrDefaultAsync();

           

            return tripdetail;
        }

        // PUT: api/Tripdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripdetail(int id, Tripdetail tripdetail)
        {
            if (id != tripdetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripdetailExists(id))
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

        // POST: api/Tripdetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tripdetail>> PostTripdetail(Tripdetail tripdetail)
        {
          if (_context.Tripdetails == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tripdetails'  is null.");
          }
            _context.Tripdetails.Add(tripdetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripdetail", new { id = tripdetail.Id }, tripdetail);
        }

        // DELETE: api/Tripdetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripdetail(int id)
        {
            if (_context.Tripdetails == null)
            {
                return NotFound();
            }
            var tripdetail = await _context.Tripdetails.FindAsync(id);
            if (tripdetail == null)
            {
                return NotFound();
            }

            _context.Tripdetails.Remove(tripdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripdetailExists(int id)
        {
            return (_context.Tripdetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
