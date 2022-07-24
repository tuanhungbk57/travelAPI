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
    public class CommunicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommunicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Communications/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<Communication>> GetCommunication(string lang)
        {
          if (_context.Communications == null)
          {
              return NotFound();
          }
            var communication = await _context.Communications.Where<Communication>(item => (item.Lang == lang)).FirstOrDefaultAsync();

            if (communication == null)
            {
                return NotFound();
            }

            return communication;
        }

        // PUT: api/Communications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunication(int id, Communication communication)
        {
            if (id != communication.Id)
            {
                return BadRequest();
            }

            _context.Entry(communication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationExists(id))
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

        // POST: api/Communications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostCommunication(Communication communication)
        {
          if (_context.Communications == null)
          {
                return -1;
          }
            _context.Communications.Add(communication);
            await _context.SaveChangesAsync();

            return communication.Id;
        }

        private bool CommunicationExists(int id)
        {
            return (_context.Communications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
