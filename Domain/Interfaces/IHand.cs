namespace Domain.Interfaces
{
    public interface IHand<T> where T : ICard
    {
        /// <summary>
        /// Retrieves the number of cards in this hand
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Outputs a string display of this hand
        /// </summary>
        string Display { get; }

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
        /// <returns>The specified card</returns>
        T this[int index]
        {
            get;
            set;
        }

        /// <summary>
        /// Removes the given card from the hand. 
        /// </summary>
        /// <param name="index">The index of the card to remove</param>
        /// <returns>The removed card</returns>
        T RemoveAt(int index);

        /// <summary>
        /// Randomizes the hand
        /// </summary>
        void Shuffle();
    }
}
