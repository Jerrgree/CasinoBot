using CasinoBot.Domain.Interfaces;

namespace CasinoBot.Logging.ConsoleLogger
{
    public class ConsoleLogger : ILoggingService
    {
        private Guid? _traceId;
        private ulong? _userId;
        private ulong? _guildId;

        public void SetLoggingInformation(Guid? traceId, ulong? userId, ulong? guildId)
        {
            _traceId = traceId;
            _userId = userId;
            _guildId = guildId;
        }

        public Task LogDebugMessage(string logMessage)
        {
            Console.WriteLine($"Debug:\tUser: {_userId}\tGuild: {_guildId}\tTrace: {_traceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logMessage}");
            return Task.CompletedTask;
        }
        public Task LogErrorMessage(string logMessage)
        {
            Console.WriteLine($"Error:\tUser: {_userId}\tGuild: {_guildId}\tTrace: {_traceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logMessage}");
            return Task.CompletedTask;
        }

        public Task LogFatalMessage(string logMessage)
        {
            Console.WriteLine($"Fatal:\tUser: {_userId}\tGuild: {_guildId}\tTrace: {_traceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logMessage}");
            return Task.CompletedTask;
        }

        public Task LogInformationalMessage(string logMessage)
        {
            Console.WriteLine($"Information:\tUser: {_userId}\tGuild: {_guildId}\tTrace: {_traceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logMessage}");
            return Task.CompletedTask;
        }

        public Task LogWarningMessage(string logMessage)
        {
            Console.WriteLine($"Warning:\tUser: {_userId}\tGuild: {_guildId}\tTrace: {_traceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logMessage}");
            return Task.CompletedTask;
        }
    }
}