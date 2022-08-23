using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMS.Logger
{
    public class NMSFileLogger : ILogger
    {
        protected readonly NMSFileLoggerProvider _nmsLoggerFileProvider;

        public NMSFileLogger([NotNull] NMSFileLoggerProvider nmsLoggerFileProvider)
        {
            _nmsLoggerFileProvider = nmsLoggerFileProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var threadId = Thread.CurrentThread.ManagedThreadId; // Get the current thread ID to use in the log file.

            Task.Run(async () =>
            {
                // Run in a seperate task so the thread isn't waiting for it to be finished.
                var fullFilePath = _nmsLoggerFileProvider.Options.FolderPath + "/" + _nmsLoggerFileProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd")); // Get the full log file path. Seperated by day.
                var logRecord = string.Format("{0} [{1}] [{2}] {3} {4}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z") + "]", threadId, logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : ""); // Format the log entry.

                try
                {
                    // Ensure that only one thread can write to the text file to avoid issues with opening the text file.
                    await _nmsLoggerFileProvider.WriteFileLock.WaitAsync();

                    // Write the log entry to the text file.
                    using (var streamWriter = new StreamWriter(fullFilePath, true))
                    {
                        await streamWriter.WriteLineAsync(logRecord);
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    // Ensure that the lock is released once the log entry has been written to the text file.
                    _nmsLoggerFileProvider.WriteFileLock.Release();
                }
            });
        }
    }
}
