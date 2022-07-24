using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

namespace NTH.TravelAPI.Controllers.Website.ServiceContact
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public NewsletterController(DapperContext dapper)
        {
            _dapper = dapper;
        }

        // GET api/<CompanyController>/5
        [HttpGet("{lang}")]
        public async Task<object> Get(string lang)
        {
            var procedureName = "Proc_Newsletter_GetInfo";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }
    }
}
