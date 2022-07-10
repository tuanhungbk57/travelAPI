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
    public class SubscribesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubscribesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Subscribes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscribe>>> GetSubscribes()
        {
          if (_context.Subscribes == null)
          {
              return NotFound();
          }
            return await _context.Subscribes.ToListAsync();
        }

        // GET: api/Subscribes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscribe>> GetSubscribe(int id)
        {
          if (_context.Subscribes == null)
          {
              return NotFound();
          }
            var subscribe = await _context.Subscribes.FindAsync(id);

            if (subscribe == null)
            {
                return NotFound();
            }

            return subscribe;
        }

        // PUT: api/Subscribes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscribe(int id, Subscribe subscribe)
        {
            if (id != subscribe.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscribeExists(id))
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

        // POST: api/Subscribes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscribe>> PostSubscribe(Subscribe subscribe)
        {
          if (_context.Subscribes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Subscribes'  is null.");
          }
            _context.Subscribes.Add(subscribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscribe", new { id = subscribe.Id }, subscribe);
        }

        // DELETE: api/Subscribes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribe(int id)
        {
            if (_context.Subscribes == null)
            {
                return NotFound();
            }
            var subscribe = await _context.Subscribes.FindAsync(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            _context.Subscribes.Remove(subscribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscribeExists(int id)
        {
            return (_context.Subscribes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
