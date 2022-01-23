namespace CasinoBot.Domain.Interfaces
{
    public interface ILoggingService
    {
        /// <summary>
        /// Sets logging information for use when logging
        /// </summary>
        /// <param name="traceId">The trace ID tying together logs of a particular request</param>
        /// <param name="userId">The user ID that a request was invoked by</param>
        /// <param name="guildId">The guild ID that a request was invoked from</param>
        void SetLoggingInformation(Guid? traceId, ulong? userId, ulong? guildId);
        /// <summary>
        /// Writes a debug message to the logging output
        /// </summary>
        /// <param name="logMessage">The log message to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogDebugMessage(string logMessage);

        /// <summary>
        /// Writes a informational message to the logging output
        /// </summary>
        /// <param name="logMessage">The log message to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogInformationalMessage(string logMessage);

        /// <summary>
        /// Writes a warning message to the logging output
        /// </summary>
        /// <param name="logMessage">The log message to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogWarningMessage(string logMessage);

        /// <summary>
        /// Writes a error message to the logging output
        /// </summary>
        /// <param name="logMessage">The log message to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogErrorMessage(string logMessage);

        /// <summary>
        /// Writes a fatal message to the logging output
        /// </summary>
        /// <param name="logMessage">The log message to write</param>
        /// <returns>A task indicating the status of the asynchronous operation</returns>
        Task LogFatalMessage(string logMessage);
    }
}
