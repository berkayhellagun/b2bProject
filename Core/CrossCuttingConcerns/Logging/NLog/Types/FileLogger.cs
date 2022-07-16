using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.NLog.Types
{
    public class FileLogger : LoggerBase
    {
        public FileLogger() : base("FileLogger")
        {

        }
    }
}
