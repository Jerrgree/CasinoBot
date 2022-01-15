namespace CasinoBot.Domain.Interfaces
{
    internal interface ISortableCard<T> : ICard, IComparable<T>, IEquatable<T> where T : ICard
    {
    }
}
