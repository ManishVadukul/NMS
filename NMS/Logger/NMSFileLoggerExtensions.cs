using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.Logger
{
    public static class NMSFileLoggerExtensions
    {
        public static ILoggingBuilder NMSFileLogger(this ILoggingBuilder builder, Action<NMSFileLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, NMSFileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
