using CasinoBot.Domain.Enums.StandardPlayingCards;
using CasinoBot.Domain.Models.StandardPlayingCards;
using System.Text;

namespace CasinoBot.Interaction.Discord.Client.Extensions
{
    internal static class StandardPlayingCardExtensions
    {
        internal static string GetDisplayString(this StandardPlayingCard card)
        {
            var sb = new StringBuilder();

            sb.Append(card.Face switch
            {
                StandardPlayingCardFace.Two => ":two:",
                StandardPlayingCardFace.Three => ":three:",
                StandardPlayingCardFace.Four => ":four:",
                StandardPlayingCardFace.Five => ":five:",
                StandardPlayingCardFace.Six => ":six:",
                StandardPlayingCardFace.Seven => ":seven:",
                StandardPlayingCardFace.Eight => ":eight:",
                StandardPlayingCardFace.Nine => ":nine:",
                StandardPlayingCardFace.Ten => ":keycap_ten:",
                StandardPlayingCardFace.Jack => ":regional_indicator_j:",
                StandardPlayingCardFace.Queen => ":regional_indicator_q:",
                StandardPlayingCardFace.King => ":regional_indicator_k:",
                StandardPlayingCardFace.Ace => ":regional_indicator_a:",
                _ => throw new ArgumentException("The card contains in invalid face", nameof(card.Face))
            });
            sb.Append(" of ");
            sb.Append(card.Suit switch
            {
                StandardPlayingCardSuit.Hearts => ":heart:",
                StandardPlayingCardSuit.Diamonds => ":diamonds:",
                StandardPlayingCardSuit.Spades => ":spades:",
                StandardPlayingCardSuit.Clubs => ":clubs:",
                _ => throw new ArgumentException("The card contains in invalid suit", nameof(card.Suit))
            });

            return sb.ToString();
        }
    }
}
