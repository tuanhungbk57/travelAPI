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
    public class TourdetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TourdetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tourdetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tourdetail>>> GetTourDetails()
        {
          if (_context.TourDetails == null)
          {
              return NotFound();
          }
            return await _context.TourDetails.ToListAsync();
        }

        // GET: api/Tourdetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tourdetail>> GetTourdetail(int id)
        {
          if (_context.TourDetails == null)
          {
              return NotFound();
          }
            var tourdetail = await _context.TourDetails.FindAsync(id);

            if (tourdetail == null)
            {
                return NotFound();
            }

            return tourdetail;
        }

        /// <summary>
        /// Lấy về object theo master và ngôn ngữ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{tourId}/{lang}")]
        public async Task<ActionResult<Tourdetail>> GetTourdetailByMasterAndLang(int tourId, string lang)
        {
            if (_context.TourDetails == null)
            {
                return NotFound();
            }
            var tourdetail = await _context.TourDetails.Where<Tourdetail>(item => item.TourMasterId == tourId && item.Lang == lang).FirstOrDefaultAsync();

            if (tourdetail == null)
            {
                return NotFound();
            }

            return tourdetail;
        }

        // PUT: api/Tourdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourdetail(int id, Tourdetail tourdetail)
        {
            if (id != tourdetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(tourdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourdetailExists(id))
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

        // POST: api/Tourdetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tourdetail>> PostTourdetail(Tourdetail tourdetail)
        {
          if (_context.TourDetails == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TourDetails'  is null.");
          }
            _context.TourDetails.Add(tourdetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTourdetail", new { id = tourdetail.Id }, tourdetail);
        }

        // DELETE: api/Tourdetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourdetail(int id)
        {
            if (_context.TourDetails == null)
            {
                return NotFound();
            }
            var tourdetail = await _context.TourDetails.FindAsync(id);
            if (tourdetail == null)
            {
                return NotFound();
            }

            _context.TourDetails.Remove(tourdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourdetailExists(int id)
        {
            return (_context.TourDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
