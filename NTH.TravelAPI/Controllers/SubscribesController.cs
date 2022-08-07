using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using NTH.Core.Helper;
using NTH.Core.Models;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscribesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetList(DataSourceLoadOptions loadOptions)
        {
            var source = _context.Subscribes.Select(o => new {
                o.Id,
                o.Email,
                o.FullName,
                o.CreatedDate
            });

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }

      
    }
}
