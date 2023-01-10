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

namespace NTH.TravelAPI.Controllers.Website.Blog
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public BlogController(ApplicationDbContext context, DapperContext dapper)
        {
            _dapper = dapper;
        }

        // GET: api/blog/vi/lang
        /// <summary>
        /// Lấy về danh sách blog
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("{lang}/lang")]
        public async Task<IEnumerable<object>> GetBlogsByLang(string lang)
        {
            var procedureName = "Proc_Blog_GetListByLang";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryAsync<object>
                    (procedureName, new { v_lang = lang }, commandType: CommandType.StoredProcedure);
                return des;
            }

        }


        /// <summary>
        /// Lấy về chi tiết 1 blog theo ngôn ngữ
        /// </summary>
        /// <param name="path">path của des</param>
        /// <param name="lang">ngôn ngữ</param>
        /// <returns></returns>
        [HttpGet("{path}/{lang}")]
        public async Task<object> GetBlog(string path, string lang)
        {
            var procedureName = "Proc_Blog_GetByPathAndLang";
            using (var connection = _dapper.CreateConnection())
            {
                var des = await connection.QueryFirstOrDefaultAsync<object>
                    (procedureName, new { v_lang = lang, v_path = path }, commandType: CommandType.StoredProcedure);
                return des;
            }
        }

    }
}
