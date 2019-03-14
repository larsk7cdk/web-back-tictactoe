using System;
using Microsoft.Extensions.Logging;

namespace web_back_tictactoe.logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _fileName;
        private readonly Func<string, LogLevel, bool> _filter;

        public FileLoggerProvider(Func<string, LogLevel, bool> filter, string fileName)
        {
            _filter = filter;
            _fileName = fileName;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, _filter, _fileName);
        }

        public void Dispose()
        {
        }
    }
}