using CasinoBot.Domain.Interfaces;

namespace CasinoBot.Interaction.Discord.Client.Models
{
    internal class LogEntry : ILogEntry
    {
        public string Message { get; private set; }
        public ulong? UserId { get; private set; }
        public Guid? TraceId { get; private set; }

        public LogEntry(string message, ulong? userId = null, Guid? traceId = null )
        {
            Message = message;
            UserId = userId;
            TraceId = traceId;
        }
    }
}
