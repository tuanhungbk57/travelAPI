using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/Footers")]
    [ApiController]
    [Authorize]
    public class FootersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FootersController(ApplicationDbContext context)
        {
            _context = context;
        }


        

        /// <summary>
        /// Lấy về object theo ngôn ngữ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{lang}")]
        public async Task<ActionResult<Footer>> GetFooterByLang(string lang)
        {
            if (_context.Footers == null)
            {
                return NotFound();
            }
            var footer = await _context.Footers.Where<Footer>(item => item.Lang == lang).FirstOrDefaultAsync();



            return footer;
        }

        // PUT: api/Tourdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFooter(int id, Footer footer)
        {
            if (id != footer.Id)
            {
                return BadRequest();
            }

            _context.Entry(footer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FooterExists(id))
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
        public async Task<int> PostFooter(Footer footer)
        {
           
            _context.Footers.Add(footer);
            await _context.SaveChangesAsync();

            return footer.Id;
        }

        // DELETE: api/Tourdetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFooter(int id)
        {
            if (_context.Footers == null)
            {
                return NotFound();
            }
            var footer = await _context.Footers.FindAsync(id);
            if (footer == null)
            {
                return NotFound();
            }

            _context.Footers.Remove(footer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FooterExists(int id)
        {
            return (_context.Footers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
