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
using NTH.Core.Models.Blog;

namespace NTH.TravelAPI.Controllers.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BlogMastersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/BlogMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogMaster>>> GetBlogMasters()
        {
            if (_context.BlogMasters == null)
            {
                return NotFound();
            }
            return await _context.BlogMasters.ToListAsync();
        }

        // GET: api/BlogMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogMaster>> GetDestinationMaster(int id)
        {
            if (_context.BlogMasters == null)
            {
                return NotFound();
            }
            var blogMaster = await _context.BlogMasters.FindAsync(id);

            if (blogMaster == null)
            {
                return NotFound();
            }

            return blogMaster;
        }

        // PUT: api/BlogMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogMaster(int id, BlogMaster blogMaster)
        {
            if (id != blogMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogMasterExists(id))
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

        // POST: api/BlogMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogMaster>> PostBlogMaster(BlogMaster blogMaster)
        {
            if (_context.BlogMasters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BlogMasters'  is null.");
            }
            _context.BlogMasters.Add(blogMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestinationMaster", new { id = blogMaster.Id }, blogMaster);
        }

        // DELETE: api/BlogMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogMaster(int id)
        {
            if (_context.BlogMasters == null)
            {
                return NotFound();
            }
            var blogMaster = await _context.BlogMasters.FindAsync(id);
            var blogDetails = await _context.BlogDetails.Where<BlogDetail>(item => (item.BlogURL == blogMaster.BlogURL)).ToListAsync();
            var blogs = await _context.Blogs.Where<Blogg>(item => (item.BlogURL == blogMaster.BlogURL)).ToListAsync();

            if (blogMaster == null)
            {
                return NotFound();
            }
            //xoa master, blog, detail va post
            _context.BlogMasters.Remove(blogMaster);
            foreach (var item in blogs)
            {
                _context.Blogs.Remove(item);

            }
            foreach (var item in blogDetails)
            {
                var blogPosts = await _context.BlogPosts.Where<BlogPost>(i => (i.BlogDetailID == item.Id)).ToListAsync();
                foreach (var b in blogPosts)
                {
                    _context.BlogPosts.Remove(b);
                }
                _context.BlogDetails.Remove(item);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogMasterExists(int id)
        {
            return (_context.BlogMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
