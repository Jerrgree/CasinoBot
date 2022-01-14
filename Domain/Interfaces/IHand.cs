namespace Domain.Interfaces
{
    public interface IHand<T> : IEnumerable<(T Card, bool isPublic)> where T : ICard
    {
        /// <summary>
        /// Retrieves the number of cards in this hand
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a new card to the hand, hidden from the table
        /// </summary>
        /// <param name="card">The new card to be added</param>
        void AddCard(T card);

        /// <summary>
        /// Adds a new card to the hand
        /// </summary>
        /// <param name="card">The new card to be added</param>
        /// <param name="isPublic">Indicates if this card is visible to the table. </param>
        void AddCard(T card, bool isPublic);

        /// <summary>
        /// Accesses the indicated card
        /// </summary>
        /// <param name="index">The index of the card to retrieve</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to access an index outside of the hand's range</exception>
        /// <returns>The specified card, along with it's access level</returns>
        (T Card, bool isPublic) this[int index]
        {
            get;
        }

        /// <summary>
        /// Removes the given card from the hand. 
        /// </summary>
        /// <param name="index">The index of the card to remove</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to access an index outside of the hand's range</exception>
        /// <returns>The removed card</returns>
        T RemoveAt(int index);

        /// <summary>
        /// Randomizes the hand
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when called on an empty hand</exception>
        void Shuffle();
    }
}
