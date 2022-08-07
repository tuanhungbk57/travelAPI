using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NTH.Core.Data;
using NTH.Core.Helper;
using NTH.Core.Models;
using NTH.Travel.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Service
{
    public class UploadService : IUploadService
    {
        private readonly IConfiguration Configuration;
        private readonly ApplicationDbContext _context;
        private readonly IDbUtil _dbUtil;

        public UploadService(IConfiguration configuration, ApplicationDbContext context, IDbUtil dbUtil)
        {
            Configuration = configuration;
            _context = context;
            _dbUtil = dbUtil;
        }
        public async Task<int> Upload(int folderID, IFormFileCollection files, int folderType = 0, int parentID = 0)
        {
            try
            {
                //var folderName = Path.GetFullPath("F:\\test\\Images\\test01 02 023");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                string folderName = await _dbUtil.ExecuteScala<string>("Folder_GetNameByID", new { folderId = folderID });
                var folderFullPath = BuildPath(folderName, folderType);

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderFullPath);
                if (!Directory.Exists(folderFullPath))
                {
                    Directory.CreateDirectory(folderFullPath);
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fileName = file.FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        //var dbPath = Path.Combine(folderName, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        await StoreImageToDb(folderID, folderType, folderName, fileName, parentID);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            return 1;
        }

        /// <summary>
        /// Tạo đường dẫn lưu trữ ảnh
        /// </summary>
        /// <param name="folderId">ID của folder chứa ảnh</param>
        /// <param name="folderType">Loại foler (des or trip)</param>
        /// <returns></returns>
        public string BuildPath(string folderName, int? folderType = 0)
        {
            var path = "";
            var routPath = Configuration["Folder:rootPath"];

            string folderTypeName = Configuration["Folder:Tri"];
            if (folderType == 0)
            {
                folderTypeName = Configuration["Folder:Des"];
            }
            else if (folderType == 1)
            {
                folderTypeName = Configuration["Folder:Tri"];
            }
            else if (folderType == 2) folderTypeName = Configuration["Folder:Gen"];

            path = $@"{routPath}/{folderTypeName}/{folderName}";
            return path;
        }

        public async Task<int> StoreImageToDb(int folderId, int folderType, string folderName, string imgName, int parentID)
        {
            FolderImage folderimg = new FolderImage
            {
                FolderId = folderId,
                FolderName = folderName,
                FolderType = folderType,
                ImageName = imgName,
                ParentId = parentID
            };
            //_context.Entry(folderimg).State = EntityState.Modified;
            _context.FolderImages.Add(folderimg);
            return await _context.SaveChangesAsync();
        }
    }
}
