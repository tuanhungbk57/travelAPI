using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Helper;
using NTH.Core.Models;
using NTH.Travel.BL.Contracts;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactRepo _contactRepo;

        public ContactsController(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }


        // GET: api/Contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contacts = await _contactRepo.GetContacts();
                return Ok(contacts);
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }

        }

        // GET: api/Contacts
        [HttpGet("all")]
        public async Task<IActionResult> GetContactsList()
        {
            try
            {
                var contacts = await _contactRepo.GetContactsList();
                return Ok(contacts);
            }
            catch (Exception ex)
            {

                //log error
                return StatusCode(500, ex.Message);
            }

        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
          if (_context.Contacts == null)
          {
              return NotFound();
          }
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
          if (_context.Contacts == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Contacts'  is null.");
          }
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
