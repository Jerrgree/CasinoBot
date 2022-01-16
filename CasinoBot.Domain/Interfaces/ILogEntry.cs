namespace CasinoBot.Domain.Interfaces
{
    public interface ILogEntry
    {
        /// <summary>
        /// The message to write to the logs
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The ID of the user associated with this log
        /// </summary>
        public ulong? UserId { get; set; }
        /// <summary>
        /// The trace ID of the request associated with this log
        /// </summary>
        public Guid? TraceId { get; set; }
    }
}
