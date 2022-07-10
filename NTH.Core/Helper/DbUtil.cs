using Dapper;
using Microsoft.EntityFrameworkCore;
using NTH.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Helper
{
    public class DbUtil: IDbUtil
    {
        private DapperContext _context;
        public DbUtil(DapperContext context)
        { 
            _context = context;
        }

        public  async Task<T> ExecuteScala<T>(string proc, object param = null)
        {
            var result = default(T);
            using (var cnn = _context.CreateConnection())
            {
                result = await cnn.ExecuteScalarAsync<T>(proc, param, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        /// <summary>
        /// Thực thi 1 thủ tục,  trả về 1 danh sách
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proc"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public  async Task<IEnumerable<T>> Query<T>(string proc = "a", object param = null)
        {
            List<T> result = new List<T>();
            using (var cnn = _context.CreateConnection())
            {
                result = (await cnn.QueryAsync<T>(proc, param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            }
            return result;
        }
    }
}
