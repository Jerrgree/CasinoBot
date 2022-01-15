using CasinoBot.Domain.Models.StandardPlayingCards;
using CasinoBot.Interaction.Discord.Client.Extensions;
using Discord;
using Discord.Interactions;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public class DrawCardModule : InteractionModuleBase
    {
        [SlashCommand("draw", "Draw card(s)")]
        public async Task Echo([Summary(description: "The number of cards to draw"), MinValue(1)] int numberOfCards = 1)
        {
            var cards = StandardPlayingCard.CreateDeck(1).Shuffle().Draw(numberOfCards);

            var embed = new EmbedBuilder()
                .WithTitle("Cards")
                .WithDescription("Your cards:")
                .WithColor(Color.DarkRed);

            foreach(var card in cards)
            {
                embed.AddFieldWithoutTitle(card.GetDisplayString(), true);
            }
            await RespondAsync(embed: embed.Build());
        }
    }
}
