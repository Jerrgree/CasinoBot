using CasinoBot.Domain.Enums.StandardPlayingCards;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.Decks;

namespace CasinoBot.Domain.Models.StandardPlayingCards
{
    public class StandardPlayingCard : ISortableCard<StandardPlayingCard>
    {
        public StandardPlayingCardFace Face { get; private set; }
        public StandardPlayingCardSuit Suit { get; private set; }

        public StandardPlayingCard(StandardPlayingCardFace face, StandardPlayingCardSuit suit)
        {
            Face = face;
            Suit = suit;
        }

        public static Deck<StandardPlayingCard> CreateDeck(int numberOfDecks = 1)
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


        #region Equality

        public static bool operator ==(StandardPlayingCard rhs, StandardPlayingCard lhs) => rhs.Equals(lhs);
        public static bool operator !=(StandardPlayingCard lhs, StandardPlayingCard rhs) => !rhs.Equals(lhs);

        public override bool Equals(object? obj)
        {
            if (obj is StandardPlayingCard other)
            {
                return Equals(other);
            }
            return false;
        }

        public bool Equals(StandardPlayingCard? other)
        {
            // null does not equal null
            if (other is null) return false;

            return other.Face == Face;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Face, Suit);
        }


        #endregion

        #region Comparison
        public static bool operator <(StandardPlayingCard rhs, StandardPlayingCard lhs) => lhs.CompareTo(rhs) < 0;
        public static bool operator >(StandardPlayingCard rhs, StandardPlayingCard lhs) => lhs.CompareTo(rhs) > 0;
        public static bool operator <=(StandardPlayingCard rhs, StandardPlayingCard lhs) => lhs.CompareTo(rhs) <= 0;
        public static bool operator >=(StandardPlayingCard rhs, StandardPlayingCard lhs) => lhs.CompareTo(rhs) >= 0;

        public int CompareTo(StandardPlayingCard? other)
        {
            if (other is null) return 1;

            return ((int)other.Face).CompareTo((int)Face);
        }

        #endregion
    }
}
