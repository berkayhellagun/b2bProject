using Core.CrossCuttingConcerns.Logging.NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.MemoryCaching.Type
{
    public class FileLogger : LoggerBase
    {
        public FileLogger() : base("File")
        {

        }
    }
}
