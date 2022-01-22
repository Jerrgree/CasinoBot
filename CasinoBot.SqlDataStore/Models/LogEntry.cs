using CasinoBot.Domain.Interfaces;

namespace CasinoBot.SqlDataStore.Models
{
    internal class LogEntry : ILogEntry
    {
        public LogEntry(string logMessage)
        {
            Message = logMessage;
        }
        public string Message { get; set; }

        public ulong? UserId { get; set; }

        public Guid? TraceId { get; set; }
    }
}
