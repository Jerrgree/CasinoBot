using Domain.Enums.StandardPlayingCards;
using Domain.Models.Decks;
using Domain.Models.StandardPlayingCards;
using System.Linq;

namespace Domain.Factories
{

    public sealed class StandardPlayingCardFactory
    {
        public Deck<StandardPlayingCard> CreateDeck(int numberOfDecks = 1)
        {
            if (numberOfDecks <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfDecks), "The numberOfDecks must be greater than 0");

            var validFaces = Enum.GetValues(typeof(StandardPlayingCardFace)).Cast<StandardPlayingCardFace>().ToList();
            var validSuits = Enum.GetValues(typeof(StandardPlayingCardSuit)).Cast<StandardPlayingCardSuit>().ToList();
            var newDeck = new Deck<StandardPlayingCard>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                var newCards = from face in validFaces
                               from suit in validSuits
                               select new StandardPlayingCard(face, suit);

                newDeck.Add(newCards);
            }
            return newDeck;
        }
    }
}
