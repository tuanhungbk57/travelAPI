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
    public class BlogDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDetail>>> GetBlogDetails()
        {
            if (_context.BlogDetails == null)
            {
                return NotFound();
            }
            return await _context.BlogDetails.ToListAsync();
        }

        // GET: api/BlogDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetail>> GetBlogDetail(int id)
        {
            if (_context.BlogDetails == null)
            {
                return NotFound();
            }
            var blogDetail = await _context.BlogDetails.FindAsync(id);

            if (blogDetail == null)
            {
                return NotFound();
            }

            return blogDetail;
        }

        // GET: api/BlogDetails/5
        /// <summary>
        /// Lấy về danh sách BlogDetails theo blogURL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{blogURL}/blog")]
        public async Task<ActionResult<IEnumerable<BlogDetail>>> GetBlogDetailByBlog(string blogURL)
        {
            if (_context.BlogDetails == null)
            {
                return NotFound();
            }
            var trip = await _context.BlogDetails.Where<BlogDetail>(item => (item.BlogURL == blogURL)).ToListAsync();

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        [HttpGet("{blogURL}/{blogDetailURL}")]
        public async Task<ActionResult<IEnumerable<BlogDetail>>> GetBlogDetailBlogMasterAndBlog(string blogURL, string blogDetailURL)
        {
            if (_context.BlogDetails == null)
            {
                return NotFound();
            }
            var fullblogDetailURL = blogURL + "/" + blogDetailURL;
            var trip = await _context.BlogDetails.Where<BlogDetail>(item => (item.BlogURL == blogURL && item.BlogDetailURL == fullblogDetailURL)).ToListAsync();

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        // PUT: api/BlogDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogDetail(int id, BlogDetail trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogDetailExists(id))
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

        // POST: api/BlogDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogDetail>> PostBlogDetail(BlogDetail blogDetail)
        {
            if (_context.BlogDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BlogDetails'  is null.");
            }
            blogDetail.CreatedDate = DateTime.Now;
            _context.BlogDetails.Add(blogDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogDetail", new { id = blogDetail.Id }, blogDetail);
        }

        // DELETE: api/BlogDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogDetail(int id)
        {
            if (_context.BlogDetails == null)
            {
                return NotFound();
            }
            var blogDetail = await _context.BlogDetails.FindAsync(id);
            var blogPosts = await _context.BlogPosts.Where<BlogPost>(item =>(item.BlogDetailID == id)).ToListAsync();
            if (blogDetail == null)
            {
                return NotFound();
            }

            _context.BlogDetails.Remove(blogDetail);
            foreach (var item in blogPosts)
            {
                _context.BlogPosts.Remove(item);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogDetailExists(int id)
        {
            return (_context.BlogDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
