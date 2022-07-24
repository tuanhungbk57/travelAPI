using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            return await _context.Tours.ToListAsync();
        }

        // GET: api/Tours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetTour(int id)
        {
          if (_context.Tours == null)
          {
              return NotFound();
          }
            var tour = await _context.Tours.FindAsync(id);

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        /// <summary>
        /// Lấy về tour object theo full path
        /// </summary>
        /// <param name="tourPath"></param>
        /// <returns></returns>
        [HttpGet("{desPath}/{tripPath}/{tourPath}")]
        public async Task<ActionResult<Tour>> GetTourByTourURL(string desPath, string tripPath, string tourPath)
        {
            if (_context.Tours == null)
            {
                return NotFound();
            }
            string fullPath = desPath + "/" + tripPath + "/" + tourPath;
            var tour = await _context.Tours.Where<Tour>(item => item.TourPath == fullPath).FirstOrDefaultAsync();

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        /// <summary>
        /// Lấy về danh sách tour theo trip master
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        [HttpGet("{tripId}/trip-master")]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTourByTripId(int tripId)
        {
            if (_context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours.Where<Tour>(item =>(item.TripId == tripId)).ToListAsync();

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        // PUT: api/Tours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(int id, Tour tour)
        {
            if (id != tour.Id)
            {
                return BadRequest();
            }

            _context.Entry(tour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(id))
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

        // POST: api/Tours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(Tour tour)
        {
          if (_context.Tours == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Tours'  is null.");
          }
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTour", new { id = tour.Id }, tour);
        }

        // DELETE: api/Tours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            if (_context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourExists(int id)
        {
            return (_context.Tours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
