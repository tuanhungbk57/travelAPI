using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

namespace NTH.TravelAPI.Controllers.Website
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public FooterController(DapperContext dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// Lấy về chi tiết footer theo ngôn ngữ
        /// </summary>
        /// <param name="path">path của trip</param>
        /// <param name="lang">ngôn ngữ</param>
        /// <returns></returns>
        [HttpGet("{lang}")]
        public async Task<object> GetFooter(string lang)
        {
            var procedureName = "Proc_Footer_GetByLang";
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }
    }
}
