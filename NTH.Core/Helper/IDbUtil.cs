using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Helper
{
    public interface IDbUtil
    {
        Task<T> ExecuteScala<T>(string proc, object param = null);
        Task<IEnumerable<T>> Query<T>(string proc = "a", object param = null);
    }
}
