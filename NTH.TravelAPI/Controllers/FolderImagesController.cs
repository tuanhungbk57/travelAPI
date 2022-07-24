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
    public class FolderImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FolderImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FolderImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderImage>>> GetFolderImages()
        {
          if (_context.FolderImages == null)
          {
              return NotFound();
          }
            return await _context.FolderImages.ToListAsync();
        }

        // GET: api/FolderImages/5
        /// <summary>
        /// Danh sách ảnh của DesId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FolderImage>>> GetFolderImage(uint id)
        {
          if (_context.FolderImages == null)
          {
              return NotFound();
          }
            var folderImages = await _context.FolderImages.Where<FolderImage>(item => ( (uint) item.FolderId == id )).ToListAsync();

            if (folderImages == null)
            {
                return NotFound();
            }

            return folderImages;
        }

        /// <summary>
        /// Lấy danh sách ảnh của Gen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/gen")]
        public async Task<ActionResult<IEnumerable<FolderImage>>> GetImageByGen(uint id)
        {
            if (_context.FolderImages == null)
            {
                return NotFound();
            }
            var folderImages = await _context.FolderImages.Where<FolderImage>(item => ((uint)item.FolderId == id)).ToListAsync();

            if (folderImages == null)
            {
                return NotFound();
            }

            return folderImages;
        }

        /// <summary>
        /// Lấy danh sách ảnh của Trip theo Des
        /// </summary>
        /// <param name="desId"></param>
        /// <param name="tripId"></param>
        /// <returns></returns>
        [HttpGet("{desId}/{tripId}")]
        public async Task<ActionResult<IEnumerable<FolderImage>>> GetFolderImageByDesId(uint desId, int tripId)
        {
            if (_context.FolderImages == null)
            {
                return NotFound();
            }
            var folderImages = await _context.FolderImages.Where<FolderImage>(item => ((uint)item.FolderId == tripId && item.ParentId == desId)).ToListAsync();

            if (folderImages == null)
            {
                return NotFound();
            }

            return folderImages;
        }

        // PUT: api/FolderImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolderImage(uint id, FolderImage folderImage)
        {
            if (id != folderImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(folderImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolderImageExists(id))
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

        // POST: api/FolderImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FolderImage>> PostFolderImage(FolderImage folderImage)
        {
          if (_context.FolderImages == null)
          {
              return Problem("Entity set 'ApplicationDbContext.FolderImages'  is null.");
          }
            _context.FolderImages.Add(folderImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolderImage", new { id = folderImage.Id }, folderImage);
        }

        // DELETE: api/FolderImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolderImage(uint id)
        {
            if (_context.FolderImages == null)
            {
                return NotFound();
            }
            var folderImage = await _context.FolderImages.FindAsync(id);
            if (folderImage == null)
            {
                return NotFound();
            }

            _context.FolderImages.Remove(folderImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FolderImageExists(uint id)
        {
            return (_context.FolderImages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
