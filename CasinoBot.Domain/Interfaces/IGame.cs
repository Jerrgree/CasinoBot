namespace CasinoBot.Domain.Interfaces
{
    public interface IGame
    {
        Task Play(IEnumerable<ulong> players, ulong guild);
    }
}
