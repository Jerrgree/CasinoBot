using Domain.Enums.StandardPlayingCards;
using Domain.Interfaces;
using Domain.Models.StandardPlayingCards;

namespace Games.BlackJack.Domain.Models
{
    public class Player
    {
        public IHand<StandardPlayingCard> Hand { get; }



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
                    if (score > 10)
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
