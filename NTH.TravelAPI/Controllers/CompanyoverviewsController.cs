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
    public class CompanyoverviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyoverviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Companyoverviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Companyoverview>>> GetCompanyoverviews()
        {
          if (_context.Companyoverviews == null)
          {
              return NotFound();
          }
            return await _context.Companyoverviews.ToListAsync();
        }

        // GET: api/Companyoverviews2/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<IEnumerable<Companyoverview>>> GetCompanyoverview(string lang)
        {
            if (_context.Companyoverviews == null)
            {
                return NotFound();
            }
            var companyoverview = await _context.Companyoverviews.Where<Companyoverview>(item => (item.Lang == lang)).ToListAsync();

            if (companyoverview == null)
            {
                return NotFound();
            }

            return companyoverview;
        }

        // PUT: api/Companyoverviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyoverview(int id, Companyoverview companyoverview)
        {
            if (id != companyoverview.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyoverview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyoverviewExists(id))
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

        // POST: api/Companyoverviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostCompanyoverview(Companyoverview companyoverview)
        {
          if (_context.Companyoverviews == null)
          {
              //return Problem("Entity set 'ApplicationDbContext.Companyoverviews'  is null.");
          }
            _context.Companyoverviews.Add(companyoverview);
            await _context.SaveChangesAsync();
            return companyoverview.Id;
        }

        // DELETE: api/Companyoverviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyoverview(int id)
        {
            if (_context.Companyoverviews == null)
            {
                return NotFound();
            }
            var companyoverview = await _context.Companyoverviews.FindAsync(id);
            if (companyoverview == null)
            {
                return NotFound();
            }

            _context.Companyoverviews.Remove(companyoverview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyoverviewExists(int id)
        {
            return (_context.Companyoverviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
