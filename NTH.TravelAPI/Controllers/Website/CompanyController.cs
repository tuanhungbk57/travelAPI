using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTH.TravelAPI.Controllers.Website
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public CompanyController( DapperContext dapper)
        {
            _dapper = dapper;
        }
      

        // GET api/<CompanyController>/5
        [HttpGet("{lang}")]
        public async Task<object> Get(string lang)
        {
            var procedureName = "Company_GetInfo";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }

        // POST api/<CompanyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
