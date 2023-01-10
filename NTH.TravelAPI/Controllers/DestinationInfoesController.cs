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
    public class DestinationInfoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DestinationInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: api/DestinationInfoes/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<DestinationInfo>> GetDestinationInfo(string lang)
        {
          if (_context.DestinationInfos == null)
          {
              return NotFound();
          }
            var destinationInfo = await _context.DestinationInfos.Where<DestinationInfo>(item => (item.Lang == lang)).FirstOrDefaultAsync();

            

            return destinationInfo;
        }

        // PUT: api/DestinationInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestinationInfo(int? id, DestinationInfo destinationInfo)
        {
            if (id != destinationInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(destinationInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationInfoExists(id))
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

        // POST: api/DestinationInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostDestinationInfo(DestinationInfo destinationInfo)
        {
          if (_context.DestinationInfos == null)
          {
                return -1;
          }
            _context.DestinationInfos.Add(destinationInfo);
            await _context.SaveChangesAsync();

            return destinationInfo.Id;
        }

        // DELETE: api/DestinationInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinationInfo(int? id)
        {
            if (_context.DestinationInfos == null)
            {
                return NotFound();
            }
            var destinationInfo = await _context.DestinationInfos.FindAsync(id);
            if (destinationInfo == null)
            {
                return NotFound();
            }

            _context.DestinationInfos.Remove(destinationInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinationInfoExists(int? id)
        {
            return (_context.DestinationInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
