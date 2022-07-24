using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers.Website
{
    [Route("api/web/[controller]")]
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
        public async Task<ActionResult<IEnumerable<Homepage>>> GetHomepages()
        {
          if (_context.Homepages == null)
          {
              return NotFound();
          }
            return await _context.Homepages.ToListAsync();
        }

        // GET: api/Homepages/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<Homepage>> GetHomepage(string lang)
        {
          if (_context.Homepages == null)
          {
              return NotFound();
          }
            var homepage = await _context.Homepages.Where<Homepage>(item => (item.Lang == lang)).FirstOrDefaultAsync();

            if (homepage == null)
            {
                return NotFound();
            }

            return homepage;
        }

        // PUT: api/Homepages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomepage(int id, Homepage homepage)
        {
            if (id != homepage.Id)
            {
                return BadRequest();
            }

            _context.Entry(homepage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomepageExists(id))
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
        public async Task<ActionResult<Homepage>> PostHomepage(Homepage homepage)
        {
          if (_context.Homepages == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Homepages'  is null.");
          }
            _context.Homepages.Add(homepage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomepage", new { id = homepage.Id }, homepage);
        }

        private bool HomepageExists(int id)
        {
            return (_context.Homepages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
