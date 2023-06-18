using CasinoBot.Domain.Enums.StandardPlayingCards;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.Hands;
using CasinoBot.Domain.Models.StandardPlayingCards;

namespace CasinoBot.Games.BlackJack.Domain.Models
{
    public class Player
    {
        public IHand<StandardPlayingCard> Hand { get; }

        public Player()
        {
            Hand = new Hand<StandardPlayingCard>();
        }

        public int ComputeScore()
        {
            int score = 0;
            int aces = 0;
            foreach (var card in Hand)
            {
                // Save aces for last
                if (card.Face == StandardPlayingCardFace.Ace)
                {
                    aces++;
                    score++;
                }
                else
                {
                    var rank = (int)card.Face;

                    // Face cards count as 10
                    if (rank > 10)
                    {
                        score += 10;
                    }
                    else
                    {
                        score += rank;
                    }
                }
            }

            for (int i = 0; i < aces; i++)
            {
                // If the ace can be promoted to 11, do so
                if (score + 10 <= 21)
                {
                    score += 10;
                }
                // Otherwise stop and keep the score as it is
                else
                {
                    break;
                }
            }

            return score;
        }
    }
}
