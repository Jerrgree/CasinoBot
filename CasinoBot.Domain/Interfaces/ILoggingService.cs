namespace CasinoBot.Domain.Interfaces
{
    public interface ILoggingService
    {
        Task LogDebugMessage(ILogEntry logEntry);
        Task LogInformationalMessage(ILogEntry logEntry);
        Task LogWarningMessage(ILogEntry logEntry);
        Task LogErrorMessage(ILogEntry logEntry);
        Task LogFatalMessage(ILogEntry logEntry);
    }
}
