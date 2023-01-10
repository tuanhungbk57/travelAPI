using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using System.Data;

namespace NTH.TravelAPI.Controllers.Website.Blog
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class BlogDetailController : ControllerBase
    {

        private readonly DapperContext _dapper;
        public BlogDetailController(DapperContext dapper)
        {
            _dapper = dapper;
        }
        // GET: api/<BlogDetailController>
        [HttpGet("{blogstr}/blog/{lang}/lang")]
        public async Task<IEnumerable<object>> GetBlogDetailsByLang(string blogstr, string lang)
        {
            var procedureName = "Proc_BlogDetail_GetListByBlogAndLang";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryAsync<object>
                    (procedureName, new { v_lang = lang, v_blog = blogstr }, commandType: CommandType.StoredProcedure);
                return des;
            }

        }

        /// <summary>
        /// Lấy về chi tiết 1 BlogPost theo ngôn ngữ
        /// </summary>
        /// <param name="path">path của trip</param>
        /// <param name="lang">ngôn ngữ</param>
        /// <returns></returns>
        [HttpGet("{path}/{lang}")]
        public async Task<object> GetBlogPost(string path, string lang)
        {
            var procedureName = "Proc_BlogPost_GetByPathAndLang";
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang, v_path = path }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }
    }
}
