using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using NTH.Core.Models;
using System.Data;

namespace NTH.TravelAPI.Controllers.Website.ServiceContact
{
    [Route("api/web/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DapperContext _dapper;

        public ContactController(DapperContext dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// Tạo mới 1 contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Post(Contact contact)
        {
            var procedureName = "Proc_Contact_Insert";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var obj = new { v_fullName = contact.FullName,
                    v_email = contact.Email,
                    v_phone = contact.Phone,
                    v_contactRequest = contact.ContactRequest,
                    v_postcode = contact.Postcode,
                    v_location = contact.Location,
                    v_country = contact.Country,
                    v_potentialChannel = contact.PotentialChannel,
                    v_createdDate = DateTime.Now
                };
                var des = await connection.ExecuteAsync
                    (procedureName, obj, commandType: CommandType.StoredProcedure);
                return des;
            }
        }

        /// <summary>
        /// Tạo mới 1 subscribe
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost("subscribe")]
        public async Task<object> SubscribeNewsletter(Subscribe subscribe)
        {
            var procedureName = "Proc_Subscribe_Insert";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _dapper.CreateConnection())
            {
                var obj = new
                {
                    v_fullName = subscribe.FullName,
                    v_email = subscribe.Email,
                    v_phone = subscribe.Phone,
                    v_note = subscribe.Note,
                    v_createdDate = DateTime.Now
                };
                var des = await connection.ExecuteAsync
                    (procedureName, obj, commandType: CommandType.StoredProcedure);
                return des;
            }
        }
    }
}
