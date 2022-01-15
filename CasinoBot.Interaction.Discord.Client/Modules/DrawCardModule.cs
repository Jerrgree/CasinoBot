using CasinoBot.Domain.Models.StandardPlayingCards;
using Discord.Interactions;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public class DrawCardModule : InteractionModuleBase
    {
        [SlashCommand("draw", "Draw card(s)")]
        public async Task Echo([Summary(description: "The number of cards to draw"), MinValue(1)] int numberOfCards = 1)
        {
            var cards = StandardPlayingCard.CreateDeck(1).Shuffle().Draw(numberOfCards);
            var result = $"Draw {string.Join(", ", cards.Select(c => c.Display))}";
            await RespondAsync(result);
        }
    }
}
