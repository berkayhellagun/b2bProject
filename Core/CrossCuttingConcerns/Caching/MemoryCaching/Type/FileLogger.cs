using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.MemoryCaching.Type
{
    public class FileLogger : MemoryCacheManager
    {
        public FileLogger() : base("File")
        {

        }
    }
}
