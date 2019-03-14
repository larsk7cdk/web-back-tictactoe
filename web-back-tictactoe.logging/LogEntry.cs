using System;

namespace web_back_tictactoe.logging
{
    internal class LogEntry
    {
        public int EventId { get; internal set; }
        public string Message { get; internal set; }
        public string LogLevel { get; internal set; }
        public DateTime CreatedTime { get; internal set; }
    }
}