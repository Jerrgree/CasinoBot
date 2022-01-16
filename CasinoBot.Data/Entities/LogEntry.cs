using CasinoBot.Domain.Enums;

namespace CasinoBot.Data.Entities
{
    public class LogEntry
    {
        public long LogEntryId { get; set; }
        public LogLevel LogLevel { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDateTimeUtc { get; set; }
        public ulong? UserId { get; set; }
        public Guid? TraceId { get; set; }
    }
}
