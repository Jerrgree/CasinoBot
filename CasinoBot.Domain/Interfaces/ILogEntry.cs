namespace CasinoBot.Domain.Interfaces
{
    public interface ILogEntry
    {
        public string Message { get; set; }
        public ulong? UserId { get; set; }
        public Guid? TraceId { get; set; }
    }
}
