using Dapper;
using NTH.Core.Data;
using NTH.Core.Models;
using NTH.Travel.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Repository
{
    public class ContactRepo : IContactRepo
    {
        private readonly DapperContext _context;

        public ContactRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var query = "SELECT * FROM Contact";
            using (var connection = _context.CreateConnection())
            {
                var c = await connection.QueryAsync<Contact>(query);
                return c.ToList();
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsList()
        {
            var procedureName = "Contact_GetAll";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryAsync<Contact>
                    (procedureName, null, commandType: CommandType.StoredProcedure);
                return company;
            }
        }
    }
}
