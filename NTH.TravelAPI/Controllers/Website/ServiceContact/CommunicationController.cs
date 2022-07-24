using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTH.TravelAPI.Controllers.Website.ServiceContact
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public CommunicationController(DapperContext dapper)
        {
            _dapper = dapper;
        }

        // GET api/<CompanyController>/5
        [HttpGet("{lang}")]
        public async Task<object> Get(string lang)
        {
            var procedureName = "Proc_Communication_GetInfo";
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
