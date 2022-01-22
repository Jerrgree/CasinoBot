using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Interfaces
{
    public interface ITable<out T> where T : class
    {
        public long TableId { get; }
        public ulong GuildId { get; }
        public TableType TableType { get; }
        public IEnumerable<IPlayer<T>> Players { get; }
    }
}
