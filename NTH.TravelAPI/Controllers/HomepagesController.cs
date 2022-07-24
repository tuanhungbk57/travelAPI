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
    public class HomepagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomepagesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Homepages/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<IEnumerable<Homepage>>> GetHomepage(string lang)
        {
          if (_context.Homepages == null)
          {
              return NotFound();
          }
            var homepage = await _context.Homepages.Where<Homepage>(item => (item.Lang == lang)).ToListAsync();

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
        public async Task<int> PostHomepage(Homepage homepage)
        {
          if (_context.Homepages == null)
          {
                //return Problem("Entity set 'ApplicationDbContext.Homepages'  is null.");
                return -1;
          }
            _context.Homepages.Add(homepage);
            await _context.SaveChangesAsync();

            return homepage.Id;
        }


        private bool HomepageExists(int id)
        {
            return (_context.Homepages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
