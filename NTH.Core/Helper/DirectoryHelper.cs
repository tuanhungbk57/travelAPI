using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Core.Helper
{
    public class DirectoryHelper
    {
      public static void CreateDirectory(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
    }
}
