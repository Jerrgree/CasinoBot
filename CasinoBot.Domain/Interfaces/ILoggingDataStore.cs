using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Models;

namespace CasinoBot.Domain.Interfaces
{
    public interface ILoggingDataStore
    {
        Task<Response> InsertLogEntry(string logMessage, DateTime logDate, LogLevel logLevel, Guid? traceId, ulong? userId, ulong? guildId);
    }
}
