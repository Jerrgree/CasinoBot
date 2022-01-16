using CasinoBot.Domain.Interfaces;

namespace CasinoBot.Logging.ConsoleLogger
{
    public class ConsoleLogger : ILoggingService
    {
        public Task LogDebugMessage(ILogEntry logEntry)
        {
            Console.WriteLine($"Debug:\tUser: {logEntry.UserId}\tTrace: {logEntry.TraceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logEntry.Message}");
            return Task.CompletedTask;
        }
        public Task LogErrorMessage(ILogEntry logEntry)
        {
            Console.WriteLine($"Error:\tUser: {logEntry.UserId}\tTrace: {logEntry.TraceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logEntry.Message}");
            return Task.CompletedTask;
        }

        public Task LogFatalMessage(ILogEntry logEntry)
        {
            Console.WriteLine($"Fatal:\tUser: {logEntry.UserId}\tTrace: {logEntry.TraceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logEntry.Message}");
            return Task.CompletedTask;
        }

        public Task LogInformationalMessage(ILogEntry logEntry)
        {
            Console.WriteLine($"Information:\tUser: {logEntry.UserId}\tTrace: {logEntry.TraceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logEntry.Message}");
            return Task.CompletedTask;
        }

        public Task LogWarningMessage(ILogEntry logEntry)
        {
            Console.WriteLine($"Warning:\tUser: {logEntry.UserId}\tTrace: {logEntry.TraceId}\tTime: {DateTime.Now.ToLongDateString()}\n{logEntry.Message}");
            return Task.CompletedTask;
        }
    }
}