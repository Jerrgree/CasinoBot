namespace CasinoBot.Domain.Interfaces
{
    public interface IDeck<T> where T : ICard
    {
        /// <summary>
        /// Returns the remaining number cards in the deck
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Shuffles the remaining cards in the deck
        /// </summary>
        /// <returns>The shuffled deck</returns>
        public IDeck<T> Shuffle();

        /// <summary>
        /// Draws a single card from the top of the deck
        /// </summary>
        /// <returns></returns>
        public T Draw();

        /// <summary>
        /// Draws multiple cards from the top of the deck
        /// </summary>
        /// <param name="numberOfCards">The number of cards to draw</param>
        /// <returns></returns>
        public IEnumerable<T> Draw(int numberOfCards);

        /// <summary>
        /// Creates hand for the number of specified players, given the number of cards provided
        /// </summary>
        /// <param name="numberOfPlayers">The number </param>
        /// <param name="numberOfCards"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<T>> Deal(int numberOfPlayers, int numberOfCards);

        /// <summary>
        /// Adds a single card to the bottom of the deck
        /// </summary>
        /// <param name="card">The card to add</param>
        public void Add(T card);

        /// <summary>
        /// Adds multiple cards to the bottom of the deck
        /// </summary>
        /// <param name="cards">The cards to add</param>
        public void Add(IEnumerable<T> cards);
    }
}
