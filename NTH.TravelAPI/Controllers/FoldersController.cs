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
    public class FoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Folders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolders()
        {
          if (_context.Folders == null)
          {
              return NotFound();
          }
            return await _context.Folders.ToListAsync();
        }

        [HttpGet("des")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetDesFolders()
        {
            if (_context.Folders == null)
            {
                return NotFound();
            }
            return await _context.Folders.Where<Folder>(item => item.Type == 0).ToListAsync();
        }

        [HttpGet("general")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetGenFolders()
        {
            if (_context.Folders == null)
            {
                return NotFound();
            }
            return await _context.Folders.Where<Folder>(item => item.Type == 2).ToListAsync();
        }

        // GET: api/Folders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> GetFolder(uint id)
        {
          if (_context.Folders == null)
          {
              return NotFound();
          }
            var folder = await _context.Folders.FindAsync(id);

            if (folder == null)
            {
                return NotFound();
            }

            return folder;
        }

        // GET: api/Folders/5
        [HttpGet("{parentId}/parent")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolderByParent(int parentId)
        {
            if (_context.Folders == null)
            {
                return NotFound();
            }
            var folders = await _context.Folders.Where<Folder>(item => (item.ParentId == parentId)).ToListAsync();

            if (folders == null)
            {
                return NotFound();
            }

            return folders;
        }

        // PUT: api/Folders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolder(uint id, Folder folder)
        {
            if (id != folder.Id)
            {
                return BadRequest();
            }

            _context.Entry(folder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolderExists(id))
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

        // POST: api/Folders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Folder>> PostFolder(Folder folder)
        {
          if (_context.Folders == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Folders'  is null.");
          }
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { id = folder.Id }, folder);
        }

        // DELETE: api/Folders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(uint id)
        {
            if (_context.Folders == null)
            {
                return NotFound();
            }
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FolderExists(uint id)
        {
            return (_context.Folders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
