﻿using System;
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
    public class DestinationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DestinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
          if (_context.Destinations == null)
          {
              return NotFound();
          }
            return await _context.Destinations.ToListAsync();
        }

        // GET: api/Destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
          if (_context.Destinations == null)
          {
              return NotFound();
          }
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return destination;
        }

        // GET: api/Destinations/viet-nam/vi
        [HttpGet("{path}/{lang}")]
        public async Task<ActionResult<Destination>> GetDestination(string path, string lang)
        {
            if (_context.Destinations == null)
            {
                return NotFound();
            }
            var destination = await _context.Destinations.Where<Destination>(item => (item.Lang == lang && item.DestinationURL == path)).FirstOrDefaultAsync();


            return destination;
        }
        // PUT: api/Destinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // POST: api/Destinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
          if (_context.Destinations == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Destinations'  is null.");
          }
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestination", new { id = destination.Id }, destination);
        }

        // DELETE: api/Destinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            if (_context.Destinations == null)
            {
                return NotFound();
            }
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinationExists(int id)
        {
            return (_context.Destinations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
