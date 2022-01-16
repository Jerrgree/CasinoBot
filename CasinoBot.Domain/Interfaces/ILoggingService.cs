namespace CasinoBot.Domain.Interfaces
{
    public interface ILoggingService
    {
        /// <summary>
        /// Writes a debug message to the logging output
        /// </summary>
        /// <param name="logEntry">The log entry to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogDebugMessage(ILogEntry logEntry);

        /// <summary>
        /// Writes a informational message to the logging output
        /// </summary>
        /// <param name="logEntry">The log entry to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogInformationalMessage(ILogEntry logEntry);

        /// <summary>
        /// Writes a warning message to the logging output
        /// </summary>
        /// <param name="logEntry">The log entry to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogWarningMessage(ILogEntry logEntry);

        /// <summary>
        /// Writes a error message to the logging output
        /// </summary>
        /// <param name="logEntry">The log entry to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogErrorMessage(ILogEntry logEntry);

        /// <summary>
        /// Writes a fatal message to the logging output
        /// </summary>
        /// <param name="logEntry">The log entry to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogFatalMessage(ILogEntry logEntry);
    }
}
