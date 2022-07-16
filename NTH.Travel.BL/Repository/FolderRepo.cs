using NTH.Core.Data;
using NTH.Core.Helper;
using NTH.Core.Models;
using NTH.Travel.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Repository
{
    public class FolderRepo : IFolder
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadService _uploadService;

        public FolderRepo(ApplicationDbContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task<int> CreateFolder(Folder folder)
        {
            try
            {
                var fullPath = _uploadService.BuildPath(folder.Name, folder.Type);
                DirectoryHelper.CreateDirectory(fullPath);
                if (_context.Folders == null)
                {
                    return 0;
                }
                _context.Folders.Add(folder);
               return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
    }
}
