using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTH.Travel.BL.Contracts;
using System.Net.Http.Headers;

namespace NTH.TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
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
    }
}
