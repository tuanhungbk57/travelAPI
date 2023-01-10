using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models.Blog;

namespace NTH.TravelAPI.Controllers.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogGeneralsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogGeneralsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/BlogGenerals/5
        [HttpGet("{lang}")]
        public async Task<ActionResult<BlogGeneral>> GetBlogGeneral(string lang)
        {
            if (_context.BlogGenerals == null)
            {
                return NotFound();
            }
            var blogGeneral = await _context.BlogGenerals.Where<BlogGeneral>(item => (item.Lang == lang)).FirstOrDefaultAsync();



            return blogGeneral;
        }

        // PUT: api/BlogGenerals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogGeneral(int? id, BlogGeneral blogGeneral)
        {
            if (id != blogGeneral.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogGeneral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogGeneralExists(id))
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

        // POST: api/BlogGenerals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<int> PostBlogGeneral(BlogGeneral blogGeneral)
        {
            if (_context.BlogGenerals == null)
            {
                return -1;
            }
            _context.BlogGenerals.Add(blogGeneral);
            await _context.SaveChangesAsync();

            return blogGeneral.Id;
        }

        // DELETE: api/BlogGenerals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogGeneral(int? id)
        {
            if (_context.BlogGenerals == null)
            {
                return NotFound();
            }
            var blogGeneral = await _context.BlogGenerals.FindAsync(id);
            if (blogGeneral == null)
            {
                return NotFound();
            }

            _context.BlogGenerals.Remove(blogGeneral);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogGeneralExists(int? id)
        {
            return (_context.BlogGenerals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
