using Domain.Interfaces;
using Domain.Models.StandardPlayingCards;

namespace Games.BlackJack.Domain.Models
{
    public class Player
    {
        public IHand<StandardPlayingCard> Hand { get; }



        public readonly int ComputeScore(bool usePublicOnlyCards)
        {
            foreach (var card in Hand)
            {

            }

            return 1;
        }
    }
}
