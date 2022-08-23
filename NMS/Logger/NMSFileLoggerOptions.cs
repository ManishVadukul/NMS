using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.Logger
{ 
    public class NMSFileLoggerOptions
    {
        public virtual string FilePath { get; set; }

        public virtual string FolderPath { get; set; }
    }
}
