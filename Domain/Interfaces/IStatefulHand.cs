namespace CasinoBot.Domain.Interfaces
{
    /// <summary>
    /// A hand that tracks a provided state of each card
    /// </summary>
    /// <typeparam name="T">The type of card to use</typeparam>
    /// <typeparam name="U">The type of state to use</typeparam>
    public interface IStatefulHand<T, U> : IEnumerable<(T Card, U State)>, IHand<T>
                                                                where T : ICard
                                                                where U : class, new()
    {

        /// <summary>
        /// Adds a new card to the hand
        /// </summary>
        /// <param name="card">The new card to be added</param>
        /// <param name="state">The state of the </param>
        void AddCard(T card, U state);

        /// <summary>
        /// Accesses the indicated card and its state
        /// </summary>
        /// <param name="index">The index of the card to retrieve</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to access an index outside of the hand's range</exception>
        /// <returns>The specified card and its state</returns>
        new (T Card, U State) this[int index]
        {
            get;
        }

        /// <summary>
        /// Removes the given card from the hand. 
        /// </summary>
        /// <param name="index">The index of the card to remove</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to access an index outside of the hand's range</exception>
        /// <returns>The removed card with its state</returns>
        new (T Card, U State) RemoveAt(int index);
    }
}
