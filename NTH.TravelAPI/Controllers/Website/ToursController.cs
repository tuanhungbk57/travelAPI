using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTH.TravelAPI.Controllers.Website
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public ToursController(DapperContext dapper)
        {
            _dapper = dapper;
        }
        
        /// <summary>
        /// Lấy về danh sách tour để hiện giao diện trip
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("{trip}/trip/{lang}/lang")]
        public async Task<IEnumerable<object>> GetTripsByLang(string trip, string lang)
        {
            var procedureName = "Proc_Tour_GetListByTripAnLang";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryAsync<object>
                    (procedureName, new { v_lang = lang, v_trip = trip }, commandType: CommandType.StoredProcedure);
                return des;
            }

        }

        /// <summary>
        /// Lấy về chi tiết 1 trip theo ngôn ngữ
        /// </summary>
        /// <param name="path">path của trip</param>
        /// <param name="lang">ngôn ngữ</param>
        /// <returns></returns>
        [HttpGet("{path}/{lang}")]
        public async Task<object> GetTrip(string path, string lang)
        {
            var procedureName = "Proc_Tour_GetByPathAndLang";
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang, v_path = path }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }

    }
}
