using CasinoBot.Data;
using CasinoBot.Data.Entities;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models;

namespace CasinoBot.SqlDataStore
{
    public class CasinoBotSqlLoggingDataStore : ILoggingDataStore
    {
        private readonly CasinoContext _dbContext;
        public CasinoBotSqlLoggingDataStore(CasinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> InsertLogEntry(string logMessage, DateTime logDateTimeUtc, LogLevel logLevel, Guid? traceId, ulong? userId, ulong? guildId)
        {
            try
            {
                var logEntry = new LogEntry()
                {
                    LogMessage = logMessage,
                    LogDateTimeUtc = logDateTimeUtc,
                    LogLevel = logLevel,
                    TraceId = traceId,
                    UserId = userId
                };

                _dbContext.LogEntries.Add(logEntry);

                await _dbContext.SaveChangesAsync();

                return new Response();
            }
            catch (Exception ex)
            {
                // Since we failed to insert a log entry, logging service could potentially be unreliable.
                // Write to console instead
                Console.WriteLine($"Failed to insert new log entry: of {logLevel} - {logMessage}\n" +
                    $"Exception = {ex}");

                return new Response(false, "Failed to insert log entry");
            }
        }
    }
}
