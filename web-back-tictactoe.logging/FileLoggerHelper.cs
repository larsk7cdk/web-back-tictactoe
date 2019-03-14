using System;
using System.IO;
using System.Threading;

namespace web_back_tictactoe.logging
{
    internal class FileLoggerHelper
    {
        private static readonly ReaderWriterLock Locker = new ReaderWriterLock();
        private readonly string _fileName;

        public FileLoggerHelper(string fileName)
        {
            this._fileName = fileName;
        }

        internal void InsertLog(LogEntry logEntry)
        {
            var directory = Path.GetDirectoryName(_fileName);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            try
            {
                Locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllText(_fileName,
                    $"{logEntry.CreatedTime} {logEntry.EventId} {logEntry.LogLevel} {logEntry.Message}" +
                    Environment.NewLine);
            }
            finally
            {
                Locker.ReleaseWriterLock();
            }
        }
    }
}