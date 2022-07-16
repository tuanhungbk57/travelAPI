using NTH.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Contracts
{
    public interface IFolder
    {
        Task<int> CreateFolder(Folder folder);
    }
}
