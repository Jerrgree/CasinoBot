namespace CasinoBot.Domain.Models.Players
{
    public class Player<T> where T : class
    {
        public ulong PlayerId { get; set; }
        public T PlayerState { get; set; }
    }
}
