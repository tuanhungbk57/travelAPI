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
        Task<int> Upload(int folderID, IFormFileCollection files, int folderType = 0);

    }
}
