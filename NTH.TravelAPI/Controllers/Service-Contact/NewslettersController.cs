using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers.Service_Contact
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewslettersController(ApplicationDbContext context)
        {
            _context = context;
        }

      

        // GET: api/Newsletters/vi
        [HttpGet("{lang}")]
        public async Task<ActionResult<Newsletter>> GetNewsletter(string lang)
        {
          if (_context.Newsletters == null)
          {
              return NotFound();
          }
            var newsletter = await _context.Newsletters.Where<Newsletter>(item => (item.Lang == lang)).FirstOrDefaultAsync();

            if (newsletter == null)
            {
                return NotFound();
            }

            return newsletter;
        }

        // PUT: api/Newsletters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsletter(int id, Newsletter newsletter)
        {
            if (id != newsletter.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsletter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsletterExists(id))
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

        // POST: api/Newsletters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostNewsletter(Newsletter newsletter)
        {
          if (_context.Newsletters == null)
          {
                return -1;
          }
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();

            return newsletter.Id;
        }

        // DELETE: api/Newsletters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            if (_context.Newsletters == null)
            {
                return NotFound();
            }
            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }

            _context.Newsletters.Remove(newsletter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsletterExists(int id)
        {
            return (_context.Newsletters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
