using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using NTH.Core.Helper;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeadController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("leads")]
        public async Task<IActionResult> Leads(DataSourceLoadOptions loadOptions)
        {
            var source = _context.Contacts.Select(o => new {
                o.Id,
                o.FullName,
                o.Email,
                o.Phone,
                o.Country,
                o.CreatedDate,
                o.ContactRequest
            });

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }
    }
}
