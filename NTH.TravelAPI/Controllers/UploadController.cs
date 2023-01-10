using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTH.Core.Data;
using NTH.Travel.BL.Contracts;
using System.Data;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace NTH.TravelAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly DapperContext _dapper;

        public UploadController(IUploadService uploadService, DapperContext dapper)
        {
            _uploadService = uploadService;
            _dapper = dapper;
        }
        [HttpPost("{type}/{folerId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(int type, int folerId)
        {
            try
            {
                //var file = Request.Form.Files[0];
                //var folderName = Path.Combine("F:\\test", "Images");
                //var folderName = Path.GetFullPath("F:\\test\\Images\\test01 02 023");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                //if (!Directory.Exists(folderName))
                //{
                //    Directory.CreateDirectory(folderName);
                //}
                //foreach (var file in Request.Form.Files)
                //{
                //    if (file.Length > 0)
                //    {
                //        //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                //        var fileName = file.FileName.Trim('"');
                //        var fullPath = Path.Combine(pathToSave, fileName);
                //        var dbPath = Path.Combine(folderName, fileName);

                //        using (var stream = new FileStream(fullPath, FileMode.Create))
                //        {
                //            file.CopyTo(stream);
                //        }
                //    }
                //    else
                //    {
                //        return BadRequest();
                //    }
                //}
                var files = Request.Form.Files;

               await _uploadService.Upload(folerId, files, type);

                return Ok("Success");
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("{type}/{folerId}/{parentId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadTrip(int type, int folerId, int parentId)
        {
            try
            {
                var files = Request.Form.Files;

                await _uploadService.Upload(folerId, files, type, parentId);

                return Ok("Success");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpDelete("{typename}/{foldername}/{filename}/{id}")]
        public async Task<IActionResult> DeteleFile(string typename, string foldername, string filename, int id)
        {
            try
            {
                

                var result = await _uploadService.DeleteFile(typename, foldername, filename);
                if(result == 0)
                {
                    return Ok("False");
                }
                var procedureName = "Proc_Folderimage_Delete";
                //var parameters = new DynamicParameters();
                //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
                using (var connection = _dapper.CreateConnection())
                {
                    var obj = new
                    {
                        v_id = id
                    };
                    var des = await connection.ExecuteAsync
                        (procedureName, obj, commandType: CommandType.StoredProcedure);
                }
                return Ok("Success");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
