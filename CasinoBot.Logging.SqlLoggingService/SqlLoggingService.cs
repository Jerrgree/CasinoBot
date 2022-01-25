using CasinoBot.Data;
using CasinoBot.Data.Entities;
using CasinoBot.Domain.Enums;
using CasinoBot.Domain.Interfaces;

namespace CasinoBot.Logging.SqlLoggingService
{
    public class SqlLoggingService : ILoggingService
    {
        private readonly CasinoContext _dbContext;
        private Guid? _traceId;
        private ulong? _userId;
        private ulong? _guildId;


        public void SetLoggingInformation(Guid? traceId, ulong? userId, ulong? guildId)
        {
            _traceId = traceId;
            _userId = userId;
            _guildId = guildId;
        }

        public SqlLoggingService(CasinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogDebugMessage(string logMessage)
        {
            await WriteLogMessage(logMessage, LogLevel.Debug);
        }

        public async Task LogErrorMessage(string logMessage)
        {
            await WriteLogMessage(logMessage, LogLevel.Error);
        }

        public async Task LogFatalMessage(string logMessage)
        {
            await WriteLogMessage(logMessage, LogLevel.Fatal);
        }

        public async Task LogInformationalMessage(string logMessage)
        {
            await WriteLogMessage(logMessage, LogLevel.Information);
        }

        public async Task LogWarningMessage(string logMessage)
        {
            await WriteLogMessage(logMessage, LogLevel.Warning);
        }

        private async Task WriteLogMessage(string logMessage, LogLevel logLevel)
        {
            try
            {
                _dbContext.LogEntries.Add(new LogEntry()
                {
                    LogDateTimeUtc = DateTime.UtcNow,
                    LogLevel = logLevel,
                    LogMessage = logMessage,
                    TraceId = _traceId,
                    UserId = _userId
                });

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Error in the logging service means that log writing might be unreliable, fallback to the console
                Console.WriteLine($"Exception encountered writing log entry: {ex}");
            }
        }
    }
}