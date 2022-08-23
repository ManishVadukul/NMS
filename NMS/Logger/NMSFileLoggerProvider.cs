using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMS.Logger
{
    [ProviderAlias("NMS")]
    public class NMSFileLoggerProvider : ILoggerProvider
    {
        public readonly NMSFileLoggerOptions Options;

        public SemaphoreSlim WriteFileLock;

        public NMSFileLoggerProvider(IOptions<NMSFileLoggerOptions> _options)
        {
            WriteFileLock = new SemaphoreSlim(1, 1);
            Options = _options.Value;

            if (!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory(Options.FolderPath);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new NMSFileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
