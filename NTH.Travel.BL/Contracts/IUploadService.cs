using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Contracts
{
    public interface IUploadService
    {
        Task<int> Upload(int folderID, IFormFileCollection files, int folderType = 0,int parentID = 0);
        string BuildPath(string folderName, int? folderType);
        Task<int> StoreImageToDb(int folderId, int folderType, string folderName, string imgName, int parentID);
        Task<int> DeleteFile(string typeName, string folderName, string fileName);

    }
}
