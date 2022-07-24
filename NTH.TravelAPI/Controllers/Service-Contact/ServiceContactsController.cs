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
    public class ServiceContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceContactsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{lang}")]
        public async Task<ActionResult<ServiceContact>> GetHomepage(string lang)
        {
            if (_context.Homepages == null)
            {
                return NotFound();
            }
            var homepage = await _context.ServiceContacts.Where<ServiceContact>(item => (item.Lang == lang)).FirstOrDefaultAsync();

            if (homepage == null)
            {
                return NotFound();
            }

            return homepage;
        }

        // PUT: api/ServiceContacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceContact(int id, ServiceContact serviceContact)
        {
            if (id != serviceContact.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceContactExists(id))
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

        // POST: api/ServiceContacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostServiceContact(ServiceContact serviceContact)
        {
          if (_context.ServiceContacts == null)
          {
              return -1;
          }
            _context.ServiceContacts.Add(serviceContact);
            await _context.SaveChangesAsync();

            return serviceContact.Id;
        }

       

        private bool ServiceContactExists(int id)
        {
            return (_context.ServiceContacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
