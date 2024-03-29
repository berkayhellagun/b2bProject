﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public interface IFileHelper
    {
        public string Upload(IFormFile file, string root);
        public string Update(IFormFile file, string path, string root);
        public void Delete(string path);
    }
}
