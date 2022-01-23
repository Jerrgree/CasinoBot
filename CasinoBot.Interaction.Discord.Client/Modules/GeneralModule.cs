using CasinoBot.Domain.Enums.StandardPlayingCards;
using CasinoBot.Domain.Interfaces;
using CasinoBot.Domain.Models.StandardPlayingCards;
using CasinoBot.Interaction.Discord.Client.Extensions;
using Discord;
using Discord.Interactions;

namespace CasinoBot.Interaction.Discord.Client.Modules
{
    public class GeneralModule : InteractionModuleBase
    {
        private readonly ILoggingService _loggingService;

        public GeneralModule(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [SlashCommand("draw", "Draw card(s)")]
        public async Task Draw([Summary(description: "The number of cards to draw"), MinValue(1), MaxValue(52)] int numberOfCards = 1)
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

        [SlashCommand("guess", "Guess the suit of a random card")]
        public async Task Guess([Choice("Spades", (int)StandardPlayingCardSuit.Spades), Choice("Clubs", (int)StandardPlayingCardSuit.Clubs), Choice("Hearts", (int)StandardPlayingCardSuit.Hearts), Choice("Diamonds", (int)StandardPlayingCardSuit.Diamonds)] StandardPlayingCardSuit guessedSuit)
        {
            var card = StandardPlayingCard.CreateDeck(1).Shuffle().Draw();

            if (card.Suit == guessedSuit)
            {
                await RespondAsync($"Correct! The drawn card was a {card.Face} of {card.Suit}");
            }
            else
            {
                await RespondAsync($"Good try! The drawn card was a {card.Face} of {card.Suit}");
            }
        }

        public override void BeforeExecute(ICommandInfo command)
        {
            // TODO: still analyze any alternate approaches, since we cannot access this scope on the client itself for any error handling
            var userId = Context.User.Id;
            var guildId = Context.Guild?.Id;
            var traceId = Guid.NewGuid();

            _loggingService.SetLoggingInformation(traceId, userId, guildId);
        }
    }
}
