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
    public class CompanyinfoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyinfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Companyinfoes
        [HttpGet]
        public async Task<ActionResult<Companyinfo>> GetCompanyinfos()
        {
          if (_context.Companyinfos == null)
          {
              return NotFound();
          }
            return await _context.Companyinfos.FirstOrDefaultAsync();
        }

        // GET: api/Companyinfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Companyinfo>> GetCompanyinfo(int id)
        {
          if (_context.Companyinfos == null)
          {
              return NotFound();
          }
            var companyinfo = await _context.Companyinfos.FindAsync(id);

            if (companyinfo == null)
            {
                return NotFound();
            }

            return companyinfo;
        }

        // PUT: api/Companyinfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyinfo(int id, Companyinfo companyinfo)
        {
            if (id != companyinfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyinfoExists(id))
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

        // POST: api/Companyinfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Companyinfo>> PostCompanyinfo(Companyinfo companyinfo)
        {
          if (_context.Companyinfos == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Companyinfos'  is null.");
          }
            _context.Companyinfos.Add(companyinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyinfo", new { id = companyinfo.Id }, companyinfo);
        }

        // DELETE: api/Companyinfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyinfo(int id)
        {
            if (_context.Companyinfos == null)
            {
                return NotFound();
            }
            var companyinfo = await _context.Companyinfos.FindAsync(id);
            if (companyinfo == null)
            {
                return NotFound();
            }

            _context.Companyinfos.Remove(companyinfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyinfoExists(int id)
        {
            return (_context.Companyinfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
