using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers.Website
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DapperContext _dapper;

        public DestinationsController(ApplicationDbContext context, DapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        // GET: api/Destinations/vi/lang
        /// <summary>
        /// Lấy về danh sách Des theo ngôn ngữ để hiển thị lên trang chủ
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("{lang}/lang")]
        public async Task<IEnumerable<object>> GetDestinationsByLang(string lang)
        {
            var procedureName = "Proc_Destination_GetListByLang";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryAsync<object>
                    (procedureName, new {v_lang= lang}, commandType: CommandType.StoredProcedure);
                return des;
            }
            
        }

        /// <summary>
        /// Lấy về chi tiết 1 des theo ngôn ngữ
        /// </summary>
        /// <param name="path">path của des</param>
        /// <param name="lang">ngôn ngữ</param>
        /// <returns></returns>
        [HttpGet("{path}/{lang}")]
        public async Task<object> GetDestination(string path, string lang)
        {
            var procedureName = "Proc_Destination_GetByPathAndLang";
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang, v_path = path }, commandType: CommandType.StoredProcedure); 
                return des;
            }
        }

        // GET: api/Destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
          if (_context.Destinations == null)
          {
              return NotFound();
          }
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return destination;
        }

        // PUT: api/Destinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // POST: api/Destinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
          if (_context.Destinations == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Destinations'  is null.");
          }
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestination", new { id = destination.Id }, destination);
        }

       

        private bool DestinationExists(int id)
        {
            return (_context.Destinations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
