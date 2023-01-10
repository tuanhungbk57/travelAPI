﻿using System;
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
    public class TripsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
          if (_context.Trips == null)
          {
              return NotFound();
          }
            return await _context.Trips.ToListAsync();
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
          if (_context.Trips == null)
          {
              return NotFound();
          }
            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // GET: api/Trips/5
        /// <summary>
        /// Lấy về danh sách Trips theo desURL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{desURL}/des")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTripByDes(string desURL)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }
            var trip = await _context.Trips.Where<Trip>(item =>(item.DestinationURL == desURL)).ToListAsync();

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        [HttpGet("{desURL}/{tripURL}")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTripByDesAndTrip(string desURL, string tripURL)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }
            var fullTripURL = desURL + "/" + tripURL;
            var trip = await _context.Trips.Where<Trip>(item => (item.DestinationURL == desURL && item.TripURL == fullTripURL)).ToListAsync();

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // PUT: api/Trips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
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

        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
          if (_context.Trips == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Trips'  is null.");
          }
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new { id = trip.Id }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            if (_context.Trips == null)
            {
                return NotFound();
            }
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripExists(int id)
        {
            return (_context.Trips?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
