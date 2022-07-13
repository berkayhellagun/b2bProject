using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.NLog.Types
{
    public class File : LoggerBase
    {
        public File():base("File")
        {

        }
    }
}
