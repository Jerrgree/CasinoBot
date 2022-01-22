namespace CasinoBot.Domain.Interfaces
{
    public interface IPlayer<out T> where T : class
    {
        public ulong PlayerId { get; }

        public T State { get; }
    }
}
