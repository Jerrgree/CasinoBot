using Domain.Enums.StandardPlayingCards;
using Domain.Interfaces;

namespace Domain.Models.StandardPlayingCards
{
    public class StandardPlayingCard : ISortableCard<StandardPlayingCard>
    {
        public StandardPlayingCardFace Face { get; private set; }
        public StandardPlayingCardSuit Suit { get; private set; }
        public string Display => $"{Face} of {Suit}";

        public StandardPlayingCard(StandardPlayingCardFace face, StandardPlayingCardSuit suit)
        {
            Face = face;
            Suit = suit;
        }

        #region Equality

        public static bool operator ==(StandardPlayingCard rhs, StandardPlayingCard lhs) => rhs.Equals(lhs);
        public static bool operator !=(StandardPlayingCard lhs, StandardPlayingCard rhs) => !(rhs.Equals(lhs));

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

            return other.Face == this.Face;
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

            return ((int)other.Face).CompareTo((int)this.Face);
        }

        #endregion
    }
}
